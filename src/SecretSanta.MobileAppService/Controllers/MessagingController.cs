using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.MobileAppService.Services;
using SecretSanta.Models;

namespace SecretSanta.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        private readonly IParticipantRepository ParticipantRepository;
        private readonly IHistoryRepository HistoryRepository;
        private readonly IEmailService EmailService;

        public MessagingController(IParticipantRepository participantRepository, IEmailService emailService, IHistoryRepository historyRepository)
        {
            EmailService = emailService;
            ParticipantRepository = participantRepository;
            HistoryRepository = historyRepository;
        }

        // POST: api/Email
        [HttpPost(Name = "SendMatchUsingPost")]
        public async Task<IActionResult> Post([FromBody] Message model)
        {
            var gifter = ParticipantRepository.Get(model.GifterId);
            var recipient = ParticipantRepository.Get(model.RecipientId);

            var result = await EmailService.Send(gifter.Email, gifter.Name, recipient.Name);

            if (result.Successful){
                //select strftime('%Y-%m-%d %H:%M:%S',MatchDate/10000000 - 62135596800,'unixepoch') from History
                HistoryRepository.Add(new History() { GifterId = model.GifterId, RecipientId = model.RecipientId, MatchDate = DateTime.Now });
                return new OkResult();
            }
            return new BadRequestObjectResult(result);
        }
    }
}
