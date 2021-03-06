# cSharpGPIO #

cSharpGPIO is as library for writing and reading the Raspberry Pi GPIO, writen in C# with .net core 2.0

### How to compile. ###
* change to project directory

```cd  CSharpGPIO.Console```

or

```cd  CSharpGPIO.Console```

* restore packages

```dotnet restore```
* build

```dotnet build```
* publish

```dotnet publish -r linux-arm```

### generated files ###
* cSharpGPIO/CSharpGPIO.Console/bin/Debug/netcoreapp2.0/linux-arm/publish/
* cSharpGPIO/CSharpGPIO.WebApi/bin/Debug/netcoreapp2.0/linux-arm/publish/

### run ###

#### console ####

```sh
$ sudo ./CSharpGPIO.Console
```

#### WebApi ####

```sh
$ sudo ./CSharpGPIO.WebApi
```

### usage example ###

```c#
using cSharpGPIO;
using System.Threading;
...
    var gpio18 = new GPIO("18", GPIODirection.output);
    var gpio17 = new GPIO("17", GPIODirection.input);
    if (gpio18.StreamGood && gpio17.StreamGood) {
        gpio18.SetValue(gpio17.GetValue());
        Thread.Sleep(200);
    }
```
>Programming is moving zeros and ones from one place to another.

