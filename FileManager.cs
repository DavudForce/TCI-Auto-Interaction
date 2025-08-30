using System.Text.Json;
namespace adsl_Auto_Interaction_App
{
    internal class FileManager
    {
        static TagInfo fileReadTagInfo = new TagInfo() { Name = "file_read" };
        static TagInfo fileWriteTagInfo = new TagInfo() { Name = "file_write" };
        static string _settingsPath = Settings.settingPath;

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
                    DaysLeftFromTimed = 10
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
    }
}
