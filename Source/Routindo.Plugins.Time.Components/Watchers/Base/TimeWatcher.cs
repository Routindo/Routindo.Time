﻿using System;
using Routindo.Contract.Arguments;
using Routindo.Contract.Attributes;
using Routindo.Contract.Services;
using Routindo.Contract.Watchers;

namespace Routindo.Plugins.Time.Components.Base
{
    public abstract class TimeWatcher: IWatcher
    {
        public IPluginDataMananger DataManager  { get; set; } 

        public string Id { get; set; }

        public ILoggingService LoggingService { get; set; }

        [Argument(TimeWatcherArgs.Times)] public int Times { get; set; } = 1;

        public virtual WatcherResult Watch()
        {
            try
            {
                LoggingService.Trace("Watching.");
                if (DataManager == null)
                {
                    LoggingService.Debug("Creating new instance of Plugin Data Manager.");
                    DataManager = new PluginDataManager(this.Id);
                    LoggingService.Debug("Setting Data File.");
                    DataManager.SetDataFile();
                }

                var plannedExecutionTime = DataManager.GetPlannedExecutionTime();
                LoggingService.Trace("Planned Execution Time: {0}", plannedExecutionTime.ToString("G"));
                if (DateTime.Compare(DateTime.Now, plannedExecutionTime) >= 0)
                {
                    LoggingService.Debug("Time Reached");
                    var nextExecutionTime = GetNextExecutionTime();
                    LoggingService.Debug("Saving Next Execution Time: {0}", nextExecutionTime.ToString("G"));
                    DataManager.SaveNextExecutionTime(nextExecutionTime);
                    return WatcherResult.Succeed(ArgumentCollection.New());
                }

                LoggingService.Trace("Time Not Reached Yet!");
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
