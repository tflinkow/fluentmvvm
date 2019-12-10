# How does it work?

For every view model deriving from `FluentViewModelBase`, a new type that contains the backing fields for all public writable instance properties is generated at runtime.

The `FluentViewModelBase` then operates on that generated type to store the property values of the view model.

## Generating backing fields at runtime

For a view model as shown below,

```csharp
public class PersonViewModel : FluentViewModelBase
{
  public int Age
  {
    get => this.Get<int>();
    set => this.Set(value);
  }

  public string Name
  {
    get => this.Get<string>();
    set => this.Set(value);
  }
}
```

fluentmvvm generates a type at runtime that contains as many fields as there are public writable instance properties on `PersonViewModel`. The fields are named exactly like the properties, but are of type `object` and behave as their backing fields.

```csharp
public class <PersonViewModel>BackingFields : IBackingFieldProvider
{
  private object Age = default(int);
  private object Name = default(string);
}
```

The generated type implements the interface `IBackingFieldProvider` to allow others to access the fields of the generated type.

```csharp
internal interface IBackingFieldProvider
{
  object GetValueOf(string propertyName);
  bool SetValueOf(string propertyName, object value);
}
```

## Getting the value of a property

To access the backing fields based on the name of the property, the `GetValueOf` method needs to compare the specified property name to the actually existing fields, and returns the value of the field that is named exactly as the specified property name.

```csharp
public object GetValueOf(string propertyName)
{
  if (propertyName != null)
  {
    if (propertyName == nameof(this.Age))
    {
      return this.Age;
    }

    if (propertyName == nameof(this.Name))
    {
      return this.Name;
    }
  }

  throw new ArgumentException($"'PersonViewModel' does not contain a public writable instance property named '{propertyName}'.")
}
```

## Setting the value of a property

Not much different from how the `GetValueOf` method compares the specified property name to the actually existing fields, `SetValueOf` first determines which field to set.

When the correct field is found, the new value is compared to the old value and is stored in the field only if it differed.

The return value indicates whether or not the field was set and is used to determine whether a `PropertyChanged` event should be raised.

```csharp
public bool SetValueOf(string propertyName, object value)
{
  if (propertyName != null)
  {
    if (propertyName == nameof(this.Age))
    {
      if (Object.Equals(this.Age, value))
      {
        return false;
      }

      this.Age = value;
      return true;
    }

    if (propertyName == nameof(this.Name))
    {
      if (Object.Equals(this.Name, value))
      {
        return false;
      }

      this.Name = value;
      return true;
    }
  }

  throw new ArgumentException($"'PersonViewModel' does not contain a public writable instance property named '{propertyName}'.")
}
```

## Bringing it all together

For getting and setting property values `FluentViewModelBase` provides two methods:

```csharp
public T Get<T>([CallerMemberName] string propertyName = null) { ... }

public bool Set(object value, [CallerMemberName] string propertyName = null) { ... }
```

The `propertyName` parameter is automatically filled in by the compiler thanks to the `CallerMemberNameAttribute`.

```csharp
public string Name
{
  get => this.Get<string>();
  set => this.Set(value);
}
```

In the above case, `Get` is actually called with `Get<string>("Name")` and `Set` with `Set(value, "Name")`.

`Get` and `Set` then make use of the generated type's `GetValueOf` and `SetValueOf` methods and raise events etc. when necessary.
