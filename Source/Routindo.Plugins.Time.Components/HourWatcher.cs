using System;

namespace Routindo.Plugins.Time.Components
{
    public class HourWatcher: TimeWatcher
    {
        public int Times { get; set; } = 1;

        public int Minute { get; set; }

        protected override DateTime GetNextExecutionTime()
        {
            var estimatedTime = DateTime.Now;
            LoggingService.Trace("Estimated Time: ", estimatedTime.ToString("G"));
            estimatedTime = estimatedTime.AddSeconds(-estimatedTime.Second);
            LoggingService.Trace("Estimated Time Without Seconds: {0}", estimatedTime.ToString("G"));
            if (this.DataMananger.GetPlannedExecutionTime() == DateTime.MinValue)
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