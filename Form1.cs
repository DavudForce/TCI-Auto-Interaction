using Microsoft.VisualBasic.Devices;
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
        int checkInterval = 12;
        bool failFast = false;
        bool bootUp = true; // Indicates if the application still in "start up" mode

        ScheduleManager scheduleManager; // Manages 12h check schedule

        public Form1()
        {
            AgressiveNotification notification = new AgressiveNotification(5000, "Something went wrong", "and we don't know what...", "But", "We WILL find it", "and it won't be pleasant for", "You");
            notification.Show();

            InitializeComponent();

            settingsForm = new Settings(this);
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

            // Schedule the automatic 12h data check
            ScheduleNextCheck();
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
        }

        // Reusable method for scheduled or manual data checks
        private async System.Threading.Tasks.Task DoDataCheck()
        {
            var usageReport = await extract.UsageReports();
            var activeService = await extract.ActiveInternetService();
            var timedPackage = await extract.TimedPackages();
            var billing = await extract.BillingData();

            WarningManager.WarnIfNeeded.BillLimitReached(Extract.Number(billing));

            var percentages = await extract.GetPercentagesAsync();

            ProccessData(usageReport, activeService, timedPackage, billing, percentages);

            // Update next scheduled check
            scheduleManager.UpdateNextCheck(TimeSpan.FromHours(checkInterval));
        }

        void ProccessData((string, string, string) usageReport, (string, string, string, string, string) activeServiceData,
            (string, string, string, string, string) timedPackageData, string billingData, (int, int, int, int) percentages)
        {
            var todayDownloaded = J2A(usageReport.Item2);
            var todayUploaded = J2A(usageReport.Item3);
            txtDownloaded.Text = todayDownloaded[todayDownloaded.Length - 2].ToString(); // The TCI does not include "today"'s data, but instead it's last value is 0. so I actually getting "yesterday" 's value :)
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
            _ = DoDataCheck();
            ScheduleNextCheck(); // Schedule the next one recursively
        }
    }
}
