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
        int checkInterval = 6; // every n hours
        bool failFast = false;
        bool webNavigationCompelete = false;
        bool logedIn = false;
        bool bootUp = true; // Indicates if the application still in "start up" mode
        string recievedUri;
        ScheduleManager scheduleManager; // Manages 12h check schedule

        public Form1()
        {
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
            recievedUri = await ValidateConnection.RecieveUriAsync();

            if (recievedUri.StartsWith('!'))
            {
                if(recievedUri.ToLower().Remove(0,1) == "timeout")
                {
                    var res = MessageBox.Show("Server timedout. Please check your connection quality and press \"Retry\" button to retry or press \"Cancel\" button to cancel the action and close the app", "Timeout reached", MessageBoxButtons.RetryCancel);
                    if (res == DialogResult.Retry)
                        InitializeWebView();
                    else
                        this.Close();
                }
                MessageBox.Show(recievedUri.Remove(0, 1), "Server is Down");
                this.Close();
            }

            await web.EnsureCoreWebView2Async();
            web.CoreWebView2.Navigate(recievedUri);
        }

        void FillLoginForm(string usrName, string pass, string securityCode)
        {
            try
            {
                web.ExecuteScriptAsync($"document.getElementsByName('username')[0].value = '{usrName}';");
                web.ExecuteScriptAsync($"document.getElementsByName('password')[0].value = '{pass}';");
            }
            catch
            {
                Notification n = new Notification();
                n.Up(NotificationStyle.Error, "Autofill cannot be executed.", true, true);
            }
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
            if (!logedIn)
            {
                try
                {
                    // Remove the extra quotes from JS result
                    var username = await web.ExecuteScriptAsync("document.getElementsByName('username')[0].value;");
                    username = username.Trim('"');

                    var pass = await web.ExecuteScriptAsync("document.getElementsByName('password')[0].value;");
                    pass = pass.Trim('"');

                    if (string.IsNullOrWhiteSpace(username) || username == "null" || string.IsNullOrWhiteSpace(pass) || pass == "null")
                    {
                        Notification notify = new Notification();
                        notify.Up(NotificationStyle.Warning, "Please login first to use the application", 7000, true);
                        return;
                    }
                    // Simulate virtual click on hidden login button
                    await web.CoreWebView2.ExecuteScriptAsync(
                        "document.querySelector('button[name=\"LoginFromWeb\"]').click();");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Could not press login button: " + exc.Message);
                }

                btnRetrieveData.Visible = true;
                btnRetrieveData.Text = "Retrieve Data";

                Notification n = new Notification();
                n.Up(NotificationStyle.Info, "Application started and it will take a moment to initialize. You will be notified when app is initialized. The window must remain visible during initialization (but it can be minimized)", true, true);
                n.SetSize(new Size(n.Width, n.Height + 40));
                
                await Task.Delay(30000); // Wait for the site to load

                logedIn = true;

                if (!n.IsDisposed)
                {
                    n.Down();
                    n.Dispose();
                }
                Notification n2 = new Notification();
                n2.Up(NotificationStyle.Info, "Application started!", true, true);
                n2.SetSize();
            }

            await DoDataCheck();

            if (webNavigationCompelete)
                // Schedule the automatic 12h data check
                ScheduleNextCheck();

            //litContainer1.SplitterDistance = 0;
            
            splitContainer1.Panel2Collapsed = false; // Don't ask me why\\\\--|])}
            splitContainer1.Panel1Collapsed = true; // Don't ask me why ////--|])}
            
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
            scheduleManager.UpdateNextCheck(TimeSpan.FromHours(checkInterval)); // This was FromMinutes()

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
            if (activeDaysRemaining > 0)
                WarningManager.WarnIfNeeded.ActiveDaysRemaining(activeDaysRemaining);
            if (timedDaysRemaining > 0)
                WarningManager.WarnIfNeeded.TimedDaysRemaining(timedDaysRemaining);
            WarningManager.WarnIfNeeded.Percentages(activeDaysPercentage, activeTrafficPercentage, "Active");
            WarningManager.WarnIfNeeded.Percentages(timedDaysPercentage, timedTrafficPercentage, "Timed");
        }

        void ProccessData((string, string, string) usageReport, (string, string, string, string, string) activeServiceData,
            (string, string, string, string, string) timedPackageData, string billingData, (int, int, int, int) percentages)
        {


            var todayDownloaded = J2A(usageReport.Item2);
            var todayUploaded = J2A(usageReport.Item3);
            if (todayDownloaded.Length > 2)
                txtDownloaded.Text = todayDownloaded[todayDownloaded.Length - 2].ToString(); // The TCI does not include "today"'s data, but instead it's last value is 0. so I actually getting "yesterday" 's value :)
            if (todayUploaded.Length > 2)
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
                if (settings.MinimizeToSystemTray && logedIn)
                {
                    Notification n = new Notification();
                    HideToTray();
                    n.Up(NotificationStyle.Info, "Form is hided in system tray. You can alway duble-click on the icon to open it. You will still recieve notifications", true, true);
                }
            }
        }

        private async void web_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            webNavigationCompelete = true;
            if (!e.IsSuccess) return;

            try
            {
                // Inject CSS/HTML to hide the login button visually and show gray text
                string js = @"
                (function() {
                    let btn = document.querySelector('button[name=""LoginFromWeb""]');
                    if (btn) {
                        // Hide button but keep functional
                        btn.style.visibility = 'hidden';
                        btn.style.position = 'absolute';
                        btn.style.left = '-9999px';

                        if (!document.getElementById('customLoginMsg')) {
                            let msg = document.createElement('div');
                            msg.id = 'customLoginMsg';
                            msg.textContent = 'Please press OK button in the bottom of this application';
                            msg.style.color = 'gray';
                            msg.style.textAlign = 'center';
                            msg.style.padding = '10px';
                            msg.style.fontSize = '14px';
                            btn.parentNode.insertBefore(msg, btn);
                        }
                    }
                })();";
                await web.CoreWebView2.ExecuteScriptAsync(js);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Injection failed: " + ex.Message);
            }
        }

        private void web_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            webNavigationCompelete = false;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
           // splitContainer1.SplitterDistance = this.Width - 20;
            splitContainer1.Panel2Collapsed = true;
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }
    }
}
