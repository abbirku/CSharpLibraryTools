using Autofac;

namespace CoreActivities.GoogleDriveApi
{
    public class GoogleDriveApiPackage : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GoogleDriveApiManagerAdapter>().As<IIGoogleDriveApiManager>()
                   .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
