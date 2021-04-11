using Routindo.Contract.Arguments;
using Routindo.Contract.UI;
using Routindo.Plugins.Time.Components.Base;
using Routindo.Plugins.Time.Components.Daily;
using Routindo.Plugins.Time.Components.Hourly;

namespace Routindo.Plugins.Time.UI.ViewModels
{
    public class DayWatcherViewModel: PluginConfiguratorViewModelBase
    {   
        private int _times = 1;
        private int _minute = 0;
        private int _hour = 0;

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

        public int Hour
        {
            get => _hour;
            set
            {
                ClearPropertyErrors();
                _hour = value;
                ValidateNumber(_hour, i => i >= 0 && i < 24);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Configures this instance.
        /// </summary>
        public override void Configure()
        {
            this.InstanceArguments = ArgumentCollection.New()
                .WithArgument(TimeWatcherArgs.Times, Times)
                .WithArgument(DayWatcherArgs.Hour, Hour)
                .WithArgument(DayWatcherArgs.Minute, Minute);
        }

        /// <summary>Sets the arguments.</summary>
        /// <param name="arguments">The arguments.</param>
        public override void SetArguments(ArgumentCollection arguments)
        {
            if (arguments.HasArgument(TimeWatcherArgs.Times))
                Times = arguments.GetValue<int>(TimeWatcherArgs.Times);

            if (arguments.HasArgument(DayWatcherArgs.Hour))
                Hour = arguments.GetValue<int>(DayWatcherArgs.Hour);

            if (arguments.HasArgument(DayWatcherArgs.Minute))
                Minute = arguments.GetValue<int>(DayWatcherArgs.Minute);
        }
    }
}
