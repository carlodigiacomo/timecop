using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.Infrastructure
{
    public static class ServiceReferences
    {
        public static IActorRef TimecopService = ActorRefs.Nobody;
        public static IActorRef UserService = ActorRefs.Nobody;
    }
}
