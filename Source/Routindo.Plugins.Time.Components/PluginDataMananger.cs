using System;
using System.Globalization;
using System.IO;
using Routindo.Contract.Services;

namespace Routindo.Plugins.Time.Components
{
    public class PluginDataMananger : IPluginDataMananger
    {
        private readonly string _componentRuntimeId;
        private string _dataFile;

        public PluginDataMananger(string componentRuntimeId)
        {
            _componentRuntimeId = componentRuntimeId;
        }
        public void SetDataFile()
        {
            var environmentService = ServicesContainer.ServicesProvider.GetEnvironmentService();
            var dataDirectory = Path.Combine(environmentService.DataDirectory, "Routindo.Time");
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            _dataFile = Path.Combine(dataDirectory, this._componentRuntimeId);
        }

        public DateTime GetPlannedExecutionTime()
        { 
            if (!File.Exists(_dataFile))
                return DateTime.MinValue;

            var text = File.ReadAllText(_dataFile);
            if (DateTime.TryParseExact(text, "yyyyMMddHHmmss", CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal, out var datetime))
            {
                return datetime;
            }

            return DateTime.MinValue;
        }

        public void SaveNextExecutionTime(DateTime time) 
        {
            File.WriteAllText(_dataFile, time.ToString("yyyyMMddHHmmss"));
        }
    }
}
