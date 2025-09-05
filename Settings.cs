using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace adsl_Auto_Interaction_App
{
    public enum WarningTier
    {
        Politely,
        Normally,
        Aggressively,
        Godmode
    };
    public partial class Settings : Form
    {
        public static readonly string settingPath = Path.Combine(Application.StartupPath + @"data/settings.dat");
        public static readonly string schedulePath = Path.Combine(Application.StartupPath + @"data/schedule.dat");
        FileManager fileManager;
        Form1 _parentForm;

        public bool settingsWasMissing = false;

        public SettingsModule settings;
        public static SettingsModule staticSettings;
        public static WarningTier warningTier;

        public Settings(Form1 parentForm)
        {
            _parentForm = parentForm;

            InitializeComponent();

            fileManager = new FileManager();
            CheckFileManager();

            var requestResult = fileManager.GetSettings();

            if (requestResult.Item2.Success == false)
            {
                MessageBox.Show(requestResult.Item2.Details + $"\n\n ({requestResult.Item2.Tag}:{requestResult.Item2.Tag.SubTag})");
            }

            else
            {
                settings = requestResult.Item1;
                staticSettings = requestResult.Item1;

                SetWarnTier(requestResult.Item1.WarnTierSelectedIndex);
                FillData();
            }
        }

        void SetWarnTier(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    warningTier = WarningTier.Politely;
                    break;
                case 1:
                    warningTier = WarningTier.Normally;
                    break;
                case 2:
                    warningTier = WarningTier.Aggressively;
                    break;
                case 3:
                    warningTier = WarningTier.Godmode;
                    break;
                default:
                    break;
            }
        }

        void FillData()
        {
            numDownloadLimit.Value = settings.DailyDownloadLimit;
            numUploadLimit.Value = settings.DailyUploadLimit;
            numActiveDaysLeft.Value = settings.DaysLeftFromActive;
            numTimedDaysLeft.Value = settings.DaysLeftFromTimed;
            numBillingLimit.Value = settings.BillLimit;
            numTolerance.Value = settings.Tolearnce;
            cmbWarnStyle.SelectedIndex = settings.WarnTierSelectedIndex;
            numInternetStatusCheckInterval.Value = settings.InternetStatusCheckInterval;
            chckSystemTray.Checked = settings.MinimizeToSystemTray;
        }

        void CheckFileManager()
        {
            if (fileManager.initalizationStatus?.Success == null)
            {
                if (fileManager.initalizationStatus?.Tag.SubTag == "settings_missing")
                {
                    settingsWasMissing = true;
                    var res = MessageBox.Show(fileManager.initalizationStatus.Details + "\n\nDo you want to create a new empty settings file?", "Reset settings?", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        var resRes = FileManager.RestoreSettings();
                        if (resRes.Success != null && resRes.Success == false)
                        {
                            MessageBox.Show(resRes.Details + $"\n\nif you contact to app developers, give them this code: {resRes.Tag}.{resRes.Tag.SubTag}", "Restore failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            _parentForm.FailFast();
                        }
                    }

                    else _parentForm.FailFast();
                }
            }
        }

        int ToInt(decimal value)
        {
            return Convert.ToInt32(value);
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            settings = new SettingsModule()
            {
                BillLimit = numBillingLimit.Value,
                DailyDownloadLimit = ToInt(numDownloadLimit.Value),
                DailyUploadLimit = ToInt(numUploadLimit.Value),
                DaysLeftFromActive = ToInt(numActiveDaysLeft.Value),
                DaysLeftFromTimed = ToInt(numTimedDaysLeft.Value),
                Tolearnce = numTolerance.Value,
                WarnTierSelectedIndex = cmbWarnStyle.SelectedIndex,
                InternetStatusCheckInterval = ToInt(numInternetStatusCheckInterval.Value),
                MinimizeToSystemTray = chckSystemTray.Checked
            };

            SetWarnTier(cmbWarnStyle.SelectedIndex);

            staticSettings = settings;
            fileManager.SetSettings(settings);
        }
    }
}
