using Akka.Actor;
using Akka.Event;
using CommonLibrary.Infrastructure;
using CommonLibrary.User.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.User.Actors
{
    public class TestActor : ReceiveActor
    {
        public TestActor()
        {
            Receive<GetUserDetail>(c =>
            {
                var junk = c.UserId;
                //_log.Debug("received GetUserDetail vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv");
            });

            ReceiveAny(c =>
            {
                var junk = "sdsd";
                
            });

        }
    }
}
