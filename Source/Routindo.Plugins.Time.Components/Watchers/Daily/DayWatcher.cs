using System;
using Routindo.Contract.Attributes;
using Routindo.Plugins.Time.Components.Base;

namespace Routindo.Plugins.Time.Components.Daily
{
    [PluginItemInfo(ComponentUniqueId, nameof(DayWatcher),
        "Raises an event each X day(s)", Category = "Time", FriendlyName = "Daily Event")]
    public class DayWatcher : TimeWatcher  
    {
        public const string ComponentUniqueId = "9029EEB8-BFC1-48A4-8C7B-0062D79BAAFF";

        [Argument(DayWatcherArgs.Minute)] public int Minute { get; set; }

        [Argument(DayWatcherArgs.Hour)] public int Hour { get; set; }

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