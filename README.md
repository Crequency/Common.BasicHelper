<p align="center">
  <a href="#" target="_blank" rel="noopener noreferrer">
    <img width="128" src="https://raw.githubusercontent.com/Crequency/Common.BasicHelper/main/Common.BasicHelper/icon.png" alt="Common.BasicHelper Logo">
  </a>
</p>

<h1 align="center">Common.BasicHelper</h1>

<p align="center">
  <img alt="GitHub License" src="https://img.shields.io/github/license/Crequency/Common.BasicHelper">
  <img alt="GitHub workflow status" src="https://img.shields.io/github/actions/workflow/status/Crequency/Common.BasicHelper/build.yml"></img>
  <img alt="Nuget" src="https://img.shields.io/nuget/v/Common.BasicHelper">
  <img alt="Nuget" src="https://img.shields.io/nuget/dt/Common.BasicHelper?label=nuget">
  <img alt="GitHub issues" src="https://img.shields.io/github/issues/Crequency/Common.BasicHelper">
  <img alt="GitHub pull requests" src="https://img.shields.io/github/issues-pr/Crequency/Common.BasicHelper">
</p>

<p align="center">
    <img src="https://profile-counter.glitch.me/Crequency-Common.BasicHelper/count.svg"></img>
</p>

# About

Common.BasicHelper is a collection of helper functions for dotnet platform (C# mainly).

You can easily use it in your project through NuGet.

Via dotnet cli

```shell
dotnet add package Common.BasicHelper
```

# Samples

We provide some samples in `Commong.BasicHelper.Samples` project (which is a api site).

To run this project, just run commands:

```shell
cd Common.BasicHelper.Samples

dotnet run
```

<details>
<summary>The output will looks like ...</summary>

<br>

```plaintext
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:<port>
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: <path>
```

`<port>` label is the port number of the server.

Then you can visit `http://localhost:<port>/swagger/index.html` to see the samples.

</details>

# Usage

## Math

### Calculate

With `Expression` class and `Calculator` class, you can calculate your calculation.
See examples of [Expression](./Common.BasicHelper.Test/Math/Expression_Tests.cs) class and [Calculator](./Common.BasicHelper.Test/Math/Calculator_Tests.cs) class.

## Extensions

You can use follow namespace to use extensions:

```CSharp
using Common.BasicHelper.Utils.Extensions;
```

Such as extensions in `QueueHelper`:

```CSharp
var queue = new Queue<int>()
    .Push(1)
    .Push(2)
    .Pop()
    .Push(3)
    .Push(4)
    .Pop()
    .Push(5)
    ;
queue.Dump(); // Result will be "3 4 5 "
```

And you can execute a string as a system command:

```CSharp
"help".ExecuteAsCommand();
```

And you can pass arguments through parameters `args`.

## More

More extensions can be find in our docs later.

We will be appreciate if you can help us with docs site.

# Cli Tool

We provide a cli tool to help you to call Common.BasicHelper's power.

You can install it simply from nuget:

```shell
dotnet tool install --global Common.BasicHelper.Cli

# Test installation
cbt --version
```

[Nuget Package - Common.BasicHelper.Cli](https://www.nuget.org/packages/Common.BasicHelper.Cli)

## Usage

Take `Password` for example (here is the output for command `cbt pwd --help`):

```text
Common.BasicHelper.Cli 0.1.6863.950+8b01c11c384856b3a1ffbf64e6424adf3e187ed8
Copyright (C) Crequency 2024

  -g, --generate               (Default: true) Generate a password.

  -l, --length                 (Default: 12) Length of the password.

  -r, --length-range           Length range of the password, like `3,5`.

  -u, --ignore-uppercase       Ignore uppercase letters.

  -e, --ignore-lowercase       Ignore lowercase letters.

  -n, --ignore-numbers         Ignore numbers.

  -s, --ignore-symbols         Ignore symbols.

  -U, --supported-uppercase    (Default: ABCDEFGHIJKLMNOPQRSTUVWXYZ) Supported uppercase letters.

  -E, --supported-lowercase    (Default: abcdefghijklmnopqrstuvwxyz) Supported lowercase letters.

  -N, --supported-numbers      (Default: 0123456789) Supported numbers.

  -S, --supported-symbols      (Default: !@#$%^&*()_+-=[]{};':,./<>?) Supported symbols.

  --verbose                    Display verbose output.

  --dry-run                    Dry run the command.

  --help                       Display this help screen.

  --version                    Display version information.
```

The more commands you can find in `cbt help`.
