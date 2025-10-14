using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using MakuTweakerNew.Properties;
using ModernWpf.Controls;

namespace MakuTweakerNew
{
    public partial class HidePart : ContentDialog
    {
        public TaskCompletionSource<decimal> TaskCompletionSource { get; private set; }

        public HidePart()
        {
            InitializeComponent();
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> expl = MainWindow.Localization.LoadLocalization(languageCode, "expl");
            Run run1 = new Run
            {
                Text = expl["status"]["hdInfo1"],
                FontSize = 18.0,
                FontFamily = new FontFamily("Segoe UI Semilight")
            };
            Run run2 = new Run
            {
                Text = expl["status"]["hdInfo2"],
                FontSize = 18.0,
                FontFamily = new FontFamily("Segoe UI Semibold")
            };
            base.CloseButtonText = expl["status"]["hide"];
            base.PrimaryButtonText = expl["status"]["cc"];
            textBlock.Inlines.Add(run1);
            textBlock.Inlines.Add(new LineBreak());
            textBlock.Inlines.Add(run2);
            textBlock.TextAlignment = TextAlignment.Left;
            textBlock.MaxWidth = 500.0;
            TaskCompletionSource = new TaskCompletionSource<decimal>();
            if (string.IsNullOrEmpty(Settings.Default.hiddenDrives))
            {
                return;
            }
            foreach (object child in checkboxpanel.Children)
            {
                if (child is CheckBox checkBox)
                {
                    if (Settings.Default.hiddenDrives.Contains(checkBox.Content.ToString()))
                    {
                        checkBox.IsChecked = true;
                    }
                    else
                    {
                        checkBox.IsChecked = false;
                    }
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void CloseDialog(decimal result)
        {
            TaskCompletionSource.SetResult(result);
            Hide();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            CloseDialog(-1m);
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            decimal i = default(decimal);
            if (a.IsChecked == true)
            {
                i += 1m;
            }
            if (d.IsChecked == true)
            {
                i += 8m;
            }
            if (e.IsChecked == true)
            {
                i += 16m;
            }
            if (f.IsChecked == true)
            {
                i += 32m;
            }
            if (g.IsChecked == true)
            {
                i += 64m;
            }
            if (h.IsChecked == true)
            {
                i += 128m;
            }
            if (this.i.IsChecked == true)
            {
                i += 256m;
            }
            if (j.IsChecked == true)
            {
                i += 512m;
            }
            if (k.IsChecked == true)
            {
                i += 1024m;
            }
            if (l.IsChecked == true)
            {
                i += 2048m;
            }
            if (m.IsChecked == true)
            {
                i += 4096m;
            }
            if (n.IsChecked == true)
            {
                i += 8192m;
            }
            if (o.IsChecked == true)
            {
                i += 16384m;
            }
            if (p.IsChecked == true)
            {
                i += 32768m;
            }
            if (q.IsChecked == true)
            {
                i += 65536m;
            }
            if (r.IsChecked == true)
            {
                i += 131072m;
            }
            if (s.IsChecked == true)
            {
                i += 262144m;
            }
            if (t.IsChecked == true)
            {
                i += 524288m;
            }
            if (u.IsChecked == true)
            {
                i += 1048576m;
            }
            if (v.IsChecked == true)
            {
                i += 2097152m;
            }
            if (w.IsChecked == true)
            {
                i += 4194304m;
            }
            if (x.IsChecked == true)
            {
                i += 8388608m;
            }
            if (y.IsChecked == true)
            {
                i += 16777216m;
            }
            if (z.IsChecked == true)
            {
                i += 33554432m;
            }
            StringBuilder drives = new StringBuilder();
            foreach (object child in checkboxpanel.Children)
            {
                if (child is CheckBox { IsChecked: var isChecked } checkBox && isChecked == true)
                {
                    drives.Append(checkBox.Content.ToString());
                }
            }
            Settings.Default.hiddenDrives = drives.ToString();
            CloseDialog(i);
        }
    }
}
