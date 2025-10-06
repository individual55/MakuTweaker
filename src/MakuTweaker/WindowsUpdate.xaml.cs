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
    public partial class WindowsUpdate : Page
    {
        private bool isLoaded;

        private MainWindow mw = (MainWindow)Application.Current.MainWindow;

        public WindowsUpdate()
        {
            InitializeComponent();
            checkReg();
            if (Settings.Default.wu4 == 2312)
            {
                switch (checkWinVer())
                {
                    case 10240:
                        Settings.Default.wu4 = 0;
                        break;
                    case 14393:
                        Settings.Default.wu4 = 1;
                        break;
                    case 17763:
                        Settings.Default.wu4 = 2;
                        break;
                    case 18363:
                        Settings.Default.wu4 = 3;
                        break;
                    case 19042:
                        Settings.Default.wu4 = 4;
                        break;
                    case 19044:
                        Settings.Default.wu4 = 5;
                        break;
                    case 19045:
                        Settings.Default.wu4 = 6;
                        break;
                    case 22000:
                        Settings.Default.wu4 = 5;
                        break;
                    case 22621:
                        Settings.Default.wu4 = 6;
                        break;
                    case 26100:
                        Settings.Default.wu4 = 7;
                        break;
                }
            }
            pause.IsEnabled = !Settings.Default.b9;
            wu4.SelectedIndex = Settings.Default.wu4;
            wu6.IsOn = Settings.Default.wu6;
            LoadLang("ilovemakutweaker");
            wupd.IsEnabled = !Settings.Default.b7;
            isLoaded = true;
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }
            Settings.Default.wu1 = wu1.IsOn;
            if (wu1.IsOn)
            {
                try
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU").SetValue("NoAutoUpdate", 1);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate").SetValue("DoNotConnectToWindowsUpdateInternetLocations", 1);
                    return;
                }
                catch
                {
                    return;
                }
            }
            try
            {
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU").SetValue("NoAutoUpdate", 0);
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate").SetValue("DoNotConnectToWindowsUpdateInternetLocations", 0);
            }
            catch
            {
            }
        }

        private void ToggleSwitch_Toggled_1(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }
            Settings.Default.wu2 = wu2.IsOn;
            if (wu2.IsOn)
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel").SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate").SetValue("ExcludeWUDriversInQualityUpdate", 1);
                    return;
                }
                catch
                {
                    return;
                }
            }
            try
            {
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate").SetValue("ExcludeWUDriversInQualityUpdate", 0);
            }
            catch
            {
            }
        }

        private void ToggleSwitch_Toggled_2(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }
            Settings.Default.wu3 = wu3.IsOn;
            if (wu3.IsOn)
            {
                try
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\clipchamp.clipchamp_yxz26nhyzhsrt");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.549981C3F5F10_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.bingnews_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.bingweather_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.ECApp_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.gethelp_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.getstarted_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.MicrosoftEdgeDevToolsClient_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.microsoftsolitairecollection_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.microsoftstickynotes_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.people_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.powerautomatedesktop_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.todos_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.Windows.NarratorQuickStart_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.Windows.PeopleExperienceHost_cw5n1h2txyewy");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.Windows.SecureAssessmentBrowser_cw5n1h2txyewy");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.windowscamera_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.windowscommunicationsapps_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.windowsfeedbackhub_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.windowsmaps_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.windowssoundrecorder_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.yourphone_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoftcorporationii.quickassist_8wekyb3d8bbwe");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoftwindows.client.webexperience_cw5n1h2txyewy");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\DeliveryOptimization").SetValue("DODownloadMode", 0);
                    return;
                }
                catch
                {
                    return;
                }
            }
            try
            {
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\clipchamp.clipchamp_yxz26nhyzhsrt", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.549981C3F5F10_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.bingnews_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.bingweather_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.ECApp_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.gethelp_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.getstarted_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.MicrosoftEdgeDevToolsClient_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.microsoftsolitairecollection_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.microsoftstickynotes_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.people_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.powerautomatedesktop_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.todos_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.Windows.NarratorQuickStart_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.Windows.PeopleExperienceHost_cw5n1h2txyewy", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.Windows.SecureAssessmentBrowser_cw5n1h2txyewy", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.windowscamera_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.windowscommunicationsapps_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.windowsfeedbackhub_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.windowsmaps_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.windowssoundrecorder_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.yourphone_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoftcorporationii.quickassist_8wekyb3d8bbwe", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoftwindows.client.webexperience_cw5n1h2txyewy", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Windows.CBSPreview_cw5n1h2txyewy", throwOnMissingSubKey: false);
                Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned", throwOnMissingSubKey: false);
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\DeliveryOptimization").SetValue("DODownloadMode", 1);
            }
            catch
            {
            }
        }

        private void wu4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.wu4 = wu4.SelectedIndex;
            switch (wu4.SelectedIndex)
            {
                case 0:
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate").SetValue("TargetReleaseVersionInfo", "1607");
                    break;
                case 1:
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate").SetValue("TargetReleaseVersionInfo", "1809");
                    break;
                case 2:
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate").SetValue("TargetReleaseVersionInfo", "1909");
                    break;
                case 3:
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate").SetValue("TargetReleaseVersionInfo", "20H2");
                    break;
                case 4:
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate").SetValue("TargetReleaseVersionInfo", "21H2");
                    break;
                case 5:
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate").SetValue("TargetReleaseVersionInfo", "22H2");
                    break;
                case 6:
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate").SetValue("TargetReleaseVersionInfo", "24H2");
                    break;
            }
            Dictionary<string, Dictionary<string, string>> wul = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "wu");
            mw.ChSt(wul["status"]["wu4"]);
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("cmd.exe", "/c \"reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v ActiveHoursStart /t REG_DWORD /d 9 /f && reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v ActiveHoursEnd /t REG_DWORD /d 2 /f && reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v PauseFeatureUpdatesStartTime /t REG_SZ /d \"2015-01-01T00:00:00Z\" /f && reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v PauseQualityUpdatesStartTime /t REG_SZ /d \"2015-01-01T00:00:00Z\" /f && reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v PauseUpdatesExpiryTime /t REG_SZ /d \"2077-01-01T00:00:00Z\" /f && reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v PauseFeatureUpdatesEndTime /t REG_SZ /d \"2077-01-01T00:00:00Z\" /f && reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v PauseQualityUpdatesEndTime /t REG_SZ /d \"2077-01-01T00:00:00Z\" /f && reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v PauseUpdatesStartTime /t REG_SZ /d \"2015-01-01T00:00:00Z\" /f\"");
                Dictionary<string, Dictionary<string, string>> wul = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "wu");
                pause.IsEnabled = false;
                Settings.Default.b9 = !pause.IsEnabled;
                mw.ChSt(wul["status"]["wu5"]);
            }
            catch
            {
            }
        }

        private void wu6_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.wu6 = wu6.IsOn;
                if (wu6.IsOn)
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\ReserveManager").SetValue("ShippedWithReserves", 0);
                }
                else
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\ReserveManager").SetValue("ShippedWithReserves", 1);
                }
            }
        }

        private void LoadLang(string lang)
        {
            string language = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> basel = MainWindow.Localization.LoadLocalization(language, "base");
            Dictionary<string, Dictionary<string, string>> wu = MainWindow.Localization.LoadLocalization(language, "wu");
            Dictionary<string, Dictionary<string, string>> sr = MainWindow.Localization.LoadLocalization(language, "sr");
            wu1.Header = wu["main"]["wu1"];
            wu2.Header = wu["main"]["wu3"];
            wu3.Header = wu["main"]["wu4"];
            wu6.Header = wu["main"]["wu6"];
            pausel.Text = wu["main"]["wu5"];
            blockL.Text = wu["main"]["wu2"];
            l7.Text = sr["main"]["sr7l"];
            pause.Content = wu["main"]["wu5b"];
            block.Content = wu["main"]["wu6b"];
            wupd.Content = sr["main"]["b4"];
            wu1.OffContent = basel["def"]["off"];
            wu2.OffContent = basel["def"]["off"];
            wu3.OffContent = basel["def"]["off"];
            wu6.OffContent = basel["def"]["off"];
            wu1.OnContent = basel["def"]["on"];
            wu2.OnContent = basel["def"]["on"];
            wu3.OnContent = basel["def"]["on"];
            wu6.OnContent = basel["def"]["on"];
        }

        private void checkReg()
        {
            if (Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\clipchamp.clipchamp_yxz26nhyzhsrt") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.549981C3F5F10_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.bingnews_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.bingweather_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.ECApp_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.gethelp_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.getstarted_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.MicrosoftEdgeDevToolsClient_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.microsoftsolitairecollection_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.microsoftstickynotes_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.people_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.powerautomatedesktop_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.todos_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.Windows.NarratorQuickStart_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.Windows.PeopleExperienceHost_cw5n1h2txyewy") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\Microsoft.Windows.SecureAssessmentBrowser_cw5n1h2txyewy") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.windowscamera_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.windowscommunicationsapps_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.windowsfeedbackhub_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.windowsmaps_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.windowssoundrecorder_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoft.yourphone_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoftcorporationii.quickassist_8wekyb3d8bbwe") == null && Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\Deprovisioned\\microsoftwindows.client.webexperience_cw5n1h2txyewy") == null)
            {
                ModernWpf.Controls.ToggleSwitch toggleSwitch = wu3;
                RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\DeliveryOptimization", writable: true);
                bool? obj;
                if (registryKey == null)
                {
                    obj = null;
                }
                else
                {
                    object? value = registryKey.GetValue("DODownloadMode");
                    obj = ((value != null) ? new bool?(!value.Equals(0)) : ((bool?)null));
                }
                bool? flag = obj;
                toggleSwitch.IsOn = flag == true;
            }
            wu1.IsOn = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU", writable: true)?.GetValue("NoAutoUpdate")?.Equals(1) == true;
            wu2.IsOn = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate", writable: true)?.GetValue("ExcludeWUDriversInQualityUpdate")?.Equals(1) == true;
            wu6.IsOn = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\ReserveManage", writable: true)?.GetValue("ShippedWithReserves")?.Equals(0) == true;
        }

        private void wupd_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("cmd.exe", "/k del /f /s /q %windir%\\SoftwareDistribution\\Download\\*");
            wupd.IsEnabled = false;
            Settings.Default.b7 = !wupd.IsEnabled;
        }
    }
}
