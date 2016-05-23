namespace StoryQ.Converter.Wpf.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Deployment.Application;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using StoryQ.Converter.Wpf.Services.Designtime;
    using StoryQ.Converter.Wpf.Services.Runtime;

    /// <summary>
    /// Looks up services (powered by a mini DI container)
    /// </summary>
    public class ServiceLocator
    {
        private static readonly Container container;

        static ServiceLocator()
        {
            container = new Container();

            
            container.Register<IErrorhandler, PromptingErrorhandler>().AsSingleton();
            container.Register<IFileSavingService, FileSavingService>().AsSingleton();

            // Configure our services:
            if (DesignerProperties.GetIsInDesignMode(Application.Current.MainWindow))
            {
                container.Register<ILanguagePackProvider, DesigntimeLanguagePackProvider>();
                
            }
            else
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    container.Register<ILanguagePackProvider, ClickOnceLanguagePackProvider>();
                }
                else
                {
                    container.Register<ILanguagePackProvider, LocalLanguagePackProvider>();
                }
            }
           
        }

        public static T Resolve<T>() where T : class => container.Resolve<T>();

        public static T Resolve<T>(string name) where T : class => container.Resolve<T>(name);

        /// <summary>
        /// See http://www.robfe.com/2008/09/build-yourself-a-portable-dependency-injection-container/
        /// </summary>
        public class Container
        {
            private readonly Dictionary<string, Func<object>> services = new Dictionary<string, Func<object>>();
            private readonly Dictionary<Type, string> serviceNames = new Dictionary<Type, string>();

            public DependencyManager RegisterSelf<T>() => this.Register<T, T>();

            public DependencyManager Register<S, C>() where C : S => this.Register<S, C>(Guid.NewGuid().ToString());

            public DependencyManager Register<S, C>(string name) where C : S
            {
                if (!this.serviceNames.ContainsKey(typeof(S)))
                {
                    this.serviceNames[typeof(S)] = name;
                }
                return new DependencyManager(this, name, typeof(C));
            }

            public T Resolve<T>(string name) where T : class => (T)this.services[name]();

            public T Resolve<T>() where T : class => this.Resolve<T>(this.serviceNames[typeof(T)]);

            public class DependencyManager
            {
                private readonly Container container;
                private readonly Dictionary<string, Func<object>> args;
                private readonly string name;

                internal DependencyManager(Container container, string name, Type type)
                {
                    this.container = container;
                    this.name = name;

                    ConstructorInfo c = type.GetConstructors().First();
                    this.args = c.GetParameters()
                        .ToDictionary<ParameterInfo, string, Func<object>>(
                            x => x.Name,
                            x => (() => container.services[container.serviceNames[x.ParameterType]]())
                        );

                    container.services[name] = () => c.Invoke(this.args.Values.Select(x => x()).ToArray());
                }

                public DependencyManager AsSingleton()
                {
                    object value = null;
                    Func<object> service = this.container.services[this.name];
                    this.container.services[this.name] = () => value ?? (value = service());
                    return this;
                }

                public DependencyManager WithDependency(string parameter, string component)
                {
                    this.args[parameter] = () => this.container.services[component]();
                    return this;
                }

                public DependencyManager WithValue(string parameter, object value)
                {
                    this.args[parameter] = () => value;
                    return this;
                }
            }
        }

    }
}
