using Autofac;
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
            var authFilePath = AppSettingsInfo.CreateGoogleDriveAuthFile(AppSettingsInfo.GetCurrentValue<string>("AuthFileName"));
            var directoryId = AppSettingsInfo.GetCurrentValue<string>("DirectoryId");

            var builder = new ContainerBuilder();

            //Registering packages
            builder.RegisterType<Application>();
            builder.RegisterModule(new ActiveProgramPackage());
            builder.RegisterModule(new BrowserActivityPackage());
            builder.RegisterModule(new DirectoryManagerPackage());
            builder.RegisterModule(new EgmaCvPackage());
            builder.RegisterModule(new FileManagerPackage());
            builder.RegisterModule(new GoogleDriveApiPackage(authFilePath, directoryId));
            builder.RegisterModule(new RunningProgramPackage());
            builder.RegisterModule(new ScreenCapturePackage());

            //Registering implementations
            builder.RegisterType<ActiveProgramImp>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<BrowseActivityImp>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<DirectoryManagerImp>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<EgmaCvImp>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<FileManagerImp>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<GoogleDriveApiImp>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<RunningProgramImp>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<ScreenCaptureImp>()
                   .InstancePerLifetimeScope();

            return builder.Build();
        }

        static async Task Main(string[] args)
        {
            await CompositionRoot().Resolve<Application>().Run();
        }
    }
}
