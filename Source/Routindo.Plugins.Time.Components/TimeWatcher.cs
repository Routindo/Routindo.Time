using System;
using System.Globalization;
using System.IO;
using Routindo.Contract.Arguments;
using Routindo.Contract.Services;
using Routindo.Contract.Watchers;

namespace Routindo.Plugins.Time.Components
{
    public abstract class TimeWatcher: IWatcher
    {
        public IPluginDataMananger DataMananger { get; set; }

        public string Id { get; set; }
        public ILoggingService LoggingService { get; set; }

        
        public virtual WatcherResult Watch()
        {
            try
            {
                if (DataMananger == null)
                {
                    DataMananger = new PluginDataMananger(this.Id);
                    DataMananger.SetDataFile();
                }

                var plannedEecutionTime = DataMananger.GetPlannedExecutionTime();
                if (DateTime.Compare(DateTime.Now, plannedEecutionTime) >= 0)
                {
                    DataMananger.SaveNextExecutionTime(GetNextExecutionTime());
                    return WatcherResult.Succeed(ArgumentCollection.New());
                }

                return WatcherResult.NotFound;
            }
            catch (Exception exception)
            {
                LoggingService.Error(exception);
                return WatcherResult.NotFound;
            }
        }

        protected abstract DateTime GetNextExecutionTime();
    }
}
