# URIScheme

Channel | Status
-|-
CI | [![CI](https://github.com/HMBSbige/URIScheme/workflows/CI/badge.svg)](https://github.com/HMBSbige/URIScheme/actions)
NuGet.org | [![NuGet.org](https://img.shields.io/nuget/v/URIScheme.svg)](https://www.nuget.org/packages/URIScheme/)

# Usage
```csharp
const string key = @"ssa";
var service = new URISchemeService(key, @"URL:ssa Protocol", @"D:\MyAppPath\MyApp.exe --openurl");
// var service = new URISchemeService(key, @"URL:ssa Protocol", @"D:\MyAppPath\MyApp.exe --openurl", RegisterType.LocalMachine);

var isSet = service.Check();

service.Set();

service.Delete();
```