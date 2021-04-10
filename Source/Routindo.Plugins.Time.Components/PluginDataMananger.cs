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
        private ILoggingService _loggingService;
        private IEnvironmentService _environmentService;

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
            _loggingService.Debug("Getting planned execution time");
            if (!File.Exists(_dataFile))
                return DateTime.MinValue;

            _loggingService.Debug("Reading data file");
            var text = File.ReadAllText(_dataFile);
            if (DateTime.TryParseExact(text, "yyyyMMddHHmmss", CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal, out var datetime))
            {
                _loggingService.Debug("next planned time found on data file", datetime.ToString("G"));
                return datetime;
            }

            _loggingService.Debug("Returning minimum time value", datetime.ToString("G"));
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
