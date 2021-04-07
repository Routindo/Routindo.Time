using System;

namespace Routindo.Plugins.Time.Components
{
    public class MinuteWatcher: TimeWatcher
    {
        public int Times { get; set; } = 1;

        protected override DateTime GetNextExecutionTime()
        {
            return DateTime.Now.AddMinutes(Times);
        }
    }
}
