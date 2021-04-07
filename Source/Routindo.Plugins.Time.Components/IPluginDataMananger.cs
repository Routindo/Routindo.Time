using System;

namespace Routindo.Plugins.Time.Components
{
    public interface IPluginDataMananger
    { 
        void SetDataFile();
        DateTime GetPlannedExecutionTime();
        void SaveNextExecutionTime(DateTime time);
    }
}