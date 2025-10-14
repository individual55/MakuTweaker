using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using MakuTweakerNew.Properties;

namespace MakuTweakerNew
{
    public partial class SAT : Page
    {
        private bool isLoaded = false;

        public SAT()
        {
            InitializeComponent();
            LoadLang();
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> satl = MainWindow.Localization.LoadLocalization(languageCode, "sat");
            int.TryParse(mins.Text, out var number);
            hours.Text = satl["main"]["minho"] + Math.Round(number / 60.0, 2);
            isLoaded = true;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }

        private void time_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mins.Text = Math.Round(time.Value * 5.0).ToString();
        }

        private void mins_TextChanged(object sender, TextChangedEventArgs e)
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> satl = MainWindow.Localization.LoadLocalization(languageCode, "sat");
            int.TryParse(mins.Text, out var number);
            hours.Text = satl["main"]["minho"] + Math.Round((double)number / 60.0, 2);
        }

        private void mins_GotFocus(object sender, RoutedEventArgs e)
        {
            mins.Dispatcher.InvokeAsync(delegate
            {
                mins.SelectAll();
            }, DispatcherPriority.Input);
        }

        private void tenM_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("C:\\Windows\\System32\\shutdown.exe", " -s -t 600");
        }

        private void threeM_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("C:\\Windows\\System32\\shutdown.exe", " -s -t 1800");
        }

        private void oneH_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("C:\\Windows\\System32\\shutdown.exe", " -s -t 3600");
        }

        private void twoH_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("C:\\Windows\\System32\\shutdown.exe", " -s -t 7200");
        }

        private void fourH_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("C:\\Windows\\System32\\shutdown.exe", " -s -t 14400");
        }

        private void sixH_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("C:\\Windows\\System32\\shutdown.exe", " -s -t 21600");
        }

        private void shut_Click(object sender, RoutedEventArgs e)
        {
            double a = Convert.ToDouble(mins.Text);
            double b = Convert.ToDouble(60);
            Process.Start("C:\\Windows\\System32\\shutdown.exe", " -s -t " + Convert.ToString(a * b));
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("C:\\Windows\\System32\\shutdown.exe", " -a");
        }

        private void LoadLang()
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> satl = MainWindow.Localization.LoadLocalization(languageCode, "sat");
            label.Text = satl["main"]["label"];
            sat.Text = satl["main"]["info"];
            hours.Text = satl["main"]["minho"];
            os.Text = satl["main"]["os"];
            oned.Text = satl["main"]["oned"];
            tenM.Content = satl["main"]["tenM"];
            threeM.Content = satl["main"]["threeM"];
            oneH.Content = satl["main"]["oneH"];
            twoH.Content = satl["main"]["twoH"];
            fourH.Content = satl["main"]["fourH"];
            sixH.Content = satl["main"]["sixH"];
            shut.Content = satl["main"]["b1"];
            cancel.Content = satl["main"]["b2"];
        }
    }
}
