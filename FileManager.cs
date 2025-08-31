using System;
using System.IO;
using System.Text.Json;

namespace adsl_Auto_Interaction_App
{
    internal class FileManager
    {
        static TagInfo fileReadTagInfo = new TagInfo() { Name = "file_read" };
        static TagInfo fileWriteTagInfo = new TagInfo() { Name = "file_write" };
        static string _settingsPath = Settings.settingPath;
        static string _schedulePath = Settings.schedulePath;

        public Status initalizationStatus = new Status() { Tag = new TagInfo() { Name = "file_manager_self" } };

        public FileManager()
        {
            if (!Directory.Exists(Path.GetDirectoryName(_settingsPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(_settingsPath));

            if (!File.Exists(_settingsPath))
            {
                initalizationStatus.Success = null;
                initalizationStatus.Details = "Settings file cannot be found";
                initalizationStatus.Tag.SubTag = "settings_missing";
                initalizationStatus.Severity = 2;
            }
            else initalizationStatus.Success = true;
        }

        public static Status RestoreSettings()
        {
            try
            {
                SettingsModule settings = new SettingsModule()
                {
                    BillLimit = 0.0M,
                    DailyDownloadLimit = 10000,
                    DailyUploadLimit = 400,
                    DaysLeftFromActive = 10,
                    DaysLeftFromTimed = 10,
                    Tolearnce = 1.0M,
                    WarnTierSelectedIndex = 1
                };

                var settingsJson = JsonSerializer.Serialize(settings);
                File.WriteAllText(_settingsPath, settingsJson);

                return new Status() { Success = true, Tag = fileWriteTagInfo.SubTag = "restore_settings" };
            }
            catch (Exception e)
            {
                return new Status() { Success = false, Details = e.Message, Tag = fileWriteTagInfo.SubTag = "restore_settings", Severity = 5 };
            }
        }

        public (SettingsModule?, Status) GetSettings()
        {
            Status status = new Status() { Tag = fileReadTagInfo.SubTag = "read_settings" };
            try
            {
                string raw = File.ReadAllText(_settingsPath);
                SettingsModule res = JsonSerializer.Deserialize<SettingsModule>(raw);
                status.Success = true;
                return (res, status);
            }
            catch (Exception e)
            {
                status.Success = false;
                status.Details = e.Message;
                status.Severity = 5;
                return (null, status);
            }
        }

        public Status SetSettings(SettingsModule settings)
        {
            Status status = new Status() { Tag = fileReadTagInfo.SubTag = "write_settings" };
            try
            {
                var json = JsonSerializer.Serialize<SettingsModule>(settings);
                File.WriteAllText(_settingsPath, json);
                status.Success = true;
                return status;
            }
            catch (Exception e)
            {
                status.Success = false;
                status.Details = e.Message;
                status.Severity = 1;
                return status;
            }
        }

        /// <summary>
        /// Loads the schedule file, always returning a valid DateTime. Status is for logging.
        /// </summary>
        public (DateTime, Status) LoadSchedule()
        {
            DateTime nextCheck;
            Status status;
            try
            {
                if (File.Exists(_schedulePath))
                {
                    string json = File.ReadAllText(_schedulePath);
                    nextCheck = JsonSerializer.Deserialize<DateTime>(json);
                    status = new Status() { Success = true, Tag = fileReadTagInfo.SubTag = "read_schedule" };
                }
                else
                {
                    nextCheck = DateTime.Now.AddHours(12);
                    SaveSchedule(nextCheck);
                    status = new Status() { Success = true, Tag = fileWriteTagInfo.SubTag = "write_schedule" };
                }
            }
            catch (UnauthorizedAccessException ua)
            {
                nextCheck = DateTime.Now.AddHours(12);
                SaveSchedule(nextCheck);
                status = new Status() { Success = false, Details = ua.Message, Severity = 3, Tag = fileReadTagInfo.SubTag = "read_schedule" };
            }
            catch (IOException ioe)
            {
                nextCheck = DateTime.Now.AddHours(12);
                SaveSchedule(nextCheck);
                status = new Status() { Success = false, Details = ioe.Message, Severity = 3, Tag = fileReadTagInfo.SubTag = "read_schedule" };
            }
            catch
            {
                nextCheck = DateTime.Now.AddHours(12);
                SaveSchedule(nextCheck);
                status = new Status() { Success = false, Details = "Unexpected error reading schedule, created new default", Severity = 3, Tag = fileReadTagInfo.SubTag = "read_schedule" };
            }
            return (nextCheck, status);
        }

        /// <summary>
        /// Saves given DateTime. If null, defaults to 2 hours later from now.
        /// </summary>
        public Status SaveSchedule(DateTime? nextCheck)
        {
            if (nextCheck == null)
                nextCheck = DateTime.Now.AddHours(2);

            try
            {
                string json = JsonSerializer.Serialize(nextCheck);
                File.WriteAllText(_schedulePath, json);
                return new Status() { Success = true, Tag = fileWriteTagInfo.SubTag = "write_schedule" };
            }
            catch (Exception e)
            {
                return new Status() { Success = false, Details = e.Message, Severity = 3, Tag = fileWriteTagInfo.SubTag = "write_schedule" };
            }
        }
    }
}
