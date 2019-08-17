using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using LeaderAnalytics.AdaptiveClient.Utilities;

namespace LeaderAnalytics.AdaptiveClient
{
    public static class RegistrationHelperExtensions
    {
        /// <summary>
        /// Registers a ServiceManifest, a class that exposes a property for each type of service associated with an API.  A ServiceManifest is keyed to an EndPointType, an API_Name, and a ProviderName.
        /// </summary>
        /// <typeparam name="TService">Type of service manifest. Must derive from ServiceManifestFactory.</typeparam>
        /// <typeparam name="TInterface">Interface that describes TService.</typeparam>
        /// <param name="helper">An instance of RegistrationHelper.</param>
        /// <param name="endPointType">Describes the technology or protocol used by an IEndPointConfiguration.</param>
        /// <param name="apiName">An API_Name to use as a key.  Must match the API_Name of one or more IEndPointConfiguration objects. 
        /// API_Name is an arbitrary name given to the collection of services exposed by an API.  
        /// The name of a database or a domain name are examples of names that might also be used as an API_Name. </param>
        /// <param name="providerName">ProviderName typically represents some implementation of technology such as a DBMS platform.
        /// Examples might be: MSSQL, MySQL, SQLite, etc.</param>
        /// <returns></returns>
        public static RegistrationHelper RegisterServiceManifest<TService, TInterface>(this RegistrationHelper helper, string endPointType, string apiName, string providerName) where TService : ServiceManifestFactory
        {
            if (string.IsNullOrEmpty(endPointType))
                throw new ArgumentNullException(nameof(endPointType));
            if (string.IsNullOrEmpty(apiName))
                throw new ArgumentNullException(nameof(apiName));
            if (string.IsNullOrEmpty(providerName))
                throw new ArgumentNullException(nameof(providerName));

            helper.RegisterPerimeter(typeof(TInterface), apiName);

            helper.Builder.Register<Func<string, string, TInterface>>(c => {
                ILifetimeScope cxt = c.Resolve<ILifetimeScope>();
                return (ept, pn) => new ResolutionHelper(cxt).ResolveClient<TInterface>(ept, pn);
            }).PropertiesAutowired();

            helper.Builder.RegisterType<TService>().Keyed<TInterface>(endPointType + providerName).PropertiesAutowired().InstancePerLifetimeScope();
            helper.Builder.RegisterType<TService>().As<TInterface>().PropertiesAutowired().InstancePerLifetimeScope();
            return helper;
        }
    }
}
