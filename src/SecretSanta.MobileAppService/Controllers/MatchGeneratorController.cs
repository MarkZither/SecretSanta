using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SecretSanta.Models;

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SecretSanta.MobileAppService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MatchGeneratorController : ControllerBase {
        // POST: api/Email
        [HttpPost(Name = "GenerateMatchesUsingPost")]
        public async Task<IActionResult> PostAsync([FromBody] MatchGeneratorModel model) {
            var res = SecretSanta.Shared.SecretSantaGenerator.Generate(model.Participants, model.BannedPairs);
            
            return await Task.FromResult<OkResult>(Ok());
        }
    }

    public class MatchGeneratorModel {
        public IList<ParticipantDTO> Participants { get; set; }
        public List<KeyValuePair<ParticipantDTO, ParticipantDTO>> BannedPairs { get; set; }
    }
}
