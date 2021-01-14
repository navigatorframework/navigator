using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Navigator.Abstractions;
using Scrutor;

namespace Navigator
{
    /// <summary>
    /// Helper functions for configuring navigator services.
    /// </summary>
    public class NavigatorBuilder
    {
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
        /// Creates a new instance of <see cref="NavigatorBuilder"/>.
        /// </summary>
        /// <param name="options">The <see cref="NavigatorOptions"/> to use.</param>
        /// <param name="services">The <see cref="IServiceCollection"/> to attach to.</param>

        public NavigatorBuilder(Action<NavigatorOptions> options, IServiceCollection services)
        {
            Options = new NavigatorOptions();
            options.Invoke(Options);
            
            Services = services;

            services.AddSingleton(Options);
        }

        public void RegisterOrReplaceOptions()
        {
            Services.Replace(ServiceDescriptor.Singleton<INavigatorOptions>(Options));
        }
    }
}