using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_APi.model;
using Test_APi.Service;
using Test_APi.ServiceMpSeatService;

namespace Test_APi.Controllers
{

    [Route("api/Candidate")]
    [ApiController]
    [Authorize(Roles = "ElectionCommissioner")]

    public class CandidateController : ControllerBase
    {
        ICandidateService ICandidateService;
        public CandidateController(ICandidateService cadService)
        {
            ICandidateService = cadService;
        }
        [HttpPost]
        // Action to decrease MP seats in a state
        public async Task<IActionResult> insertSeat(string name, int PartyId, int StateId)
        {
            var cc = await ICandidateService.insert(new Candidate() { Name = name, PartyId = PartyId, StateId = StateId });
            if (cc != null)
            {
                return Ok(cc);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]

        public async Task<IActionResult> get()
        {
            var cc = await ICandidateService.get();
            if (cc != null)
            {
                return Ok(cc);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
