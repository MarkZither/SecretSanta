using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SecretSanta.MobileAppService.Options;

namespace SecretSanta
{
    /// <summary>
    /// Validates BaGet's options, used at startup.
    /// </summary>
    public class ValidateStartupOptions : IValidateStartupOptions
    {
        private readonly IOptions<SantaOptions> _root;
        private readonly IOptions<MailGunOptions> _mailGun;
        private readonly ILogger<ValidateStartupOptions> _logger;

        public ValidateStartupOptions(
            IOptions<SantaOptions> root,
            IOptions<MailGunOptions> mailGun,
            ILogger<ValidateStartupOptions> logger)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
            _mailGun = mailGun ?? throw new ArgumentNullException(nameof(mailGun));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool Validate()
        {
            try
            {
                // Access each option to force validations to run.
                // Invalid options will trigger an "OptionsValidationException" exception.
                _ = _root.Value;
                _ = _mailGun.Value;

                return true;
            }
            catch (OptionsValidationException e)
            {
                foreach (var failure in e.Failures)
                {
                    _logger.LogError("{OptionsFailure}", failure);
                }

                _logger.LogError(e, "SecretSanta configuration is invalid.");
                return false;
            }
        }
    }
    public interface IValidateStartupOptions
    {
        bool Validate();
    }
}
