using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;
using Hardcodet.Wpf.TaskbarNotification;
using MakuTweakerNew.Properties;
using MicaWPF.Controls;
using MicaWPF.Core.Enums;
using MicaWPF.Core.Services;
using Microsoft.Win32;
using ModernWpf;
using ModernWpf.Media.Animation;
using Newtonsoft.Json;

namespace MakuTweakerNew
{
    public partial class MainWindow : MicaWindow, IComponentConnector
    {
        public static class Localization
        {
            public static Dictionary<string, Dictionary<string, string>> LoadLocalization(string language, string category)
            {
                string localizationFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "loc");
                string jsonFile = Path.Combine(localizationFolder, language + ".json");
                if (!File.Exists(jsonFile))
                {
                    Settings.Default.lang = "en";
                    throw new FileNotFoundException("Cannot find a " + jsonFile + " localization file.\nPlease reinstall MakuTweaker.\nLanguage has been changed to English.");
                }
                string jsonContent = File.ReadAllText(jsonFile);
                Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> localizationData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>>>(jsonContent);
                if (localizationData.ContainsKey("categories"))
                {
                    Dictionary<string, Dictionary<string, Dictionary<string, string>>> categories = localizationData["categories"];
                    if (categories.ContainsKey(category))
                    {
                        return categories[category];
                    }
                }
                Settings.Default.lang = "en";
                throw new KeyNotFoundException($"Cannot find a \"{category}\" category in the {jsonFile} localization file.\nPlease reinstall MakuTweaker.\nLanguage has been changed to English.");
            }
        }

        private NavigationTransitionInfo _transitionInfo = null;

        private DispatcherTimer ExpRestart;

        private bool isAnimating = false;

        public MainWindow()
        {
            InitializeComponent();
            if (checkWinVer() < 14393)
            {
                DialogResult old = System.Windows.Forms.MessageBox.Show("Your version of Windows is not supported. To use MakuTweaker, update your system to Windows 10 1607 or higher. Do you want to download MakuTweaker Legacy Windows Edition?\n\nВаша версия Windows неподдерживается. Для использования MakuTweaker, обновитесь до Windows 10 1607 или выше. Вы хотите скачать MakuTweaker для старых Windows?", "MakuTweaker", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                if (old == System.Windows.Forms.DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo("https://adderly.top/mt")
                    {
                        UseShellExecute = true
                    });
                }
                System.Windows.Application.Current.Shutdown();
            }
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("MakuTweakerNew.BuildLab.txt"))
            {
                using StreamReader reader = new StreamReader(stream);
                string buildVersion = reader.ReadToEnd();
            }
            ExpTimer();
            if (Settings.Default.firRun)
            {
                string systemLanguage = CultureInfo.CurrentCulture.Name;
                string text = systemLanguage;
                string lang = text;
                if (lang == null)
                {
                    goto IL_0183;
                }
                if (lang.StartsWith("uk-"))
                {
                    Settings.Default.lang = "ua";
                    Settings.Default.langSI = 2;
                }
                else
                {
                    string lang2 = lang;
                    if (lang2.StartsWith("ru-"))
                    {
                        Settings.Default.lang = "ru";
                        Settings.Default.langSI = 1;
                    }
                    else
                    {
                        string lang3 = lang;
                        if (!lang3.StartsWith("en-"))
                        {
                            goto IL_0183;
                        }
                        Settings.Default.lang = "en";
                        Settings.Default.langSI = 0;
                    }
                }
                goto IL_01a1;
            }
            string themeString = Settings.Default.theme;
            WindowsTheme parsedTheme;
            if (string.IsNullOrEmpty(themeString) || themeString == "Auto")
            {
                WindowsTheme systemTheme = MicaWPFServiceUtility.ThemeService.CurrentTheme;
                ApplyTheme(systemTheme);
                Settings.Default.theme = ((systemTheme == WindowsTheme.Dark) ? "Dark" : "Light");
                Settings.Default.Save();
            }
            else if (Enum.TryParse<WindowsTheme>(themeString, out parsedTheme))
            {
                ApplyTheme(parsedTheme);
            }
            else
            {
                ApplyTheme(MicaWPFServiceUtility.ThemeService.CurrentTheme);
            }
            goto IL_02a5;
        IL_0183:
            Settings.Default.lang = "en";
            Settings.Default.langSI = 0;
            goto IL_01a1;
        IL_02a5:
            LoadLang(Settings.Default.lang);
            CheckForUpd();
            return;
        IL_01a1:
            Settings.Default.firRun = false;
            WindowsTheme currentSystemTheme = MicaWPFServiceUtility.ThemeService.CurrentTheme;
            Settings.Default.theme = ((currentSystemTheme == WindowsTheme.Dark) ? "Dark" : "Light");
            Settings.Default.firRun = false;
            Settings.Default.Save();
            ApplyTheme(currentSystemTheme);
            Settings.Default.Save();
            goto IL_02a5;
        }

        private void ApplyTheme(WindowsTheme theme)
        {
            MicaWPFServiceUtility.ThemeService.ChangeTheme(theme);
            if (theme == WindowsTheme.Dark)
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                base.Foreground = System.Windows.Media.Brushes.White;
                Separator.Stroke = System.Windows.Media.Brushes.White;
            }
            else
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                base.Foreground = System.Windows.Media.Brushes.Black;
                Separator.Stroke = System.Windows.Media.Brushes.Black;
            }
        }

        private void ExpTimer()
        {
            ExpRestart = new DispatcherTimer();
            ExpRestart.Interval = TimeSpan.FromMilliseconds(1000.0);
            ExpRestart.Tick += ExpRestart_Tick;
        }

        private void MicaWindow_Closing(object sender, CancelEventArgs e)
        {
            Settings.Default.b1 = false;
            Settings.Default.b2 = false;
            Settings.Default.b3 = false;
            Settings.Default.b4 = false;
            Settings.Default.b5 = false;
            Settings.Default.b6 = false;
            Settings.Default.b7 = false;
            Settings.Default.b8 = false;
            Settings.Default.b9 = false;
            Settings.Default.b10 = false;
            Settings.Default.b11 = false;
            Settings.Default.pwsh = false;
            Settings.Default.Save();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _transitionInfo = new EntranceNavigationTransitionInfo();
            switch (Category.SelectedIndex)
            {
                case 0:
                    MainFrame.Navigate(typeof(Explorer), null, _transitionInfo);
                    break;
                case 1:
                    MainFrame.Navigate(typeof(WindowsUpdate), null, _transitionInfo);
                    break;
                case 2:
                    MainFrame.Navigate(typeof(SysAndRec), null, _transitionInfo);
                    break;
                case 3:
                    MainFrame.Navigate(typeof(UWP), null, _transitionInfo);
                    break;
                case 4:
                    MainFrame.Navigate(typeof(Personalization), null, _transitionInfo);
                    break;
                case 5:
                    MainFrame.Navigate(typeof(ContextMenu), null, _transitionInfo);
                    break;
                case 6:
                    MainFrame.Navigate(typeof(Telemetry), null, _transitionInfo);
                    break;
                case 7:
                    MainFrame.Navigate(typeof(WindowsComponents), null, _transitionInfo);
                    break;
                case 8:
                    MainFrame.Navigate(typeof(Act), null, _transitionInfo);
                    break;
                case 9:
                    MainFrame.Navigate(typeof(AppInstall), null, _transitionInfo);
                    break;
                case 10:
                    MainFrame.Navigate(typeof(QuickSet), null, _transitionInfo);
                    break;
                case 11:
                    MainFrame.Navigate(typeof(SAT), null, _transitionInfo);
                    break;
                case 12:
                    MainFrame.Navigate(typeof(PCI), null, _transitionInfo);
                    break;
            }
            Settings.Default.lastPage = Category.SelectedIndex;
            Settings.Default.Save();
        }

        private void MicaWindow_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
        }

        private void MicaWindow_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
        }

        private void MicaWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.satstart)
            {
                Category.SelectedIndex = 4;
            }
            else if (Settings.Default.lastPage == -1)
            {
                Category.SelectedIndex = 0;
            }
            else
            {
                Category.SelectedIndex = Settings.Default.lastPage;
            }
            Enum.TryParse<BackdropType>(Settings.Default.style, out var bd);
            MicaWPFServiceUtility.ThemeService.EnableBackdrop(this, bd);
        }

        public async void ChSt(string st)
        {
            if (isAnimating)
            {
                return;
            }
            try
            {
                isAnimating = true;
                AnimY(status, 300.0, 26.0, 0.0);
                status.Text = st;
                await Task.Delay(5000);
                AnimY(status, 300.0, 0.0, 33.0);
            }
            finally
            {
                isAnimating = false;
            }
        }

        public void LoadLang(string lang)
        {
            try
            {
                string languageCode = Settings.Default.lang ?? "en";
                Dictionary<string, Dictionary<string, string>> basel = Localization.LoadLocalization(languageCode, "base");
                c1.Content = basel["catname"]["expl"];
                c2.Content = basel["catname"]["wu"];
                c3.Content = basel["catname"]["sr"];
                c4.Content = basel["catname"]["uwp"];
                c5.Content = basel["catname"]["per"];
                c6.Content = basel["catname"]["cm"];
                c7.Content = basel["catname"]["tel"];
                c8.Content = basel["catname"]["stask"];
                c9.Content = basel["catname"]["act"];
                c10.Content = basel["catname"]["oth"];
                c11.Content = basel["catname"]["quick"];
                c12.Content = basel["catname"]["sat"];
                c13.Content = basel["catname"]["pci"];
                rexplorer.Label = basel["lowtabs"]["rexp"];
                settingsButton.Label = basel["lowtabs"]["set"];
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "MakuTweaker Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                System.Windows.Forms.Application.Restart();
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void AnimY(UIElement element, double durationMilliseconds, double from, double to)
        {
            double currentY = 16.0;
            if ((element.RenderTransform != null && element.RenderTransform is TranslateTransform { Y: var currentY2 }) || element.RenderTransform == null || element.RenderTransform is MatrixTransform { Matrix: { OffsetY: var currentY3 } })
            {
            }
            DoubleAnimation moveDownAnimation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromMilliseconds(durationMilliseconds),
                EasingFunction = new QuinticEase
                {
                    EasingMode = EasingMode.EaseOut
                }
            };
            if (element.RenderTransform == null || !(element.RenderTransform is TranslateTransform))
            {
                element.RenderTransform = new TranslateTransform();
            }
            TranslateTransform translateTransform = (TranslateTransform)element.RenderTransform;
            translateTransform.BeginAnimation(TranslateTransform.YProperty, moveDownAnimation);
        }

        public void RebootNotify(int mode)
        {
            string message = string.Empty;
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> basel = Localization.LoadLocalization(languageCode, "base");
            Icon trayIcon = new Icon(GetResourceStream("MakuTweakerNew.MakuT.ico"));
            TaskbarIcon _trayIcon = new TaskbarIcon
            {
                ToolTipText = "MakuTweaker",
                Icon = trayIcon
            };
            switch (mode)
            {
                case 1:
                    message = basel["def"]["rebnotify"];
                    break;
                case 2:
                    message = basel["def"]["rebnotifyexplorer"];
                    break;
                case 3:
                    message = basel["def"]["rebnotifysfc"];
                    break;
            }
            _trayIcon.ShowBalloonTip("MakuTweaker", message, BalloonIcon.Warning);
            Task.Delay(8000).ContinueWith(delegate
            {
                _trayIcon.Dispatcher.Invoke(delegate
                {
                    _trayIcon.Dispose();
                });
            });
        }

        private Stream GetResourceStream(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream resourceStream = assembly.GetManifestResourceStream(resourceName);
            if (resourceStream == null)
            {
                throw new FileNotFoundException("Ресурс " + resourceName + " не найден.");
            }
            return resourceStream;
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            Category.SelectedIndex = -1;
            MainFrame.Navigate(typeof(SettingsAbout), null, _transitionInfo);
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (Category.SelectedIndex == -1)
            {
                settingsButton.IsEnabled = false;
            }
            else
            {
                settingsButton.IsEnabled = true;
            }
        }

        public void expk()
        {
            Process proc = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "taskkill";
            startInfo.Arguments = "/F /IM explorer.exe";
            proc.StartInfo = startInfo;
            proc.Start();
        }

        private void ExpRestart_Tick(object sender, EventArgs e)
        {
            Process.Start("explorer.exe");
            ExpRestart.Stop();
        }

        private void rexplorer_Click(object sender, RoutedEventArgs e)
        {
            expk();
            ExpRestart.Start();
        }

        private async Task CheckForUpd()
        {
            int ThisBuild = int.Parse(new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("MakuTweakerNew.BuildNumber.txt")).ReadToEnd().Trim());
            string url = "https://raw.githubusercontent.com/AdderlyMark/MakuTweaker/refs/heads/main/ver.json";
            using HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string jsonString = await response.Content.ReadAsStringAsync();
                try
                {
                    Dictionary<string, string> jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                    if (!jsonData.ContainsKey("build"))
                    {
                        return;
                    }
                    string lb = jsonData["build"];
                    if (int.Parse(lb) <= ThisBuild)
                    {
                        return;
                    }
                    Icon trayIcon = new Icon(GetResourceStream("MakuTweakerNew.MakuT.ico"));
                    TaskbarIcon _trayIcon = new TaskbarIcon
                    {
                        ToolTipText = "MakuTweaker",
                        Icon = trayIcon
                    };
                    if (Settings.Default.lang == "ru" || Settings.Default.lang == "ua" || Settings.Default.lang == "kz")
                    {
                        _trayIcon.ShowBalloonTip("MakuTweaker", "Доступно обновление MakuTweaker!\nНажмите на уведомление, чтобы перейти на страницу загрузки.", BalloonIcon.Info);
                    }
                    else
                    {
                        _trayIcon.ShowBalloonTip("MakuTweaker", "An update for MakuTweaker is available!\nClick the notification to go to the download page.", BalloonIcon.Info);
                    }
                    _trayIcon.TrayBalloonTipClicked += delegate
                    {
                        Process.Start(new ProcessStartInfo("https://adderly.top/makutweaker")
                        {
                            UseShellExecute = true
                        });
                    };
                    Task.Delay(8000).ContinueWith(delegate
                    {
                        _trayIcon.Dispatcher.Invoke(delegate
                        {
                            _trayIcon.Dispose();
                        });
                    });
                }
                catch (JsonException)
                {
                }
            }
            catch (HttpRequestException)
            {
            }
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
    }

}