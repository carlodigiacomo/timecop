using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.Infrastructure
{
    public interface IAkkaServiceReferences
    {
        IActorRef GetActor(string name);
        IActorRef GetActor<T>();
    }
}
