using System.CommandLine;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PrimoSoftware.Burner;

namespace BluRayBurner.NET
{

    class Program
    {
        static async Task Main(string[] args)
        {
            var rootCommand = new RootCommand("Blu-ray Data CLI Sample");

            // Common options
            var deviceIndexOpt = new Option<int>(name: "--device-index", description: "Index of the device to use.");

            // List Devices
            var listDevicesSubCommand = new Command("list-devices", "List the CD / DVD / BD devices attached to the system.");
            rootCommand.Add(listDevicesSubCommand);

            listDevicesSubCommand.SetHandler(() =>
            {
                var app = new BurnerApp();
                app.ListDevices();
                app.Dispose();
            });

            // Eject
            var ejectSubCommand = new Command("eject", "Open device tray");
            rootCommand.Add(ejectSubCommand);
            
            ejectSubCommand.AddOption(deviceIndexOpt);
            
            ejectSubCommand.SetHandler((deviceIndex) =>
            {
                var app = new BurnerApp();
                app.Eject(deviceIndex);
                app.Dispose();
            }, deviceIndexOpt);

            // Close tray
            var closeTraySubCommand = new Command("close-tray", "Close device tray");
            rootCommand.Add(closeTraySubCommand);
            
            closeTraySubCommand.AddOption(deviceIndexOpt);
            
            closeTraySubCommand.SetHandler((deviceIndex) =>
            {
                var app = new BurnerApp();
                app.CloseTray(deviceIndex);
                app.Dispose();
            }, deviceIndexOpt);

            // Disc Info
            var discInfoSubCommand = new Command("disc-info", "Show media profile and free space.");
            rootCommand.Add(discInfoSubCommand);
            
            discInfoSubCommand.AddOption(deviceIndexOpt);
            
            discInfoSubCommand.SetHandler((deviceIndex) =>
            {
                var app = new BurnerApp();
                app.DiscInfo(deviceIndex);
                app.Dispose();
            }, deviceIndexOpt);

            // Speed Info
            var speedInfoSubCommand = new Command("speed-info", "Show available write speeds. This depends on the media in the device");
            rootCommand.Add(speedInfoSubCommand);
            
            speedInfoSubCommand.AddOption(deviceIndexOpt);
            
            speedInfoSubCommand.SetHandler((deviceIndex) =>
            {
                var app = new BurnerApp();
                app.SpeedInfo(deviceIndex);
                app.Dispose();
            }, deviceIndexOpt);

            // Format
            var formatSubCommand = new Command("format", "Format disc in device specified by --device-index");
            rootCommand.Add(formatSubCommand);
            
            formatSubCommand.AddOption(deviceIndexOpt);

            var fullOpt = new Option<bool>(name: "--full", description: "Perform full / slow format.");
            formatSubCommand.AddOption(fullOpt);

            var forceOpt = new Option<bool>(name: "--force", description: "Perform format even if the disc in the device is already formatted.");
            formatSubCommand.AddOption(forceOpt);

            formatSubCommand.SetHandler((deviceIndex, full, force) =>
            {
                var app = new BurnerApp();
                app.Format(deviceIndex, full, force);
                app.Dispose();
            }, deviceIndexOpt, fullOpt, forceOpt);


            // Burn
            var burnSubCommand = new Command("burn", "Burn a data folder");
            rootCommand.Add(burnSubCommand);
            
            burnSubCommand.AddOption(deviceIndexOpt);

            var sourceFolderOpt = new Option<DirectoryInfo>(name: "--source-folder", 
                                                            description: "Folder to burn. The contents of the folder will be added to the disc.", 
                                                            parseArgument: result => { 
                                                                string dirPath = result.Tokens.Single().Value;
                                                                if (!Directory.Exists(dirPath))
                                                                {
                                                                    result.ErrorMessage = $"{dirPath} does not exist";
                                                                    return null;
                                                                }

                                                                return new DirectoryInfo(dirPath);                                                                
                                                            });
            burnSubCommand.AddOption(sourceFolderOpt);

            var volumeLabelOpt = new Option<string>(name: "--volume-label", description: "Volume label. Defaults to 'DATADISC'.");
            burnSubCommand.AddOption(volumeLabelOpt);

            var overwriteOpt = new Option<bool>(name: "--overwrite", description: "Overwrite existing data instead of appending to disc.");
            burnSubCommand.AddOption(overwriteOpt);

            var ejectOpt = new Option<bool>(name: "--eject", description: "Eject disc after writing.");
            burnSubCommand.AddOption(ejectOpt);

            burnSubCommand.SetHandler((deviceIndex, sourceFolder, volumeLabel, overwrite, eject) =>
            {
                var app = new BurnerApp();
                
                app.Burn(
                    deviceIndex: deviceIndex, 
                    sourceFolder: sourceFolder.FullName, 
                    udfRevision: UdfRevision.Revision102,
                    volumeLabel: volumeLabel, 
                    bdVideo: false, 
                    loadLastTrack: !overwrite, 
                    closeDisc: false, 
                    eject: eject);

                app.Dispose();
            }, deviceIndexOpt, sourceFolderOpt, volumeLabelOpt, overwriteOpt, ejectOpt);


            // run the app
            await rootCommand.InvokeAsync(args);
        }
    }
}