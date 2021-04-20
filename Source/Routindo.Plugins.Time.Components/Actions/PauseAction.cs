using System;
using System.Threading;
using Routindo.Contract.Actions;
using Routindo.Contract.Arguments;
using Routindo.Contract.Attributes;
using Routindo.Contract.Services;

namespace Routindo.Plugins.Time.Components.Actions
{
    [PluginItemInfo(ComponentUniqueId, "Pause Action",
         "Do a pause before execute next action on flow activities")]
    public class PauseAction: IAction
    {
        public const string ComponentUniqueId = "1751DB33-BA80-41AD-9412-497A8068E9C7";
        public string Id { get; set; }
        public ILoggingService LoggingService { get; set; }

        [Argument(PauseActionArgs.PauseMilliseconds, true)] public uint PauseMilliseconds { get; set; }

        public ActionResult Execute(ArgumentCollection arguments)
        {
            try
            {
                if(PauseMilliseconds <= 0)
                    throw new Exception("Pausing time is not set correctly, expected value greater than 0");

                Thread.Sleep(TimeSpan.FromMilliseconds(PauseMilliseconds));
                return ActionResult.Succeeded();
            }
            catch (Exception exception)
            {
                LoggingService.Error(exception);
                return ActionResult.Failed().WithException(exception);
            }
        }
    }

    public static class PauseActionArgs
    {
        public const string PauseMilliseconds = nameof(PauseMilliseconds);
    }
}
