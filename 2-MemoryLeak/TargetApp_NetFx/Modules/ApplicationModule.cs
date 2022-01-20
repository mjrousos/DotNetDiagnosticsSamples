using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TargetApp;

namespace TargetApp.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Add services
            builder.RegisterType<ProfilePictureService>();
            builder.RegisterType<NativeProfilePictureService>();

            // Add caches
            builder.RegisterType<ProfilePictureCacheBad>()
                .SingleInstance();
            builder.RegisterType<ProfilePictureCacheGood>()
                .SingleInstance();
        }
    }
}