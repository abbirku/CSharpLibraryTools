using Autofac;

namespace CoreActivities.BrowserActivity
{
    public class BrowserActivityPackage : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BrowserActivityAdapter>().As<IBrowserActivity>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<BrowserActivityAdaptee>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<BrowserActivityEnumAdaptee>()
                   .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
