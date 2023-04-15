using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
using SecretSanta.MobileAppService.Options;

namespace SecretSanta
{
    /// <summary>
    /// SecretSanta's options configuration, specific to the default SecretSanta application.
    /// Don't use this if you are embedding SecretSanta into your own custom ASP.NET Core application.
    /// </summary>
    public class ConfigureSantaOptions
        : IConfigureOptions<CorsOptions>
        , IConfigureOptions<FormOptions>
        , IConfigureOptions<ForwardedHeadersOptions>
        , IConfigureOptions<IISServerOptions>
        , IValidateOptions<SantaOptions>
    {
        public const string CorsPolicy = "AllowAll";

        private static readonly HashSet<string> ValidDomains
            = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "AzureTable",
            };

        private static readonly HashSet<string> ValidStorageTypes
            = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "AliyunOss",
                "AwsS3",
                "AzureBlobStorage",
                "Filesystem",
                "GoogleCloud",
                "Null",
            };

        private static readonly HashSet<string> ValidSearchTypes
            = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "AzureSearch",
                "Database",
                "Null",
            };

        public void Configure(CorsOptions options)
        {
            // TODO: Consider disabling this on production builds.
            options.AddPolicy(
                CorsPolicy,
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        }

        public void Configure(FormOptions options)
        {
            options.MultipartBodyLengthLimit = int.MaxValue;
        }

        public void Configure(ForwardedHeadersOptions options)
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

            // Do not restrict to local network/proxy
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        }

        public void Configure(IISServerOptions options)
        {
            options.MaxRequestBodySize = 262144000;
        }

        public ValidateOptionsResult Validate(string name, SantaOptions options)
        {
            var failures = new List<string>();

            if (options.MailGun == null)
            {
                failures.Add($"The '{nameof(SantaOptions.MailGun)}' config is required");
            }

            if (!ValidDomains.Contains(options.MailGun?.Domain))
            {
                failures.Add(
                    $"The '{nameof(SantaOptions.MailGun)}:{nameof(MailGunOptions.Domain)}' config is invalid. " +
                    $"Allowed values: {string.Join(", ", ValidDomains)}");
            }

            if (failures.Any())
            {
                return ValidateOptionsResult.Fail(failures);
            }

            return ValidateOptionsResult.Success;
        }
    }
}
