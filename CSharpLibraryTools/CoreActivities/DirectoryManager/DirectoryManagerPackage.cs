using Autofac;

namespace CoreActivities.DirectoryManager
{
    public class DirectoryManagerPackage: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DirectoryManagerAdapter>().As<IDirectoryManager>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<DirectoryManagerAdaptee>()
                   .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
