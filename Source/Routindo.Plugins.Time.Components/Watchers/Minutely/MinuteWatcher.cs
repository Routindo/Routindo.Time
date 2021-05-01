using System;
using Routindo.Contract.Attributes;
using Routindo.Plugins.Time.Components.Base;

namespace Routindo.Plugins.Time.Components.Minutely
{
    [PluginItemInfo(ComponentUniqueId, nameof(MinuteWatcher),
         "Raises an event each X Minute(s)", Category = "Time", FriendlyName = "Minutely Event")]
    public class MinuteWatcher: TimeWatcher
    {
        public const string ComponentUniqueId = "BFF3C255-0E70-4E12-9BB7-4E7FA9D173FE";

        protected override DateTime GetNextExecutionTime()
        {
            var estimated = DateTime.Now.AddMinutes(Times);
            return estimated.AddSeconds(-estimated.Second);
        }
    }
}
