using System;
using Microsoft.AspNetCore.Mvc;

using SecretSanta.Models;

namespace SecretSanta.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
	{

		private readonly IParticipantRepository ParticipantRepository;

		public ItemController(IParticipantRepository participantRepository)
		{
            ParticipantRepository = participantRepository;
		}

		[HttpGet]
		public IActionResult List()
		{
			return Ok(ParticipantRepository.GetAll());
		}

		[HttpGet("{Id}")]
		public Participant GetItem(string id)
		{
            Participant participant = ParticipantRepository.Get(id);
			return participant;
		}

		[HttpPost]
		public IActionResult Create([FromBody]Participant participant)
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

		[HttpPut]
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

		[HttpDelete("{Id}")]
		public void Delete(string id)
		{
			ParticipantRepository.Remove(id);
		}
	}
}
