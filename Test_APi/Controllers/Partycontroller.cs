using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_APi.model;
using Test_APi.Service;
using Test_APi.ServiceMpSeatService;

namespace Test_APi.Controllers
{
   
    [Route("api/party")]
    [ApiController]
   // [Authorize(Roles = "ElectionCommissioner")]
    public class Partycontroller : ControllerBase
    {
        IPartyService IpartyService;
        ICandidateService IcandidateService;
        public Partycontroller(IPartyService partService, ICandidateService candidateService)
        {
            IpartyService = partService;
            IcandidateService= candidateService;
        }
        [HttpPost]
        // Action to decrease MP seats in a state
        public async Task<IActionResult> insertSeat(string name,string symbol ,List<int> candidateIds)
        {
            try
            {
                var cc = await IpartyService.insert(new Party() { Name = name, Symbol = symbol, Candidates = await IcandidateService.get(candidateIds) });
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
                var cc = await IpartyService.get();
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
