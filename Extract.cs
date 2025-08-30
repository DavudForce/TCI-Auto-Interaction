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

        /// <summary>
        /// Extracts numbers as <see langword="int"/> from Arabic text. Throws exception if providen text does not contains Arabic text
        /// </summary>
        /// <param name="input">An <see langword="string"/> that contains Arabic digits</param>
        /// <returns>Returns extracted number as int32</returns>
        /// <exception cref="Exception"></exception>
        public static int Number(string input)
        {
            // 1. Replace Arabic digits with Latin digits
            var persianDigits = new[] { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };
            for (int i = 0; i < persianDigits.Length; i++)
                input = input.Replace(persianDigits[i], (char)('0' + i));

            // 2. Find the first number in the string
            var match = Regex.Match(input, @"\d+"); // "\d+" means "keep matching as long as digits continue"
            if (match.Success)
                return int.Parse(match.Value);

            throw new Exception("No number found in string");
        }

        public async Task<string> BillingData()
        {
            // Extract billing information from the dashboard
            string billing = await _web.CoreWebView2.ExecuteScriptAsync
                ("document.querySelector('.uk-alert-danger p').innerText;");

            // The result is returned as a JSON string with quotes.
            // Trim quotes to clean the output:
            billing = billing.Trim('"');
            return billing;
        }

        public async Task<(string serviceName, string daysLeft, string totalDays, string trafficLeft, string trafficTotal)> ActiveInternetService()
        {
            // --- Extract active service name ---
            string serviceName = await _web.CoreWebView2.ExecuteScriptAsync(
                "document.querySelector('.uk-card-primary h5').innerText;");
            serviceName = serviceName.Trim('"');

            // --- Days info (first .pie-volume-1 block) ---
            string daysLeft = await _web.CoreWebView2.ExecuteScriptAsync(
                "document.querySelectorAll('.pie-volume-1')[0].querySelectorAll('.percent span')[0].innerText;");
            daysLeft = daysLeft.Trim('"');

            string totalDays = await _web.CoreWebView2.ExecuteScriptAsync(
                "document.querySelectorAll('.pie-volume-1')[0].querySelectorAll('.percent span')[1].innerText;");
            totalDays = totalDays.Trim('"');

            // --- Traffic info (second .pie-volume-1 block) ---
            string trafficLeft = await _web.CoreWebView2.ExecuteScriptAsync(
                "document.querySelectorAll('.pie-volume-1')[1].querySelectorAll('.percent span')[0].innerText;");
            trafficLeft = trafficLeft.Trim('"');

            string trafficTotal = await _web.CoreWebView2.ExecuteScriptAsync(
                "document.querySelectorAll('.pie-volume-1')[1].querySelectorAll('.percent span')[1].innerText;");
            trafficTotal = trafficTotal.Trim('"');

            return (serviceName, daysLeft, totalDays, trafficLeft, trafficTotal);
        }

        public async Task<(string, string, string)> TimedPackages()
        {
            // Extract timed package name
            string timedName = await _web.CoreWebView2.ExecuteScriptAsync(
            "document.querySelector('.uk-card-sucsess h5').innerText;");
            timedName = timedName.Trim('"');

            // Extract days left and traffic left for timed package
            string timedDays = await _web.CoreWebView2.ExecuteScriptAsync(
            "document.querySelectorAll('.pie-volume-2.percentage .percent span')[0].innerText;");
            timedDays = timedDays.Trim('"');

            string timedTraffic = await _web.CoreWebView2.ExecuteScriptAsync(
            "document.querySelectorAll('.pie-volume-2.percentage .percent span')[1].innerText;");
            timedTraffic = timedTraffic.Trim('"');

            return (timedName, timedDays, timedTraffic);
        }

        public async Task<(string, string, string)> UsageReports()
        {
            // Extract usage report from Chart.js
            string labels = await _web.CoreWebView2.ExecuteScriptAsync(
            "Chart.instances[0].data.labels;");

            string downloadData = await _web.CoreWebView2.ExecuteScriptAsync(
            "Chart.instances[0].data.datasets[0].data;");

            string uploadData = await _web.CoreWebView2.ExecuteScriptAsync(
            "Chart.instances[0].data.datasets[1].data;");
            // Note: These return JSON arrays (e.g., ["1404/06/01","1404/06/02"]).
            // You can deserialize them into C# arrays using System.Text.Json.

            return (labels, downloadData, uploadData);
        }
    }
}
