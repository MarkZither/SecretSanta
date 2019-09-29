using FluentEmail.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.MobileAppService.Services
{
    public interface IEmailService
    {
        Task<SendResponse> Send(string toEmail, string toName, string givePresentTo);
    }
}
