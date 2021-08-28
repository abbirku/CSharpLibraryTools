using Autofac;

namespace CoreActivities.RunningPrograms
{
    public class RunningProgramPackage : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RunningProgramAdapter>().As<IRunningPrograms>()
                   .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
