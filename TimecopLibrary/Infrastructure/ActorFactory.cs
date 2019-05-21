using Akka.Actor;
using Akka.DI.Core;
using CommonLibrary.Infrastructure;
using CommonLibrary.Timecop.Actors;
using CommonLibrary.User.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimecopLibrary.Infrastructure
{
    public class ActorFactory : IActorFactory
    {
        private ActorSystem _actorSystem;
        private IServiceProvider _serviceProvider;
        //private IActorRef _timecopServiceActor;
        //private IActorRef _userServiceActor;
        //private bool _actorsCreated = false;

        private IAkkaServiceReferences _akkaServiceReferences;

        public ActorFactory(ActorSystem actorSystem, IServiceProvider serviceProvider, IAkkaServiceReferences akkaServiceReferences)
        {
            _serviceProvider = serviceProvider;
            _actorSystem = actorSystem;
            _akkaServiceReferences = akkaServiceReferences;

            //if (_actorsCreated == false)
            //{
            //    _timecopServiceActor = CreateActorByName<TimecopService>(TopLevelActorNames.TimecopService);
            //    _userServiceActor = CreateActorByName<UserService>(TopLevelActorNames.UserService);
            //}
        }

        public IActorRef FindTopLevelActor<T>() where T : ActorBase
        {
            var actor = _akkaServiceReferences.GetActor<T>();
            if (actor != null) return actor;
            throw new Exception("Top level actor not found");
        }

        public IActorRef CreateActor<T>() where T : ActorBase
        {
            return _actorSystem.ActorOf(_actorSystem.DI().Props(typeof(T)));
        }

        public IActorRef CreateActorByName<T>(string name) where T : ActorBase
        {
            return _actorSystem.ActorOf(_actorSystem.DI().Props(typeof(T)), name);
        }

        public ActorSystem ActorSystem()
        {
            return _actorSystem;
        }

    }
}
