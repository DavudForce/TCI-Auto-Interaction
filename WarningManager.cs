using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adsl_Auto_Interaction_App
{
    public enum WarningReason
    {
        BillLimitReached,
        DownloadLimitReached,
        UploadLimitReached,
        ActiveDaysLimitReached,
        TimedDaysLimitReached,
        PercentagesDontMatch,
        PercentagesDontMatchWithTolerance
    };

    /// <summary>
    /// Provides various warning services!
    /// </summary>
    public class WarningManager
    {
        public static void RiseWarning(WarningReason reason, string text)
        {
            switch (reason)
            {
                case WarningReason.BillLimitReached:
                    break;
                case WarningReason.DownloadLimitReached:
                    break;
                case WarningReason.UploadLimitReached:
                    break;
                case WarningReason.ActiveDaysLimitReached:
                    break;
                case WarningReason.TimedDaysLimitReached:
                    break;
                case WarningReason.PercentagesDontMatch:
                    break;
                case WarningReason.PercentagesDontMatchWithTolerance:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Has handy methods that checks and only if neccecary, warns the user.
        /// </summary>
        public static class WarnIfNeeded
        {
            public static void BillLimitReached(int currentBill)
            {
                var billLimit = Settings.staticSettings.BillLimit;

                if(currentBill >= Convert.ToInt32(billLimit))
                {
                    WarningManager.RiseWarning(WarningReason.BillLimitReached, "You've reached your internet bill limit!");
                }
            }
        }
    }
}
