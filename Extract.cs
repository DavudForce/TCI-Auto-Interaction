using Microsoft.Web.WebView2.WinForms;
using System.Text.RegularExpressions;

namespace adsl_Auto_Interaction_App
{
    /// <summary>
    /// Extracts something from something
    /// </summary>
    internal class Extract
    {
        private WebView2 _web;
        public Extract(WebView2 webView)
        {
            _web = webView;
        }

        public static int Number(string input)
        {
            if (input.ToLower() == "null") return 0;
            if (input.ToLower() == "[]") return 0;
            if (string.IsNullOrWhiteSpace(input.ToLower())) return 0;
            var arabicDigits = new[] { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };
            for (int i = 0; i < arabicDigits.Length; i++)
                input = input.Replace(arabicDigits[i], (char)('0' + i));

            var match = Regex.Match(input, @"\d+");
            if (match.Success)
                return int.Parse(match.Value);

            throw new Exception("No number found in string");
        }

        public async Task<(int activeDaysPercent, int activeTrafficPercent, int timedDaysPercent, int timedTrafficPercent)> GetPercentagesAsync()
        {
            int activeDaysPercent = 0;
            int activeTrafficPercent = 0;
            int timedDaysPercent = 0;
            int timedTrafficPercent = 0;

            try
            {
                string activeDays = await _web.CoreWebView2.ExecuteScriptAsync(
                "document.querySelectorAll('.pie-volume-1')[0].getAttribute('data-percent');");
                if (activeDays.ToLower() != "null")
                    activeDaysPercent = int.Parse(activeDays.Trim('"'));

                string activeTraffic = await _web.CoreWebView2.ExecuteScriptAsync(
                    "document.querySelectorAll('.pie-volume-1')[1].getAttribute('data-percent');");
                if (activeTraffic.ToLower() != "null")
                    activeTrafficPercent = int.Parse(activeTraffic.Trim('"'));

                string timedDays = await _web.CoreWebView2.ExecuteScriptAsync(
                    "document.querySelectorAll('.pie-volume-2')[0].getAttribute('data-percent');");
                if (timedDays.ToLower() != "null")
                    timedDaysPercent = int.Parse(timedDays.Trim('"'));

                string timedTraffic = await _web.CoreWebView2.ExecuteScriptAsync(
                    "document.querySelectorAll('.pie-volume-2')[1].getAttribute('data-percent');");
                if (timedTraffic.ToLower() != "null")
                    timedTrafficPercent = int.Parse(timedTraffic.Trim('"'));

                return (activeDaysPercent, activeTrafficPercent, timedDaysPercent, timedTrafficPercent);
            }
            catch 
            {
                Notification n = new Notification();
                n.Up(NoticficationStyle.Warning, "Please wait for the page to load", 6000, true);
                return (-1 , -1, -1, -1);
            }
        }

        public async Task<string> BillingData()
        {
            try
            {
                string billing = await _web.CoreWebView2.ExecuteScriptAsync(
                "document.querySelector('.uk-alert-danger p').innerText;");
                if (billing.ToLower() == "null")
                    return "null";

                billing = billing.Trim('"');
                return billing;
            }
            catch 
            {
                Notification n = new Notification();
                n.Up(NoticficationStyle.Warning, "Please wait for the page to load", 6000, true);
                return "null";
            }
        }

        public async Task<(string serviceName, string daysLeft, string totalDays, string trafficLeft, string trafficTotal)> ActiveInternetService()
        {
            try
            {
                string serviceName = await _web.CoreWebView2.ExecuteScriptAsync(
                "document.querySelector('.uk-card-primary h5').innerText;");
                serviceName = serviceName.ToLower() != "null" ? serviceName.Trim('"') : string.Empty;

                string daysLeft = await _web.CoreWebView2.ExecuteScriptAsync(
                    "document.querySelectorAll('.pie-volume-1')[0].querySelectorAll('.percent span')[0].innerText;");
                daysLeft = daysLeft.ToLower() != "null" ? daysLeft.Trim('"') : string.Empty;

                string totalDays = await _web.CoreWebView2.ExecuteScriptAsync(
                    "document.querySelectorAll('.pie-volume-1')[0].querySelectorAll('.percent span')[1].innerText;");
                totalDays = totalDays.ToLower() != "null" ? totalDays.Trim('"') : string.Empty;

                string trafficLeft = await _web.CoreWebView2.ExecuteScriptAsync(
                    "document.querySelectorAll('.pie-volume-1')[1].querySelectorAll('.percent span')[0].innerText;");
                trafficLeft = trafficLeft.ToLower() != "null" ? trafficLeft.Trim('"') : string.Empty;

                string trafficTotal = await _web.CoreWebView2.ExecuteScriptAsync(
                    "document.querySelectorAll('.pie-volume-1')[1].querySelectorAll('.percent span')[1].innerText;");
                trafficTotal = trafficTotal.ToLower() != "null" ? trafficTotal.Trim('"') : string.Empty;

                return (serviceName, daysLeft, totalDays, trafficLeft, trafficTotal);
            }
            catch 
            {
                Notification n = new Notification();
                n.Up(NoticficationStyle.Warning, "Please wait for the page to load", 6000, true);
                return ("null", "null", "null", "null", "null");
            }

        }

        public async Task<(string timedName, string timedDaysLeft, string timedTotalDays, string timedTrafficLeft, string timedTrafficTotal)> TimedPackages()
        {
            try
            {
                string timedName = await _web.CoreWebView2.ExecuteScriptAsync(
                "document.querySelector('.uk-card-primary + div h5').innerText;");
                timedName = timedName.ToLower() != "null" ? timedName.Trim('"') : string.Empty;

                string timedDaysLeft = await _web.CoreWebView2.ExecuteScriptAsync(
                    "document.querySelectorAll('.pie-volume-2')[0].querySelectorAll('.percent span')[0].innerText;");
                timedDaysLeft = timedDaysLeft.ToLower() != "null" ? timedDaysLeft.Trim('"') : string.Empty;

                string timedTotalDays = await _web.CoreWebView2.ExecuteScriptAsync(
                    "document.querySelectorAll('.pie-volume-2')[0].querySelectorAll('.percent span')[1].innerText;");
                timedTotalDays = timedTotalDays.ToLower() != "null" ? timedTotalDays.Trim('"') : string.Empty;

                string timedTrafficLeft = await _web.CoreWebView2.ExecuteScriptAsync(
                    "document.querySelectorAll('.pie-volume-2')[1].querySelectorAll('.percent span')[0].innerText;");
                timedTrafficLeft = timedTrafficLeft.ToLower() != "null" ? timedTrafficLeft.Trim('"') : string.Empty;

                string timedTrafficTotal = await _web.CoreWebView2.ExecuteScriptAsync(
                    "document.querySelectorAll('.pie-volume-2')[1].querySelectorAll('.percent span')[1].innerText;");
                timedTrafficTotal = timedTrafficTotal.ToLower() != "null" ? timedTrafficTotal.Trim('"') : string.Empty;

                return (timedName, timedDaysLeft, timedTotalDays, timedTrafficLeft, timedTrafficTotal);
            }
            catch 
            {
                Notification n = new Notification();
                n.Up(NoticficationStyle.Warning, "Please wait for the page to load", 6000, true);
                return ("null", "null", "null", "null", "null");
            }
        }

        public async Task<(string, string, string)> UsageReports()
        {
            try
            {
                string labels = await _web.CoreWebView2.ExecuteScriptAsync(
                "Chart.instances[0].data.labels;");
                labels = labels.ToLower() != "null" ? labels : "[]";

                string downloadData = await _web.CoreWebView2.ExecuteScriptAsync(
                    "Chart.instances[0].data.datasets[0].data;");
                downloadData = downloadData.ToLower() != "null" ? downloadData : "[]";

                string uploadData = await _web.CoreWebView2.ExecuteScriptAsync(
                    "Chart.instances[0].data.datasets[1].data;");
                uploadData = uploadData.ToLower() != "null" ? uploadData : "[]";

                return (labels, downloadData, uploadData);
            }
            catch 
            {
                Notification n = new Notification();
                n.Up(NoticficationStyle.Warning, "Please wait for the page to load", 6000, true);
                return ("null", "null", "null");
            }
        }
    }
}
