using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MakuTweakerNew.Properties;
using Microsoft.Win32;
using ModernWpf.Controls;

namespace MakuTweakerNew
{
    public partial class Telemetry : System.Windows.Controls.Page
    {
        private bool isLoaded = false;

        private bool isNotify = true;

        private bool isbycheck = false;

        private MainWindow mw = (MainWindow)Application.Current.MainWindow;

        public Telemetry()
        {
            InitializeComponent();
            checkReg();
            LoadLang();
            isLoaded = true;
        }

        private void t1_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }
            Settings.Default.t1 = t1.IsOn;
            if (t1.IsOn)
            {
                try
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection").SetValue("AllowTelemetry", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection").SetValue("AllowTelemetry", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection").SetValue("MaxTelemetryAllowed", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows NT\\CurrentVersion\\Software Protection Platform").SetValue("NoGenTicket", 1);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection").SetValue("DoNotShowFeedbackNotifications", 1);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\AppCompat").SetValue("AITEnable", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\AppCompat").SetValue("AllowTelemetry", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\AppCompat").SetValue("DisableEngine", 1);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\AppCompat").SetValue("DisableInventory", 1);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\AppCompat").SetValue("DisablePCA", 1);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\AppCompat").SetValue("DisableUAR", 1);
                }
                catch
                {
                }
                isNotify = false;
                if (!isbycheck)
                {
                    t2.IsOn = true;
                    t3.IsOn = true;
                    t4.IsOn = true;
                    t5.IsOn = true;
                    t6.IsOn = true;
                    mw.RebootNotify(1);
                }
                isNotify = true;
            }
            else
            {
                try
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection").SetValue("AllowTelemetry", 1);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection").SetValue("AllowTelemetry", 1);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection").SetValue("MaxTelemetryAllowed", 1);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows NT\\CurrentVersion\\Software Protection Platform").SetValue("NoGenTicket", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection").SetValue("DoNotShowFeedbackNotifications", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\AppCompat").SetValue("AITEnable", 1);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\AppCompat").SetValue("AllowTelemetry", 1);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\AppCompat").SetValue("DisableEngine", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\AppCompat").SetValue("DisableInventory", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\AppCompat").SetValue("DisablePCA", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\AppCompat").SetValue("DisableUAR", 0);
                }
                catch
                {
                }
                isNotify = false;
                if (!isbycheck)
                {
                    t2.IsOn = false;
                    t3.IsOn = false;
                    t4.IsOn = false;
                    t5.IsOn = false;
                    t6.IsOn = false;
                    mw.RebootNotify(1);
                }
                isNotify = true;
            }
        }

        private void t2_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.t2 = t2.IsOn;
                if (t2.IsOn)
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\CapabilityAccessManager\\ConsentStore\\appDiagnostics").SetValue("Value", "Deny", RegistryValueKind.String);
                }
                else
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\CapabilityAccessManager\\ConsentStore\\appDiagnostics").SetValue("Value", "Allow", RegistryValueKind.String);
                    isbycheck = true;
                    t1.IsOn = false;
                    isbycheck = false;
                }
                if (isNotify)
                {
                    mw.RebootNotify(1);
                }
                if (t2.IsOn && t3.IsOn && t4.IsOn && t5.IsOn && t6.IsOn)
                {
                    isbycheck = true;
                    t1.IsOn = true;
                    isbycheck = false;
                }
            }
        }

        private void t3_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.t3 = t3.IsOn;
                if (t3.IsOn)
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System").SetValue("UploadUserActivities", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System").SetValue("PublishUserActivities", 0);
                }
                else
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System").SetValue("UploadUserActivities", 1);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System").SetValue("PublishUserActivities", 1);
                    isbycheck = true;
                    t1.IsOn = false;
                    isbycheck = false;
                }
                if (isNotify)
                {
                    mw.RebootNotify(1);
                }
                if (t2.IsOn && t3.IsOn && t4.IsOn && t5.IsOn && t6.IsOn)
                {
                    isbycheck = true;
                    t1.IsOn = true;
                    isbycheck = false;
                }
            }
        }

        private void t4_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.t4 = t4.IsOn;
                if (t4.IsOn)
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WDI\\{9c5a40da-b965-4fc3-8781-88dd50a6299d}").SetValue("ScenarioExecutionEnabled", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\DeviceHealthAttestationService").SetValue("EnableDeviceHealthAttestationService", 0);
                }
                else
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WDI\\{9c5a40da-b965-4fc3-8781-88dd50a6299d}").SetValue("ScenarioExecutionEnabled", 1);
                    isbycheck = true;
                    t1.IsOn = false;
                    isbycheck = false;
                }
                if (isNotify)
                {
                    mw.RebootNotify(1);
                }
                if (t2.IsOn && t3.IsOn && t4.IsOn && t5.IsOn && t6.IsOn)
                {
                    isbycheck = true;
                    t1.IsOn = true;
                    isbycheck = false;
                }
            }
        }

        private void t5_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.t5 = t5.IsOn;
                if (t5.IsOn)
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\InputPersonalization").SetValue("RestrictImplicitTextCollection", 0);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\InputPersonalization").SetValue("RestrictImplicitInkCollection", 0);
                }
                else
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\InputPersonalization").SetValue("RestrictImplicitTextCollection", 1);
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\InputPersonalization").SetValue("RestrictImplicitInkCollection", 1);
                    isbycheck = true;
                    t1.IsOn = false;
                    isbycheck = false;
                }
                if (isNotify)
                {
                    mw.RebootNotify(1);
                }
                if (t2.IsOn && t3.IsOn && t4.IsOn && t5.IsOn && t6.IsOn)
                {
                    isbycheck = true;
                    t1.IsOn = true;
                    isbycheck = false;
                }
            }
        }

        private void t6_Toggled(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Settings.Default.t6 = t6.IsOn;
                if (t6.IsOn)
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Speech").SetValue("AllowSpeechModelUpdate", 0);
                }
                else
                {
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Speech").SetValue("AllowSpeechModelUpdate", 1);
                    isbycheck = true;
                    t1.IsOn = false;
                    isbycheck = false;
                }
                if (isNotify)
                {
                    mw.RebootNotify(1);
                }
                if (t2.IsOn && t3.IsOn && t4.IsOn && t5.IsOn && t6.IsOn)
                {
                    isbycheck = true;
                    t1.IsOn = true;
                    isbycheck = false;
                }
            }
        }

        private void LoadLang()
        {
            string languageCode = Settings.Default.lang ?? "en";
            Dictionary<string, Dictionary<string, string>> tel = MainWindow.Localization.LoadLocalization(languageCode, "tel");
            Dictionary<string, Dictionary<string, string>> basel = MainWindow.Localization.LoadLocalization(languageCode, "base");
            label.Text = tel["main"]["label"];
            t1.Header = tel["main"]["t1"];
            t2.Header = tel["main"]["t2"];
            t3.Header = tel["main"]["t3"];
            t4.Header = tel["main"]["t4"];
            t5.Header = tel["main"]["t5"];
            t6.Header = tel["main"]["t6"];
            t1.OffContent = basel["def"]["off"];
            t2.OffContent = basel["def"]["off"];
            t3.OffContent = basel["def"]["off"];
            t4.OffContent = basel["def"]["off"];
            t5.OffContent = basel["def"]["off"];
            t6.OffContent = basel["def"]["off"];
            t1.OnContent = basel["def"]["on"];
            t2.OnContent = basel["def"]["on"];
            t3.OnContent = basel["def"]["on"];
            t4.OnContent = basel["def"]["on"];
            t5.OnContent = basel["def"]["on"];
            t6.OnContent = basel["def"]["on"];
        }

        private void checkReg()
        {
            ToggleSwitch toggleSwitch = t1;
            RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection");
            int isOn;
            if (registryKey != null && registryKey.GetValue("AllowTelemetry")?.Equals(0) == true)
            {
                RegistryKey? registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection");
                if (registryKey2 != null && registryKey2.GetValue("AllowTelemetry")?.Equals(0) == true)
                {
                    RegistryKey? registryKey3 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection");
                    if (registryKey3 != null && registryKey3.GetValue("MaxTelemetryAllowed")?.Equals(0) == true)
                    {
                        RegistryKey? registryKey4 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows NT\\CurrentVersion\\Software Protection Platform");
                        isOn = ((registryKey4 != null && registryKey4.GetValue("NoGenTicket")?.Equals(1) == true) ? 1 : 0);
                        goto IL_0132;
                    }
                }
            }
            isOn = 0;
            goto IL_0132;

        IL_0132:
            toggleSwitch.IsOn = (byte)isOn != 0;
            t2.IsOn = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\CapabilityAccessManager\\ConsentStore\\appDiagnostics")?.GetValue("Value")?.ToString() == "Deny";
            ToggleSwitch toggleSwitch2 = t3;
            RegistryKey? registryKey5 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System");
            int isOn2;
            if (registryKey5 == null || registryKey5.GetValue("UploadUserActivities")?.Equals(0) != true)
            {
                RegistryKey? registryKey6 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System");
                isOn2 = ((registryKey6 != null && registryKey6.GetValue("PublishUserActivities")?.Equals(0) == true) ? 1 : 0);
            }
            else
            {
                isOn2 = 1;
            }
            toggleSwitch2.IsOn = (byte)isOn2 != 0;
            ToggleSwitch toggleSwitch3 = t4;
            RegistryKey? registryKey7 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WDI\\{9c5a40da-b965-4fc3-8781-88dd50a6299d}");
            int isOn3;
            if (registryKey7 != null && registryKey7.GetValue("ScenarioExecutionEnabled")?.Equals(0) == true)
            {
                RegistryKey? registryKey8 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\DeviceHealthAttestationService");
                isOn3 = ((registryKey8 != null && registryKey8.GetValue("EnableDeviceHealthAttestationService")?.Equals(0) == true) ? 1 : 0);
            }
            else
            {
                isOn3 = 0;
            }
            toggleSwitch3.IsOn = (byte)isOn3 != 0;
            ToggleSwitch toggleSwitch4 = t5;
            RegistryKey? registryKey9 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\InputPersonalization");
            int isOn4;
            if (registryKey9 == null || registryKey9.GetValue("RestrictImplicitTextCollection")?.Equals(0) != true)
            {
                RegistryKey? registryKey10 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\InputPersonalization");
                isOn4 = ((registryKey10 != null && registryKey10.GetValue("RestrictImplicitInkCollection")?.Equals(0) == true) ? 1 : 0);
            }
            else
            {
                isOn4 = 1;
            }
            toggleSwitch4.IsOn = (byte)isOn4 != 0;
            ToggleSwitch toggleSwitch5 = t6;
            RegistryKey? registryKey11 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Speech");
            toggleSwitch5.IsOn = registryKey11 != null && registryKey11.GetValue("AllowSpeechModelUpdate")?.Equals(0) == true;
            if (t1.IsOn)
            {
                t2.IsOn = true;
                t3.IsOn = true;
                t4.IsOn = true;
                t5.IsOn = true;
                t6.IsOn = true;
            }
        }
    }
}
