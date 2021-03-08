using System;
using System.Timers;
using Umator.Contract;
using Umator.Contract.Services;

namespace Umator.Plugins.Time.Components
{
    [PluginItemInfo("EDD3D344-080B-425B-9A7E-6E46D5B944A8", "Time Watcher",
        "Watcher for date and time and reports when specific time is reached.")]
    [ResultArgumentsClass(typeof(TimeWatcherResultArguments))]
    public class DateTimeWatcher : IWatcher
    {
        public string Id { get; set; }
        public ILoggingService LoggingService { get; set; }

        [Argument(TimeWatcherArguments.Year)]
        public int? TargetYear { get; set; } = null;

        [Argument(TimeWatcherArguments.Month)]
        public int? TargetMonth { get; set; } = null;

        [Argument(TimeWatcherArguments.Day)]
        public int? TargetDay { get; set; } = null;

        [Argument(TimeWatcherArguments.Hour)]
        public int? TargetHour { get; set; } = null;

        [Argument(TimeWatcherArguments.Minute)]
        public int? TargetMinute { get; set; } = null;

        [Argument(TimeWatcherArguments.Second)]
        public int? TargetSecond { get; set; } = null;

        public WatcherResult Watch()
        {
            var now = DateTime.Now;

            // DATE
            bool timeFound = !TargetYear.HasValue || TargetYear.Value == now.Year;
            timeFound = timeFound && (!TargetMonth.HasValue || TargetMonth.Value == now.Month);
            timeFound = timeFound && (!TargetDay.HasValue || TargetDay.Value == now.Day);

            // TIME 
            timeFound = timeFound && (!TargetHour.HasValue || TargetHour.Value == now.Hour);
            timeFound = timeFound && (!TargetMinute.HasValue || TargetMinute.Value == now.Minute);
            timeFound = timeFound && (!TargetSecond.HasValue || TargetSecond.Value == now.Second);


            return !timeFound ? WatcherResult.NotFound : WatcherResult.Succeed(new ArgumentCollection()
            {
                new Argument(TimeWatcherResultArguments.Time, now)
            });
        }
    }
}
