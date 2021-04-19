using System;
using System.Globalization;
using System.IO;
using Routindo.Contract.Services;

namespace Routindo.Plugins.Time.Components
{
    public class PluginDataManager : IPluginDataMananger
    {
        private readonly string _componentRuntimeId;
        private string _dataFile;
        private readonly ILoggingService _loggingService;
        private readonly IEnvironmentService _environmentService;

        public PluginDataManager(string componentRuntimeId)
        {
            _componentRuntimeId = componentRuntimeId;
            _loggingService =
                ServicesContainer.ServicesProvider.GetLoggingService(nameof(PluginDataManager));
            _environmentService = ServicesContainer.ServicesProvider.GetEnvironmentService();
        }
        public void SetDataFile()
        {
            _loggingService.Info("Setting Data File");
            var dataDirectory = Path.Combine(_environmentService.DataDirectory, "Routindo.Time");
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            _dataFile = Path.Combine(dataDirectory, this._componentRuntimeId);
            _loggingService.Info("Data File Set successfully");
        }

        public DateTime GetPlannedExecutionTime()
        {
            _loggingService.Trace("Getting planned execution time");
            if (!File.Exists(_dataFile))
                return DateTime.MinValue;

            _loggingService.Trace("Reading data file");
            var text = File.ReadAllText(_dataFile);
            if (DateTime.TryParseExact(text, "yyyyMMddHHmmss", CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal, out var datetime))
            {
                _loggingService.Trace("next planned time found on data file", datetime.ToString("G"));
                return datetime;
            }

            _loggingService.Trace("Returning minimum time value", datetime.ToString("G"));
            return DateTime.MinValue;
        }

        public void SaveNextExecutionTime(DateTime time) 
        {
            _loggingService.Debug("Saving planned execution time: ", time.ToString("G"));
            File.WriteAllText(_dataFile, time.AddSeconds(-time.Second).ToString("yyyyMMddHHmmss"));
            _loggingService.Debug("planned execution time saved successfully");
        }
    }
}
