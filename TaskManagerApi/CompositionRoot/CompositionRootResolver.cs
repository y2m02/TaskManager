using System.Collections.Generic;
using System.Reflection;
using Autofac;
using TaskManagerApi.Models;
using TaskManagerApi.Repositories;
using Module = Autofac.Module;

namespace TaskManagerApi.CompositionRoot
{
    public class CompositionRootResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<TaskManagerContext>().InstancePerRequest();

            RegisterQueryServices(builder, typeof(StoreRepository).Assembly);
        }

        private static void RegisterQueryServices(ContainerBuilder builder, Assembly assembly)
        {
            var names = new List<string> {"Repository", "Adapter", "Service", "Factory", "Operation"};
            foreach (var name in names)
                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.Name.EndsWith(name))
                    .AsImplementedInterfaces();
        }
    }
}