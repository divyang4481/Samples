using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Conventions;
using Nancy.Diagnostics;

namespace NancyApplication1
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
        }

        protected override void RequestStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
        }

        protected override IEnumerable<Func<System.Reflection.Assembly, bool>> AutoRegisterIgnoredAssemblies
        {
            get
            {
                return base.AutoRegisterIgnoredAssemblies;
            }
        }

        protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Static", @"Static"));

            //nancyConventions.StaticContentsConventions.Add((context, path) =>
            //    {
                    
            //    });
        }

        protected override Nancy.Diagnostics.DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get { return new DiagnosticsConfiguration {Password = @"pass"}; }
        }
    }
}