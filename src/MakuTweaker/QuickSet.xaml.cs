using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MakuTweakerNew.Properties;
using Microsoft.Win32;

namespace MakuTweakerNew
{
    public partial class QuickSet : Page, IComponentConnector
    {
        private MainWindow mw = (MainWindow)Application.Current.MainWindow;

        public QuickSet()
        {
            InitializeComponent();
            LoadLang();
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            if (t1.IsOn)
            {
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("Hidden", 1);
            }
            if (t2.IsOn)
            {
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("HideFileExt", 0);
            }
            if (t3.IsOn)
            {
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("LaunchTo", 1);
            }
            if (t4.IsOn)
            {
                try
                {
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace\\DelegateFolders\\{F5FB2C77-0E2F-4A16-A381-3E560C68BC83}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace\\DelegateFolders\\{F5FB2C77-0E2F-4A16-A381-3E560C68BC83}");
                }
                catch
                {
                }
            }
            if (t5.IsOn)
            {
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel").SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", 0);
            }
            if (t6.IsOn)
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel").SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", 0);
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\NamingTemplates").SetValue("ShortcutNameTemplate", "%s.lnk");
                }
                catch
                {
                }
            }
            if (t7.IsOn)
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("ShowTaskViewButton", 0);
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("TaskbarDa", 0);
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("TaskbarMn", 0);
                }
                catch { }
            }
            if (t8.IsOn)
            {
                Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\SearchSettings").SetValue("IsDynamicSearchBoxEnabled", 0);
            }
            if (t9.IsOn)
            {
                Registry.CurrentUser.CreateSubKey("Software\\Policies\\Microsoft\\Windows\\Explorer").SetValue("DisableSearchBoxSuggestions", 1);
            }
            if (t10.IsOn)
            {
                Process.Start("cmd.exe", "/c \"reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v ActiveHoursStart /t REG_DWORD /d 9 /f && reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v ActiveHoursEnd /t REG_DWORD /d 2 /f && reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v PauseFeatureUpdatesStartTime /t REG_SZ /d \"2015-01-01T00:00:00Z\" /f && reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v PauseQualityUpdatesStartTime /t REG_SZ /d \"2015-01-01T00:00:00Z\" /f && reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v PauseUpdatesExpiryTime /t REG_SZ /d \"2077-01-01T00:00:00Z\" /f && reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v PauseFeatureUpdatesEndTime /t REG_SZ /d \"2077-01-01T00:00:00Z\" /f && reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v PauseQualityUpdatesEndTime /t REG_SZ /d \"2077-01-01T00:00:00Z\" /f && reg add HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings /v PauseUpdatesStartTime /t REG_SZ /d \"2015-01-01T00:00:00Z\" /f\"");
            }
            if (t12.IsOn)
            {
                Registry.CurrentUser.CreateSubKey("Control Panel\\Accessibility\\StickyKeys").SetValue("Flags", "506");
                Registry.CurrentUser.CreateSubKey("Control Panel\\Accessibility\\Keyboard Response").SetValue("Flags", "122");
                Registry.CurrentUser.CreateSubKey("Control Panel\\Accessibility\\ToggleKeys").SetValue("Flags", "58");
            }
            if (t13.IsOn)
            {
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Clipboard").SetValue("EnableClipboardHistory", 1);
            }
            if (t14.IsOn)
            {
                Registry.CurrentUser.CreateSubKey("Control Panel\\Desktop").SetValue("MenuShowDelay", "50");
            }
            if (t15.IsOn)
            {
                Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager").SetValue("AutoChkTimeout", 60);
            }
            if (t16.IsOn)
            {
                Process.Start("powershell.exe", "-Command \"& dism /online /Enable-Feature /FeatureName:DirectPlay /All\"");
            }
            if (t17.IsOn)
            {
                Process.Start("powershell.exe", "-Command \"& Add-WindowsCapability -Online -Name NetFx3~~~~\"");
            }
            if (t19.IsOn)
            {
                Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\BitLocker").SetValue("PreventDeviceEncryption", 1, RegistryValueKind.DWord);
            }
            mw.RebootNotify(3);
        }

        private void LoadLang()
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> basel = MainWindow.Localization.LoadLocalization(languageCode, "base");
            Dictionary<string, Dictionary<string, string>> quick = MainWindow.Localization.LoadLocalization(languageCode, "quick");
            label.Text = quick["main"]["label"];
            info.Text = quick["main"]["info"];
            start.Content = quick["main"]["b"];
            t1.Header = quick["main"]["t1"];
            t2.Header = quick["main"]["t2"];
            t3.Header = quick["main"]["t3"];
            t4.Header = quick["main"]["t4"];
            t5.Header = quick["main"]["t5"];
            t6.Header = quick["main"]["t6"];
            t7.Header = quick["main"]["t8"];
            t8.Header = quick["main"]["t9"];
            t9.Header = quick["main"]["t10"];
            t10.Header = quick["main"]["t11"];
            t12.Header = quick["main"]["t13"];
            t13.Header = quick["main"]["t14"];
            t14.Header = quick["main"]["t15"];
            t15.Header = quick["main"]["t16"];
            t16.Header = quick["main"]["t17"];
            t17.Header = quick["main"]["t18"];
            t19.Header = quick["main"]["t20"];
            t1.OnContent = basel["def"]["on"];
            t2.OnContent = basel["def"]["on"];
            t3.OnContent = basel["def"]["on"];
            t4.OnContent = basel["def"]["on"];
            t5.OnContent = basel["def"]["on"];
            t6.OnContent = basel["def"]["on"];
            t7.OnContent = basel["def"]["on"];
            t8.OnContent = basel["def"]["on"];
            t9.OnContent = basel["def"]["on"];
            t10.OnContent = basel["def"]["on"];
            t12.OnContent = basel["def"]["on"];
            t13.OnContent = basel["def"]["on"];
            t14.OnContent = basel["def"]["on"];
            t15.OnContent = basel["def"]["on"];
            t16.OnContent = basel["def"]["on"];
            t17.OnContent = basel["def"]["on"];
            t19.OnContent = basel["def"]["on"];
            t1.OffContent = basel["def"]["off"];
            t2.OffContent = basel["def"]["off"];
            t3.OffContent = basel["def"]["off"];
            t4.OffContent = basel["def"]["off"];
            t5.OffContent = basel["def"]["off"];
            t6.OffContent = basel["def"]["off"];
            t7.OffContent = basel["def"]["off"];
            t8.OffContent = basel["def"]["off"];
            t9.OffContent = basel["def"]["off"];
            t10.OffContent = basel["def"]["off"];
            t12.OffContent = basel["def"]["off"];
            t13.OffContent = basel["def"]["off"];
            t14.OffContent = basel["def"]["off"];
            t15.OffContent = basel["def"]["off"];
            t16.OffContent = basel["def"]["off"];
            t17.OffContent = basel["def"]["off"];
            t19.OffContent = basel["def"]["off"];
        }
    }
}
