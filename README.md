<p align="center">
  <a href="#" target="_blank" rel="noopener noreferrer">
    <img width="128" src="https://raw.githubusercontent.com/Crequency/Common.BasicHelper/main/Common.BasicHelper/icon.png" alt="Common.BasicHelper Logo">
  </a>
</p>

<h1 align="center">Common.BasicHelper</h1>

# About

Common.BasicHelper is a collection of helper functions for dotnet platform (C# mainly).

You can easily use it in your project through NuGet.

Via dotnet cli

```shell
dotnet add package Common.BasicHelper
```

# Usage

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

More extensions can be find in our docs later.




