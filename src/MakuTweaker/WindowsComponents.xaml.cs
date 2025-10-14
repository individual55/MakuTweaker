using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MakuTweakerNew.Properties;
using Microsoft.Win32;

namespace MakuTweakerNew
{
    public partial class WindowsComponents : Page
    {
        private bool isLoaded = false;

        private MainWindow mw = (MainWindow)Application.Current.MainWindow;

        public WindowsComponents()
        {
            InitializeComponent();
            if (checkWinEd() == "Core" || checkWinEd() == "CoreSingleLanguage" || checkWinEd() == "CoreCountrySpecific" || checkWinEd() == "CoreN")
            {
                sr8L.Visibility = Visibility.Visible;
                lgp.Visibility = Visibility.Visible;
            }
            dp.IsEnabled = !Settings.Default.b1;
            dnet.IsEnabled = !Settings.Default.b2;
            sxs.IsEnabled = !Settings.Default.b5;
            pv.IsEnabled = !Settings.Default.b10;
            lgp.IsEnabled = !Settings.Default.b8;
            pwsh.IsEnabled = !Settings.Default.pwsh;
            LoadLang(Settings.Default.lang);
            isLoaded = true;
        }

        private string checkWinEd()
        {
            string keyPath = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
            string valueName = "EditionID";
            using RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath);
            object value = key.GetValue(valueName);
            return value.ToString();
        }

        private void LoadLang(string lang)
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> basel = MainWindow.Localization.LoadLocalization(languageCode, "base");
            Dictionary<string, Dictionary<string, string>> stask = MainWindow.Localization.LoadLocalization(languageCode, "stask");
            Dictionary<string, Dictionary<string, string>> sr = MainWindow.Localization.LoadLocalization(languageCode, "sr");
            Dictionary<string, Dictionary<string, string>> oth = MainWindow.Localization.LoadLocalization(languageCode, "oth");
            label.Text = stask["main"]["label"];
            l1.Text = sr["main"]["sr1l"];
            l2.Text = sr["main"]["sr2l"];
            l5.Text = sr["main"]["sr5l"];
            l8.Text = oth["main"]["o11"];
            l10.Text = sr["main"]["pwsh"];
            l11.Text = sr["main"]["dvrq"];
            sr8L.Text = sr["main"]["sr8l"];
            dp.Content = sr["main"]["b1"];
            dnet.Content = sr["main"]["b1"];
            sxs.Content = sr["main"]["b3"];
            lgp.Content = sr["main"]["b5"];
            dvr.Content = sr["main"]["b6"];
            pv.Content = oth["main"]["o11b"];
            pwsh.Content = oth["main"]["o11b"];
        }

        private void pwsh_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("powershell", "-Command Set-ExecutionPolicy RemoteSigned -Force")
            {
                CreateNoWindow = true,
                UseShellExecute = false
            });
            pwsh.IsEnabled = false;
            Settings.Default.pwsh = !pwsh.IsEnabled;
        }

        private void dp_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("cmd.exe", "/C dism /online /Enable-Feature /FeatureName:DirectPlay /All");
            dp.IsEnabled = false;
            Settings.Default.b1 = !dp.IsEnabled;
        }

        private void dnet_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("powershell.exe", "/C Add-WindowsCapability -Online -Name NetFx3~~~~\"");
            dnet.IsEnabled = false;
            Settings.Default.b2 = !dnet.IsEnabled;
        }

        private void sxs_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("cmd.exe", "/C dism /Online /Cleanup-Image /StartComponentCleanup /ResetBase");
            mw.RebootNotify(3);
            sxs.IsEnabled = false;
            Settings.Default.b5 = !sxs.IsEnabled;
        }

        private void lgp_Click(object sender, RoutedEventArgs e)
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> sr = MainWindow.Localization.LoadLocalization(languageCode, "sr");
            string batContent = "\r\n            pushd \"%~dp0\"\r\n\r\n            dir /b %SystemRoot%\\servicing\\Packages\\Microsoft-Windows-GroupPolicy-ClientExtensions-Package~3*.mum >List.txt \r\n            dir /b %SystemRoot%\\servicing\\Packages\\Microsoft-Windows-GroupPolicy-ClientTools-Package~3*.mum >>List.txt \r\n\r\n            for /f %%i in ('findstr /i . List.txt 2^>nul') do dism /online /norestart /add-package:\"%SystemRoot%\\servicing\\Packages\\%%i\"";
            string tempBatFilePath = Path.Combine(Path.GetTempPath(), "script.bat");
            File.WriteAllText(tempBatFilePath, batContent);
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c \"" + tempBatFilePath + "\"";
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.CreateNoWindow = false;
            mw.ChSt(sr["status"]["sr8"]);
            try
            {
                process.Start();
            }
            catch
            {
            }
            lgp.IsEnabled = false;
            Settings.Default.b8 = !lgp.IsEnabled;
        }

        private void pv_Click(object sender, RoutedEventArgs e)
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> oth = MainWindow.Localization.LoadLocalization(languageCode, "oth");
            try
            {
                using (RegistryKey key = Registry.ClassesRoot.CreateSubKey("Applications\\photoviewer.dll\\shell\\open"))
                {
                    key.SetValue("MuiVerb", "@photoviewer.dll,-3043");
                }
                using (RegistryKey key2 = Registry.ClassesRoot.CreateSubKey("Applications\\photoviewer.dll\\shell\\open\\command"))
                {
                    key2.SetValue("", "%SystemRoot%\\System32\\rundll32.exe \"%ProgramFiles%\\Windows Photo Viewer\\PhotoViewer.dll\", ImageViewer_Fullscreen %1", RegistryValueKind.String);
                }
                using (RegistryKey key3 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows Photo Viewer\\Capabilities\\FileAssociations"))
                {
                    key3.SetValue(".bmp", "PhotoViewer.FileAssoc.Tiff");
                    key3.SetValue(".gif", "PhotoViewer.FileAssoc.Tiff");
                    key3.SetValue(".jpeg", "PhotoViewer.FileAssoc.Tiff");
                    key3.SetValue(".jpg", "PhotoViewer.FileAssoc.Tiff");
                    key3.SetValue(".png", "PhotoViewer.FileAssoc.Tiff");
                }
                pv.IsEnabled = false;
                Settings.Default.b10 = true;
                mw.ChSt(oth["status"]["o2b"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void dvr_Click(object sender, RoutedEventArgs e)
        {
            Registry.CurrentUser.CreateSubKey("System\\GameConfigStore").SetValue("GameDVR_Enabled", 0, RegistryValueKind.DWord);
            Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\GameDVR").SetValue("AllowGameDVR", 0, RegistryValueKind.DWord);
            dvr.IsEnabled = false;
        }
    }
}