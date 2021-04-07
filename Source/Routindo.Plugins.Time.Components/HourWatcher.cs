using System;

namespace Routindo.Plugins.Time.Components
{
    public class HourWatcher: TimeWatcher
    {
        public int Times { get; set; } = 1;

        public int Minute { get; set; }

        protected override DateTime GetNextExecutionTime()
        {
            var estimatedTime = DateTime.Now.AddHours(Times);

            if (estimatedTime.Minute != Minute)
            {
                var diffMinutes = Math.Abs(estimatedTime.Minute - Minute);
                estimatedTime = estimatedTime.AddMinutes(diffMinutes * (estimatedTime.Minute > Minute ? -1 : 1));
            }

            return estimatedTime;
        }
    }
}