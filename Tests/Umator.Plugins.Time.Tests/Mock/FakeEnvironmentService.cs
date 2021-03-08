using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umator.Contract.Services;

namespace Umator.Plugins.Time.Tests.Mock
{
    public class FakeEnvironmentService: IEnvironmentService
    {
        public FakeEnvironmentService(string dataDirectory, string logsDirectory, string configDirectory)
        {
            DataDirectory = dataDirectory;
            LogsDirectory = logsDirectory;
            ConfigDirectory = configDirectory;
        }

        public string DataDirectory { get; }
        public string LogsDirectory { get; }
        public string ConfigDirectory { get; }
    }
}
