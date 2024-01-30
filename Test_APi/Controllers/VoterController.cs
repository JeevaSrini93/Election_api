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
       //insert voter details
        public async Task<IActionResult> insertSeat(string VoterIdNumber, string password, string Name, string Address)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPatch]
        //approve the voter
        public async Task<IActionResult> update(int VoterId, bool approve)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }
        [HttpGet]

        public async Task<IActionResult> get()
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        //to get the voter validate
        [HttpGet("single")]

        public async Task<IActionResult> getSingle(string VoterIdNumber, string password)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }
    }
}
