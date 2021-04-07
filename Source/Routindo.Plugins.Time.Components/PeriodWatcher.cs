using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Routindo.Contract;
using Routindo.Contract.Services;

namespace Routindo.Plugins.Time.Components
{
    public class PeriodWatcher: IWatcher
    {
        public string Id { get; set; }
        public ILoggingService LoggingService { get; set; }

        public TimeUnit EachPeriod { get; set; }

        public int EachPeriodTimes { get; set; }



        public WatcherResult Watch()
        {
            try
            {
                var lastTimelapsed = DateTime.Now.AddDays(-3);

                switch (EachPeriod)
                {
                    
                }


                return WatcherResult.Succeed(ArgumentCollection.New());
            }
            catch (Exception exception)
            {
                LoggingService.Error(exception);
                return WatcherResult.NotFound;
            }
        }
    }
}
