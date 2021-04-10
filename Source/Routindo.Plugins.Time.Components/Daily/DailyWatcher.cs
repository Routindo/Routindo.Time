using System;
using Routindo.Contract.Attributes;
using Routindo.Plugins.Time.Components.Base;

namespace Routindo.Plugins.Time.Components.Daily
{
    public class DailyWatcher : TimeWatcher
    {
        [Argument(DailyWatcherArgs.Minute)] public int Minute { get; set; }

        [Argument(DailyWatcherArgs.Hour)] public int Hour { get; set; }

        protected override DateTime GetNextExecutionTime()
        {
            var estimatedTime = DateTime.Now.AddDays(Times);

            if (estimatedTime.Hour != Hour)
            {
                var diffHours = Math.Abs(estimatedTime.Hour - Hour);
                estimatedTime = estimatedTime.AddMinutes(diffHours * (estimatedTime.Hour > Hour ? -1 : 1)); 
            }

            if (estimatedTime.Minute != Minute)
            {
                var diffMinutes = Math.Abs(estimatedTime.Minute - Minute);
                estimatedTime = estimatedTime.AddMinutes(diffMinutes * (estimatedTime.Minute > Minute ? -1 : 1));
            }

            return estimatedTime;
        }
    }
}