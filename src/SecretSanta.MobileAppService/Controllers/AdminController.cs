using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SecretSanta.MobileAppService;
using SecretSanta.Models;

using StackExchange.Profiling;

namespace SecretSanta.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IParticipantRepository ParticipantRepository;

        public AdminController(IParticipantRepository participantRepository, ILogger<AdminController> logger)
        {
            _logger = logger;
            ParticipantRepository = participantRepository;
        }

        [Authorize]
        [DisableCors]
        [HttpGet(Name = "GetAllParticipantsUsingGet")]
        public async Task<IEnumerable<Participant>> List()
        {
            using (MiniProfiler.Current.Step("Getting List of Participants"))
            {
                _logger.LogDebug(EventIds.TestingDebugLogging, "Testing logging in controller");
                return ParticipantRepository.GetAll();
            }
        }
    }
}
