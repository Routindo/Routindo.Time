using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Routindo.Contract.Arguments;
using Routindo.Contract.UI;
using Routindo.Plugins.Time.Components.Base;

namespace Routindo.Plugins.Time.UI.ViewModels
{
    public class MinuteWatcherViewModel: PluginConfiguratorViewModelBase
    {
        private int _times = 1;

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

        public override void Configure()
        {
            this.InstanceArguments = ArgumentCollection.New()
                .WithArgument(TimeWatcherArgs.Times, Times);
        }

        public override void SetArguments(ArgumentCollection arguments)
        {
            if (arguments.HasArgument(TimeWatcherArgs.Times))
                Times = arguments.GetValue<int>(TimeWatcherArgs.Times);
        }
    }
}
