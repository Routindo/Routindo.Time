using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Routindo.Contract.Arguments;
using Routindo.Contract.UI;
using Routindo.Plugins.Time.Components.Base;
using Routindo.Plugins.Time.Components.Hourly;

namespace Routindo.Plugins.Time.UI.ViewModels
{
    public class HourWatcherViewModel: PluginConfiguratorViewModelBase
    { 
        private int _times = 1;
        private int _minute = 0;

        public int Times
        {
            get => _times;
            set
            {
                ClearPropertyErrors();
                _times = value;
                ValidateNumber(_times, i => i > 0);
                OnPropertyChanged();
            }
        }

        public int Minute
        {
            get => _minute;
            set
            {
                ClearPropertyErrors();
                _minute = value;
                ValidateNumber(_minute, i => i >= 0 && i < 60);
                OnPropertyChanged();
            }
        }

        public override void Configure()
        {
            this.InstanceArguments = ArgumentCollection.New()
                .WithArgument(TimeWatcherArgs.Times, Times)
                .WithArgument(HourWatcherArgs.Minute, Minute);
        }

        public override void SetArguments(ArgumentCollection arguments)
        {
            if (arguments.HasArgument(TimeWatcherArgs.Times))
                Times = arguments.GetValue<int>(TimeWatcherArgs.Times);

            if (arguments.HasArgument(HourWatcherArgs.Minute))
                Minute = arguments.GetValue<int>(HourWatcherArgs.Minute);
        }
    }
}
