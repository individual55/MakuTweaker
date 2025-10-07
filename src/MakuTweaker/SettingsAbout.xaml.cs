using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
            credN.Text = "Mark Adderly, Nikitori\nNikitori\nMassgrave";
            lang.SelectedIndex = Settings.Default.langSI;
            relang();
            if (isDevBuild)
            {
                build.Visibility = Visibility.Visible;
            }
            else
            {
                build.Visibility = Visibility.Collapsed;
            }
            isLoaded = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://adderly.top")
            {
                UseShellExecute = true
            });
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://boosty.to/adderly")
            {
                UseShellExecute = true
            });
        }

        private void Image_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://t.me/adderly324")
            {
                UseShellExecute = true
            });
        }

        private void Image_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://youtube.com/@MakuAdarii")
            {
                UseShellExecute = true
            });
        }

        private void theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
            Dictionary<string, Dictionary<string, string>> ab = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "ab");
            credL.Text = ab["main"]["credL"];
            label.Text = ab["main"]["label"];
            web.Content = ab["main"]["atop"];
            langL.Text = ab["main"]["lang"];
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MakuTweakerNew.BuildLab.txt");
            using StreamReader reader = new StreamReader(stream);
            build.Text = (isDevBuild ? ("shh... тише будь | build: " + reader.ReadToEnd()) : ("build: " + reader.ReadToEnd()));
        }

        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            build.Visibility = Visibility.Visible;
        }

        private void Image_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            copyleft.Text = "Mark Adderly, Nikitori / 2023 - 2025 (\"Code only fo money, huh?\" - individual55)";
        }
    }
}

