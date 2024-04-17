# Build on macOS

## Build

Configure project:

```bash
source configure.sh
```

Restore packages:

```bash
dotnet restore
```

Build:

```bash
# x64
dotnet build --property:Configuration=Debug --property:Platform=x64

# arm64
dotnet build --property:Configuration=Debug --property:Platform=arm64
```

