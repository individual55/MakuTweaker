﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MakuTweakerNew.Properties;

namespace MakuTweakerNew
{
    public partial class Other : Page
    {
        private CancellationTokenSource cancl;

        private bool skipcur;

        private bool skipped;

        private bool isLoaded;

        private MainWindow mw = (MainWindow)Application.Current.MainWindow;

        public Other()
        {
            InitializeComponent();
            checkReg();
            LoadLang();
            isLoaded = true;
        }

        private void t3_Toggled(object sender, RoutedEventArgs e)
        {
        }

        private void t5_Toggled(object sender, RoutedEventArgs e)
        {
        }

        private void t6_Toggled(object sender, RoutedEventArgs e)
        {
        }

        private void t7_Toggled(object sender, RoutedEventArgs e)
        {
        }

        private void t8_Toggled(object sender, RoutedEventArgs e)
        {
        }

        private void t9_Toggled(object sender, RoutedEventArgs e)
        {
        }

        private void LoadLang()
        {
            string language = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> app = MainWindow.Localization.LoadLocalization(language, "app");
            MainWindow.Localization.LoadLocalization(language, "base");
            label.Text = app["main"]["label"];
            start.Content = app["main"]["start"];
            stop.Content = app["main"]["stop"];
            skip.Content = app["main"]["skip"];
            winget.Content = app["main"]["winget"];
        }

        private void checkReg()
        {
        }

        private async void start_Click(object sender, RoutedEventArgs e)
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> app = MainWindow.Localization.LoadLocalization(languageCode, "app");
            int count = 0;
            ModernWpf.Controls.ToggleSwitch[] array = new ModernWpf.Controls.ToggleSwitch[24]
            {
            a1, a2, a3, a4, a5, a6, a7, a8, a9, a10,
            a11, a12, a13, a14, a15, a16, a17, a18, a19, a20,
            a21, a22, a23, a24
            };
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].IsOn)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                FadeOutAll();
                FadeIn(p, 300.0);
                FadeIn(t, 300.0);
                start.Visibility = Visibility.Collapsed;
                winget.Visibility = Visibility.Collapsed;
                skip.Visibility = Visibility.Visible;
                stop.Visibility = Visibility.Visible;
                cancl = new CancellationTokenSource();
                CancellationToken token = cancl.Token;
                t.Text = $"{app["status"]["inst"]} | 0/{count}";
                mw.Category.IsEnabled = false;
                mw.ABCB.IsEnabled = false;
                p.Maximum = count;
                p.Value = 0.0;
                await Task.Delay(300);
                (ModernWpf.Controls.ToggleSwitch, string)[] appPackages = new (ModernWpf.Controls.ToggleSwitch, string)[24]
                {
                (a1, "Google.Chrome"),
                (a2, "Telegram.TelegramDesktop"),
                (a3, "7zip.7zip"),
                (a4, "Valve.Steam"),
                (a5, "Discord.Discord"),
                (a6, "OBSProject.OBSStudio"),
                (a7, "VideoLAN.VLC"),
                (a8, "clsid2.mpc-hc"),
                (a9, "voidtools.Everything"),
                (a10, "Spotify.Spotify"),
                (a11, "dotPDN.PaintDotNet"),
                (a12, "qBittorrent.qBittorrent"),
                (a13, "Mozilla.Firefox"),
                (a14, "Vivaldi.Vivaldi"),
                (a15, "64Gram.64Gram"),
                (a16, "HandBrake.HandBrake"),
                (a17, "ByteDance.CapCut"),
                (a18, "Audacity.Audacity"),
                (a19, "AnyDesk.AnyDesk"),
                (a20, "ShareX.ShareX"),
                (a21, "RamenSoftware.Windhawk"),
                (a22, "CrystalDewWorld.CrystalDiskInfo"),
                (a23, "PrismLauncher.PrismLauncher"),
                (a24, "Notepad++.Notepad++")
                };
                (ModernWpf.Controls.ToggleSwitch toggle, string packageName)[] array2 = appPackages;
                for (int j = 0; j < array2.Length; j++)
                {
                    var (toggle, packageName) = array2[j];
                    if (token.IsCancellationRequested)
                    {
                        t.Text = app["status"]["instC"];
                        break;
                    }
                    if (toggle.IsOn)
                    {
                        t.Text = $"{app["status"]["inst"]} | {packageName} | {p.Value}/{count}";
                        if (!(await InstallApp(packageName, token)))
                        {
                            t.Text = app["status"]["instC"];
                            p.ShowError = true;
                            break;
                        }
                        p.Value++;
                    }
                }
                FadeInAll();
                FadeOut(p, 300.0);
                FadeOut(t, 300.0);
                start.Visibility = Visibility.Visible;
                winget.Visibility = Visibility.Visible;
                skip.Visibility = Visibility.Collapsed;
                stop.Visibility = Visibility.Collapsed;
                mw.Category.IsEnabled = true;
                mw.ABCB.IsEnabled = true;
                if (!token.IsCancellationRequested)
                {
                    t.Text = app["status"]["instS"];
                    SystemSounds.Asterisk.Play();
                }
            }
            else
            {
                mw.ChSt(app["status"]["havent"]);
                SystemSounds.Asterisk.Play();
            }
        }

        private async Task<bool> InstallApp(string packageName, CancellationToken token)
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> app = MainWindow.Localization.LoadLocalization(languageCode, "app");
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = "-Command winget install " + packageName + " --accept-source-agreements",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true
            };
            try
            {
                Process process = new Process
                {
                    StartInfo = startInfo
                };
                try
                {
                    DateTime startTime = DateTime.Now;
                    skipped = false;
                    skipcur = false;
                    process.Start();
                    while (!process.HasExited)
                    {
                        await Task.Delay(100);
                        if (!token.IsCancellationRequested && !skipcur)
                        {
                            continue;
                        }
                        try
                        {
                            if (!process.HasExited)
                            {
                                process.Kill(entireProcessTree: true);
                                await process.WaitForExitAsync();
                            }
                        }
                        catch (Exception)
                        {
                        }
                        skipped = skipcur;
                        skipcur = false;
                        return !token.IsCancellationRequested;
                    }
                    string errorOutput = await process.StandardError.ReadToEndAsync();
                    await Task.Run(delegate
                    {
                        process.WaitForExit();
                    });
                    if ((DateTime.Now - startTime).TotalSeconds < 5.0)
                    {
                        MessageBox.Show(app["main"]["toofastD1"] + packageName + app["main"]["toofastD2"], "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    if (!string.IsNullOrWhiteSpace(errorOutput))
                    {
                        if (errorOutput.Contains("CommandNotFoundException"))
                        {
                            MessageBox.Show(app["main"]["wgerr"], "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
                            return false;
                        }
                        MessageBox.Show(app["main"]["apperr1"] + packageName + app["main"]["apperr2"], "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                }
                finally
                {
                    if (process != null)
                    {
                        ((IDisposable)process).Dispose();
                    }
                }
            }
            catch (Exception ex2)
            {
                MessageBox.Show("Error:\n\n" + ex2.Message, "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            return true;
        }

        private void FadeIn(UIElement element, double durationSeconds)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromMilliseconds(durationSeconds),
                EasingFunction = new QuadraticEase
                {
                    EasingMode = EasingMode.EaseIn
                }
            };
            element.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
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

        private void FadeOutAll()
        {
            ModernWpf.Controls.ToggleSwitch[] array = new ModernWpf.Controls.ToggleSwitch[24]
            {
            a1, a2, a3, a4, a5, a6, a7, a8, a9, a10,
            a11, a12, a13, a14, a15, a16, a17, a18, a19, a20,
            a21, a22, a23, a24
            };
            foreach (ModernWpf.Controls.ToggleSwitch toggles in array)
            {
                FadeOut(toggles, 300.0);
                toggles.IsEnabled = false;
            }
        }

        private void FadeInAll()
        {
            ModernWpf.Controls.ToggleSwitch[] array = new ModernWpf.Controls.ToggleSwitch[24]
            {
            a1, a2, a3, a4, a5, a6, a7, a8, a9, a10,
            a11, a12, a13, a14, a15, a16, a17, a18, a19, a20,
            a21, a22, a23, a24
            };
            foreach (ModernWpf.Controls.ToggleSwitch toggles in array)
            {
                FadeIn(toggles, 300.0);
                toggles.IsEnabled = true;
            }
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            cancl?.Cancel();
        }

        private async void skip_Click(object sender, RoutedEventArgs e)
        {
            skipcur = true;
        }

        private void winget_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo pcs = new ProcessStartInfo
            {
                FileName = "ms-windows-store://pdp/?productId=9NBLGGH4NNS1",
                UseShellExecute = true
            };
            Process process = new Process();
            process.StartInfo = pcs;
            process.Start();
        }
    }
}
