using System;
using Routindo.Contract.Attributes;
using Routindo.Plugins.Time.Components.Base;

namespace Routindo.Plugins.Time.Components.Hourly
{
    [PluginItemInfo(ComponentUniqueId, nameof(HourWatcher),
        "Raises an event each X Hour(s)", Category = "Time", FriendlyName = "Hourly Event")]
    public class HourWatcher: TimeWatcher
    {
        public const string ComponentUniqueId = "70782F28-D08F-4332-ABED-738BBC05AE41";

        [Argument(HourWatcherArgs.Minute)] public int Minute { get; set; }

        protected override DateTime GetNextExecutionTime()
        {
            var estimatedTime = DateTime.Now;
            LoggingService.Trace("Estimated Time: ", estimatedTime.ToString("G"));
            estimatedTime = estimatedTime.AddSeconds(-estimatedTime.Second);
            LoggingService.Trace("Estimated Time Without Seconds: {0}", estimatedTime.ToString("G"));
            if (this.DataManager.GetPlannedExecutionTime() == DateTime.MinValue)
            {
                LoggingService.Trace("No Planned Execution Time yet");
                if (DateTime.Now.Minute < Minute)
                {
                    LoggingService.Trace("Setting estimated time in the same hour.");
                    estimatedTime = new DateTime(estimatedTime.Year, estimatedTime.Month, estimatedTime.Day, estimatedTime.Hour, Minute, 0);
                }
            }
            else
            {
                LoggingService.Trace("Planned Execution time found");
                estimatedTime = estimatedTime.AddHours(Times);
                LoggingService.Trace("Next Planned Execution time: {0}", estimatedTime.ToString("G"));
                if (estimatedTime.Minute != Minute)
                {
                    var diffMinutes = Math.Abs(estimatedTime.Minute - Minute);
                    estimatedTime = estimatedTime.AddMinutes(diffMinutes * (estimatedTime.Minute > Minute ? -1 : 1));
                }
            }
            LoggingService.Trace("Final Next Planned Execution time: {0}", estimatedTime.ToString("G"));
            return estimatedTime;
        }
    }
}