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
using System.Windows.Shapes;
using MakuTweakerNew.Properties;
using ModernWpf.Controls;

namespace MakuTweakerNew
{
    public partial class ILOVEMAKUTWEAKERDialog : ContentDialog
    {
        public TaskCompletionSource<int> TaskCompletionSource { get; private set; }

        public ILOVEMAKUTWEAKERDialog(string app)
        {
            InitializeComponent();
            Dictionary<string, Dictionary<string, string>> uwp = MainWindow.Localization.LoadLocalization(Settings.Default.lang ?? "en", "uwp");
            Run run1 = new Run
            {
                Text = $"{uwp["main"]["suredialogT1"]} {app} {uwp["main"]["suredialogT2"]}\n",
                FontSize = 14.0,
                FontFamily = new FontFamily("Segoe UI")
            };
            Run run2 = new Run
            {
                Text = uwp["main"]["suredialogT3"] + "\n",
                FontSize = 14.0,
                FontFamily = new FontFamily("Segoe UI")
            };
            Run run3 = new Run
            {
                Text = (uwp["main"]["suredialogT4"] ?? ""),
                FontSize = 18.0,
                FontFamily = new FontFamily("Segoe UI Semibold")
            };
            base.CloseButtonText = uwp["main"]["suredialogNS"];
            textBlock.Inlines.Add(run1);
            textBlock.Inlines.Add(new LineBreak());
            textBlock.Inlines.Add(run2);
            textBlock.Inlines.Add(new LineBreak());
            textBlock.Inlines.Add(run3);
            textBlock.TextAlignment = TextAlignment.Left;
            TaskCompletionSource = new TaskCompletionSource<int>();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ILOVEMAKUTWEAKER.Text == "ILOVEMAKUTWEAKER")
            {
                base.PrimaryButtonText = "OK";
            }
            else
            {
                base.PrimaryButtonText = string.Empty;
            }
        }

        private void CloseDialog(int result)
        {
            TaskCompletionSource.SetResult(result);
            Hide();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            CloseDialog(1);
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            CloseDialog(0);
        }
    }
}
