[![NuGet Version](https://img.shields.io/nuget/v/fluentmvvm)](https://www.nuget.org/packages/fluentmvvm/)
[![License](https://img.shields.io/github/license/flinkow/fluentmvvm)](https://github.com/flinkow/fluentmvvm/blob/master/LICENSE)

# fluentmvvm

> An easy-to-use, fast ViewModelBase providing an intuitive fluent API to naturally express property setters.

## Contents

- [Features](https://github.com/flinkow/fluentmvvm#features)
- [Performance](https://github.com/flinkow/fluentmvvm#performance)
- [Remarks](https://github.com/flinkow/fluentmvvm#remarks)
- [License](https://github.com/flinkow/fluentmvvm#license)

# Features

To make use of fluentmvvm's features, the first step is to download the [nuget package](https://www.nuget.org/packages/fluentmvvm/).

Then simply let your view models inherit from `FluentViewModelBase`.

```csharp
public class PersonViewModel : FluentViewModelBase
```

## Property Declaration

Instead of defining backing fields for each property, simply use `Get<T>()` for the property getter and `Set(value)` for the property setter (read more about how it works [here](https://github.com/flinkow/fluentmvvm#howitworks)).

```csharp
public string Name
{
  get => this.Get<string>();
  set => this.Set(value);
}
```

`Set` raises a `PropertyChanged` event only if the new value is different from the old value.

### Remarks

Note that this mechanism only works for public instance properties with a `set` accessor.

## Dependencies between properties

For cases where a (computed) property's value depends on the values of other properties, `Affects` can be used after `Set`.

It raises a `PropertyChanged` event for the specified property if the new value was different from the old one.

```csharp
public string FullName => $"{this.FirstName} {this.LastName}";

public string FirstName
{
  get => this.Get<string>();
  set => this.Set(value)
             .Affects(nameof(this.FullName));
}

public string LastName
{
  get => this.Get<string>();
  set => this.Set(value)
             .Affects(nameof(this.FullName));
}
```

## Dependencies between properties and commands
For cases when a commands `CanExecute` is based on the value of some property, `Affects` can be used with the command to raise the `CanExecuteChanged` event for.

```csharp
public ICommand BuyThingsCommand { get; }

public int Balance
{
  get => this.Get<int>();
  set => this.Set(value)
             .Affects(this.BuyThingsCommand);
}
```

### Remarks

`Affects` accepts a parameter of type `ICommand`. However, if the command does not contain a

```csharp
public void RaiseCanExecuteChanged()
```

method, an exception is thrown. You can also derive from the interface `IWpfCommand` provided by fluentmvvm in order to make sure that aforementioned method exists on your command implementations.

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

- `Set` checks whether the new value differs from the current value of the property. If it does differ, it sets the value to the property and raises a `PropertyChanged` event. If it does not, nothing happens and all following `Affects` calls do not execute either.

- When expressing a dependency between one property and another, `Affects` can be used with the name of the property to raise a `PropertyChanged` event for. The event is raised only if the new value of the property differed from the old value.

- If your command implementations derive from `IWpfCommand`, dependencies between properties and commands can also be expressed using `Affects`. That way, when the new value of the property differs from the old value, a `CanExecuteChanged` event is raised for the command.

- `WasUpdated` returns a `bool` value indicating whether the property value was updated (and thus whether a `PropertyChanged` event was raised for the property).

# Performance

The benchmark result below compares fluentmvvm to a naive implementation without using any `Set` methods (called "bare" in the figure) and to a more sophisticated approach (called "expression" in the figure) that provides a `Set<T>(Expression<Func<T>> propertyExpression, T oldValue, T newValue)` method (for example, Galasoft.MvvmLight does).

<p align="center">
  <img src="https://github.com/flinkow/fluentmvvm/blob/master/performance.PNG" />
</p>

```ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.17134.1130 (1803/April2018Update/Redstone4)
Intel Core i7-4700MQ CPU 2.40GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=2338349 Hz, Resolution=427.6522 ns, Timer=TSC
.NET Core SDK=3.0.100
  [Host]    : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  RyuJitX64 : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
Job=RyuJitX64  Jit=RyuJit  Platform=X64  
```

# Remarks
As code is dynamically emitted at runtime, fluentmvvm can not be used in Ahead of time (AOT) environments such as Xamarin.Android, Xamarin.iOS, Mono or .NET Native.

# License

(c) Thomas Flinkow 2019 · [github](https://github.com/flinkow) · [email](flinkow@thomas-flinkow.de)

Distributed under the MIT license. See [`LICENSE`](https://github.com/flinkow/fluentmvvm/blob/master/LICENSE) for more information.
