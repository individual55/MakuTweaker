using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using MakuTweakerNew.Properties;
using Microsoft.Win32;

namespace MakuTweakerNew
{
    public partial class PCI : Page
    {
        public struct RECT
        {
            public int Left;

            public int Top;

            public int Right;

            public int Bottom;
        }

        public static class NvmlWrapper
        {
            [DllImport("nvml.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int nvmlInit_v2();

            [DllImport("nvml.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int nvmlShutdown();

            [DllImport("nvml.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int nvmlDeviceGetHandleByIndex_v2(int index, out nint device);

            [DllImport("nvml.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int nvmlDeviceGetPowerUsage(nint device, out uint powerMilliwatts);

            [DllImport("nvml.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int nvmlDeviceGetTemperature(nint device, int sensorType, out uint temp);

            public static double TryGetGpuPowerWatts(int index = 0)
            {
                try
                {
                    if (nvmlInit_v2() != 0)
                    {
                        return -1.0;
                    }
                    if (nvmlDeviceGetHandleByIndex_v2(index, out var device) != 0)
                    {
                        return -1.0;
                    }
                    if (nvmlDeviceGetPowerUsage(device, out var powerMw) != 0)
                    {
                        return -1.0;
                    }
                    nvmlShutdown();
                    return (double)powerMw / 1000.0;
                }
                catch
                {
                    return -1.0;
                }
            }

            public static double TryGetGpuTemperature(int index = 0)
            {
                try
                {
                    if (nvmlInit_v2() != 0)
                    {
                        return -1.0;
                    }
                    if (nvmlDeviceGetHandleByIndex_v2(index, out var device) != 0)
                    {
                        return -1.0;
                    }
                    if (nvmlDeviceGetTemperature(device, 0, out var temp) != 0)
                    {
                        return -1.0;
                    }
                    nvmlShutdown();
                    return temp;
                }
                catch
                {
                    return -1.0;
                }
            }
        }

        private bool isLoaded = false;

        private bool isNotify = true;

        private bool isbycheck = false;

        private MainWindow mw = (MainWindow)Application.Current.MainWindow;

        private List<GpuInfo> _gpus = new List<GpuInfo>();

        private List<StorageInfo> _storageDevices = new List<StorageInfo>();

        public PCI()
        {
            Environment.SetEnvironmentVariable("LHM_NO_RING0", "1");

            InitializeComponent();
            base.PreviewKeyDown += PCI_PreviewKeyDown;
            LoadLang();
            ShowRamInfo();
            ShowCpuInfo();
            ShowCpuExtraInfo();
            ShowMotherboardInfo();
            LoadGpuList();
            LoadStorageList();

            isLoaded = true;
        }

        [DllImport("user32.dll")]
        private static extern bool PrintWindow(nint hwnd, nint hDC, uint nFlags);

        [DllImport("user32.dll")]
        private static extern nint GetWindowDC(nint hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(nint hWnd, nint hDC);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(nint hWnd, out RECT lpRect);

        private void PCI_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                SaveDataToTxt();
            }

            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && e.Key == Key.S)
            {
                SaveDataToTxt();
                e.Handled = true;
            }

            if (e.Key == Key.F2 || e.Key == Key.Tab)
            {
                CaptureBenchmarkScreenshot();
            }
        }

        private void TakeWindowScreenshot()
        {
            Window window = Application.Current.MainWindow;
            nint hwnd = new WindowInteropHelper(window).Handle;
            int oldStyle = GetWindowLong(hwnd, -16);
            int noFrameStyle = oldStyle & -12582913 & -262145 & -524289 & -131073 & -65537;

            SetWindowLong(hwnd, -16, noFrameStyle);
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, 39u);
            Thread.Sleep(60);
            GetWindowRect(hwnd, out var rect);

            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            using (Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            {
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    nint hdc = gfx.GetHdc();
                    PrintWindow(hwnd, hdc, 0u);
                    gfx.ReleaseHdc(hdc);
                }
                try
                {
                    BitmapSource bs = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    Clipboard.SetImage(bs);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message ?? "", "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
                }

                string screenshotsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MakuTweaker Screenshots");
                Directory.CreateDirectory(screenshotsDir);
                string filename = Path.Combine(screenshotsDir, $"screenshot_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.jpg");

                bmp.Save(filename, ImageFormat.Jpeg);

                MessageBox.Show("The screenshot has been saved on your desktop in the *MakuTweaker Screenshots* folder\nand also copied to the clipboard!\n\nСкриншот сохранён на рабочем столе в папке *MakuTweaker Screenshots*\nА также скопирован в буфер обмена!", "Сохранено", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            SetWindowLong(hwnd, -16, oldStyle);
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, 39u);

            [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
            static extern int GetWindowLong(nint hWnd, int nIndex);
            [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
            static extern bool GetWindowRect(nint hWnd, out RECT lpRect);
            [DllImport("user32.dll", EntryPoint = "PrintWindow")]
            static extern bool PrintWindow(nint hwnd, nint hdcBlt, uint nFlags);
            [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
            static extern int SetWindowLong(nint hWnd, int nIndex, int dwNewLong);
            [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
            static extern bool SetWindowPos(nint hWnd, nint hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        }

        private void CaptureBenchmarkScreenshot()
        {
            if (benchmarkResultText == null)
            {
                TakeWindowScreenshot();
                return;
            }

            string originalText = benchmarkResultText.Text;
            Dictionary<string, Dictionary<string, string>> pci = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "pci");
            string f2tipLocalization = pci["main"]["f2tip"];
            string cleanedText = originalText.Replace(f2tipLocalization, "").Trim();
            benchmarkResultText.Text = cleanedText;
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, (Action)delegate
            {
            });

            TakeWindowScreenshot();
            benchmarkResultText.Text = originalText;
        }

        private async void StartBenchmarkButton_Click(object sender, RoutedEventArgs e)
        {
            startBenchmarkButton.IsEnabled = false;
            Dictionary<string, Dictionary<string, string>> pci = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "pci");
            bool isMultithreaded = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);
            benchmarkResultText.Text = (isMultithreaded ? (pci["main"]["running_multicore"] ?? "") : (pci["main"]["running"] ?? ""));

            (double score, long ElapsedMilliseconds) result = await Task.Run(delegate
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                long num = 0L;

                if (isMultithreaded)
                {
                    int processorCount = Environment.ProcessorCount;
                    long[] threadOps = new long[processorCount];
                    Parallel.For(0, processorCount, new ParallelOptions
                    {
                        MaxDegreeOfParallelism = processorCount
                    }, delegate (int i)
                    {
                        double num7 = 1.000001 + i * 1E-05;
                        double num8 = 1.000002 + i * 2E-05;
                        long num9 = 1234567 + i;
                        long num10 = 0L;
                        Random random2 = new Random(i * 37 + Environment.TickCount);
                        while (stopwatch.ElapsedMilliseconds < 10000)
                        {
                            for (int j = 0; j < 200000; j++)
                            {
                                num7 = Math.Sin(num7) * Math.Cos(num8) + Math.Sqrt(Math.Abs(num7 + num8));
                                num8 = num7 * 0.999999 + num8 * 1E-06 + random2.NextDouble();
                                num9 = (num9 * 1664525 + 1013904223) & 0xFFFFFFFFu;
                                num10 += 3;
                            }
                        }
                        threadOps[i] = num10;
                    });
                    num = threadOps.Sum();
                }
                else
                {
                    double num2 = 1.000001;
                    double num3 = 1.000002;
                    long num4 = 1234567L;
                    long num5 = 0L;
                    Random random = new Random(Environment.TickCount);
                    while (stopwatch.ElapsedMilliseconds < 10000)
                    {
                        for (int num6 = 0; num6 < 200000; num6++)
                        {
                            num2 = Math.Sin(num2) * Math.Cos(num3) + Math.Sqrt(Math.Abs(num2 + num3));
                            num3 = num2 * 0.999999 + num3 * 1E-06 + random.NextDouble();
                            num4 = (num4 * 1664525 + 1013904223) & 0xFFFFFFFFu;
                            num5 += 3;
                        }
                    }
                    num = num5;
                }
                stopwatch.Stop();
                double totalSeconds = stopwatch.Elapsed.TotalSeconds;
                double item = num / totalSeconds / 100000.0;

                return (score: item, stopwatch.ElapsedMilliseconds);
            });

            string scoreText = $"{result.score:N0}";
            benchmarkResultText.Text = (isMultithreaded ? $"{pci["main"]["test1multi"]} {pci["main"]["f2tip"]}\n{pci["main"]["test2"]} {scoreText} {pci["main"]["test3"]}" : $"{pci["main"]["test1"]} {pci["main"]["f2tip"]}\n{pci["main"]["test2"]} {scoreText} {pci["main"]["test3"]}");
            startBenchmarkButton.IsEnabled = true;
        }

        public double GetGpuPowerUsageFallback(string gpuName)
        {
            try
            {
                if (gpuName.Contains("NVIDIA", StringComparison.OrdinalIgnoreCase))
                {
                    double nvmlPower = NvmlWrapper.TryGetGpuPowerWatts();
                    if (nvmlPower > 0.0)
                    {
                        return nvmlPower;
                    }
                }
                return GetGpuEstimatedTdp(gpuName);
            }
            catch (Exception)
            {
                return 0.0;
            }
        }

        private double GetGpuEstimatedTdp(string name)
        {
            if (name.Contains("3050"))
            {
                return 115.0;
            }
            if (name.Contains("3060"))
            {
                return 170.0;
            }
            if (name.Contains("3070"))
            {
                return 220.0;
            }
            if (name.Contains("3080"))
            {
                return 320.0;
            }
            if (name.Contains("1030"))
            {
                return 30.0;
            }
            if (name.Contains("A770"))
            {
                return 225.0;
            }
            return 100.0;
        }

        private void PCI_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void DeleteDriverFile(string fileName)
        {
            try
            {
                string driverPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                if (File.Exists(driverPath))
                {
                    File.Delete(driverPath);
                }
            }
            catch (Exception) { }
        }

        private void LoadLang()
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> pci = MainWindow.Localization.LoadLocalization(languageCode, "pci");
            label.Text = pci["main"]["label"];
            labelcpu.Text = pci["main"]["processorlabel"];
            cpul.Text = pci["main"]["processorname"];
            cpucorel.Text = pci["main"]["processorcores"];
            threadsl.Text = pci["main"]["processorthr"];
            corespeedl.Text = pci["main"]["processorfreq"];
            l3cashl.Text = pci["main"]["processorcache"];
            labelRAM.Text = pci["main"]["ramlabel"];
            raml.Text = pci["main"]["ramtotal"];
            ddrl.Text = pci["main"]["ramddr"];
            freql.Text = pci["main"]["ramfreq"];
            MOTHERBOARD.Text = pci["main"]["mblabel"];
            mbnamel.Text = pci["main"]["mbname"];
            biosverl.Text = pci["main"]["mbver"];
            biosdatel.Text = pci["main"]["mbdate"];
            video.Text = pci["main"]["vlabel"];
            videol.Text = pci["main"]["vname"];
            vraml.Text = pci["main"]["vmem"];
            ssdLabel.Text = pci["main"]["ssdl"];
            ssdnLabel.Text = pci["main"]["sname"];
            ssdcLabel.Text = pci["main"]["smem"];
            benchmarkLabel.Text = pci["main"]["benchtitle"];
            startBenchmarkButton.Content = pci["main"]["benchbutton"];
            benchmarkResultText.Text = pci["main"]["benchtip"];
            lookresults.Content = pci["main"]["lookresulbutton"];
        }

        private void ShowCpuInfo()
        {
            try
            {
                string cpuName = "Unknown";
                int coreCount = 0;
                int threadCount = 0;
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select Name, NumberOfCores, NumberOfLogicalProcessors from Win32_Processor"))
                {
                    foreach (ManagementBaseObject item in searcher.Get())
                    {
                        cpuName = item["Name"]?.ToString()?.Trim() ?? cpuName;
                        coreCount += Convert.ToInt32(item["NumberOfCores"]);
                        threadCount += Convert.ToInt32(item["NumberOfLogicalProcessors"]);
                    }
                }
                cpue.Text = cpuName ?? "";
                cpucore.Text = $"{coreCount}";
                threads.Text = $"{threadCount}";
            }
            catch (Exception ex)
            {
                cpue.Text = ex.Message ?? "";
                cpucore.Text = "N/A";
                threads.Text = "N/A";
            }
        }

        private void ShowCpuExtraInfo()
        {
            try
            {
                int maxClockSpeed = 0;
                int l3Cache = 0;
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select MaxClockSpeed, L3CacheSize from Win32_Processor"))
                {
                    foreach (ManagementBaseObject item in searcher.Get())
                    {
                        maxClockSpeed = Convert.ToInt32(item["MaxClockSpeed"]);
                        l3Cache += Convert.ToInt32(item["L3CacheSize"]);
                    }
                }
                double l3MB = Math.Round((double)l3Cache / 1024.0, 1);
                double maxGHz = Math.Round((double)maxClockSpeed / 1000.0, 2);
                corespeed.Text = $"{maxGHz} GHz";
                l3cash.Text = $"{l3MB} MB";
            }
            catch (Exception ex)
            {
                corespeed.Text = ex.Message ?? "";
                l3cash.Text = "N/A";
            }
        }

        private void ShowRamInfo()
        {
            try
            {
                double totalMemoryGB = 0.0;
                int memoryTypeCode = 0;
                int memorySpeed = 0;

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Capacity, MemoryType, SMBIOSMemoryType, Speed FROM Win32_PhysicalMemory"))
                {
                    foreach (ManagementBaseObject item in searcher.Get())
                    {
                        if (item["Capacity"] != null)
                        {
                            totalMemoryGB += Convert.ToDouble(item["Capacity"]) / 1073741824.0;
                        }
                        if (item["SMBIOSMemoryType"] != null)
                        {
                            memoryTypeCode = Convert.ToInt32(item["SMBIOSMemoryType"]);
                        }
                        if (item["Speed"] != null)
                        {
                            memorySpeed = Convert.ToInt32(item["Speed"]);
                        }
                    }
                }

                if (1 == 0) { }

                string text = memoryTypeCode switch
                {
                    20 => "DDR",
                    21 => "DDR2",
                    22 => "DDR2 FB-DIMM",
                    24 => "DDR3",
                    26 => "DDR4",
                    27 => "LPDDR",
                    28 => "LPDDR2",
                    29 => "LPDDR3",
                    30 => "LPDDR4",
                    31 => "DDR5",
                    32 => "LPDDR5",
                    33 => "LPDDR5X",
                    34 => "LPDDR5",
                    35 => "LPDDR5X",
                    _ => "Unknown",
                };
                if (1 == 0) { }

                string memoryType = text;
                int realSpeed = memorySpeed;

                if (memoryType.StartsWith("LPDDR5") && memorySpeed > 7000)
                {
                    realSpeed = 6400;
                }
                else if (memoryType.Contains("DDR4") && memorySpeed > 4000)
                {
                    realSpeed /= 2;
                }
                else if (memoryType.Contains("DDR3") && memorySpeed > 3000)
                {
                    realSpeed /= 2;
                }
                if (memoryType == "Unknown" && memorySpeed > 7000)
                {
                    realSpeed = (int)Math.Round(memorySpeed * 0.75);
                }

                rama.Text = $"{Math.Round(totalMemoryGB, 1)} GB";
                ddre.Text = memoryType;
                freq.Text = $"{realSpeed} MHz";
            }
            catch (Exception ex)
            {
                rama.Text = "Unknown: " + ex.Message;
                ddre.Text = "N/A";
                freq.Text = "N/A";
            }
        }

        private void ShowMotherboardInfo()
        {
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Product, Manufacturer FROM Win32_BaseBoard"))
                {
                    foreach (ManagementBaseObject item in searcher.Get())
                    {
                        string product = item["Product"]?.ToString() ?? "Unknown";
                        string manufacturer = item["Manufacturer"]?.ToString() ?? "Unknown";
                        mbname.Text = manufacturer + " " + product;
                    }
                }

                using ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("SELECT SMBIOSBIOSVersion, ReleaseDate FROM Win32_BIOS");
                foreach (ManagementBaseObject item2 in searcher2.Get())
                {
                    string biosVersion = item2["SMBIOSBIOSVersion"]?.ToString() ?? "Unknown";
                    string biosDateRaw = item2["ReleaseDate"]?.ToString() ?? "";
                    string biosDateFormatted = "Unknown";
                    if (!string.IsNullOrEmpty(biosDateRaw) && biosDateRaw.Length >= 8)
                    {
                        string year = biosDateRaw.Substring(0, 4);
                        string month = biosDateRaw.Substring(4, 2);
                        string day = biosDateRaw.Substring(6, 2);
                        biosDateFormatted = $"{day}.{month}.{year}";
                    }
                    biosver.Text = biosVersion;
                    biosdate.Text = biosDateFormatted;
                }
            }
            catch (Exception ex)
            {
                mbname.Text = ex.Message ?? "";
                biosver.Text = "N/A";
                biosdate.Text = "N/A";
            }
        }

        private void LoadStorageList()
        {
            try
            {
                _storageDevices = StorageHelper.GetAllStorageDevices();
                ssdComboBox.Items.Clear();
                if (_storageDevices.Count == 0)
                {
                    ssdnValue.Text = "N/A";
                    ssdcValue.Text = "N/A";
                    return;
                }
                for (int i = 0; i < _storageDevices.Count; i++)
                {
                    string displayName = ((!string.IsNullOrWhiteSpace(_storageDevices[i].Name)) ? _storageDevices[i].Name : $"Drive #{i + 1}");
                    ssdComboBox.Items.Add($"{i + 1}. {displayName}");
                }
                ssdComboBox.SelectedIndex = 0;
                UpdateStorageInfo(0);
            }
            catch (Exception)
            {
                ssdnValue.Text = "N/A";
                ssdcValue.Text = "N/A";
            }
        }

        private void SSDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ssdComboBox.SelectedIndex >= 0)
            {
                UpdateStorageInfo(ssdComboBox.SelectedIndex);
            }
        }

        private void UpdateStorageInfo(int index)
        {
            if (index >= 0 && index < _storageDevices.Count)
            {
                StorageInfo storage = _storageDevices[index];
                ssdnValue.Text = storage.Name;
                ssdcValue.Text = storage.CapacityFormatted;
            }
        }

        private void LoadGpuList()
        {
            try
            {
                _gpus = GpuHelper.GetAllGpus();
                videoComboBox.Items.Clear();
                if (_gpus.Count == 0)
                {
                    videon.Text = "N/A";
                    vram.Text = "N/A";
                    return;
                }
                for (int i = 0; i < _gpus.Count; i++)
                {
                    string displayName = ((!string.IsNullOrWhiteSpace(_gpus[i].Name)) ? _gpus[i].Name : $"GPU #{i + 1}");
                    videoComboBox.Items.Add($"{i + 1}. {displayName}");
                }
                videoComboBox.SelectedIndex = 0;
                UpdateGpuInfo(0);
            }
            catch (Exception)
            {
                videon.Text = "N/A";
                vram.Text = "N/A";
            }
        }

        private void VideoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (videoComboBox.SelectedIndex >= 0)
            {
                UpdateGpuInfo(videoComboBox.SelectedIndex);
            }
        }

        private void UpdateGpuInfo(int index)
        {
            if (index >= 0 && index < _gpus.Count)
            {
                GpuInfo gpu = _gpus[index];
                videon.Text = gpu.Name;
                vram.Text = gpu.VRamFormatted;
            }
        }

        private void LookResults_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // MAKUBENCH, HAHA!

                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://adderly.top/makubench",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message ?? "", "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private void SaveDataToTxt()
        {
            try
            {
                Dictionary<string, Dictionary<string, string>> pci = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "pci");
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "TXT File| *.txt",
                    Title = "MakuTweaker",
                    FileName = "MakuTweaker System Info.txt"
                };

                if (saveFileDialog.ShowDialog() != true)
                {
                    return;
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("MakuTweaker // MarkAdderly");
                sb.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sb.AppendLine();
                sb.AppendLine();
                StringBuilder stringBuilder = sb;
                StringBuilder stringBuilder2 = stringBuilder;
                StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(8, 1, stringBuilder);
                handler.AppendLiteral("=== ");
                handler.AppendFormatted(pci["main"]["processorlabel"]);
                handler.AppendLiteral(" ===");
                stringBuilder2.AppendLine(ref handler);
                stringBuilder = sb;
                StringBuilder stringBuilder3 = stringBuilder;
                handler = new StringBuilder.AppendInterpolatedStringHandler(1, 2, stringBuilder);
                handler.AppendFormatted(pci["main"]["processorname"]);
                handler.AppendLiteral(" ");
                handler.AppendFormatted(cpue.Text);
                stringBuilder3.AppendLine(ref handler);
                stringBuilder = sb;
                StringBuilder stringBuilder4 = stringBuilder;
                handler = new StringBuilder.AppendInterpolatedStringHandler(1, 2, stringBuilder);
                handler.AppendFormatted(pci["main"]["processorcores"]);
                handler.AppendLiteral(" ");
                handler.AppendFormatted(cpucore.Text);
                stringBuilder4.AppendLine(ref handler);
                stringBuilder = sb;
                StringBuilder stringBuilder5 = stringBuilder;
                handler = new StringBuilder.AppendInterpolatedStringHandler(1, 2, stringBuilder);
                handler.AppendFormatted(pci["main"]["processorthr"]);
                handler.AppendLiteral(" ");
                handler.AppendFormatted(threads.Text);
                stringBuilder5.AppendLine(ref handler);
                stringBuilder = sb;
                StringBuilder stringBuilder6 = stringBuilder;
                handler = new StringBuilder.AppendInterpolatedStringHandler(1, 2, stringBuilder);
                handler.AppendFormatted(pci["main"]["processorfreq"]);
                handler.AppendLiteral(" ");
                handler.AppendFormatted(corespeed.Text);
                stringBuilder6.AppendLine(ref handler);
                stringBuilder = sb;
                StringBuilder stringBuilder7 = stringBuilder;
                handler = new StringBuilder.AppendInterpolatedStringHandler(1, 2, stringBuilder);
                handler.AppendFormatted(pci["main"]["processorcache"]);
                handler.AppendLiteral(" ");
                handler.AppendFormatted(l3cash.Text);
                stringBuilder7.AppendLine(ref handler);
                sb.AppendLine();
                stringBuilder = sb;
                StringBuilder stringBuilder8 = stringBuilder;
                handler = new StringBuilder.AppendInterpolatedStringHandler(8, 1, stringBuilder);
                handler.AppendLiteral("=== ");
                handler.AppendFormatted(pci["main"]["ramlabel"]);
                handler.AppendLiteral(" ===");
                stringBuilder8.AppendLine(ref handler);
                stringBuilder = sb;
                StringBuilder stringBuilder9 = stringBuilder;
                handler = new StringBuilder.AppendInterpolatedStringHandler(1, 2, stringBuilder);
                handler.AppendFormatted(pci["main"]["ramtotal"]);
                handler.AppendLiteral(" ");
                handler.AppendFormatted(rama.Text);
                stringBuilder9.AppendLine(ref handler);
                stringBuilder = sb;
                StringBuilder stringBuilder10 = stringBuilder;
                handler = new StringBuilder.AppendInterpolatedStringHandler(1, 2, stringBuilder);
                handler.AppendFormatted(pci["main"]["ramddr"]);
                handler.AppendLiteral(" ");
                handler.AppendFormatted(ddre.Text);
                stringBuilder10.AppendLine(ref handler);
                stringBuilder = sb;
                StringBuilder stringBuilder11 = stringBuilder;
                handler = new StringBuilder.AppendInterpolatedStringHandler(1, 2, stringBuilder);
                handler.AppendFormatted(pci["main"]["ramfreq"]);
                handler.AppendLiteral(" ");
                handler.AppendFormatted(freq.Text);
                stringBuilder11.AppendLine(ref handler);
                sb.AppendLine();
                stringBuilder = sb;
                StringBuilder stringBuilder12 = stringBuilder;
                handler = new StringBuilder.AppendInterpolatedStringHandler(8, 1, stringBuilder);
                handler.AppendLiteral("=== ");
                handler.AppendFormatted(pci["main"]["mblabel"]);
                handler.AppendLiteral(" ===");
                stringBuilder12.AppendLine(ref handler);
                stringBuilder = sb;
                StringBuilder stringBuilder13 = stringBuilder;
                handler = new StringBuilder.AppendInterpolatedStringHandler(1, 2, stringBuilder);
                handler.AppendFormatted(pci["main"]["mbname"]);
                handler.AppendLiteral(" ");
                handler.AppendFormatted(mbname.Text);
                stringBuilder13.AppendLine(ref handler);
                stringBuilder = sb;
                StringBuilder stringBuilder14 = stringBuilder;
                handler = new StringBuilder.AppendInterpolatedStringHandler(1, 2, stringBuilder);
                handler.AppendFormatted(pci["main"]["mbver"]);
                handler.AppendLiteral(" ");
                handler.AppendFormatted(biosver.Text);
                stringBuilder14.AppendLine(ref handler);
                stringBuilder = sb;
                StringBuilder stringBuilder15 = stringBuilder;
                handler = new StringBuilder.AppendInterpolatedStringHandler(1, 2, stringBuilder);
                handler.AppendFormatted(pci["main"]["mbdate"]);
                handler.AppendLiteral(" ");
                handler.AppendFormatted(biosdate.Text);
                stringBuilder15.AppendLine(ref handler);
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine();
                stringBuilder = sb;
                StringBuilder stringBuilder16 = stringBuilder;
                handler = new StringBuilder.AppendInterpolatedStringHandler(8, 1, stringBuilder);
                handler.AppendLiteral("=== ");
                handler.AppendFormatted(pci["main"]["vlabel"]);
                handler.AppendLiteral(" ===");
                stringBuilder16.AppendLine(ref handler);

                if (_gpus.Count == 0)
                {
                    sb.AppendLine("No GPU found");
                }
                else
                {
                    for (int i = 0; i < _gpus.Count; i++)
                    {
                        GpuInfo gpu = _gpus[i];
                        stringBuilder = sb;
                        StringBuilder stringBuilder17 = stringBuilder;
                        handler = new StringBuilder.AppendInterpolatedStringHandler(3, 2, stringBuilder);
                        handler.AppendLiteral("[");
                        handler.AppendFormatted(i + 1);
                        handler.AppendLiteral("] ");
                        handler.AppendFormatted(gpu.Name);
                        stringBuilder17.AppendLine(ref handler);
                        stringBuilder = sb;
                        StringBuilder stringBuilder18 = stringBuilder;
                        handler = new StringBuilder.AppendInterpolatedStringHandler(1, 2, stringBuilder);
                        handler.AppendFormatted(pci["main"]["vmem"]);
                        handler.AppendLiteral(" ");
                        handler.AppendFormatted(gpu.VRamFormatted);
                        stringBuilder18.AppendLine(ref handler);
                        sb.AppendLine();
                    }
                }

                sb.AppendLine();
                sb.AppendLine();
                stringBuilder = sb;
                StringBuilder stringBuilder19 = stringBuilder;
                handler = new StringBuilder.AppendInterpolatedStringHandler(8, 1, stringBuilder);
                handler.AppendLiteral("=== ");
                handler.AppendFormatted(pci["main"]["ssdl"]);
                handler.AppendLiteral(" ===");
                stringBuilder19.AppendLine(ref handler);

                for (int i = 0; i < _storageDevices.Count; i++)
                {
                    StorageInfo storage = _storageDevices[i];
                    stringBuilder = sb;
                    StringBuilder stringBuilder20 = stringBuilder;
                    handler = new StringBuilder.AppendInterpolatedStringHandler(3, 2, stringBuilder);
                    handler.AppendLiteral("[");
                    handler.AppendFormatted(i + 1);
                    handler.AppendLiteral("] ");
                    handler.AppendFormatted(storage.Name);
                    stringBuilder20.AppendLine(ref handler);
                    stringBuilder = sb;
                    StringBuilder stringBuilder21 = stringBuilder;
                    handler = new StringBuilder.AppendInterpolatedStringHandler(1, 2, stringBuilder);
                    handler.AppendFormatted(pci["main"]["smem"]);
                    handler.AppendLiteral(" ");
                    handler.AppendFormatted(storage.CapacityFormatted);
                    stringBuilder21.AppendLine(ref handler);
                    sb.AppendLine();
                }
                sb.AppendLine();
                File.WriteAllText(saveFileDialog.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("System information saved successfully!\nСистемная информация была успешно сохранена!", "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message ?? "", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }
    }
}
