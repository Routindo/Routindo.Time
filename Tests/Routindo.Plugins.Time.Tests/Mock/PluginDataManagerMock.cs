using System;
using Routindo.Contract.Services;
using Routindo.Plugins.Time.Components;

namespace Routindo.Plugins.Time.Tests.Mock
{
    public class PluginDataManagerMock: IPluginDataMananger
    {
        private DateTime _plannedDataTime;
        private ILoggingService _loggingService;

        public PluginDataManagerMock()
        {
            _loggingService = ServicesContainer.ServicesProvider.GetLoggingService("Mock", typeof(PluginDataManager));
        }
        public void SetDataFile()
        {
            _loggingService.Debug("Setting data file");
        }

        public DateTime GetPlannedExecutionTime()
        {
            _loggingService.Debug("Getting next planned execution time: {0}", _plannedDataTime.ToString("G"));
            return _plannedDataTime;
        }

        public void SaveNextExecutionTime(DateTime time)
        {
            _loggingService.Debug("Saving next planned execution time: {0}", time.ToString("G"));
            _plannedDataTime = time.AddSeconds(-time.Second);
            _loggingService.Debug("next planned execution time saved: {0}", _plannedDataTime.ToString("G"));
        }
    }
}
