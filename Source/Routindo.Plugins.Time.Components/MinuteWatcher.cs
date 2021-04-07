using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Routindo.Contract;
using Routindo.Contract.Services;

namespace Routindo.Plugins.Time.Components
{
    public class MinuteWatcher: IWatcher
    {

        private string _dataFile;

        public string Id { get; set; }
        public ILoggingService LoggingService { get; set; }

        public int Times { get; set; }

        private bool _dataFileSet = false;
        public WatcherResult Watch()
        {
            try
            {
                if (!_dataFileSet)
                {
                    SetDataFile();
                    _dataFileSet = true;
                }

                var dateTime = GetLastExecutionTime();
                if (DateTime.Now.Subtract(dateTime).TotalMinutes > Times)
                {
                    SaveLastExecutionTime(DateTime.Now);
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

        private void SetDataFile()
        {
            var environmentService = ServicesContainer.ServicesProvider.GetEnvironmentService();
            var dataDirectory = Path.Combine(environmentService.DataDirectory, "Routindo.Time");
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            _dataFile = Path.Combine(dataDirectory, this.Id);
        }

        private DateTime GetLastExecutionTime()
        {
            if(!File.Exists(_dataFile))
                return DateTime.MinValue;
            
            var text = File.ReadAllText(_dataFile);
            if (DateTime.TryParseExact(text, "yyyyMMddHHmmss", CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal, out var datetime))
            {
                return datetime;
            }
            
            return DateTime.MinValue;
        }

        private void SaveLastExecutionTime(DateTime time)
        {
            File.WriteAllText(_dataFile, time.ToString("yyyyMMddHHmmss"));
        }
    }
}
