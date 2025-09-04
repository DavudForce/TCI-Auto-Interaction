using Microsoft.VisualBasic.Devices;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Timer = System.Windows.Forms.Timer;

namespace adsl_Auto_Interaction_App
{
    public partial class Form1 : Form
    {
        Extract extract;
        Settings settingsForm;
        SettingsModule settings;
        Timer checkTimer;
        int checkInterval = 6; // every 6 hours
        bool failFast = false;
        bool webNavigationCompelete = false;
        bool bootUp = true; // Indicates if the application still in "start up" mode

        ScheduleManager scheduleManager; // Manages 12h check schedule

        public Form1()
        {
            /*
            AgressiveNotification notification = new AgressiveNotification(false, 5000, "", "Something went wrong", "and we don't know what...", "But", "We will find it out", "and it won't be pleasant for you", "Consider this as a \"friendly\" warning", "", "Growth often hides in discomfort. A seed breaks apart before it becomes a tree, and so too must we shed outdated beliefs before new understanding takes root. Pain, though unwelcome, can sharpen clarity and teach compassion. Each setback is also a teacher, revealing the depth of our patience, the breadth of our courage, and the unshakable truth that we are capable of more than we once imagined.", "Resilience does not demand that we face everything alone. Human strength flourishes in connection: in the hands that reach out when we stumble, in the voices that remind us of hope when our own grows faint.", "To lean on others is not weakness but wisdom, for resilience is collective as much as individual.\r\n\r\nUltimately, growth is not measured by how fast we rise but by how deeply we root ourselves in the lessons learned along the way. To endure, to adapt, and to emerge renewed—this is the essence of resilience, and the quiet triumph of being fully alive.", "", "Now go ahead", "Use our application");
            notification.Show();
            notification.Focus();
            notification.BringToFront();
            notification.Activate();

            Notification n = new Notification();
            n.Up(NoticficationStyle.Info, "Gugunated gaga drinks now avalible!", 5000);
            */

            InitializeComponent();

            settingsForm = new Settings(this);
            settings = settingsForm.settings;
            checkInterval = settings.InternetStatusCheckInterval;
            if (settingsForm.settingsWasMissing)
                WarningManager.RiseSettingsFileMissingWarning();

            checkTimer = new Timer();

            scheduleManager = new ScheduleManager(); // Initialize schedule manager
        }

        public void FailFast()
        {
            failFast = true;
            this.Close();
        }

        async void InitializeWebView()
        {
            await web.EnsureCoreWebView2Async();
            web.CoreWebView2.Navigate("https://internet.tci.ir/panel");
        }

        void FillLoginForm(string usrName, string pass, string securityCode)
        {
            web.ExecuteScriptAsync($"document.getElementsByName('username')[0].value = '{usrName}';");
            web.ExecuteScriptAsync($"document.getElementsByName('password')[0].value = '{pass}';");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeWebView();
            CheckConnection();
            connectionCheckTimer.Start();

            extract = new Extract(web);
        }

        bool CheckConnection()
        {
            bool connected = new Computer().Network.IsAvailable;

            txtConnectionState.Text = connected ? "yes" : "no";
            return connected;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            CheckConnection();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            FillLoginForm(txtUsername.Text, txtPass.Text, "2");
        }

        private async void btnRetrieveData_Click(object sender, EventArgs e)
        {
            await DoDataCheck();

            if (webNavigationCompelete)
                // Schedule the automatic 12h data check
                ScheduleNextCheck();
            else MessageBox.Show("Please wait for the WebView to finish navigation");
        }

        // Reusable method for scheduled or manual data checks
        private async Task DoDataCheck()
        {
            var usageReport = await extract.UsageReports();
            var activeService = await extract.ActiveInternetService();
            var timedPackage = await extract.TimedPackages();
            var billing = await extract.BillingData();
            var percentages = await extract.GetPercentagesAsync();

            ProccessData(usageReport, activeService, timedPackage, billing, percentages);
            // Update next scheduled check
            //scheduleManager.UpdateNextCheck(TimeSpan.FromHours(checkInterval));
            //Shortned for debugging 
            scheduleManager.UpdateNextCheck(TimeSpan.FromMinutes(checkInterval));

            CheckWarnings(usageReport, activeService, timedPackage, billing, percentages);
        }

        private void CheckWarnings((string, string, string) usageReport, (string, string, string, string, string) activeServiceData, (string, string, string, string, string) timedPackageData, string billingData, (int, int, int, int) percentages)
        {
            int currentBill = 0;
            int activeDaysRemaining = 0;
            int timedDaysRemaining = 0;
            // Convert billing data
            if (billingData.ToLower() != "null")
                currentBill = Extract.Number(billingData);

            // Extract daily upload/download from usage report
            var todayDownloaded = J2A(usageReport.Item2);
            var todayUploaded = J2A(usageReport.Item3);

            int currentDownload = todayDownloaded.Length > 1 ? todayDownloaded[todayDownloaded.Length - 2] : 0;
            int currentUpload = todayUploaded.Length > 1 ? todayUploaded[todayUploaded.Length - 2] : 0;

            // Extract active service days
            if (activeServiceData.Item2.ToLower() != "null")
                activeDaysRemaining = Extract.Number(activeServiceData.Item2);

            // Extract timed service days
            if (timedPackageData.Item2.ToLower() != "null")
                timedDaysRemaining = Extract.Number(timedPackageData.Item2);

            // Extract percentages
            int activeDaysPercentage = percentages.Item1;
            int activeTrafficPercentage = percentages.Item2;
            int timedDaysPercentage = percentages.Item3;
            int timedTrafficPercentage = percentages.Item4;

            // Run all warning checks
            if (currentBill > 0)
                WarningManager.WarnIfNeeded.BillLimitReached(currentBill);
            if (currentDownload > 0)
                WarningManager.WarnIfNeeded.DailyDownloadLimitReached(currentDownload);
            if (currentUpload > 0)
                WarningManager.WarnIfNeeded.DailyUploadLimitReached(currentUpload);
            if(activeDaysRemaining > 0)
                WarningManager.WarnIfNeeded.ActiveDaysRemaining(activeDaysRemaining);
            if(timedDaysRemaining > 0)
                WarningManager.WarnIfNeeded.TimedDaysRemaining(timedDaysRemaining);
            WarningManager.WarnIfNeeded.Percentages(activeDaysPercentage, activeTrafficPercentage, "Active");
            WarningManager.WarnIfNeeded.Percentages(timedDaysPercentage, timedTrafficPercentage, "Timed");
        }

        void ProccessData((string, string, string) usageReport, (string, string, string, string, string) activeServiceData,
            (string, string, string, string, string) timedPackageData, string billingData, (int, int, int, int) percentages)
        {


            var todayDownloaded = J2A(usageReport.Item2);
            var todayUploaded = J2A(usageReport.Item3);
            if(todayDownloaded.Length > 2)
                txtDownloaded.Text = todayDownloaded[todayDownloaded.Length - 2].ToString(); // The TCI does not include "today"'s data, but instead it's last value is 0. so I actually getting "yesterday" 's value :)
            if(todayUploaded.Length > 2)
                txtUploaded.Text = todayUploaded[todayUploaded.Length - 2].ToString(); // ^^^ Same as above comment ^^^

            txtActiveServiceName.Text = activeServiceData.Item1;
            txtActiveServiceDaysLeft.Text = $"{Extract.Number(activeServiceData.Item2)}/{Extract.Number(activeServiceData.Item3)}";
            txtActiveServiceTraficLeft.Text = $"{Extract.Number(activeServiceData.Item4)}/{Extract.Number(activeServiceData.Item5)} MB";

            txtTimedServiceName.Text = timedPackageData.Item1;
            txtTimedServiceDaysLeft.Text = $"{Extract.Number(timedPackageData.Item2)}/{Extract.Number(timedPackageData.Item3)}";
            txtTimedServiceTrafficLeft.Text = $"{Extract.Number(timedPackageData.Item4)}/{Extract.Number(timedPackageData.Item5)} MB";

            txtBilling.Text = Extract.Number(billingData).ToString();

            lblActiveDaysPercentage.Text = percentages.Item1.ToString() + "%";
            lblActiveTrafficPercentage.Text = percentages.Item2.ToString() + "%";
            lblTimedDaysPercentage.Text = percentages.Item3.ToString() + "%";
            lblTimedTrafficPercentage.Text = percentages.Item4.ToString() + "%";
        }

        /// <summary>
        /// Converts JSON integer array into C# int arrays
        /// </summary>
        /// <param name="json">JSON int array</param>
        /// <returns>The representing C# int array</returns>
        int[] J2A(string json)
        {
            return JsonSerializer.Deserialize<int[]>(json);
        }

        private void btnOpenSettings_Click(object sender, EventArgs e)
        {
            settingsForm.ShowDialog();
            settings = settingsForm.settings;
        }

        // Scheduling logic using Timer
        private void ScheduleNextCheck()
        {
            // Stop previous tick event to avoid stacking
            checkTimer.Stop();
            checkTimer.Tick -= CheckTimer_Tick;

            TimeSpan delay = scheduleManager.GetRemainingTime();
            if (delay <= TimeSpan.Zero)
            {
                // Immediate run if overdue
                _ = DoDataCheck();
                delay = TimeSpan.FromHours(checkInterval);
            }

            checkTimer.Interval = (int)delay.TotalMilliseconds;
            checkTimer.Tick += CheckTimer_Tick;
            checkTimer.Start();
        }

        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            checkTimer.Stop();
            _ = DoDataCheck(); // _ means "I don't care about results!" and is a variable that you cannot store anything inside
            ScheduleNextCheck(); // Schedule the next one recursively
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show(e.CloseReason.ToString());
        }
        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            RestoreFromTray();
        }

        private void HideToTray()
        {
            if (settings.MinimizeToSystemTray)
            {
                trayIcon.Visible = true;
                this.Hide(); // hide the window
            }
        }

        private void RestoreFromTray()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            trayIcon.Visible = false;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                if (settings.MinimizeToSystemTray)
                {
                    Notification n = new Notification();
                    HideToTray();
                    n.UpMost(NoticficationStyle.Info, "Application minimized to system tray. You can re-open it via duble clicking on the icon", 6000);
                }
            }
        }

        private void web_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            webNavigationCompelete = true;
        }

        private void web_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            webNavigationCompelete = false;
        }
    }
}
