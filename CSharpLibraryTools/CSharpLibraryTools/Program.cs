using Autofac;
using CoreActivities.ActiveProgram;
using CoreActivities.BrowserActivity;
using CoreActivities.DirectoryManager;
using CoreActivities.EgmaCV;
using CoreActivities.FileManager;
using System.Threading.Tasks;

namespace CSharpLibraryTools
{
    class Program
    {
        /// <summary>
        /// Need to only inject the packages inside CompositionRoot
        /// </summary>
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>();
            builder.RegisterModule(new ActiveProgramPackage());
            builder.RegisterModule(new BrowserActivityPackage());
            builder.RegisterModule(new DirectoryManagerPackage());
            builder.RegisterModule(new EgmaCvPackage());
            builder.RegisterModule(new FileManagerPackage());

            return builder.Build();
        }

        static async Task Main(string[] args)
        {
            await CompositionRoot().Resolve<Application>().Run();
        }
    }
}
