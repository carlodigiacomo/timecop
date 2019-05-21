using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CommonLibrary.Infrastructure;
using CommonLibrary.Timecop.Actors;
using CommonLibrary.User.Actors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using NLog.Web;
using TimecopLibrary.Infrastructure;

namespace TimecopAPI
{
    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var logger = _loggerFactory.CreateLogger<Startup>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var actorSystem = ActorSystem.Create(Configuration["AppSettings:ActorSystemName"], ConfigurationLoader.Load());

            logger.LogInformation("actorsystem created");

            // Add Autofac
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance(actorSystem)
                          .ExternallyOwned();
            logger.LogInformation("register actorsystem");

            containerBuilder.RegisterType<ActorFactory>()
             .As<IActorFactory>()
             .SingleInstance();
            logger.LogInformation("register actor factory");

            //containerBuilder.RegisterType<WorkerService>().As<IWorkerService>();
            //containerBuilder.RegisterType<UserService>();

            containerBuilder.RegisterModule<DefaultModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            actorSystem.UseAutofac(container);

            ServiceReferences.TimecopService = actorSystem.ActorOf(Props.Create(() => new TimecopService()), TopLevelActorNames.TimecopService);
            ServiceReferences.UserService = actorSystem.ActorOf(Props.Create(() => new UserService()), TopLevelActorNames.UserService);

            logger.LogInformation("sending back autofac");
            return new AutofacServiceProvider(container);
        }

        public class DefaultModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<AkkaServiceReferences>().As<IAkkaServiceReferences>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //env.ConfigureNLog("nlog.config");
            //loggerFactory.AddNLog();
            ////add NLog.Web
            ////app.usenlog();

            app.UseHttpsRedirection();
            app.UseMvc();

            //lifetime.ApplicationStarted.Register(() =>
            //{
            //    app.ApplicationServices.GetService<ActorSystem>(); // start Akka.NET
            //});
            //lifetime.ApplicationStopping.Register(() =>
            //{
            //    app.ApplicationServices.GetService<ActorSystem>().Terminate().Wait();
            //});

        }
    }
}
