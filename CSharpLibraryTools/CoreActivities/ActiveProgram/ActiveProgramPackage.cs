using Autofac;

namespace CoreActivities.ActiveProgram
{
    public class ActiveProgramPackage : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ActiveProgramAdapter>().As<IActiveProgram>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<ActiveProgramAdaptee>()
                   .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
