using FluentEmail.Core;
using FluentEmail.Core.Defaults;
using FluentEmail.Core.Models;
using FluentEmail.Mailgun;
using FluentEmail.Razor;
using Microsoft.Extensions.Options;
using RazorLight;
using RazorLight.Compilation;

using SecretSanta.MobileAppService.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.MobileAppService.Services
{
    public class MailGunService : IEmailService
    {
        private MailGunOptions _options { get; set; }
        public MailGunService(IOptions<MailGunOptions> config)
        {
            _options = config.Value;
        }
        public async Task<SendResponse> Send(string toEmail, string toName, string givePresentTo)
        {
            var sender = new MailgunSender(
                _options.Domain, // Mailgun Domain
                _options.APIKey, // Mailgun API Key
                MailGunRegion.EU
            );

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();
            //string subject = $"Dein Wichtelpartner ist...";
            string subject = $"Your secret santa selection";

            IFluentEmail email = null;
            try
            {
                email = Email
                   .From("santa@secretsanta.mark-burton.com")
                   .To(toEmail, toName)
                   .Subject(subject)
                   .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/EmailTemplate.cshtml", new { Name = givePresentTo, GifterName = toName });
            }
            catch (TemplateCompilationException tex)
            {
                string body = $"your secret santa present is for {givePresentTo}";

                email = Email
                   .From("santa@secretsanta.mark-burton.com")
                   .To(toEmail, toName)
                   .Subject(subject)
                   .UsingTemplateEngine(new ReplaceRenderer())
                   .Body(string.Format("Hello {0}, <br /> {1} ", toName, body));
                string x = tex.Message;
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }

            var response = await email.SendAsync();

            return response;
        }
    }
}
