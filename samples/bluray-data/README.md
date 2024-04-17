## bluray-data

A command line tool for formatting and recording data to Blu-ray Disc (e.g. BD-R, BD-RE)

### Set PATH to compiled programs 

### Linux and macOS

```bash
# .NET 6.0
export PATH=$PWD/bin/net60/:$PATH
```

#### Windows 

```powershell
# .NET 6.0
$env:Path = "$( (pwd).Path )/bin/net60/;" + $env:Path 

# or

# .NET 4.8
$env:Path = "$( (pwd).Path )/bin/net48/;" + $env:Path 
```

### List options

```sh
bluray-data --help
```

### List available devices

```sh
bluray-data list-devices
```

### Eject

```sh
bluray-data eject --device-index 0
```

### Close tray

```sh
bluray-data close-tray --device-index 0
```


### Disc Info

```sh
bluray-data disc-info --device-index 0
```

### Write Speed Info

```sh
bluray-data speed-info --device-index 0
```


### Quick format

```sh
bluray-data format --device-index 0

# force
bluray-data format --force --device-index 0

```

### Full format

```sh
# this may take a long time
bluray-data format --full --device-index 0

# force
bluray-data format --full --force --device-index 0
```

### Burn

#### Start a new disc

> NOTE: We use the `--overwrite` flag  to start a new disc. 

```sh
# Linux and macOS (bash /zsh)
bluray-data burn \
    --overwrite \
    --source-folder $HOME/DataDisc/Elephant \
    --volume-label Animals \
    --eject \
    --device-index 0
```

```sh
# Windows (PowerShell)
bluray-data burn `
    --overwrite `
    --source-folder $HOME/DataDisc/Elephant `
    --volume-label Animals `
    --eject `
    --device-index 0
```

#### Append to existing data

> NOTE: No `--overwrite` flag

```sh
# Linux and macOS (bash /zsh)
bluray-data close-tray --device-index 0

bluray-data burn \
    --source-folder $HOME/DataDisc/Hippo \
    --volume-label Animals \
    --eject \
    --device-index 0
```

```sh
# Windows (PowerShell)
bluray-data close-tray --device-index 0

bluray-data burn `
    --source-folder $HOME/DataDisc/Hippo `
    --volume-label Animals `
    --eject `
    --device-index 0
```