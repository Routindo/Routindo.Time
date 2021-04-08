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
                LoggingService.Debug("Watching.");
                if (DataMananger == null)
                {
                    LoggingService.Debug("Creating new instance of Plugin Data Manager.");
                    DataMananger = new PluginDataMananger(this.Id);
                    LoggingService.Debug("Setting Data File.");
                    DataMananger.SetDataFile();
                }

                var plannedEecutionTime = DataMananger.GetPlannedExecutionTime();
                LoggingService.Debug("Planned Execution Time: {0}", plannedEecutionTime.ToString("G"));
                if (DateTime.Compare(DateTime.Now, plannedEecutionTime) >= 0)
                {
                    LoggingService.Debug("Time Reached");
                    var nextExecutionTime = GetNextExecutionTime();
                    LoggingService.Debug("Saving Next Execution Time: {0}", nextExecutionTime.ToString("G"));
                    DataMananger.SaveNextExecutionTime(nextExecutionTime);

                    return WatcherResult.Succeed(ArgumentCollection.New());
                }

                LoggingService.Debug("Time Not Reached Yet!");
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
