using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using MakuTweakerNew.Properties;
using Microsoft.Win32;

namespace MakuTweakerNew
{
    public partial class Explorer : Page
    {
        private MainWindow mw = (MainWindow)Application.Current.MainWindow;

        private bool isLoaded = false;

        public Explorer()
        {
            InitializeComponent();
            checkReg();
            if (checkWinVer() >= 22621)
            {
                e1.Visibility = Visibility.Collapsed;
            }
            else
            {
                e5.Visibility = Visibility.Collapsed;
            }
            if (Settings.Default.b11)
            {
                showall.IsEnabled = false;
            }
            LoadLang(Settings.Default.lang);
            isLoaded = true;
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }
            Settings.Default.e1 = e1.IsOn;
            if (e1.IsOn)
            {
                try
                {
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{A0953C92-50DC-43bf-BE83-3742FED03C9C}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{f86fa3ab-70d2-4fc7-9c99-fcbf05467f3a}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{A0953C92-50DC-43bf-BE83-3742FED03C9C}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{f86fa3ab-70d2-4fc7-9c99-fcbf05467f3a}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{A8CDFF1C-4878-43be-B5FD-F8091C1C60D0}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{d3162b92-9365-467a-956b-92703aca08af}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{A8CDFF1C-4878-43be-B5FD-F8091C1C60D0}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{d3162b92-9365-467a-956b-92703aca08af}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{374DE290-123F-4565-9164-39C4925E467B}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{088e3905-0323-4b02-9826-5d99428e115f}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{374DE290-123F-4565-9164-39C4925E467B}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{088e3905-0323-4b02-9826-5d99428e115f}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{3ADD1653-EB32-4cb0-BBD7-DFA0ABB5ACCA}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{24ad3ad4-a569-4530-98e1-ab02f9417aa8}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{3ADD1653-EB32-4cb0-BBD7-DFA0ABB5ACCA}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{24ad3ad4-a569-4530-98e1-ab02f9417aa8}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{1CF1260C-4DD0-4ebb-811F-33C572699FDE}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{3dfdf296-dbec-4fb4-81d1-6a3438bcf4de}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{1CF1260C-4DD0-4ebb-811F-33C572699FDE}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{3dfdf296-dbec-4fb4-81d1-6a3438bcf4de}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}");
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}");
                    try
                    {
                        Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}");
                        Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}");
                        return;
                    }
                    catch
                    {
                        return;
                    }
                }
                catch
                {
                    return;
                }
            }
            try
            {
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{A0953C92-50DC-43bf-BE83-3742FED03C9C}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{f86fa3ab-70d2-4fc7-9c99-fcbf05467f3a}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{A0953C92-50DC-43bf-BE83-3742FED03C9C}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{f86fa3ab-70d2-4fc7-9c99-fcbf05467f3a}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{A8CDFF1C-4878-43be-B5FD-F8091C1C60D0}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{d3162b92-9365-467a-956b-92703aca08af}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{A8CDFF1C-4878-43be-B5FD-F8091C1C60D0}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{d3162b92-9365-467a-956b-92703aca08af}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{374DE290-123F-4565-9164-39C4925E467B}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{088e3905-0323-4b02-9826-5d99428e115f}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{374DE290-123F-4565-9164-39C4925E467B}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{088e3905-0323-4b02-9826-5d99428e115f}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{3ADD1653-EB32-4cb0-BBD7-DFA0ABB5ACCA}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{24ad3ad4-a569-4530-98e1-ab02f9417aa8}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{3ADD1653-EB32-4cb0-BBD7-DFA0ABB5ACCA}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{24ad3ad4-a569-4530-98e1-ab02f9417aa8}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{1CF1260C-4DD0-4ebb-811F-33C572699FDE}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{3dfdf296-dbec-4fb4-81d1-6a3438bcf4de}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{1CF1260C-4DD0-4ebb-811F-33C572699FDE}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{3dfdf296-dbec-4fb4-81d1-6a3438bcf4de}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}");
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}");
            }
            catch
            {
            }
        }

        private void e2_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }
            Settings.Default.e2 = e2.IsOn;
            if (e2.IsOn)
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("Hidden", 1);
                    return;
                }
                catch
                {
                    return;
                }
            }
            try
            {
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("Hidden", 0);
            }
            catch
            {
            }
        }

        private void e3_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }
            Settings.Default.e3 = e3.IsOn;
            if (e3.IsOn)
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("HideFileExt", 0);
                    return;
                }
                catch
                {
                    return;
                }
            }
            try
            {
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("HideFileExt", 1);
            }
            catch
            {
            }
        }

        private void e4_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }
            Settings.Default.e4 = e4.IsOn;
            if (e4.IsOn)
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("LaunchTo", 1);
                    return;
                }
                catch
                {
                    return;
                }
            }
            try
            {
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("LaunchTo", 2);
            }
            catch
            {
            }
        }

        private void e5_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }
            Settings.Default.e5 = e5.IsOn;
            if (e5.IsOn)
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\CLSID\\{e88865ea-0e1c-4e20-9aa6-edcd0212c87c}").SetValue("System.IsPinnedToNameSpaceTree", 0);
                    return;
                }
                catch
                {
                    return;
                }
            }
            try
            {
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\CLSID\\{e88865ea-0e1c-4e20-9aa6-edcd0212c87c}").SetValue("System.IsPinnedToNameSpaceTree", 1);
            }
            catch
            {
            }
        }

        private void e6_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }
            Settings.Default.e6 = e6.IsOn;
            if (e6.IsOn)
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel").SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", 0);
                    return;
                }
                catch
                {
                    return;
                }
            }
            try
            {
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel").SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", 1);
            }
            catch
            {
            }
        }

        private void e7_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }
            Settings.Default.e7 = e7.IsOn;
            if (e7.IsOn)
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\NamingTemplates").SetValue("ShortcutNameTemplate", "%s.lnk");
                    return;
                }
                catch
                {
                    return;
                }
            }
            try
            {
                Registry.CurrentUser.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\NamingTemplates");
            }
            catch
            {
            }
        }

        private void fix_Click(object sender, RoutedEventArgs e)
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> expl = MainWindow.Localization.LoadLocalization(languageCode, "expl");
            try
            {
                Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace\\DelegateFolders\\{F5FB2C77-0E2F-4A16-A381-3E560C68BC83}");
                Registry.LocalMachine.DeleteSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace\\DelegateFolders\\{F5FB2C77-0E2F-4A16-A381-3E560C68BC83}");
                mw.ChSt(expl["status"]["e8"]);
                Settings.Default.isE8Hidden = true;
                FadeOut(e8, 300.0);
                FadeOut(fix, 300.0);
                e8.IsEnabled = false;
                fix.IsEnabled = false;
            }
            catch
            {
                mw.ChSt(expl["status"]["e8"]);
                Settings.Default.isE8Hidden = true;
                FadeOut(e8, 300.0);
                FadeOut(fix, 300.0);
                e8.IsEnabled = false;
                fix.IsEnabled = false;
            }
        }

        private void FadeOut(UIElement element, double durationSeconds)
        {
            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromMilliseconds(durationSeconds),
                EasingFunction = new QuadraticEase
                {
                    EasingMode = EasingMode.EaseOut
                }
            };
            element.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
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

        private void LoadLang(string lang)
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> expl = MainWindow.Localization.LoadLocalization(languageCode, "expl");
            Dictionary<string, Dictionary<string, string>> main = MainWindow.Localization.LoadLocalization(languageCode, "base");
            lab.Text = expl["main"]["label"];
            e1.Header = expl["main"]["e1"];
            e2.Header = expl["main"]["e2"];
            e3.Header = expl["main"]["e3"];
            e4.Header = expl["main"]["e4"];
            e5.Header = expl["main"]["e5"];
            e6.Header = expl["main"]["e6"];
            e7.Header = expl["main"]["e7"];
            e8.Text = expl["main"]["e8"];
            fix.Content = expl["main"]["e8b"];
            e9.Text = expl["main"]["e9"];
            hide.Content = expl["main"]["choose"];
            showall.Content = expl["main"]["showall"];
            e1.OnContent = main["def"]["on"];
            e2.OnContent = main["def"]["on"];
            e3.OnContent = main["def"]["on"];
            e4.OnContent = main["def"]["on"];
            e5.OnContent = main["def"]["on"];
            e6.OnContent = main["def"]["on"];
            e7.OnContent = main["def"]["on"];
            e1.OffContent = main["def"]["off"];
            e2.OffContent = main["def"]["off"];
            e3.OffContent = main["def"]["off"];
            e4.OffContent = main["def"]["off"];
            e5.OffContent = main["def"]["off"];
            e6.OffContent = main["def"]["off"];
            e7.OffContent = main["def"]["off"];
        }

        private void checkReg()
        {
            e1.IsOn = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{A0953C92-50DC-43bf-BE83-3742FED03C9C}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{f86fa3ab-70d2-4fc7-9c99-fcbf05467f3a}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{A0953C92-50DC-43bf-BE83-3742FED03C9C}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{f86fa3ab-70d2-4fc7-9c99-fcbf05467f3a}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{A8CDFF1C-4878-43be-B5FD-F8091C1C60D0}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{d3162b92-9365-467a-956b-92703aca08af}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{A8CDFF1C-4878-43be-B5FD-F8091C1C60D0}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{d3162b92-9365-467a-956b-92703aca08af}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{374DE290-123F-4565-9164-39C4925E467B}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{088e3905-0323-4b02-9826-5d99428e115f}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{374DE290-123F-4565-9164-39C4925E467B}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{088e3905-0323-4b02-9826-5d99428e115f}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{3ADD1653-EB32-4cb0-BBD7-DFA0ABB5ACCA}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{24ad3ad4-a569-4530-98e1-ab02f9417aa8}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{3ADD1653-EB32-4cb0-BBD7-DFA0ABB5ACCA}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{24ad3ad4-a569-4530-98e1-ab02f9417aa8}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{1CF1260C-4DD0-4ebb-811F-33C572699FDE}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{3dfdf296-dbec-4fb4-81d1-6a3438bcf4de}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{1CF1260C-4DD0-4ebb-811F-33C572699FDE}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{3dfdf296-dbec-4fb4-81d1-6a3438bcf4de}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}") == null;
            e2.IsOn = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", writable: true)?.GetValue("Hidden")?.Equals(1) == true;
            e3.IsOn = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", writable: true)?.GetValue("HideFileExt")?.Equals(0) == true;
            e4.IsOn = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", writable: true)?.GetValue("LaunchTo")?.Equals(1) == true;
            e5.IsOn = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\CLSID\\{e88865ea-0e1c-4e20-9aa6-edcd0212c87c}", writable: true)?.GetValue("System.IsPinnedToNameSpaceTree")?.Equals(0) == true;
            e6.IsOn = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel", writable: true)?.GetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}")?.Equals(0) == true;
            e7.IsOn = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\NamingTemplates", writable: true)?.GetValue("ShortcutNameTemplate")?.Equals("%s.lnk") == true;
            if (Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace\\DelegateFolders\\{F5FB2C77-0E2F-4A16-A381-3E560C68BC83}") == null || Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace\\DelegateFolders\\{F5FB2C77-0E2F-4A16-A381-3E560C68BC83}") == null)
            {
                e8.Visibility = Visibility.Collapsed;
                fix.Visibility = Visibility.Collapsed;
            }
        }

        private async void hide_Click(object sender, RoutedEventArgs e)
        {
            HidePart dialog = new HidePart();
            await dialog.ShowAsync();
            decimal resulty = await dialog.TaskCompletionSource.Task;
            if (resulty != -1m)
            {
                Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer").SetValue("NoDrives", resulty, RegistryValueKind.DWord);
                mw.RebootNotify(2);
            }
            showall.IsEnabled = true;
            Settings.Default.b11 = false;
        }

        private void showall_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.hiddenDrives = string.Empty;
            Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer").SetValue("NoDrives", 0, RegistryValueKind.DWord);
            mw.RebootNotify(2);
            showall.IsEnabled = false;
            Settings.Default.b11 = true;
        }
    }
}
