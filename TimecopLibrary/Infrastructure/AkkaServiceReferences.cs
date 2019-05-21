using Akka.Actor;
using CommonLibrary.Timecop.Actors;
using CommonLibrary.User.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.Infrastructure
{
    public class AkkaServiceReferences : IAkkaServiceReferences
    {
        public IActorRef GetActor(string name)
        {
            switch (name)
            {
                case TopLevelActorNames.TimecopService:
                    return ServiceReferences.TimecopService;
                case TopLevelActorNames.UserService:
                    return ServiceReferences.UserService;
                default:
                    break;
            }
            throw new NotImplementedException();
        }

        public IActorRef GetActor<T>()
        {
            var @switch = new Dictionary<Type, IActorRef> {
                { typeof(TimecopService), ServiceReferences.TimecopService },
                { typeof(UserService), ServiceReferences.UserService }
            };

            return @switch.ContainsKey(typeof(T)) ? @switch[typeof(T)] : null;
        }
    }
}
