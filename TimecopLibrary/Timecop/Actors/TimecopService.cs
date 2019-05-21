using Akka.Actor;
using Akka.Event;

namespace CommonLibrary.Timecop.Actors
{
    public class TimecopService : ReceiveActor
    {
		private readonly ILoggingAdapter _log = Context.GetLogger();

        public TimecopService() { }
    }
}
