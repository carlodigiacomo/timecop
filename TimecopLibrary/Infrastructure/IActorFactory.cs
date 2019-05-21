using System;
using System.Collections.Generic;
using Akka.Actor;
using System.Text;

namespace CommonLibrary.Infrastructure
{
    public interface IActorFactory
    {
        IActorRef CreateActor<T>() where T : ActorBase;
        IActorRef CreateActorByName<T>(string name) where T : ActorBase;
        IActorRef FindTopLevelActor<T>() where T : ActorBase;
        ActorSystem ActorSystem();
    }
}
