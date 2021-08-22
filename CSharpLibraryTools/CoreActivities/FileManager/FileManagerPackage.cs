using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreActivities.FileManager
{
    public class FileManagerPackage : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileAdapter>().As<IFile>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<FileInfoAdapter>().As<IFileInfo>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<FileManagerAdapter>().As<IFileManager>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<FileStreamAdapter>().As<IFileStream>()
                   .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
