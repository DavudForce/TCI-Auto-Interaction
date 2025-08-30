using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adsl_Auto_Interaction_App
{
    public class SettingsModule
    {
        public int DailyDownloadLimit { get; set; }
        public int DailyUploadLimit { get; set; }
        public int DaysLeftFromActive { get; set; }
        public int DaysLeftFromTimed { get; set; }
        public decimal BillLimit { get; set; }
    }
}
