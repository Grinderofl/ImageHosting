using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;

namespace ImageHosting.Core.Modules
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterTypes(
                ThisAssembly.GetExportedTypes()
                    .Where(x => x.Namespace != null && x.Namespace.EndsWith("Core"))
                    .ToArray());
        }
    }
}