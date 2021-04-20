using System;
using Routindo.Contract.Arguments;
using Routindo.Contract.UI;
using Routindo.Plugins.Time.Components.Actions;

namespace Routindo.Plugins.Time.UI.ViewModels
{
    public class PauseActionViewModel: PluginConfiguratorViewModelBase
    {
        private int _milliseconds = 100;
        private int _seconds;
        private int _minutes;

        public PauseActionViewModel()
        {
        }
        public int Minutes
        {
            get => _minutes;
            set
            {
                ClearPropertyErrors();
                _minutes = value;
                ValidateNumber(value, i => i >= 0);
                OnPropertyChanged(nameof(ShowErrorMessage));
                OnPropertyChanged();
            }
        }

        public int Seconds
        {
            get => _seconds;
            set
            {
                ClearPropertyErrors();
                _seconds = value;
                ValidateNumber(_seconds, i => i >= 0 && i< 60 );
                OnPropertyChanged(nameof(ShowErrorMessage));
                OnPropertyChanged();
            }
        }

        public int Milliseconds
        {
            get => _milliseconds;
            set
            {
                ClearPropertyErrors();
                _milliseconds = value;
                ValidateNumber(_milliseconds, i => i >= 0 && i< 1000);
                OnPropertyChanged(nameof(ShowErrorMessage));
                OnPropertyChanged();
            }
        }

        public bool ShowErrorMessage
        {
            get
            {
                var ms = GetTotalMilliseconds();
                ClearPropertyErrors();
                if (ms <= 0)
                {
                    AddPropertyError(nameof(ShowErrorMessage), "Total Milliseconds is not correct");
                }
                return ms <= 0;
            }
        }

        public uint GetTotalMilliseconds()
        {
            TimeSpan ts = new TimeSpan(0, 0, Minutes, Seconds, Milliseconds);
            return Convert.ToUInt32(ts.TotalMilliseconds);
        }

        public override void Configure()
        {
            this.InstanceArguments = ArgumentCollection.New()
                .WithArgument(PauseActionArgs.PauseMilliseconds, GetTotalMilliseconds());
        }

        public override void SetArguments(ArgumentCollection arguments)
        {
            if (arguments.HasArgument(PauseActionArgs.PauseMilliseconds))
            {
                var msValue = arguments.GetValue<uint>(PauseActionArgs.PauseMilliseconds);
                SetFromMilliseconds(msValue);
            }
        }

        private void SetFromMilliseconds(uint msValue)
        {
            TimeSpan ts = TimeSpan.FromMilliseconds(msValue);
            this.Milliseconds = ts.Milliseconds;
            this.Seconds = ts.Seconds;
            this.Minutes = ts.Minutes + (ts.Hours * 60) + (ts.Days * 24 * 60);
        }
    }
}
