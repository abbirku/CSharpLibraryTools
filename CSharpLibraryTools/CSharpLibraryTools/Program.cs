﻿using Autofac;
using CoreActivities.ActiveProgram;
using CoreActivities.BrowserActivity;
using CoreActivities.DirectoryManager;
using CoreActivities.EgmaCV;
using CoreActivities.FileManager;
using CoreActivities.GoogleDriveApi;
using CoreActivities.RunningPrograms;
using CoreActivities.ScreenCapture;
using System.Threading.Tasks;

namespace CSharpLibraryTools
{
    class Program
    {
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>();
            builder.RegisterModule(new ActiveProgramPackage());
            builder.RegisterModule(new BrowserActivityPackage());
            builder.RegisterModule(new DirectoryManagerPackage());
            builder.RegisterModule(new EgmaCvPackage());
            builder.RegisterModule(new FileManagerPackage());
            builder.RegisterModule(new GoogleDriveApiPackage());
            builder.RegisterModule(new RunningProgramPackage());

            builder.RegisterType<ScreenCaptureAdapter>().As<IScreenCapture>()
                   .InstancePerLifetimeScope();

            return builder.Build();
        }

        static async Task Main(string[] args)
        {
            await CompositionRoot().Resolve<Application>().Run();
        }
    }
}
