using Autofac;

namespace CoreActivities.EgmaCV
{
    public class EgmaCvPackage : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EgmaCvAdapter>().As<IEgmaCv>()
                   .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
