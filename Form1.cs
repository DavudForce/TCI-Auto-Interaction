using Microsoft.VisualBasic.Devices;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace adsl_Auto_Interaction_App
{
    public partial class Form1 : Form
    {
        Extract extract;
        Settings settingsForm;
        SettingsModule settings;
        bool failFast = false;

        public Form1()
        {
            InitializeComponent();

            settingsForm = new Settings(this);
        }

        public void FailFast()
        {
            failFast = true; // Later if I create a closing event, I can check if this flag is true, then immediately close the app
            this.Close();
        }

        async void InitializeWebView()
        {
            // Ensure CoreWebView2 is initialized
            await web.EnsureCoreWebView2Async();

            // Navigate to the TCI login page
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
            bool connected = false;

            connected = new Computer().Network.IsAvailable;

            if (connected)
            {
                txtConnectionState.Text = "yes";
                return true;
            }

            txtConnectionState.Text = "no";
            return false;
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
            var usageReport = await extract.UsageReports();
            var activeService = await extract.ActiveInternetService();
            var timedPackage = await extract.TimedPackages();
            var billing = await extract.BillingData();

            ProccessData(usageReport, activeService, timedPackage, billing);
        }




        void ProccessData((string, string, string) usageReport, (string, string, string, string, string) activeServiceData, (string, string, string) timedPackageData, string billingData)
        {
            var todayDownloaded = J2A(usageReport.Item2);
            var todayUploaded = J2A(usageReport.Item3);
            txtDownloaded.Text = todayDownloaded[todayDownloaded.Length - 2].ToString();
            txtUploaded.Text = todayUploaded[todayUploaded.Length - 2].ToString();

            txtActiveServiceName.Text = activeServiceData.Item1;
            txtActiveServiceDaysLeft.Text = Extract.Number(activeServiceData.Item2).ToString() + "/" + Extract.Number(activeServiceData.Item3).ToString();
            txtActiveServiceTraficLeft.Text = activeServiceData.Item4;

            txtTimedServiceName.Text = timedPackageData.Item1;
            txtTimedServiceDaysLeft.Text = Extract.Number(timedPackageData.Item2).ToString();
            txtTimedServiceTrafficLeft.Text = Extract.Number(timedPackageData.Item3).ToString();

            txtBilling.Text = Extract.Number(billingData).ToString();
        }

        /// <summary>
        /// Converts JSON string array into C# string array
        /// </summary>
        /// <param name="json">JSON array to be converted</param>
        /// <returns></returns>
        int[] J2A(string json)
        {
            return JsonSerializer.Deserialize<int[]>(json);
        }

        private void btnOpenSettings_Click(object sender, EventArgs e)
        {
            settingsForm.ShowDialog();
            settings = settingsForm.settings;
        }
    }
}
