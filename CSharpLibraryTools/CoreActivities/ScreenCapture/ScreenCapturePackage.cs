using Autofac;

namespace CoreActivities.ScreenCapture
{
    public class ScreenCapturePackage : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ScreenCaptureAdapter>().As<IScreenCapture>()
                   .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
