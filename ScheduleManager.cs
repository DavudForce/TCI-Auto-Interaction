
using adsl_Auto_Interaction_App;

/// <summary>
/// Manages next-check time for scheduled tasks.
/// </summary>
public class ScheduleManager
{
    FileManager fileManager = new FileManager();
    private DateTime nextCheck;

    public ScheduleManager()
    {
        var data = fileManager.LoadSchedule();
        nextCheck = data.Item1; // Always valid now
    }

    public void UpdateNextCheck(TimeSpan interval)
    {
        nextCheck = DateTime.Now.Add(interval);
        fileManager.SaveSchedule(nextCheck);
    }

    public TimeSpan GetRemainingTime()
    {
        return nextCheck - DateTime.Now;
    }
}