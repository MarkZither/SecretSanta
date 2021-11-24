using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SecretSanta.Models;

using StackExchange.Profiling;

namespace SecretSanta.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {

        private readonly IParticipantRepository ParticipantRepository;

        public AdminController(IParticipantRepository participantRepository)
        {
            ParticipantRepository = participantRepository;
        }

        [Authorize]
        [HttpGet(Name = "GetAllParticipantsUsingGet")]
        public async Task<IEnumerable<Participant>> List()
        {
            using (MiniProfiler.Current.Step("Getting List of Participants"))
            {
                return ParticipantRepository.GetAll();
            }
        }
    }
}
