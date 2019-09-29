using System;
using System.Threading.Tasks;
using Xunit;

namespace SecretSanta.MobileAppService.Test
{
    public class EmailTest
    {
        [Fact]
        public async Task SendReturnsString()
        {
            var emailService = new SecretSanta.MobileAppService.Services.MailGunService(new Config.MailGunConfiguration() {Domain = "", APIKey = "" });
            var result = await emailService.Send("mark.burton@zither-it.co.uk", "Mark", "Caitriona");
            Assert.True(result.Successful);
        }

        [Fact]
        public async Task SendToCaitrionaReturnsString()
        {
            var emailService = new SecretSanta.MobileAppService.Services.MailGunService(new Config.MailGunConfiguration() { Domain = "", APIKey = "" });
            var result = await emailService.Send("caitrionaflynn@yahoo.co.uk", "Caitriona", "Clive");
            Assert.True(result.Successful);
        }
    }
}
