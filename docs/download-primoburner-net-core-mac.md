# Download PrimoBurner .NET Core for macOS

Change to the directory where you cloned this repository:

```bash
cd primoburner-net
```

## PrimoBurner .NET Core

In the script below, change the tag to the release that you need. For the available versions check the [PrimoBurner .NET Core](https://github.com/primoburner/primoburner-net-core/releases) releases.   

### Intel / x64

```bash
# select version and platform
tag="v5.0.1-demo.1"
platform="darwin"

# download
mkdir -p ./sdk/net60
curl \
  --location \
  --output ./sdk/net60/primoburner-net60-$tag-$platform.zip \
  https://github.com/primoburner/primoburner-net-core/releases/download/$tag/primoburner-net60-$tag-$platform.zip

# unzip
pushd ./sdk/net60
unzip primoburner-net60-$tag-$platform.zip
popd
```

### Apple Silicon / arm64

```bash
# select version and platform
tag="v5.0.1-demo.1"
platform="darwin-arm64"

# download
mkdir -p ./sdk/net60
curl \
  --location \
  --output ./sdk/net60/primoburner-net60-$tag-$platform.zip \
  https://github.com/primoburner/primoburner-net-core/releases/download/$tag/primoburner-net60-$tag-$platform.zip

# unzip
pushd ./sdk/net60
unzip primoburner-net60-$tag-$platform.zip
popd
```
