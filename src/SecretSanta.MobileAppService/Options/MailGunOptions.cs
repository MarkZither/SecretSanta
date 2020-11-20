using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.MobileAppService.Options
{
    public class MailGunOptions : IValidatableObject
    {
        public const string MailGun = "MailGun";

        /// <summary>
        /// If true, emails will be sent
        /// </summary>
        public bool Enabled { get; set; }

        [Required]
        public string Domain { get; set; }
        public string APIKey { get; set; }

        public void Configure(MailGunOptions options)
        {
            throw new NotImplementedException();
        }

        // https://github.com/dotnet/runtime/issues/38491
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            const string helpUrl = "https://markzither.github.io/BaGet/quickstart/azure/#azure-blob-storage";

            if (Enabled && string.IsNullOrEmpty(Domain))
            {
                yield return new ValidationResult(
                    $"The {nameof(Domain)} configuration is required if mirroring is enabled. See {helpUrl}",
                    new[] { nameof(Domain) });
            }

            if (Enabled && string.IsNullOrEmpty(APIKey))
            {
                yield return new ValidationResult(
                    $"The {nameof(APIKey)} configuration is required if mirroring is enabled. See {helpUrl}",
                    new[] { nameof(APIKey) });
            }
        }
    }
}
