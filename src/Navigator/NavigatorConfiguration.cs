using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Navigator
{
    /// <summary>
    /// Helper functions for configuring navigator services.
    /// </summary>
    public class NavigatorConfiguration
    {
        public NavigatorProviderConfiguration WithProvider { get; protected set; }

        /// <summary>
        /// Gets the <see cref="NavigatorOptions"/> that are being used.
        /// </summary>
        /// <value>
        /// The <see cref="NavigatorOptions"/> 
        /// </value>
        public NavigatorOptions Options { get; private set; }
        
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> services are attached to.
        /// </summary>
        /// <value>
        /// The <see cref="IServiceCollection"/> services are attached to.
        /// </value>
        public IServiceCollection Services { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="NavigatorConfiguration"/>.
        /// </summary>
        /// <param name="options">The <see cref="NavigatorOptions"/> to use.</param>
        /// <param name="services">The <see cref="IServiceCollection"/> to attach to.</param>

        public NavigatorConfiguration(Action<NavigatorOptions> options, IServiceCollection services)
        {
            Options = new NavigatorOptions();
            options.Invoke(Options);
            
            Services = services;

            services.AddSingleton(Options);
        }
        

        public void RegisterOrReplaceOptions()
        {
            Services.Replace(ServiceDescriptor.Singleton<NavigatorOptions>(Options));
        }
    }

    public class NavigatorProviderConfiguration
    {
        private readonly NavigatorConfiguration _navigatorConfiguration;

        public NavigatorProviderConfiguration(NavigatorConfiguration navigatorConfiguration)
        {
            _navigatorConfiguration = navigatorConfiguration;
        }

        public NavigatorConfiguration Provider(Action<NavigatorOptions>? optionsAction, Action<IServiceCollection>? servicesAction)
        {
            optionsAction?.Invoke(_navigatorConfiguration.Options);
            
            servicesAction?.Invoke(_navigatorConfiguration.Services);

            return _navigatorConfiguration;
        }
    }
}