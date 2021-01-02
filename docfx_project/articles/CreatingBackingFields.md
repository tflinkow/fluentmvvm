[↩ Go back home](https://github.com/flinkow/fluentmvvm)

# How it works: Creating backing fields at runtime

## Contents

- [Using a dictionary to act as the backing fields](TODO#features)
- [A specialized read-only dictionary](TODO#features)
- [The disadvantage and an ugly-solution](TODO#features)

One goal of `fluentmvvm` was to write properties and let `fluentmvvm` take care of supplying the backing fields.

```csharp
public int Age
{
    get => this.GetInt32();
    set => this.Set(value);
}
```

The following sections give insight into how the backing fields will be created at runtime.

## Note

> Versions before `0.5.0-alpha` did not use the following approach but instead generated types at runtime by emitting CIL directly. The advantage was that boxing could be avoided entirely. Apart from maintainability, a significant disadvantage was that the `Get` and `Set` methods ran in `O(n)`, with `n` being the number of properties. Using a dictionary allows for the `Get` and `Set` methods to run in close to `O(1)` (almost constant time, no matter the number of properties).

## Using a dictionary to act as the backing fields

A field essentially is a key-value pair consisting of the name of the field and its value. Therefore it is easy to think of fields as the elements of a `Dictionary<string, TValue>`.

What this means is instead of having

```csharp
private int age;

public int Age
{
    get => this.age;
    set => this.age = value;
}
```

we can simply use

```csharp
private readonly IDictionary<string, int> intFields = new Dictionary<string, int>();

public int Age
{
    get => this.intFields["Age"];
    set => this.intFields["Age"] = value;    
}
```

## A specialized read-only dictionary

The standard `Dictionary<TKey, TValue>` is already highly optimized but contains features that are not needed for our use case (such as resizing, adding, and removing elements after creation).

And maybe the most important: the `Set` method of `FluentViewModelBase` raises the `PropertyChanged` event only if the new value was different from the old value. Therefore it is necessary to retrieve a value from the dictionary before setting the value. `Dictionary<TKey, TValue>` requires two lookups to do this, i.e.

```csharp
public void Set(int value, [CallerMemberName] string propertyName = null)
{
    if(this.intFields.TryGetValue(propertyName, out int value)) // first lookup
    {
        if(value != newValue)
        {
            this.intFields[propertyName] = value; // second lookup!
            // ...
        }
    }
}
```

If we can do that with only one lookup, we can drastically reduce the time needed to set values.

By writing a custom `IReadOnlyDictionary<string, TValue>` we could not only accomplish that but also achieve performance gains on older runtimes (about 22% on .NET Framework 4.8) in general and significant performance gains (and improved memory usage) when only one element is in the dictionary<sup id="a1">[†](#SingleElementDictionary)</sup> (about 25% on .NET Core 3.1 and about 47% on .NET Framework 4.8).

This custom dictionary is the [`FixedSizeDictionary<TValue>`](TODO). It is a simplified dictionary with separate chaining for collision resolution. The capacity (number of buckets) is fixed and determined based on the number of elements that shall be added to it. It is chosen to be a power of two with the additional constraint that the load factor (i.e., the ratio between available and occupied slots) never exceeds `ln(2)`.

It features a `TryGetEntry` method, which can be used both for getting and setting a value:

```csharp
public void Set(int value, [CallerMemberName] string propertyName = null)
{
    if(this.intFields.TryGetEntry(propertyName, out FixedSizeDictionary<int>.Entry? entry)) // one lookup
    {
        if(value != entry.Value) // entry.Value is the old value
        {
            entry.Value = value; // setting the new value - no additional lookup!
            // ...
        }
    }
}
```

## The disadvantage and an ugly-solution

Sadly, a big disadvantage of this approach is that one `FixedSizeDictionary<TValue>` can only be used for fields of type `TValue`. There is no way to put fields of type `int` into the same dictionary as fields of type `string` etc., without using [boxing](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing).

This is why all classes, `struct`s and enumerations for which no explicit overload and thus no designated dictionary exists will be put into one `FixedSizeDictionary<object>`. For reference types, this is fine (although not incredibly beautiful), but for value types, the process of boxing (making and `object` from the value) is quite costly. I believe, however, that this is a sacrifice that's worth making in order to have the `Get` and `Set` methods run in almost `O(1)`.

# Footnotes

<b id="SingleElementDictionary">†</b> Because we create a designated dictionary for every primitive type, it is beneficial to have a fast and memory-efficient solution for dictionaries with only a few entries. This is important because some types (e.g., `DateTime`, `uint`, etc.) are not used as often as others (`int`, `string`, etc.). If a view model has only one property of a certain primitive type, it makes sense for the dictionary only to have one entry too. `Dictionary<TKey, TValue>` will always have [at least three entries](https://source.dot.net/#System.Private.CoreLib/HashHelpers.cs,e8668bf19da49963). [↩ Back](#a1)

# License

(c) Thomas Flinkow 2019 - 2021 · [GitHub](https://github.com/flinkow) · [email](flinkow@thomas-flinkow.de)

Distributed under the MIT license. See [`LICENSE`](https://github.com/flinkow/fluentmvvm/blob/master/LICENSE) for more information.
