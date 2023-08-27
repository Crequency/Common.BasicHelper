# CommandsExecutor

This is a class to help to run commands in system shell.

It provides 2 static methods with each have a extension method.

## Static Methods

### Methods and arguments with description

- `GetExecutionResult` -> `string`

  This method execute command in system shell, and return standard output content.
  
  `string` **command** - command name, when **findInPath** is true, file path will find from `Path`.
  
  `string` **args** - start up arguments.
  
  `[bool]` **findInPath** = false - indecate whether get file path in `Path`.
  
  `[Action<ProcessStartInfo>?]` **action** = null - user can user this argument to operate `ProcessStartInfo`.

- `GetExecutionResultAsync` -> `Task<string>`

  This method has same function and 4 same parameters as `GetExecutionResult`, but execute outside program asynchronously, 
you can await this method.
  
  `string`, `string`, `[bool]`, `[Action<ProcessStartInfo>?]`

  `[CancellationToken?]` **token** = null - you can use this object to cancel this task, once **token**!.`IsCancellationRequested` is true, the outside process will be killed.

### Usage

```csharp
using Common.BasicHelper.Core.Shell;

var output = CommandsExecutor.GetExecutionResult(
    "help", // command, use `help` for example
    "",
    true
);

var output_async - CommandsExecutor.GetExecutionResultAsync(
    "help",
    "",
    true
);

Console.WriteLine(output);
Console.WriteLine(output_async);
```

## Extension Methods

### Methods and arguments with description

- `ExecuteAsCommand` -> `string`

  This extension method is a wrapping method for `GetExecutionResult`.
  
  *this* `string` **command**
  
  `[string?]` **args** = null
  
  `[bool]` **findInPath** = true
  
  `[Action<ProcessStartInfo>?]` **action** = null

- `ExecuteAsCommandAsync` -> `Task<string>`
  
  This extension method is a wrapping method for `GetExecutionResultAsync`.
  
  *this* `string` **command**
  
  `[string?]` **args** = null
  
  `[bool]` **findInPath** = true
  
  `[Action<ProcessStartInfo>?]` **action** = null
  
  `[CancellationToken?]` **token** = default

### Usage

```csharp
using Common.BasicHelper.Core.Shell;

Console.WriteLine("help".ExecuteAsCommand());
```

## OOP

This is a static class.

## Examples

- In [KitX Dashboard](https://github.com/Crequency/KitX-Dashboard/blob/03e4d3b127de69a34df00f6ac34db7ec6bd1a0d4/Network/NetworkHelper.cs#L139) project, we use these functions to get system version info.

