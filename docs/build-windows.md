# Build on Windows

## Build

Configure project:

```powershell
. .\configure.ps1
```

Restore packages:

```powershell
dotnet restore
```

Build:

```powershell
# x64
dotnet build --property:Configuration=Debug --property:Platform=x64

# arm64
dotnet build --property:Configuration=Debug --property:Platform=arm64
```
