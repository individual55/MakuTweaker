using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using MakuTweakerNew.Properties;
using Microsoft.Win32;

namespace MakuTweakerNew
{
    public partial class Act : Page
    {
        private MainWindow mw = (MainWindow)Application.Current.MainWindow;

        private string kms;

        private string kmskey;

        public Act()
        {
            InitializeComponent();
            LoadLang();
        }

        private async void HWIDAct()
        {
            try
            {
                mw.Category.IsEnabled = false;
                mw.ABCB.IsEnabled = false;
                string languageCode = Settings.Default.lang ?? "en";
                Dictionary<string, Dictionary<string, string>> act = MainWindow.Localization.LoadLocalization(languageCode, "act");
                string result = await ExecuteCommandAsync(await GetEmbeddedCmdContentAsync("MakuTweakerNew.HWID.cmd"));
                if (result.Contains("Activation is not required"))
                {
                    SystemSounds.Asterisk.Play();
                    mw.ChSt(act["status"]["alrd"]);
                }
                else if (result.Contains("Evaluation editions cannot be activated"))
                {
                    p.ShowError = true;
                    mw.ChSt(act["status"]["err"]);
                    MessageBox.Show(act["status"]["eval"], "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                else if (result.Contains("Not Connected"))
                {
                    p.ShowError = true;
                    mw.ChSt(act["status"]["err"]);
                    MessageBox.Show(act["status"]["nocon"], "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                else if (result.Contains("permanently activated"))
                {
                    SystemSounds.Asterisk.Play();
                    mw.ChSt(act["status"]["success"]);
                }
                else if (result.Contains("missing"))
                {
                    mw.ChSt(act["status"]["err"]);
                    MessageBox.Show(act["status"]["hwerr"], "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                else
                {
                    mw.ChSt(act["status"]["err"]);
                    MessageBox.Show(act["status"]["hwerr"], "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                PostActAnim();
                mw.Category.IsEnabled = true;
                mw.ABCB.IsEnabled = true;
            }
            catch
            {
                string languageCode2 = Settings.Default.lang ?? "en";
                Dictionary<string, Dictionary<string, string>> act2 = MainWindow.Localization.LoadLocalization(languageCode2, "act");
                mw.ChSt(act2["status"]["err"]);
                MessageBox.Show(act2["status"]["hwerr"], "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
                PostActAnim();
                mw.Category.IsEnabled = true;
                mw.ABCB.IsEnabled = true;
            }
        }

        private async Task<string> GetEmbeddedCmdContentAsync(string resourceName)
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            return await reader.ReadToEndAsync();
        }

        private async Task<string> ExecuteCommandAsync(string cmdContent)
        {
            string tempCmdFile = Path.Combine(Path.GetTempPath(), "hwid.cmd");
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Path.Combine(desktopPath, "HWID_Activation_Log.txt");
            await File.WriteAllTextAsync(tempCmdFile, cmdContent);
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/C \"" + tempCmdFile + "\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            string output = await process.StandardOutput.ReadToEndAsync();
            await process.StandardError.ReadToEndAsync();
            process.WaitForExit();
            return output;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HWIDAct();
            ActAnim();
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

        private void ActAnim()
        {
            FadeOut(mact1, 300.0);
            FadeOut(mact2, 300.0);
            FadeOut(mact3, 300.0);
            FadeOut(mact6, 300.0);
            FadeOut(mact7, 300.0);
            FadeOut(mact9, 300.0);
            FadeOut(offi, 300.0);
            FadeOut(autooffact, 300.0);
            FadeIn(p, 300.0);
            FadeIn(t, 300.0);
            autoact.IsEnabled = false;
            autooffact.IsEnabled = false;
            mact1.IsEnabled = false;
            mact2.IsEnabled = false;
            mact3.IsEnabled = false;
            mact6.IsEnabled = false;
            mact7.IsEnabled = false;
            mact9.IsEnabled = false;
        }

        private void OffiActAnim()
        {
            FadeOut(mact1, 300.0);
            FadeOut(mact2, 300.0);
            FadeOut(mact3, 300.0);
            FadeOut(mact6, 300.0);
            FadeOut(mact7, 300.0);
            FadeOut(mact9, 300.0);
            FadeIn(p, 300.0);
            FadeIn(t, 300.0);
            autoact.IsEnabled = false;
            autooffact.IsEnabled = false;
            mact1.IsEnabled = false;
            mact2.IsEnabled = false;
            mact3.IsEnabled = false;
            mact6.IsEnabled = false;
            mact7.IsEnabled = false;
            mact9.IsEnabled = false;
        }

        private void PostOffiActAnim()
        {
            FadeIn(mact1, 300.0);
            FadeIn(mact2, 300.0);
            FadeIn(mact3, 300.0);
            FadeIn(mact6, 300.0);
            FadeIn(mact7, 300.0);
            FadeIn(mact9, 300.0);
            FadeOut(p, 300.0);
            FadeOut(t, 300.0);
            autoact.IsEnabled = true;
            autooffact.IsEnabled = true;
            mact1.IsEnabled = true;
            mact2.IsEnabled = true;
            mact3.IsEnabled = true;
            mact6.IsEnabled = true;
            mact7.IsEnabled = true;
            mact9.IsEnabled = true;
        }

        private void PostActAnim()
        {
            FadeIn(mact1, 300.0);
            FadeIn(mact2, 300.0);
            FadeIn(mact3, 300.0);
            FadeIn(mact6, 300.0);
            FadeIn(mact7, 300.0);
            FadeIn(mact9, 300.0);
            FadeIn(offi, 300.0);
            FadeIn(autooffact, 300.0);
            FadeOut(p, 300.0);
            FadeOut(t, 300.0);
            autoact.IsEnabled = true;
            autooffact.IsEnabled = true;
            mact1.IsEnabled = true;
            mact2.IsEnabled = true;
            mact3.IsEnabled = true;
            mact6.IsEnabled = true;
            mact7.IsEnabled = true;
            mact9.IsEnabled = true;
        }

        private void ManualActAnim()
        {
            FadeIn(p, 300.0);
            FadeIn(t, 300.0);
            autoact.IsEnabled = false;
            autooffact.IsEnabled = false;
            mact1.IsEnabled = false;
            mact2.IsEnabled = false;
            mact3.IsEnabled = false;
            mact6.IsEnabled = false;
            mact7.IsEnabled = false;
            mact9.IsEnabled = false;
        }

        private void ManualPostActAnim()
        {
            FadeOut(p, 300.0);
            FadeOut(t, 300.0);
            autoact.IsEnabled = true;
            autooffact.IsEnabled = true;
            mact1.IsEnabled = true;
            mact2.IsEnabled = true;
            mact3.IsEnabled = true;
            mact6.IsEnabled = true;
            mact7.IsEnabled = true;
            mact9.IsEnabled = true;
        }

        private void mact3_Click(object sender, RoutedEventArgs e)
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> act = MainWindow.Localization.LoadLocalization(languageCode, "act");
            switch (mact2.SelectedIndex)
            {
                case 0:
                    kms = "kms.digiboy.ir";
                    break;
                case 1:
                    kms = "kms.ddns.net";
                    break;
                case 2:
                    kms = "k.zpale.com";
                    break;
                case 3:
                    kms = "mvg.zpale.com";
                    break;
            }
            CMDAsync("cscript C:\\Windows\\System32\\slmgr.vbs /skms " + kms, 0);
        }

        private async Task CMDAsync(string command, int act)
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> actv = MainWindow.Localization.LoadLocalization(languageCode, "act");
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/C " + command,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.Default,
                StandardErrorEncoding = Encoding.Default
            };
            await Task.Run(delegate
            {
                using Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();
                string text = process.StandardOutput.ReadToEnd();
                string text2 = process.StandardError.ReadToEnd();
                process.WaitForExit();
                string text3 = (string.IsNullOrEmpty(text) ? "" : text);
                if (!string.IsNullOrEmpty(text2))
                {
                    text3 = text3 + "\n\nError:\n" + text2;
                }
                string text4 = text3.ToLower();
                if (text4.Contains("error"))
                {
                    base.Dispatcher.Invoke(delegate
                    {
                        SystemSounds.Hand.Play();
                        if (act == 0)
                        {
                            mw.ChSt(actv["status"]["kmserr"]);
                        }
                        else
                        {
                            mw.ChSt(actv["status"]["keyerr"]);
                        }
                    });
                }
                else
                {
                    base.Dispatcher.Invoke(delegate
                    {
                        if (act == 0)
                        {
                            mw.ChSt(actv["status"]["srv"]);
                        }
                        else
                        {
                            mw.ChSt(actv["status"]["edit"]);
                        }
                    });
                }
            });
        }

        private async void mact7_Click(object sender, RoutedEventArgs e)
        {
            mw.Category.IsEnabled = false;
            mw.ABCB.IsEnabled = false;
            ManualActAnim();
            string languageCode = Settings.Default.lang ?? "en";
            MainWindow.Localization.LoadLocalization(languageCode, "act");
            string keyPath = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
            string valueName = "EditionID";
            string ed = "Enterprise";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath))
            {
                object value = key.GetValue(valueName);
                ed = value.ToString();
            }
            switch (ed)
            {
                case "Professional":
                    kmskey = "W269N-WFGWX-YVC9B-4J6C9-T83GX";
                    break;
                case "Core":
                    kmskey = "TX9XD-98N7V-6WMQ6-BX7FG-H8Q99";
                    break;
                case "CoreSingleLanguage":
                    kmskey = "TX9XD-98N7V-6WMQ6-BX7FG-H8Q99";
                    break;
                case "Education":
                    kmskey = "NW6C2-QMPVW-D7KKK-3GKT6-VCFB2";
                    break;
                case "ProEducation":
                    kmskey = "NW6C2-QMPVW-D7KKK-3GKT6-VCFB2";
                    break;
                case "Enterprise":
                    kmskey = "NPPR9-FWDCX-D2C8J-H872K-2YT43";
                    break;
                case "IoTEnterprise":
                    kmskey = "NPPR9-FWDCX-D2C8J-H872K-2YT43";
                    break;
                case "EnterpriseS":
                    kmskey = "M7XTQ-FN8P6-TTKYV-9D4CC-J462D";
                    break;
                case "IoTEnterpriseS":
                    kmskey = "KBN8V-HFGQ4-MGXVD-347P6-PDQGT";
                    break;
            }
            await CMDAsync("cscript C:\\Windows\\System32\\slmgr.vbs /ipk " + kmskey, 1);
            await CMDAsyncFinal("chcp 1251 && cscript C:\\Windows\\System32\\slmgr.vbs /ato", isfinal: true);
        }

        private async Task CMDAsyncFinal(string command, bool isfinal)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> act = MainWindow.Localization.LoadLocalization(languageCode, "act");
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/C " + command,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.GetEncoding(866),
                StandardErrorEncoding = Encoding.GetEncoding(866)
            };
            await Task.Run(delegate
            {
                using Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();
                string text = process.StandardOutput.ReadToEnd();
                string text2 = process.StandardError.ReadToEnd();
                process.WaitForExit();
                string text3 = (string.IsNullOrEmpty(text) ? "" : text);
                if (!string.IsNullOrEmpty(text2))
                {
                    text3 = text3 + "\n\nError:\n" + text2;
                }
                string text4 = text3.ToLower();
                if (text4.Contains("product activated successfully") || text4.Contains("успешно"))
                {
                    base.Dispatcher.Invoke(delegate
                    {
                        SystemSounds.Asterisk.Play();
                        mw.ChSt(act["status"]["success"]);
                    });
                }
                else if (text4.Contains("error") || text4.Contains("ошибка") || text4.Contains("0x"))
                {
                    base.Dispatcher.Invoke(delegate
                    {
                        p.ShowError = true;
                        mw.ChSt(act["status"]["err"]);
                        MessageBox.Show(act["status"]["error"], "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
                    });
                }
                base.Dispatcher.Invoke(delegate
                {
                    ManualPostActAnim();
                    mw.Category.IsEnabled = true;
                    mw.ABCB.IsEnabled = true;
                });
            });
        }

        private void LoadLang()
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> act = MainWindow.Localization.LoadLocalization(languageCode, "act");
            label.Text = act["main"]["label"];
            hwid.Text = act["main"]["hwid"];
            autoact.Content = act["main"]["button20"];
            autooffact.Content = act["main"]["button20"];
            mact3.Content = act["main"]["button20"];
            mact7.Content = act["main"]["button20"];
            mact1.Text = act["main"]["step1"];
            mact6.Text = act["main"]["step3"];
            mact9.Text = act["main"]["kms"];
            offi.Text = act["main"]["offi"];
            t.Text = act["status"]["inprog"];
        }

        private void autooffact_Click(object sender, RoutedEventArgs e)
        {
            OffiActAnim();
            OffiAct();
        }

        private async void OffiAct()
        {
            try
            {
                mw.Category.IsEnabled = false;
                mw.ABCB.IsEnabled = false;
                string languageCode = Settings.Default.lang ?? "en";
                Dictionary<string, Dictionary<string, string>> act = MainWindow.Localization.LoadLocalization(languageCode, "act");
                string result = await ExecuteCommandAsync(await GetEmbeddedCmdContentAsync("MakuTweakerNew.Office.cmd"));
                if (result.Contains("permanently activated"))
                {
                    SystemSounds.Asterisk.Play();
                    mw.ChSt(act["status"]["success"]);
                }
                else if (result.Contains("missing"))
                {
                    mw.ChSt(act["status"]["err"]);
                    MessageBox.Show(act["status"]["offierr"], "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                else
                {
                    mw.ChSt(act["status"]["err"]);
                    MessageBox.Show(act["status"]["offierr"], "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                PostOffiActAnim();
                mw.Category.IsEnabled = true;
                mw.ABCB.IsEnabled = true;
            }
            catch
            {
                string languageCode2 = Settings.Default.lang ?? "en";
                Dictionary<string, Dictionary<string, string>> act2 = MainWindow.Localization.LoadLocalization(languageCode2, "act");
                mw.ChSt(act2["status"]["err"]);
                MessageBox.Show(act2["status"]["offierr"], "MakuTweaker", MessageBoxButton.OK, MessageBoxImage.Hand);
                PostOffiActAnim();
                mw.Category.IsEnabled = true;
                mw.ABCB.IsEnabled = true;
            }
        }
    }

}
