using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_APi.model;
using Test_APi.Service;
using Test_APi.ServiceMpSeatService;
namespace Test_APi.Controllers
{
    [Route("api/vote")]
    [ApiController]

    public class VoterController : ControllerBase
    {
        IVoterService IVoterService;
        public VoterController(IVoterService voteService)
        {
            IVoterService = voteService;
        }
        [HttpPost]
        // Action to decrease MP seats in a state
        public async Task<IActionResult> insertSeat(string VoterIdNumber, string password, string Name, string Address)
        {
            var cc = await IVoterService.insert(new Voter() { VoterIdNumber = VoterIdNumber, Address = Address, Name = Name, password = password });
            if (cc != null)
            {
                return Ok(cc);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPatch]
        public async Task<IActionResult> update(int VoterId, bool approve)
        {
            var cc = await IVoterService.approve(VoterId, approve);
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
            var cc = await IVoterService.get();
            if (cc != null)
            {
                return Ok(cc);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("single")]

        public async Task<IActionResult> getSingle(string VoterIdNumber, string password)
        {
            var cc = await IVoterService.getSingle(VoterIdNumber, password);
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
