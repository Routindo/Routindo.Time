using System;
using Routindo.Plugins.Time.Components;

namespace Routindo.Plugins.Time.Tests.Mock
{
    public class PluginDataManagerMock: IPluginDataMananger
    {
        private DateTime _plannedDataTime;
        public void SetDataFile()
        {
            
        }

        public DateTime GetPlannedExecutionTime()
        {
            return _plannedDataTime;
        }

        public void SaveNextExecutionTime(DateTime time)
        {
            _plannedDataTime = time;
        }
    }
}
