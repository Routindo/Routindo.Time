using Routindo.Contract;
using Routindo.Contract.Attributes;
using Routindo.Plugins.Time.Components.Minutely;
using Routindo.Plugins.Time.UI.Views;

[assembly: ComponentConfigurator(typeof(MinuteWatcherView), MinuteWatcher.ComponentUniqueId, "Configurator for Minutes Watcher")]