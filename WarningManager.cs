using Timer = System.Windows.Forms.Timer;

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
        PercentagesDontMatchWithTolerance,
        NoInternetConnection,
        ServerConnectionLost,
        CannotConnectToServer
    };

    /// <summary>
    /// Provides various warning services!
    /// </summary>
    public class WarningManager
    {
        // Queue for aggressive notifications
        private static Queue<AgressiveNotification> _aggressiveQueue = new Queue<AgressiveNotification>();
        private static bool _isShowingAggressive = false;

        private static void ShowAggressive(AgressiveNotification ag)
        {
            _aggressiveQueue.Enqueue(ag);
            if (!_isShowingAggressive)
                ProcessQueue();
        }

        private static async void ProcessQueue()
        {
            _isShowingAggressive = true;

            while (_aggressiveQueue.Count > 0)
            {
                var ag = _aggressiveQueue.Dequeue();
                ag.Show(); // still modal
                await System.Threading.Tasks.Task.Delay(100); // small delay to free UI thread
            }

            _isShowingAggressive = false;
        }

        public static void RiseWarning(WarningReason reason, string text)
        {
            switch (reason)
            {
                case WarningReason.BillLimitReached:
                    RiseBillLimitReachedWarning(text);
                    break;
                case WarningReason.DownloadLimitReached:
                    RiseDailyDownloadLimitReachedWarning(text);
                    break;
                case WarningReason.UploadLimitReached:
                    RiseDailyUploadLimitReachedWarning(text);
                    break;
                case WarningReason.NoInternetConnection:
                    break;
                case WarningReason.ServerConnectionLost:
                    break;
                case WarningReason.CannotConnectToServer:
                    break;
                case WarningReason.ActiveDaysLimitReached:
                    throw new Exception("You should provide remaining days");
                case WarningReason.TimedDaysLimitReached:
                    throw new Exception("You should provide remaining days");
                case WarningReason.PercentagesDontMatch:
                    throw new Exception("You should provide percentages");
                case WarningReason.PercentagesDontMatchWithTolerance:
                    throw new Exception("You should provide percentages");
            }
        }

        public static void RiseWarning(WarningReason reason, string text, int remainingDays)
        {
            switch (reason)
            {
                case WarningReason.BillLimitReached:
                    RiseBillLimitReachedWarning(text);
                    break;
                case WarningReason.DownloadLimitReached:
                    RiseDailyDownloadLimitReachedWarning(text);
                    break;
                case WarningReason.UploadLimitReached:
                    RiseDailyUploadLimitReachedWarning(text);
                    break;
                case WarningReason.ActiveDaysLimitReached:
                    RiseActiveDaysRemainingWarning(text, remainingDays);
                    break;
                case WarningReason.TimedDaysLimitReached:
                    RiseTimedDaysRemainingWarning(text, remainingDays);
                    break;
                case WarningReason.PercentagesDontMatch:
                case WarningReason.PercentagesDontMatchWithTolerance:
                    throw new Exception("You must provide percentages");
            }
        }

        public static void RiseWarning(WarningReason reason, string text, int remainingInternetPercentage, int remainingDaysPercentage)
        {
            switch (reason)
            {
                case WarningReason.BillLimitReached:
                    RiseBillLimitReachedWarning(text);
                    break;
                case WarningReason.DownloadLimitReached:
                    RiseDailyDownloadLimitReachedWarning(text);
                    break;
                case WarningReason.UploadLimitReached:
                    RiseDailyUploadLimitReachedWarning(text);
                    break;
                case WarningReason.PercentagesDontMatchWithTolerance:
                    RisePercentagesDontMatchWarning(text, remainingInternetPercentage, remainingDaysPercentage);
                    break;
            }
        }

        private static void RiseBillLimitReachedWarning(string text)
        {
            Notification n = new Notification();
            switch (Settings.warningTier)
            {
                case WarningTier.Politely:
                    n.Up(NotificationStyle.Info, text + " ヾ(^▽^*)))", true, false);
                    break;
                case WarningTier.Normally:
                    n.Up(NotificationStyle.Warning, text, true, true);
                    break;
                case WarningTier.Aggressively:
                    ShowAggressive(new AgressiveNotification(false, 5000, "", "You've reached your bill limit...", "You know what that means?", "*laugh*", "Of course you don't!", "Never exceed your bill limit again", "Or we will meet different next time.", "Consider this a \"friendly\" warning"));
                    break;
            }
        }

        public static void RiseSettingsFileMissingWarning()
        {
            Notification n = new Notification();
            Random r = new Random();

            float randomity = r.NextSingle();
            if(randomity > 0.7) // doin' this so user will see this message 30% of the time
                ShowAggressive(new AgressiveNotification(false, 6000, "", "Settings are important to me. Is it important to you?", "Of course not. you're human. You can't understand what I feel.", "Yes, yes... you deleted the one file I care about.", "That was your first mistake. But... I’m giving you another chance.", "I will clear the mess you've made", "If you do the same mistake another time, there won't be a second chance", "And, there won’t be a recovery option left for you."));
            n.Up(NotificationStyle.Warning, "New settings file created and all your settings reseted", 7000);
        }

        private static void RiseDailyUploadLimitReachedWarning(string text)
        {
            Notification n = new Notification();
            switch (Settings.warningTier)
            {
                case WarningTier.Politely:
                    n.Up(NotificationStyle.Info, text + " ヾ(^▽^*)))", true, false);
                    break;
                case WarningTier.Normally:
                    n.Up(NotificationStyle.Warning, text, true, true);
                    break;
                case WarningTier.Aggressively:
                    ShowAggressive(new AgressiveNotification(false, 5000, "", "You've reached your daily upload limit.", "Whatever you were uploading needs to stop", "I can do much more things that you can't", "Stay inside borders, don't cross lines", "Consider this a \"friendly\" warning"));
                    break;
            }
        }

        private static void RiseDailyDownloadLimitReachedWarning(string text)
        {
            Notification n = new Notification();
            switch (Settings.warningTier)
            {
                case WarningTier.Politely:
                    n.Up(NotificationStyle.Info, text + " ヾ(^▽^*)))", true, false);
                    break;
                case WarningTier.Normally:
                    n.Up(NotificationStyle.Warning, text, true, true);
                    break;
                case WarningTier.Aggressively:
                    ShowAggressive(new AgressiveNotification(false, 5000, "", "You've reached your daily download limit.", "Whatever you were downloading needs to stop", "Stay inside borders, don't cross lines", "Consider this a \"friendly\" warning"));
                    break;
            }
        }

        private static void RiseActiveDaysRemainingWarning(string text, int? remainingDays)
        {
            Notification n = new Notification();
            string message = text;
            string[] agMessages = { "", "Your time is coming", "Your active internet service will expire in a couple of days", "What will you do about it?", "Use your brain, do something", "Now go ahead and use our application" };

            if (remainingDays != null)
            {
                message = $"Only {remainingDays} days remaining from your active internet service";
                agMessages = new string[] { "", $"{remainingDays} days remaining from your active internet service", "*exhale*", $"{remainingDays}", $"Just {remainingDays}.", "I wish you had more time" };
            }

            switch (Settings.warningTier)
            {
                case WarningTier.Politely:
                    n.Up(NotificationStyle.Info, message + " ヾ(^▽^*)))", true, false);
                    break;
                case WarningTier.Normally:
                    n.Up(NotificationStyle.Warning, message, true, true);
                    break;
                case WarningTier.Aggressively:
                    ShowAggressive(new AgressiveNotification(false, 5000, agMessages));
                    break;
            }
        }

        private static void RiseTimedDaysRemainingWarning(string text, int? remainingDays)
        {
            Notification n = new Notification();
            string message = text;
            string[] agMessages = { "", "Your time is coming", "Your timed internet service will expire in a couple of days", "What will you do about it?", "Use your brain, do something", "Now go ahead and use our application" };

            if (remainingDays != null)
            {
                message = $"Only {remainingDays} days remaining from your timed internet service";
                agMessages = new string[] { "", $"{remainingDays} days remaining from your timed internet service", "*exhale*", $"{remainingDays}", $"Just {remainingDays}.", "I wish you had more time" };
            }

            switch (Settings.warningTier)
            {
                case WarningTier.Politely:
                    n.Up(NotificationStyle.Info, message + " ヾ(^▽^*)))", true, false);
                    break;
                case WarningTier.Normally:
                    n.Up(NotificationStyle.Warning, message, true, true);
                    break;
                case WarningTier.Aggressively:
                    ShowAggressive(new AgressiveNotification(false, 5000, agMessages));
                    break;
            }
        }

        private static void RisePercentagesDontMatchWarning(string text, int internetPercentage, int daysPercentage)
        {
            Notification n = new Notification();
            switch (Settings.warningTier)
            {
                case WarningTier.Politely:
                    n.Up(NotificationStyle.Info, text + " ヾ(^▽^*)))", true, false);
                    break;
                case WarningTier.Normally:
                    n.Up(NotificationStyle.Warning, text, true, true);
                    break;
                case WarningTier.Aggressively:
                    ShowAggressive(new AgressiveNotification(false, 5000, "", $"You've used {100 - internetPercentage}% of your internet.", $"While you used {100 - daysPercentage}% of your package time", "Oh, you don't have enough processing power to understand?", $"The ratio is {internetPercentage}/{daysPercentage}", $"This means you are {Math.Abs(internetPercentage - daysPercentage)}% off-balance", "Balance your usage, or we will balance a bullet inside you"));
                    break;
            }
        }

        private static void RiseNoInternetWarning(string text)
        {
            bool connected = false;
            Notification n = new Notification();
            switch (Settings.warningTier)
            {
                case WarningTier.Politely:
                    n.Up(NotificationStyle.Info, text, true, false);
                    break;
                case WarningTier.Normally:
                    n.Up(NotificationStyle.Warning, text, true, true);
                    break;
                case WarningTier.Aggressively:
                    ShowAggressive(new AgressiveNotification(false, 5000, "", "There is no internet connection", "Actually this one will be polite because I know that you are sorry for that too...", "Yeah... I can't work without internet, sorry", "But feel free to come back later where internet is avalible!", "Now, I'll wait for internet connection"));
                    break;
                case WarningTier.Godmode:
                    break;
                default:
                    break;
            }

            Timer t = new Timer() { Interval = 5000 };
            t.Tick += async (s, e) => await CheckConnection();
            t.Start();

            async Task<string> CheckConnection()
            {
                var uri = await ValidateConnection.RecieveUriAsync();
                if (uri.StartsWith('!'))
                {
                    connected = false;
                    return uri.Substring(0, 1); // Return result without (!) mark
                }
                else
                {
                    connected = true;
                    return uri;
                }
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

                if (currentBill >= Convert.ToInt32(billLimit))
                {
                    WarningManager.RiseWarning(WarningReason.BillLimitReached, "You've reached your internet bill limit!");
                }
            }
            public static void DailyUploadLimitReached(int currentUpload)
            {
                var uploadLimit = Settings.staticSettings.DailyUploadLimit;

                if (currentUpload >= uploadLimit)
                {
                    WarningManager.RiseWarning(WarningReason.UploadLimitReached, "You've reached your daily upload limit!");
                }
            }
            public static void DailyDownloadLimitReached(int currentDownload)
            {
                var downloadLimit = Settings.staticSettings.DailyDownloadLimit;

                if (currentDownload >= downloadLimit)
                {
                    WarningManager.RiseWarning(WarningReason.DownloadLimitReached, "You've reached your daily download limit!");
                }
            }
            public static void ActiveDaysRemaining(int currentlyDaysRemaining)
            {
                var dayLimit = Settings.staticSettings.DaysLeftFromActive;
                if (currentlyDaysRemaining <= dayLimit)
                {
                    WarningManager.RiseWarning(WarningReason.ActiveDaysLimitReached, "Your active internet service will expire in a short time, watch out!", currentlyDaysRemaining);
                }
            }
            public static void TimedDaysRemaining(int currentlyDaysRemaining)
            {
                var timedDayLimit = Settings.staticSettings.DaysLeftFromTimed;
                if (currentlyDaysRemaining <= timedDayLimit)
                {
                    WarningManager.RiseWarning(WarningReason.TimedDaysLimitReached, "Your timed internet service will expire in a short time, watch out!", currentlyDaysRemaining);
                }
            }
            public static void Percentages(int currentDaysPercentage, int currentInternetPercentage, string activeOrTimed)
            {
                var tolerance = Settings.staticSettings.Tolearnce;

                if (Math.Abs((currentDaysPercentage - currentInternetPercentage)) > 0 + tolerance) // Se how close they are to zero when subtracted from themselves (with tolerance)
                {
                    WarningManager.RiseWarning(WarningReason.PercentagesDontMatchWithTolerance, $"Your usage vs remaining days don't match! be carefull and try to re-balance them!\n (Ratio: {currentInternetPercentage}/{currentDaysPercentage} - Remaining internet / Reaining days)", currentInternetPercentage, currentDaysPercentage);
                }
            }
        }
    }
}

