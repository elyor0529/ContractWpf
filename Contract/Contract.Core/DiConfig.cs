using System;
using System.Collections.Generic;
using System.Configuration;
using Contract.Core.Extensions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Contract.Core
{
    public class DiConfig
    {
        private DiConfig()
        {
            Container = new UnityContainer();
            ConfigurationManager.GetSection("unity")
                .TryAs<UnityConfigurationSection>(x => x.Configure(Container, "container"));
        }

        private IUnityContainer Container { get;  set; }
		
        public static DiConfig Instance
        {
            get { return Nested.SingleInstance; }
        }

        public static T Resolve<T>(string name)
        {
            return Instance.Container.Resolve<T>(name);
        }

        public static T Resolve<T>()
        {
            return Instance.Container.Resolve<T>();
        }

        public static void Register<T, TDescendant>() where TDescendant : T
        {
            Instance.Container.RegisterType<T, TDescendant>();
        }

        public static void Register<T, TDescendant>(string name) where TDescendant : T
        {
            Instance.Container.RegisterType<T, TDescendant>(name);
        }

        public static void Register<T, TDescendant>(Func<IUnityContainer, object> method) where TDescendant : T
        {
            Instance.Container.RegisterType<T, TDescendant>(new InjectionFactory(method));
        }

        public static void RegisterInstance<T>(T instance)
        {
            Instance.Container.RegisterInstance(instance);
        }

        public static void RegisterInstance<T>(T instance, string name)
        {
            Instance.Container.RegisterInstance(name, instance);
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            return Instance.Container.ResolveAll<T>();
        }

        private class Nested
        {
            internal static readonly DiConfig SingleInstance = new DiConfig();

            static Nested()
            {
            }
        }
    }
}