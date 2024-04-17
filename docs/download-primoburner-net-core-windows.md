# Download PrimoBurner .NET Core for Windows

> Scripts are PowerShell

Change to the directory where you cloned this repository:

```bash
cd primoburner-net
```

## PrimoBurner Core

In the script below, change the tag to the release that you need. For the available versions check the [PrimoBurner .NET Core](https://github.com/primoburner/primoburner-net-core/releases) releases.   

```powershell
# select version and platform
$tag='v5.0.1-demo.1'
$platform='windows'

# download
new-item -Force -ItemType Directory ./sdk/net60
curl.exe `
  --location `
  --output ./sdk/net60/primoburner-net60-$tag-$platform.zip `
  https://github.com/primoburner/primoburner-net-core/releases/download/$tag/primoburner-net60-$tag-$platform.zip

new-item -Force -ItemType Directory ./sdk/net48
curl.exe `
  --location `
  --output ./sdk/net48/primoburner-net48-$tag-$platform.zip `
  https://github.com/primoburner/primoburner-net-core/releases/download/$tag/primoburner-net48-$tag-$platform.zip

# unzip
pushd sdk/net60
expand-archive -Force -Path primoburner-net60-$tag-$platform.zip -DestinationPath .
popd

pushd sdk/net48
expand-archive -Force -Path primoburner-net48-$tag-$platform.zip -DestinationPath .
popd
```
