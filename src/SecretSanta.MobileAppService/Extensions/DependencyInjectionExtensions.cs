using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Linq;
using SecretSanta.MobileAppService.Options;

namespace SecretSanta.MobileAppService.Extensions
{
    public static partial class DependencyInjectionExtensions
    {
        /// <summary>
        /// Configures and validates options.
        /// </summary>
        /// <typeparam name="TOptions">The options type that should be added.</typeparam>
        /// <param name="services">The dependency injection container to add options.</param>
        /// <param name="key">
        /// The configuration key that should be used when configuring the options.
        /// If null, the root configuration will be used to configure the options.
        /// </param>
        /// <returns>The dependency injection container.</returns>
        public static IServiceCollection AddSantaServiceOptions<TOptions>(
            this IServiceCollection services,
            string key = null)
            where TOptions : class
        {
            services.AddSingleton<IValidateOptions<TOptions>>(new ValidateSantaOptions<TOptions>(key));
            services.AddSingleton<IConfigureOptions<TOptions>>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                if (key != null)
                {
                    config = config.GetSection(key);
                }

                return new BindOptions<TOptions>(config);
            });

            return services;
        }

        private static void AddConfiguration(this IServiceCollection services)
        {
            services.AddSantaServiceOptions<SantaOptions>();
            services.AddSantaServiceOptions<MailGunOptions>(nameof(MailGunOptions.MailGun));
        }

        public static IServiceCollection AddSantaServices(this IServiceCollection services)
        {
            services.AddConfiguration();
            services.TryAddSingleton<IValidateStartupOptions, ValidateStartupOptions>();

            return services;
        }

        public static IServiceCollection AddSantaHealthChecks(this IServiceCollection services, IConfiguration config)
        {
            var targetHost = "www.microsoft.com";
            var targetHostIpAddresses = Dns.GetHostAddresses(targetHost).Select(h => h.ToString()).ToArray();

            var targetHost2 = "localhost";
            var targetHost2IpAddresses = Dns.GetHostAddresses(targetHost2).Select(h => h.ToString()).ToArray();
            var maximumMemory = 104857600;

            services.AddHealthChecks()
                .AddDnsResolveHealthCheck(setup =>
                {
                    setup.ResolveHost(targetHost).To(targetHostIpAddresses)
                    .ResolveHost(targetHost2).To(targetHost2IpAddresses);
                }, tags: new string[] { "dns" }, name: "DNS Check")
                .AddPingHealthCheck(setup =>
                {
                    setup.AddHost("127.0.0.1", 5000);
                }, tags: new string[] { "ping" }, name: "Ping Check")
                .AddTcpHealthCheck(setup =>
                {
                    setup.AddHost("127.0.0.1", 1116);
                }, tags: new string[] { "tcp" }, name: "TCP port Check")
                .AddPrivateMemoryHealthCheck(maximumMemory
                , tags: new string[] { "privatememory" }, name: "PrivateMemory Check")
                .AddWorkingSetHealthCheck(maximumMemory
                , tags: new string[] { "workingset" }, name: "WorkingSet Check")
                .AddVirtualMemorySizeHealthCheck(maximumMemory
                , tags: new string[] { "virtualmemory" }, name: "VirtualMemory Check", failureStatus: HealthStatus.Degraded)
                .AddSqlServer(config.GetConnectionString("LoginServiceDb"), tags: new string[] { "sqlserver" })
                .AddIdentityServer(new Uri("http://localhost:7777"), tags: new string[] { "idsvr" });

            return services;
        }
    }
}
