using System;
using System.Collections.Generic;
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
    public partial class StartMenuAndTaskbar : Page
    {
        private bool isLoaded;

        private MainWindow mw = (MainWindow)Application.Current.MainWindow;

        public StartMenuAndTaskbar()
        {
            InitializeComponent();
            checkReg();
            if (checkWinVer() >= 22631)
            {
                st1.Visibility = Visibility.Collapsed;
            }
            if (checkWinVer() >= 26120)
            {
                st2.Visibility = Visibility.Collapsed;
                st3.Visibility = Visibility.Collapsed;
            }
            LoadLang(Settings.Default.lang);
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

        private void st1_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }
            Settings.Default.st1 = st1.IsOn;
            if (st1.IsOn)
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("ShowTaskViewButton", 0);
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("TaskbarDa", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\PolicyManager\\default\\NewsAndInterests\\AllowNewsAndInterests").SetValue("value", 0, RegistryValueKind.DWord);
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("TaskbarMn", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Dsh").SetValue("AllowNewsAndInterests", 0);
                    return;
                }
                catch (Exception)
                {
                    return;
                }
            }
            try
            {
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("ShowTaskViewButton", 1);
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("TaskbarDa", 1);
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("TaskbarMn", 1);
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\PolicyManager\\default\\NewsAndInterests\\AllowNewsAndInterests").SetValue("value", 1);
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Dsh").SetValue("AllowNewsAndInterests", 1);
            }
            catch
            {
            }
        }

        private void st2_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }
            Settings.Default.st2 = st2.IsOn;
            if (st2.IsOn)
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Taskband").SetValue("MinThumbSizePX", 500);
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Taskband").SetValue("MinThumbSizePX", 170);
                }
                catch
                {
                }
            }
            mw.RebootNotify(2);
        }

        private void st3_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }
            Settings.Default.st3 = st3.IsOn;
            if (st3.IsOn)
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel").SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", 0);
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("ExtendedUIHoverTime", 10);
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").SetValue("ExtendedUIHoverTime", 750);
                }
                catch
                {
                }
            }
            mw.RebootNotify(2);
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
            catch
            {
            }
        }

        private void LoadLang(string lang)
        {
            string language = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> basel = MainWindow.Localization.LoadLocalization(language, "base");
            Dictionary<string, Dictionary<string, string>> stask = MainWindow.Localization.LoadLocalization(language, "stask");
            label.Text = stask["main"]["label"];
            st1.Header = stask["main"]["st1"];
            st2.Header = stask["main"]["st2"];
            st3.Header = stask["main"]["st3"];
            st5.Header = stask["main"]["st5"];
            st1.OnContent = basel["def"]["on"];
            st2.OnContent = basel["def"]["on"];
            st3.OnContent = basel["def"]["on"];
            st5.OnContent = basel["def"]["on"];
            st1.OffContent = basel["def"]["off"];
            st2.OffContent = basel["def"]["off"];
            st3.OffContent = basel["def"]["off"];
            st5.OffContent = basel["def"]["off"];
        }

        private void checkReg()
        {
            st1.IsOn = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", writable: true)?.GetValue("ShowTaskViewButton")?.Equals(0) == true;
            st2.IsOn = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Taskband", writable: true)?.GetValue("MinThumbSizePX")?.Equals(500) == true;
            st3.IsOn = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel", writable: true)?.GetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}")?.Equals(0) == true;
            st5.IsOn = Registry.CurrentUser.OpenSubKey("Software\\Policies\\Microsoft\\Windows\\Explorer", writable: true)?.GetValue("DisableSearchBoxSuggestions")?.Equals(1) == true;
        }
    }
}
