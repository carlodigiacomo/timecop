using Akka.Actor;
using Akka.DI.Core;
using Akka.Event;
using CommonLibrary.User.Commands;
using CommonLibrary.User.Events;

namespace CommonLibrary.User.Actors
{
    public class UserService : ReceiveActor, ILogReceive
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();
        private readonly ILoggingAdapter logger = Logging.GetLogger(Context);

        public UserService()
        {

            Receive<GetUserDetail>(c =>
            {
                _log.Debug("received GetUserDetail xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                logger.Debug("received GetUserDetail xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");

                //no receive of any kind in testactor
                var testProps = Context.DI().Props<TestActor>();
                var test = Context.ActorOf(testProps);
                test.Forward(c);

                //this will send properly
                var testActor = Context.ActorOf(Props.Create(() => new TestActor()));
                testActor.Forward(c);


                Sender.Tell(new UserDetailResult("Carlo", "DiGiacomo"));
            });

            ReceiveAny(c =>
            {
                _log.Debug($"received {c}");
            });
        }
    }
}
