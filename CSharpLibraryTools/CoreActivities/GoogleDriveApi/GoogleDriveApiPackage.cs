using Autofac;

namespace CoreActivities.GoogleDriveApi
{
    public class GoogleDriveApiPackage : Module
    {
        private readonly string _authfilePath;
        private readonly string _directoryId;

        public GoogleDriveApiPackage(string authfilePath,
            string directoryId)
        {
            _authfilePath = authfilePath;
            _directoryId = directoryId;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GoogleDriveApiManagerAdapter>().As<IGoogleDriveApiManager>()
                    .WithParameter("authfilePath", _authfilePath)
                    .WithParameter("directoryId", _directoryId)
                    .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
