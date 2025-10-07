using System.IO;
using System.Windows;
using System.Windows.Threading;
using MakuTweakerNew.Properties;

// I know the code here is bad, but if you want, I'll fix it.

namespace MakuTweakerNew
{
    public partial class App : Application
    {
        private readonly string logFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public App()
        {
            base.DispatcherUnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            HandleCrash("Unhandled UI Exception", e.Exception, 2);
            e.Handled = true;
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleCrash("Unhandled Critical Exception", e.ExceptionObject as Exception, 1);
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            HandleCrash("Unhandled Task Exception", e.Exception, 3);
            e.SetObserved();
        }

        private void HandleCrash(string errorType, Exception? ex, int exitCode)
        {
            if (ex != null)
            {
                Exception logException = ex.InnerException ?? ex;
                string errorDetails = $"MakuTweaker {Settings.Default.ver} Crash [{DateTime.Now:yyyy-MM-dd HH:mm:ss}]\n{errorType}\n\n" + GetExceptionDetails(logException);
                try
                {
                    Directory.CreateDirectory(logFolder);
                    string path = Path.Combine(logFolder, $"makutw-crash_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");
                    string chatMessage = "If MakuTweaker crashed through no fault of your own, please report this crash in the chat:\r\nhttps://t.me/adderlychat\n\nЕсли MakuTweaker крашнулся не по вашей вине, то, пожалуйста, сообщите об этом краше в чат:\r\nhttps://t.me/adderlychat";
                    errorDetails = errorDetails + "\n\n" + chatMessage;
                    File.WriteAllText(path, errorDetails);
                    MessageBox.Show("Unfortunately, MakuTweaker Has Crashed! :(\n\nError: " + logException.Message + "\n\nCrash Log Saved To Desktop.", "MakuTweaker Crash", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                catch
                {
                    MessageBox.Show("Unfortunately, MakuTweaker Has Crashed! :(\n\nError: " + logException.Message + "\n\nCrash Log Failed to Save.", "MakuTweaker Crash", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                Application.Current.Shutdown(exitCode);
            }
        }

        private string GetExceptionDetails(Exception ex)
        {
            return $"[Message]\n{ex.Message}\n\n[StackTrace]\n{ex.StackTrace}\n\n[TargetSite]\n{ex.TargetSite}\n\n[Data]\n{((ex.Data.Count > 0) ? string.Join(", ", ex.Data.Keys) : "No Data")}\n\n";
        }
    }
}

