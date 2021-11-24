using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using SecretSanta.Models;

using StackExchange.Profiling;

namespace SecretSanta.Controllers
{
    [Route("api/[controller]")]
    public class ParticipantController : Controller
    {

        private readonly IParticipantRepository ParticipantRepository;

        public ParticipantController(IParticipantRepository participantRepository)
        {
            ParticipantRepository = participantRepository;
        }

        [HttpGet(Name = "GetParticipantsUsingGet")]
        public async Task<IEnumerable<Participant>> List()
        {
            using (MiniProfiler.Current.Step("Getting List of Participants"))
            {
                return ParticipantRepository.GetAllForLoggedInUser(new Guid("71A32736-7B5B-4020-86F4-CCBF529802CE"));
            }
        }

        [HttpGet("{id}", Name = "GetParticipantByIdUsingGet")]
        public Participant GetItem(int id)
        {
            Participant participant = ParticipantRepository.Get(id);
            return participant;
        }

        [HttpPost(Name = "CreateParticipantUsingPost")]
        public IActionResult Create([FromBody] Participant participant)
        {
            try
            {
                if (participant == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }

                ParticipantRepository.Add(participant);

            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return Ok(participant);
        }

        [HttpPut(Name = "UpdateParticipantUsingPut")]
        public IActionResult Edit([FromBody] Participant participant)
        {
            try
            {
                if (participant == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }
                ParticipantRepository.Update(participant);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteParticipantUsingDelete")]
        public void Delete(int id)
        {
            ParticipantRepository.Remove(id);
        }
    }
}
