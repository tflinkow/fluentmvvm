[↩ Go back home](https://github.com/flinkow/fluentmvvm)

# How it works: Improved performance with string interning

## Contents

- [String Interning](TODO#features)
- [How it all comes together](TODO#features)
- [The solution](TODO#features)

## Prerequisites

The `Get` and `Set` methods make use of the [`[CallerMemberName]`](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.callermembernameattribute?view=net-5.0) attribute. At compile-time, code like this

```csharp
public int Age
{
    get => this.GetInt32();
    set => this.Set(value);
}
```

gets transformed into this code:

```csharp
public int Age
{
    get => this.GetInt32("Age");
    set => this.Set(value, "Age");
}
```

## String Interning

Interning denotes the concept of only keeping one string in memory no matter how often it is used.

```csharp
string x = "Hello World";
string y = "Hello World";

bool areEqual = Object.ReferenceEquals(x, y); // true
```

The benefits of string interning are less memory consumption and faster string comparisons (see the source of [`String.Equals`](https://source.dot.net/#System.Private.CoreLib/String.Comparison.cs,31b307b02a3bd6b9,references)).

### String interning at runtime

Only literal strings will be interned<sup id="a1">[†](#StringInterningFootnote)</sup>. 
That means that no string created at runtime will be interned, regardless of whether a literal with the same value exists and thus was interned.

```csharp
string x = "Hello";   // will be interned (literal)
            
string y = "He";      // will be interned (literal)
string z = y + "llo"; // will not be interned (not a literal!)

bool areEqual = Object.ReferenceEquals(x, z); // false
```

There are, however, two methods on the `String` class, namely [`String.Intern`](https://docs.microsoft.com/en-us/dotnet/api/system.string.intern?view=net-5.0#System_String_Intern_System_String_) and [`String.IsInterned`](https://docs.microsoft.com/en-us/dotnet/api/system.string.isinterned?view=net-5.0) to intern strings at runtime.

```csharp
string x = "Hello";   // will be interned (literal)
            
string y = "He";      // will be interned (literal)
string z = String.Intern(y + "llo"); // z is now interned

bool areEqual = Object.ReferenceEquals(x, z); // true
```

The disadvantage of that is that interned strings cannot be garbage collected for the entire duration of the application. Even if `x` and `z` go out of scope or explicitly deleted, no memory will be freed.

## How it all comes together

As described [here](TODO), all `Get` and `Set` methods of `FluentViewModelBase` internally use a dictionary which acts as the backing fields.
The dictionary's key is of type `string`, and when comparing keys **it does not suffice only to compare hash codes** because different strings may have the same hash code<sup id="a2">[‡](#PigeonHolePrincipleFootnote)</sup>.

```csharp
// Below code is drastically simplified to show the point.

public bool TryGetEntry(string key, [NotNullWhen(true)] out Entry? entry)
{
    int hash = key.GetHashCode();
    Entry? possibleEntry = this.buckets[this.DetermineIndex(hash)];
 
    // Compare the hash code first, then we need to compare the keys too!
    if (possibleEntry.HashCode == hash && possibleEntry.Key == key)
    {
        possibleEntry = next;
        return true;
    }

    ...
}
```

Improving the performance of the string comparison, therefore, improves the dictionary lookup's performance, thus improving the performance of all `Get` and `Set` methods.

## The solution
All property names are already in the intern pool because they are literals in the code, thanks to `[CallerMemberName]`. Upon creation of the `FixedSizeDictionary`, we reflect over all properties of the view model and use the name as the key in the dictionary.

If we use the interned string here, we can improve comparison performance later on.

```csharp
// Below code is drastically simplified to show the point.

IEnumerable<PropertyInfo> properties = ...

foreach (PropertyInfo property in properties)
{
    var name = String.Intern(propertyInfo.Name); // this is the important thing!
    var defaultValue = ...

    this.Add(name, defaultValue);
}
```

Note that `fluentmvvm` only uses this approach if literals are interned, because if they are not, this approach is much slower.

# Footnotes

<b id="StringInterningFootnote">†</b> One cannot rely on string interning to happen at all because it is an implementation detail. Thus every runtime version can choose whether to intern literal strings. Further, applying the [`[CompilationRelaxations]`](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.compilationrelaxationsattribute?view=net-5.0) attribute to an assembly instructs the runtime not to intern strings at all (however, [certain runtimes ignore the attribute altogether](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.compilationrelaxationsattribute?view=net-5.0#remarks)). [↩ Back](#a1)

<b id="PigeonHolePrincipleFootnote">‡</b> There are (almost) infinitely many strings possible, but as `GetHashCode` returns `int`, there are only `2^32` possible values. The so-called universe of keys thus is infinite, while the universe of hash codes is much smaller and finite. Therefore it is incredibly likely that two strings have the same hash code once we deal with a big enough amount of strings. [↩ Back](#a2)

# Further reading

See [this](https://docs.microsoft.com/en-us/dotnet/api/system.string.intern?view=net-5.0#remarks) or [this](https://stackoverflow.com/questions/8054471/string-interning-in-net-framework-what-are-the-benefits-and-when-to-use-inter) for more information on string interning.

# License

(c) Thomas Flinkow 2019 - 2021 · [GitHub](https://github.com/flinkow) · [email](flinkow@thomas-flinkow.de)

Distributed under the MIT license. See [`LICENSE`](https://github.com/flinkow/fluentmvvm/blob/master/LICENSE) for more information.
