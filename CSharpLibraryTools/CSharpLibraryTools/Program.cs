using Autofac;
using CoreActivities.ActiveProgram;
using CoreActivities.BrowserActivity;
using CoreActivities.DirectoryManager;

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

            return builder.Build();
        }

        static void Main(string[] args)
        {
            CompositionRoot().Resolve<Application>().Run();
        }
    }
}
