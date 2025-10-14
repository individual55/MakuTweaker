using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;
using MakuTweakerNew.Properties;
using Microsoft.Win32;

namespace MakuTweakerNew
{
    public partial class SysAndRec : System.Windows.Controls.Page
    {
        private bool isLoaded = false;

        private MainWindow mw = (MainWindow)System.Windows.Application.Current.MainWindow;

        public SysAndRec()
        {
            InitializeComponent();
            sr1.IsOn = Settings.Default.sr1;
            sr4.IsOn = Settings.Default.sr4;
            sr5.IsOn = Settings.Default.sr5;
            t13.IsOn = Settings.Default.powercfgsl;
            sfc.IsEnabled = !Settings.Default.b3;
            dism.IsEnabled = !Settings.Default.b4;
            temp.IsEnabled = !Settings.Default.b6;
            checkReg();
            LoadLang();
            isLoaded = true;
        }

        private int checkWinVer()
        {
            string keyPath = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
            string valueName = "CurrentBuild";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath))
            {
                if (key != null)
                {
                    object value = key.GetValue(valueName);
                    if (value != null && int.TryParse(value.ToString(), out var build))
                    {
                        return build;
                    }
                }
            }
            return 19045;
        }

        private void sr1_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.sr1 = sr1.IsOn;
                if (sr1.IsOn)
                {
                    Process.Start("cmd.exe", "/c \"bcdedit /set \"{current}\" bootmenupolicy legacy\"");
                }
                else
                {
                    Process.Start("cmd.exe", "/c \"bcdedit /set \"{current}\" bootmenupolicy standard\"");
                }
            }
        }

        private void sr4_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.sr4 = sr4.IsOn;
                if (sr4.IsOn)
                {
                    Process.Start("cmd.exe", "/c \"bcdedit /set \"{globalsettings}\" advancedoptions true\"");
                }
                else
                {
                    Process.Start("cmd.exe", "/c \"bcdedit /set \"{globalsettings}\" advancedoptions false\"");
                }
            }
        }

        private void sr5_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.sr5 = sr5.IsOn;
                if (sr5.IsOn)
                {
                    Process.Start("cmd.exe", "/k compact /compactos:always");
                }
                else
                {
                    Process.Start("cmd.exe", "/k compact /compactos:never");
                }
                sr5.IsEnabled = false;
            }
        }

        private void sr6_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.sr6 = sr6.IsOn;
                if (sr6.IsOn)
                {
                    Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\BitLocker").SetValue("PreventDeviceEncryption", 1, RegistryValueKind.DWord);
                }
                else
                {
                    Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\BitLocker").SetValue("PreventDeviceEncryption", 0, RegistryValueKind.DWord);
                }
            }
        }

        private void sfc_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("cmd.exe", "/k sfc /scannow");
            mw.RebootNotify(3);
            sfc.IsEnabled = false;
            Settings.Default.b3 = !sfc.IsEnabled;
        }

        private void dism_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("cmd.exe", "/C DISM /Online /Cleanup-Image /RestoreHealth");
            mw.RebootNotify(3);
            dism.IsEnabled = false;
            Settings.Default.b4 = !dism.IsEnabled;
        }

        private void temp_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("cmd.exe", "/k del /q /f %temp%");
            temp.IsEnabled = false;
            Settings.Default.b6 = !temp.IsEnabled;
        }

        private void t9_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.o9 = t9.IsOn;
                if (t9.IsOn)
                {
                    Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager").SetValue("AutoChkTimeout", 60);
                }
                else
                {
                    Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager").SetValue("AutoChkTimeout", 8);
                }
            }
        }

        private void LoadLang()
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> sr = MainWindow.Localization.LoadLocalization(languageCode, "sr");
            Dictionary<string, Dictionary<string, string>> basel = MainWindow.Localization.LoadLocalization(languageCode, "base");
            Dictionary<string, Dictionary<string, string>> oth = MainWindow.Localization.LoadLocalization(languageCode, "oth");
            Dictionary<string, Dictionary<string, string>> stask = MainWindow.Localization.LoadLocalization(languageCode, "stask");
            label.Text = sr["main"]["label"];
            l3.Text = sr["main"]["sr3l"];
            l4.Text = sr["main"]["sr4l"];
            l6.Text = sr["main"]["sr6l"];
            l9.Text = oth["main"]["o10"];
            sfc.Content = sr["main"]["b2"];
            dism.Content = sr["main"]["b2"];
            temp.Content = sr["main"]["b4"];
            report.Content = oth["main"]["o10b"];
            sr1.Header = sr["main"]["sr1"];
            sr4.Header = sr["main"]["sr4"];
            sr5.Header = sr["main"]["sr5"];
            sr6.Header = sr["main"]["sr6"];
            t9.Header = oth["main"]["o9"];
            t10.Header = sr["main"]["sr7"];
            t11.Header = sr["main"]["sr8"];
            t12.Header = sr["main"]["sr11"];
            t13.Header = sr["main"]["sr10"];
            t14.Header = oth["main"]["o1"];
            t15.Header = oth["main"]["o2"];
            t16.Header = oth["main"]["o3"];
            t18.Header = sr["main"]["sr9"];
            st5.Header = stask["main"]["st5"];
            sr1.OffContent = basel["def"]["off"];
            sr4.OffContent = basel["def"]["off"];
            sr5.OffContent = basel["def"]["off"];
            sr6.OffContent = basel["def"]["off"];
            t9.OffContent = basel["def"]["off"];
            t10.OffContent = basel["def"]["off"];
            t11.OffContent = basel["def"]["off"];
            t12.OffContent = basel["def"]["off"];
            t13.OffContent = basel["def"]["off"];
            t14.OffContent = basel["def"]["off"];
            t15.OffContent = basel["def"]["off"];
            t16.OffContent = basel["def"]["off"];
            t18.OffContent = basel["def"]["off"];
            st5.OffContent = basel["def"]["off"];
            sr1.OnContent = basel["def"]["on"];
            sr4.OnContent = basel["def"]["on"];
            sr5.OnContent = basel["def"]["on"];
            sr6.OnContent = basel["def"]["on"];
            t9.OnContent = basel["def"]["on"];
            t10.OnContent = basel["def"]["on"];
            t11.OnContent = basel["def"]["on"];
            t12.OnContent = basel["def"]["on"];
            t13.OnContent = basel["def"]["on"];
            t14.OnContent = basel["def"]["on"];
            t15.OnContent = basel["def"]["on"];
            t16.OnContent = basel["def"]["on"];
            t18.OnContent = basel["def"]["on"];
            st5.OnContent = basel["def"]["on"];
        }

        private void checkReg()
        {
            sr6.IsOn = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\BitLocker")?.GetValue("PreventDeviceEncryption")?.Equals(1) == true;
            t9.IsOn = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager")?.GetValue("AutoChkTimeout")?.Equals(60) == true;
            t10.IsOn = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceGuard\\Scenarios")?.GetValue("HypervisorEnforcedCodeIntegrity")?.Equals(0) == true;
            t11.IsOn = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Power")?.GetValue("HibernateEnabled")?.Equals(0) == true;
            t12.IsOn = !(Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management")?.GetValue("PagingFiles") is string[] arr) || arr.All((string s) => string.IsNullOrWhiteSpace(s));
            t14.IsOn = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", writable: true)?.GetValue("EnableSmartScreen")?.Equals(0) == true || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer", writable: true)?.GetValue("SmartScreenEnabled")?.Equals("Off") == true;
            t15.IsOn = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", writable: true)?.GetValue("EnableLUA")?.Equals(0) == true;
            t16.IsOn = Registry.CurrentUser.OpenSubKey("Control Panel\\Accessibility\\StickyKeys", writable: true)?.GetValue("Flags")?.Equals("506") == true || Registry.CurrentUser.OpenSubKey("Control Panel\\Accessibility\\ToggleKeys", writable: true)?.GetValue("Flags")?.Equals("58") == true || Registry.CurrentUser.OpenSubKey("Control Panel\\Accessibility\\Keyboard Response", writable: true)?.GetValue("Flags")?.Equals("122") == true;
            t18.IsOn = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceGuard")?.GetValue("EnableVirtualizationBasedSecurity")?.Equals(0) == true;
            st5.IsOn = Registry.CurrentUser.OpenSubKey("Software\\Policies\\Microsoft\\Windows\\Explorer", writable: true)?.GetValue("DisableSearchBoxSuggestions")?.Equals(1) == true;
        }

        private void t10_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                if (t10.IsOn)
                {
                    Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceGuard\\Scenarios").SetValue("HypervisorEnforcedCodeIntegrity", 0);
                }
                else
                {
                    Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceGuard\\Scenarios").SetValue("HypervisorEnforcedCodeIntegrity", 1);
                }
                mw.RebootNotify(1);
            }
        }

        private void t11_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                if (t11.IsOn)
                {
                    Process.Start("cmd.exe", "/C powercfg /h off");
                }
                else
                {
                    Process.Start("cmd.exe", "/C powercfg /h on");
                }
                mw.RebootNotify(1);
            }
        }

        private void t12_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                if (t12.IsOn)
                {
                    Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management").SetValue("PagingFiles", new string[0], RegistryValueKind.MultiString);
                }
                else
                {
                    Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management").SetValue("PagingFiles", new string[1] { "?:\\pagefile.sys" }, RegistryValueKind.MultiString);
                }
                mw.RebootNotify(1);
            }
        }

        private void t13_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                if (t13.IsOn)
                {
                    Process.Start("cmd.exe", "/C powercfg -change -monitor-timeout-ac 0");
                    Process.Start("cmd.exe", "/C powercfg -change -monitor-timeout-dc 0");
                    Process.Start("cmd.exe", "/C powercfg -change -standby-timeout-ac 0");
                    Process.Start("cmd.exe", "/C powercfg -change -standby-timeout-dc 0");
                    Settings.Default.powercfgsl = true;
                }
                else
                {
                    Process.Start("cmd.exe", "/C powercfg -change -monitor-timeout-ac 10");
                    Process.Start("cmd.exe", "/C powercfg -change -monitor-timeout-dc 5");
                    Process.Start("cmd.exe", "/C powercfg -change -standby-timeout-ac 30");
                    Process.Start("cmd.exe", "/C powercfg -change -standby-timeout-dc 15");
                    Settings.Default.powercfgsl = false;
                }
                Settings.Default.Save();
            }
        }

        private void t14_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                if (t14.IsOn)
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System").SetValue("EnableSmartScreen", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer").SetValue("SmartScreenEnabled", "Off", RegistryValueKind.String);
                    Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Attachments").SetValue("SaveZoneInformation", 1, RegistryValueKind.DWord);
                }
                else
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System").SetValue("EnableSmartScreen", 1);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer").SetValue("SmartScreenEnabled", "Warn", RegistryValueKind.String);
                    Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Attachments").SetValue("SaveZoneInformation", 0, RegistryValueKind.DWord);
                }
            }
        }

        private void t15_Toggled(object sender, RoutedEventArgs e)
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> sr = MainWindow.Localization.LoadLocalization(languageCode, "sr");
            if (!isLoaded)
            {
                return;
            }
            if (checkWinVer() >= 22621 && t15.IsOn)
            {
                DialogResult res = System.Windows.Forms.MessageBox.Show(sr["status"]["uacwarn"], "MakuTweaker", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (res == DialogResult.No)
                {
                    t15.IsOn = false;
                    return;
                }
            }
            if (t15.IsOn)
            {
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System").SetValue("EnableLUA", 0);
                Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Attachments")?.SetValue("SaveZoneInformation", 1, RegistryValueKind.DWord);
                Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Associations")?.SetValue("LowRiskFileTypes", ".exe;.msi;.bat;", RegistryValueKind.String);
            }
            else
            {
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System").SetValue("EnableLUA", 1);
            }
        }

        private void t16_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                if (t16.IsOn)
                {
                    Registry.CurrentUser.CreateSubKey("Control Panel\\Accessibility\\StickyKeys").SetValue("Flags", "506");
                    Registry.CurrentUser.CreateSubKey("Control Panel\\Accessibility\\Keyboard Response").SetValue("Flags", "122");
                    Registry.CurrentUser.CreateSubKey("Control Panel\\Accessibility\\ToggleKeys").SetValue("Flags", "58");
                }
                else
                {
                    Registry.CurrentUser.CreateSubKey("Control Panel\\Accessibility\\StickyKeys").SetValue("Flags", "510");
                    Registry.CurrentUser.CreateSubKey("Control Panel\\Accessibility\\Keyboard Response").SetValue("Flags", "126");
                    Registry.CurrentUser.CreateSubKey("Control Panel\\Accessibility\\ToggleKeys").SetValue("Flags", "62");
                }
                mw.RebootNotify(1);
            }
        }

        private void report_Click(object sender, RoutedEventArgs e)
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> oth = MainWindow.Localization.LoadLocalization(languageCode, "oth");
            Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog1.Filter = "HTML (*.html)|*.html";
            saveFileDialog1.Title = "Microsoft Battery Report";
            saveFileDialog1.FileName = "battery-report.html";

            if (saveFileDialog1.ShowDialog() == true)
            {
                string reportPath = saveFileDialog1.FileName;

                try
                {
                    Process.Start("cmd.exe", "/c powercfg /batteryreport /output \"" + reportPath + "\"");
                    mw.ChSt(oth["status"]["o1b"]);
                }
                catch { }
            }
        }

        private void t18_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                if (t18.IsOn)
                {
                    Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceGuard").SetValue("EnableVirtualizationBasedSecurity", 0);
                }
                else
                {
                    Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceGuard").SetValue("EnableVirtualizationBasedSecurity", 1);
                }
                mw.RebootNotify(1);
            }
        }

        private void st5_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }

            Settings.Default.st5 = st5.IsOn;
            if (st5.IsOn)
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey("Software\\Policies\\Microsoft\\Windows\\Explorer").SetValue("DisableSearchBoxSuggestions", 1);
                    return;
                }
                catch
                {
                    return;
                }
            }

            try
            {
                Registry.CurrentUser.CreateSubKey("Software\\Policies\\Microsoft\\Windows\\Explorer").SetValue("DisableSearchBoxSuggestions", 0);
            }
            catch { }
        }
    }
}
