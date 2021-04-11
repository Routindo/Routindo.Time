using Routindo.Contract.Attributes;
using Routindo.Plugins.Time.Components.Daily;
using Routindo.Plugins.Time.Components.Hourly;
using Routindo.Plugins.Time.Components.Minutely;
using Routindo.Plugins.Time.UI.Views;

[assembly: ComponentConfigurator(typeof(MinuteWatcherView), MinuteWatcher.ComponentUniqueId, "Configurator for Minutes Watcher")]
[assembly: ComponentConfigurator(typeof(HourWatcherView), HourWatcher.ComponentUniqueId, "Configurator for Hours Watcher")]
[assembly: ComponentConfigurator(typeof(DayWatcherView), DayWatcher.ComponentUniqueId, "Configurator for Days Watcher")]