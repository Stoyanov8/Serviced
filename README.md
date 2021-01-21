# What is Serviced

Serviced is a simple lightweight library that handles service registrations for you.

## How to setup

```csharp
// Register ( how ironic ) Serviced in your Startup.cs
services.AddServiced(AppDomain.CurrentDomain.GetAssemblies());
```
Pass as parameter the assemblies where your services are located.

**Note:** If you use 
```csharp
AppDomain.CurrentDomain.GetAssemblies()
```
the library will scan all assemblies for you and register every service.

## How to use
There are 3 major interfaces

 - ITransient
 - IScoped
 - ISingleton
 
All of them have generic and non-generic version. 

**Example:** Lets say you want to register a service named "HomeService" with interface "IHomeService" and you want to register it as transient service. 

```csharp
class HomeService: IHomeService, ITransient<IHomeService>
```
The above interface basically saves you from this syntax in Startup.cs
```csharp
services.AddTransient<IHomeService,HomeService>();
```
Another example is, lets say you want to register "HomeService" without an interface. Simply inherit the **ITransient** interface

```csharp
class HomeService: ITransient
```
Below is the Startup.cs equivalent, that is no longer needed.
```csharp
services.AddTransient<HomeService>();
```

