using System;
using System.Threading;
using System.Threading.Tasks;
using ConsoleTables;
using PrimoSoftware.Burner;

namespace BluRayBurner.NET
{
    public class BurnerApp : IDisposable
    {
        private Burner m_Burner = new Burner();

        public BurnerApp()
        {
            // Initialize the SDK
            Library.Initialize();

            // Set license string
            const string license = @"<primoSoftware></primoSoftware>";
            Library.SetLicense(license);

            m_Burner.Open();
        }

        public void Dispose()
        {
            // Close Burner
            m_Burner.Close();
            m_Burner = null;

            // Shutdown the SDK
            Library.Shutdown();
        }

        public void ListDevices()
        {
            try
            {
                var table = new ConsoleTable("Index", "Title", "Writer");

                int writerCount = 0;

                DeviceInfo[] devices = m_Burner.EnumerateDevices();
                foreach (var dev in devices)
                {
                    table.AddRow(dev.Index, dev.Title, dev.IsWriter);
                    if (dev.IsWriter)
                        writerCount++;
                }

                table.Write();
                Console.WriteLine();

                if (0 == writerCount)
                    throw new Exception("No writer devices found.");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }

        public void Eject(int deviceIndex)
        {
            try
            {
                m_Burner.SelectDevice(deviceIndex, false);
                m_Burner.Eject();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }

        public void CloseTray(int deviceIndex)
        {
            try
            {
                m_Burner.SelectDevice(deviceIndex, false);
                m_Burner.CloseTray();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }

        public void DiscInfo(int deviceIndex)
        {
            try
            {
                // Select device. Exclusive access is not required.
                m_Burner.SelectDevice(deviceIndex, false);

                // Get and display the media profile
                ulong freeSpace = (ulong)m_Burner.MediaFreeSpace * (int)BlockSize.Dvd;

                // Media profile
                var labelMediaType = m_Burner.MediaProfileString;
                Console.WriteLine($"Media Profile: {labelMediaType}");

                // Capacity
                var labelFreeSpace = String.Format("Media Free Space: {0}GB", ((double)freeSpace / (1e9)).ToString("0.00"));
                Console.WriteLine(labelFreeSpace);

                m_Burner.ReleaseDevice();
            }
            catch(BurnerException bme)
            {
                m_Burner.ReleaseDevice();
                ShowErrorMessage(bme);
            }
        }

        public void SpeedInfo(int deviceIndex)
        {
            try
            {
                // Select device. Exclusive access is not required.
                m_Burner.SelectDevice(deviceIndex, false);

                // Speed
                var table = new ConsoleTable("Index", "Name", "Transfer Rate");

                SpeedInfo [] speeds = m_Burner.EnumerateWriteSpeeds();
                for (int i = 0; i < speeds.Length; i++)
                {
                    table.AddRow(i, speeds[i].ToString(), $"{speeds[i].TransferRateKB} KB");
                }

                table.Write();
                Console.WriteLine();

                m_Burner.ReleaseDevice();
            }
            catch(BurnerException bme)
            {
                m_Burner.ReleaseDevice();
                ShowErrorMessage(bme);
            }
        }

        public void Format(int deviceIndex, bool full, bool force)
        {
            try
            {
                m_Burner.SelectDevice(deviceIndex, true);

                if (!force)
                {
                    if (m_Burner.MediaIsFullyFormatted)
                        if (!ConsoleUtils.Confirm("Media is already formatted. Do you want to format it again?"))
                        {
                            m_Burner.ReleaseDevice();
                            return;
                        }

                    if (ConsoleUtils.Confirm("Formatting will destroy all the information on the disc. Do you want to continue?"))
                    {
                        m_Burner.ReleaseDevice();
                        return;
                    }
                }

                Console.WriteLine("Formatting. Please wait...");

                m_Burner.FormatProgress += new BurnerEvents.FormatProgress(Burner_FormatProgress);

                FormatSettings settings = new FormatSettings();
                settings.Quick = !full;
                settings.Force = force;

                m_Burner.Format(settings);

                m_Burner.ReleaseDevice();
            }
            catch (BurnerException bme)
            {
                m_Burner.ReleaseDevice();
                ShowErrorMessage(bme);
            }
        }

        private void Burner_FormatProgress(double percentCompleted)
        {
            Console.WriteLine("Formatting {0}% ...", (int)percentCompleted);
        }

        public void Burn(int deviceIndex, string sourceFolder, 
                            UdfRevision udfRevision, bool bdVideo, string volumeLabel, 
                            bool loadLastTrack, bool closeDisc, bool eject)
        {
            try
            {
                m_Burner.SelectDevice(deviceIndex, true);

                m_Burner.FileProgress	+= new BurnerEvents.FileProgress(Burner_FileProgress);

                BurnSettings settings = new BurnSettings();

                settings.SourceFolder = sourceFolder;

                settings.ImageType = ImageType.Udf;
                settings.UdfRevision = udfRevision;
                settings.BDVideo = bdVideo;

                settings.VolumeLabel = volumeLabel;
                
                settings.Eject = eject;
                settings.CloseDisc = closeDisc;
                settings.LoadLastTrack = loadLastTrack;

                // SpeedInfo speed = (SpeedInfo)comboBoxRecordingSpeed.SelectedItem;
                // settings.WriteSpeedKB = (int)speed.TransferRateKB;

                m_Burner.Burn(settings);

                m_Burner.ReleaseDevice();
            }
            catch(BurnerException bme)
            {
                m_Burner.ReleaseDevice();
                ShowErrorMessage(bme);
            }
        }

        private void Burner_FileProgress(int file, string fileName, int percentCompleted)
        {
            Console.WriteLine("Burning {0} {1}% ...", fileName, percentCompleted);
        }


        private void ShowErrorMessage(Exception e)
        {
            if (null == e)
                return;

            Console.WriteLine($"Error: {e.Message}");
        }
    }
}
