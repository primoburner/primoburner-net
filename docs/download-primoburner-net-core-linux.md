# Download PrimoBurner .NET Core and Assets on Linux

Change to the directory where you cloned this repository:

```bash
cd primoburner-cpp
```

## PrimoBurner Core

In the script below, change the tag to the release that you need. For the available versions check the [PrimoBurner .NET Core](https://github.com/primoburner/primoburner-net-core/releases) releases.   

### AMD / Intel x64

```bash
# select version and platform
tag="v5.0.1-demo.1"
platform="linux"

# download
mkdir -p ./sdk/net60
curl \
  --location \
  --output ./sdk/net60/primoburner-net60-$tag-$platform.tar.gz \
  https://github.com/primoburner/primoburner-net-core/releases/download/$tag/primoburner-net60-$tag-$platform.tar.gz
  
# unzip
pushd ./sdk/net60
tar -xvf primoburner-net60-$tag-$platform.tar.gz
popd
```

### ARM arm64

```bash
# select version and platform
tag="v5.0.1-demo.1"
platform="linux-arm64"

# download
mkdir -p ./sdk/net60
curl \
  --location \
  --output ./sdk/net60/primoburner-net60-$tag-$platform.tar.gz \
  https://github.com/primoburner/primoburner-net-core/releases/download/$tag/primoburner-net60-$tag-$platform.tar.gz
  
# unzip
pushd ./sdk/net60
tar -xvf primoburner-net60-$tag-$platform.tar.gz
popd
```
