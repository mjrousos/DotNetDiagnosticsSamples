using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TargetApp.Services;

namespace TargetApp.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Add services
            builder.RegisterType<GoodWorker>()
                .SingleInstance();
            builder.RegisterType<BadWorker>()
                .SingleInstance();
        }
    }
}