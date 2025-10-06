using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MakuTweakerNew.Properties;
using Microsoft.Win32;

namespace MakuTweakerNew
{
    public partial class Personalization : Page
    {
        private bool isLoaded;

        private MainWindow mw = (MainWindow)System.Windows.Application.Current.MainWindow;

        public Personalization()
        {
            InitializeComponent();
            color.SelectedIndex = Settings.Default.p3;
            checkReg();
            LoadLang();
            if (checkWinVer() < 22000)
            {
                p1.Visibility = Visibility.Collapsed;
            }
            sr2.IsOn = Settings.Default.sr2;
            sr3.IsOn = Settings.Default.sr3;
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

        private void apN_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, Dictionary<string, string>> per = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "per");
            Settings.Default.p2 = newname.Text;
            string folderName = newname.Text;
            string command = "reg add HKCU\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\NamingTemplates /v RenameNameTemplate /t REG_SZ /d \"" + folderName + "\" /f";
            Process.Start("cmd.exe", "/c " + command);
            mw.ChSt(per["status"]["apN"]);
        }

        private void stN_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, Dictionary<string, string>> per = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "per");
            Settings.Default.p2 = string.Empty;
            newname.Text = string.Empty;
            string command = "reg delete HKCU\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\NamingTemplates /v RenameNameTemplate /f";
            Process.Start("cmd.exe", "/c " + command);
            mw.ChSt(per["status"]["stN"]);
        }

        private void p1_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.p1 = p1.IsOn;
                if (p1.IsOn)
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\PolicyManager\\current\\device\\Education").SetValue("EnableEduThemes", 1);
                }
                else
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\PolicyManager\\current\\device\\Education").SetValue("EnableEduThemes", 0);
                }
                mw.RebootNotify(1);
            }
        }

        private void apC_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, Dictionary<string, string>> per = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "per");
            Settings.Default.p3 = color.SelectedIndex;
            string regPath = "Control Panel\\Colors";
            string highlightValue = "";
            string hotTrackingColorValue = "";
            switch (color.SelectedIndex)
            {
                case 0:
                    highlightValue = "51 153 255";
                    hotTrackingColorValue = "0 102 204";
                    break;
                case 1:
                    highlightValue = "0 100 100";
                    hotTrackingColorValue = "0 100 100";
                    break;
                case 2:
                    highlightValue = "180 0 180";
                    hotTrackingColorValue = "110 0 110";
                    break;
                case 3:
                    highlightValue = "0 90 30";
                    hotTrackingColorValue = "0 90 30";
                    break;
                case 4:
                    highlightValue = "100 40 0";
                    hotTrackingColorValue = "100 40 0";
                    break;
                case 5:
                    highlightValue = "135 0 0";
                    hotTrackingColorValue = "135 0 0";
                    break;
                case 6:
                    highlightValue = "15, 0, 120";
                    hotTrackingColorValue = "15, 0, 120";
                    break;
                case 7:
                    highlightValue = "40 40 40";
                    hotTrackingColorValue = "40 40 40";
                    break;
                default:
                    highlightValue = "51 153 255";
                    hotTrackingColorValue = "0 102 204";
                    return;
            }
            RegistryKey key = Registry.CurrentUser.OpenSubKey(regPath, writable: true);
            if (key != null)
            {
                key.SetValue("HightLight", highlightValue, RegistryValueKind.String);
                key.SetValue("Hilight", highlightValue, RegistryValueKind.String);
                key.SetValue("HotTrackingColor", hotTrackingColorValue, RegistryValueKind.String);
            }
            mw.ChSt(per["status"]["apC"]);
            mw.RebootNotify(1);
        }

        private void p2_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.p4 = p2.IsOn;
                if (p2.IsOn)
                {
                    Process.Start("cmd.exe", "/c \"reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\\WindowMetrics\" /v CaptionHeight /t REG_SZ /d -270 /f\"");
                    Process.Start("cmd.exe", "/c \"reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\\WindowMetrics\" /v CaptionWidth /t REG_SZ /d -270 /f\"");
                }
                else
                {
                    Process.Start("cmd.exe", "/c \"reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\\WindowMetrics\" /v CaptionHeight /t REG_SZ /d -330 /f\"");
                    Process.Start("cmd.exe", "/c \"reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\\WindowMetrics\" /v CaptionWidth /t REG_SZ /d -330 /f\"");
                }
                mw.RebootNotify(1);
            }
        }

        private void p3_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.o6 = p3.IsOn;
                if (p3.IsOn)
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System").SetValue("DisableAcrylicBackgroundOnLogon", 1);
                }
                else
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System").SetValue("DisableAcrylicBackgroundOnLogon", 0);
                }
            }
        }

        private void LoadLang()
        {
            string language = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> per = MainWindow.Localization.LoadLocalization(language, "per");
            Dictionary<string, Dictionary<string, string>> basel = MainWindow.Localization.LoadLocalization(language, "base");
            Dictionary<string, Dictionary<string, string>> sr = MainWindow.Localization.LoadLocalization(language, "sr");
            Dictionary<string, Dictionary<string, string>> oth = MainWindow.Localization.LoadLocalization(language, "oth");
            label.Text = per["main"]["label"];
            l1.Text = per["main"]["p1l"];
            newname.Watermark = per["main"]["newname"];
            apN.Content = per["main"]["b1"];
            apC.Content = per["main"]["b1"];
            stN.Content = per["main"]["b2"];
            l2.Text = per["main"]["p2l"];
            c1.Content = per["main"]["c1"];
            c2.Content = per["main"]["c2"];
            c3.Content = per["main"]["c3"];
            c4.Content = per["main"]["c4"];
            c5.Content = per["main"]["c5"];
            c6.Content = per["main"]["c6"];
            c7.Content = per["main"]["c7"];
            c8.Content = per["main"]["c8"];
            p1.Header = per["main"]["p3"];
            p2.Header = per["main"]["p4"];
            p3.Header = per["main"]["p5"];
            p4.Header = per["main"]["p7"];
            p5.Header = per["main"]["p8"];
            p6.Header = oth["main"]["o5"];
            sr2.Header = sr["main"]["sr2"];
            sr3.Header = sr["main"]["sr3"];
            p1.OffContent = basel["def"]["off"];
            p2.OffContent = basel["def"]["off"];
            p3.OffContent = basel["def"]["off"];
            p4.OffContent = basel["def"]["off"];
            p5.OffContent = basel["def"]["off"];
            sr2.OffContent = basel["def"]["off"];
            sr3.OffContent = basel["def"]["off"];
            p6.OffContent = basel["def"]["off"];
            p1.OnContent = basel["def"]["on"];
            p2.OnContent = basel["def"]["on"];
            p3.OnContent = basel["def"]["on"];
            p4.OnContent = basel["def"]["on"];
            p5.OnContent = basel["def"]["on"];
            sr2.OnContent = basel["def"]["on"];
            sr3.OnContent = basel["def"]["on"];
            p6.OnContent = basel["def"]["on"];
        }

        private void checkReg()
        {
            newname.Text = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\NamingTemplates")?.GetValue("RenameNameTemplate")?.ToString();
            ModernWpf.Controls.ToggleSwitch toggleSwitch = p1;
            RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\PolicyManager\\current\\device\\Education");
            toggleSwitch.IsOn = registryKey != null && registryKey.GetValue("EnableEduThemes")?.Equals(1) == true;
            ModernWpf.Controls.ToggleSwitch toggleSwitch2 = p2;
            RegistryKey? registryKey2 = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop\\WindowMetrics");
            int isOn;
            if (registryKey2 == null || registryKey2.GetValue("CaptionHeight")?.Equals(-270) != true)
            {
                RegistryKey? registryKey3 = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop\\WindowMetrics");
                isOn = ((registryKey3 != null && registryKey3.GetValue("CaptionWidth")?.Equals(-270) == true) ? 1 : 0);
            }
            else
            {
                isOn = 1;
            }
            toggleSwitch2.IsOn = (byte)isOn != 0;
            ModernWpf.Controls.ToggleSwitch toggleSwitch3 = p3;
            RegistryKey? registryKey4 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System");
            toggleSwitch3.IsOn = registryKey4 != null && registryKey4.GetValue("DisableAcrylicBackgroundOnLogon")?.Equals(1) == true;
            ModernWpf.Controls.ToggleSwitch toggleSwitch4 = p4;
            RegistryKey? registryKey5 = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize");
            toggleSwitch4.IsOn = registryKey5 != null && registryKey5.GetValue("EnableTransparency")?.Equals(0) == true;
            ModernWpf.Controls.ToggleSwitch toggleSwitch5 = p5;
            object obj = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize")?.GetValue("AppsUseLightTheme");
            toggleSwitch5.IsOn = obj is int && (int)obj == 0 && Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize")?.GetValue("SystemUsesLightTheme") is int b && b == 0;
            p6.IsOn = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System")?.GetValue("verbosestatus")?.Equals(1) == true;
        }

        private void p4_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                if (p4.IsOn)
                {
                    Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize").SetValue("EnableTransparency", 0);
                }
                else
                {
                    Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize").SetValue("EnableTransparency", 1);
                }
            }
        }

        private void p5_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                if (p5.IsOn)
                {
                    Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize").SetValue("AppsUseLightTheme", 0);
                    Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize").SetValue("SystemUsesLightTheme", 0);
                }
                else
                {
                    Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize").SetValue("AppsUseLightTheme", 1);
                    Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize").SetValue("SystemUsesLightTheme", 1);
                }
                mw.RebootNotify(2);
                System.Windows.Forms.Application.Restart();
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void p6_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                if (p6.IsOn)
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System").SetValue("verbosestatus", 1);
                }
                else
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System").SetValue("verbosestatus", 0);
                }
            }
        }

        private void sr2_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.sr2 = sr2.IsOn;
                if (sr2.IsOn)
                {
                    Process.Start("cmd.exe", "/c \"bcdedit /set \"{globalsettings}\" custom:16000067 true\"");
                }
                else
                {
                    Process.Start("cmd.exe", "/c \"bcdedit /set \"{globalsettings}\" custom:16000067 false\"");
                }
            }
        }

        private void sr3_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.sr3 = sr3.IsOn;
                if (sr3.IsOn)
                {
                    Process.Start("cmd.exe", "/c \"bcdedit /set \"{globalsettings}\" custom:16000069 true\"");
                }
                else
                {
                    Process.Start("cmd.exe", "/c \"bcdedit /set \"{globalsettings}\" custom:16000069 false\"");
                }
            }
        }
    }
}
