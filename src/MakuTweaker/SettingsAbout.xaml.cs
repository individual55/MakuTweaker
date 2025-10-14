using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using MakuTweakerNew.Properties;
using MicaWPF.Core.Enums;
using MicaWPF.Core.Services;
using Microsoft.Win32;
using ModernWpf;

namespace MakuTweakerNew
{
    public partial class SettingsAbout : Page
    {
        private MainWindow mw = (MainWindow)Application.Current.MainWindow;

        private bool isLoaded;

        private bool isDevBuild;

        public SettingsAbout()
        {
            InitializeComponent();
            credN.Text = "Mark Adderly\nNikitori\nNikitori, Massgrave\nindividual55";
            lang.SelectedIndex = Settings.Default.langSI;
            relang();

            if (checkWinVer() < 22000)
            {
                style.Visibility = Visibility.Collapsed;
                styleL.Visibility = Visibility.Collapsed;
            }

            WindowsTheme current = MicaWPFServiceUtility.ThemeService.CurrentTheme;
            theme.SelectedIndex = (current == WindowsTheme.Dark) ? 1 : 0;
            switch (Settings.Default.style)
            {
                case "Mica":
                    style.SelectedIndex = 0;
                    break;
                case "Tabbed":
                    style.SelectedIndex = 1;
                    break;
                case "Acrylic":
                    style.SelectedIndex = 2;
                    break;
                case "None":
                    style.SelectedIndex = 3;
                    break;
            }
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

        // Web links have been removed

        private void theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLoaded)
            {
                switch (theme.SelectedIndex)
                {
                    case 0:
                        Settings.Default.theme = "Light";
                        MicaWPFServiceUtility.ThemeService.ChangeTheme(WindowsTheme.Light);
                        ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                        mw.Foreground = Brushes.Black;
                        mw.Separator.Stroke = Brushes.Black;
                        break;
                    case 1:
                        Settings.Default.theme = "Dark";
                        MicaWPFServiceUtility.ThemeService.ChangeTheme(WindowsTheme.Dark);
                        ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                        mw.Foreground = Brushes.White;
                        mw.Separator.Stroke = Brushes.White;
                        break;
                }
                Settings.Default.Save();
            }
        }

        private void style_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLoaded)
            {
                switch (style.SelectedIndex)
                {
                    case 0:
                        MicaWPFServiceUtility.ThemeService.EnableBackdrop(mw);
                        Settings.Default.style = "Mica";
                        break;
                    case 1:
                        MicaWPFServiceUtility.ThemeService.EnableBackdrop(mw, BackdropType.Tabbed);
                        Settings.Default.style = "Tabbed";
                        break;
                    case 2:
                        MicaWPFServiceUtility.ThemeService.EnableBackdrop(mw, BackdropType.Acrylic);
                        Settings.Default.style = "Acrylic";
                        break;
                    case 3:
                        MicaWPFServiceUtility.ThemeService.EnableBackdrop(mw, BackdropType.None);
                        Settings.Default.style = "None";
                        break;
                }
            }
        }

        private void lang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLoaded)
            {
                switch (lang.SelectedIndex)
                {
                    case 0:
                        Settings.Default.lang = "en";
                        break;
                    case 1:
                        Settings.Default.lang = "ru";
                        break;
                    case 2:
                        Settings.Default.lang = "ua";
                        break;
                    case 3:
                        Settings.Default.lang = "es";
                        break;
                    case 4:
                        Settings.Default.lang = "pt";
                        break;
                    case 5:
                        Settings.Default.lang = "de";
                        break;
                    case 6:
                        Settings.Default.lang = "kz";
                        break;
                    case 7:
                        Settings.Default.lang = "jp";
                        break;
                    case 8:
                        Settings.Default.lang = "cn";
                        break;
                    case 9:
                        Settings.Default.lang = "hi";
                        break;
                }
                Settings.Default.langSI = lang.SelectedIndex;
                mw.LoadLang(Settings.Default.lang);
                relang();
            }
        }

        private void relang()
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> ab = MainWindow.Localization.LoadLocalization(languageCode, "ab");
            Dictionary<string, Dictionary<string, string>> b = MainWindow.Localization.LoadLocalization(languageCode, "base");
            credL.Text = ab["main"]["credL"];
            label.Text = ab["main"]["label"];
            langL.Text = ab["main"]["lang"];
            styleL.Text = ab["main"]["st"];
            l.Content = " " + ab["main"]["l"];
            d.Content = " " + ab["main"]["d"];
            themeL.Text = ab["main"]["th"];
            off.Content = " " + b["def"]["off"];
            Assembly assembly = Assembly.GetExecutingAssembly();
        }

        private void Image_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            copyleft.Text = "Mark Adderly, Nikitori / 2023 - 2025";
        }
    }
}




