[![NuGet Version](https://img.shields.io/nuget/v/fluentmvvm)](https://www.nuget.org/packages/fluentmvvm/)
[![License](https://img.shields.io/github/license/flinkow/fluentmvvm)](https://github.com/flinkow/fluentmvvm/blob/master/LICENSE)

# fluentmvvm

> An easy-to-use, fast ViewModelBase providing an intuitive fluent API to express property setters naturally.

## Contents

- [Features](https://github.com/flinkow/fluentmvvm#features)
- [Fluent API](https://github.com/flinkow/fluentmvvm#fluent-api)
- [Performance](https://github.com/flinkow/fluentmvvm#performance)
- [License](https://github.com/flinkow/fluentmvvm#license)

## Further reading

- [How it works: Creating backing fields at runtime](TODO)
- [How it works: Improved performance with string interning](TODO)

# Features

To make use of fluentmvvm's features, the first step is to download the [NuGet package](https://www.nuget.org/packages/fluentmvvm/).

Then simply let your view models inherit from `FluentViewModelBase`.

```csharp
public class PersonViewModel : FluentViewModelBase
```

## Property Declaration

Instead of defining backing fields for each property, simply use `Get<T>()` for the property getter and `Set(value)` for the property setter. `fluentmvvm` will create the backing fields at runtime, so that one can write the following code without having to worry about the backing field for the property:

```csharp
public string Name
{
    get => this.GetString();
    set => this.Set(value);
}
```

`Set` raises a `PropertyChanged` event only if the new value is different from the old value.

There are overloads such as `GetBoolean`, `GetInt32`, etc. for all primitive types as well as `GetDecimal`, `GetDateTime`, and `GetString`. The `Get<T>` method can be used for all other value and reference types (structs, enums, classes).

### Remarks

Note that this mechanism only works for public instance properties with a `set` accessor.

### Not creating backing fields at runtime

If you do not want backing fields to be created at runtime, you can pass `false` to the `createBackingFields` parameter of `FluentViewModelBase`.
You can then still use the fluent API with the `Set<T>(T value, ref T oldValue, ...)` overload.

## Dependencies between properties

For cases where a (computed) property's value depends on other properties' values, `Affects` can be used after `Set`.

It would raise a `PropertyChanged` event for the specified property if the new value was different from the old one.

```csharp
public string FullName => $"{this.FirstName} {this.LastName}";

public string FirstName
{
    get => this.GetString();
    set => this.Set(value)
               .Affects(nameof(this.FullName));
}

public string LastName
{
    get => this.GetString();
    set => this.Set(value)
               .Affects(nameof(this.FullName));
}
```

## Dependencies between properties and commands
For cases when a commands `CanExecute` is based on some property's value, `Affects` can be used with the command to raise the `CanExecuteChanged` event for.

```csharp
public ICommand BuyThingsCommand { get; }

public int Balance
{
    get => this.GetInt32();
    set => this.Set(value)
             .Affects(this.BuyThingsCommand);
}
```

### Remarks

`Affects` accepts a parameter of type `ICommand`. However, if the command does not contain a

```csharp
public void RaiseCanExecuteChanged()
```

method, an exception is thrown. You can also derive from the interface `IWpfCommand` provided by `fluentmvvm` to make sure that the method mentioned above exists on your command implementations.

## Fluent API

`FluentViewModelBase` provides a fluent API that can be used in property setters.

```csharp
this.When(<condition>)        // never or once
    .Set(value)               // exactly once
    .Affects(<property name>) // never, once or many times
    .Affects(<command>)       // never, once or many times
    .WasUpdated();            // never or once
```

### Overview

- If the condition passed to `When` evaluates to `false`, the calls to `Set` and `Affects` do not execute anything.

- `Set` checks whether the new value differs from the current value of the property. If it does differ, it sets the value to the property and raises a `PropertyChanged` event. If it does not, nothing happens, and all following `Affects` calls do not execute either.

- When expressing a dependency between one property and another, `Affects` can be used with the name of the property to raise a `PropertyChanged` event for. The event is raised only if the new value of the property differed from the old value.

- If your command implementations derive from `IWpfCommand`, dependencies between properties and commands can also be expressed using `Affects`. That way, when the new value of the property differs from the old value, a `CanExecuteChanged` event is raised for the command.

- `WasUpdated` returns a `bool` value indicating whether the property value was updated (and thus whether a `PropertyChanged` event was raised for the property).

# Performance

The benchmark result below compares fluentmvvm to a naive implementation without using any `Set` methods (called "default" in the figure) and to a more sophisticated approach (called "expression" in the figure) that provides a `Set<T>(Expression<Func<T>> propertyExpression, T oldValue, T newValue)` method (for example, Galasoft.MvvmLight does).

<p align="center">
  <img src="https://github.com/flinkow/fluentmvvm/blob/master/performance.PNG" />
</p>

```ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.685 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.101
  [Host]     : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
RunStrategy=Throughput
```

# License

(c) Thomas Flinkow 2019 - 2021 · [GitHub](https://github.com/flinkow) · [email](flinkow@thomas-flinkow.de)

Distributed under the MIT license. See [`LICENSE`](https://github.com/flinkow/fluentmvvm/blob/master/LICENSE) for more information.
