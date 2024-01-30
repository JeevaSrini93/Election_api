using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_APi.model;
using Test_APi.Service;

namespace Test_APi.Controllers
{
    [Route("api/votesystem")]
    [ApiController]
   // [Authorize(Roles = "ElectionCommissioner")]
    public class VotingSystemController : ControllerBase
    {
        IVotingSystemService IVotingSystemService;
        ICandidateService ICandidateService;
        public VotingSystemController(IVotingSystemService voteService, ICandidateService candidateService)
        {
            IVotingSystemService = voteService;
            ICandidateService = candidateService;
        }

        //insert candiate record
        [HttpPost]
       
        public async Task<IActionResult> insertcandiate(List<int> candidateIds)
        {
            try
            {
                var cc = await IVotingSystemService.insert(new VotingSystem() { Candidates = await ICandidateService.get(candidateIds) });
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

     //insert vote record 
        [HttpPost("vote")]

        public async Task<IActionResult> insertvote(int voterId,int CandidateId,int VotingSystemId)
        {
            try
            {
                var cc = await IVotingSystemService.vote(new Vote() { VoteId = voterId, CandidateId = CandidateId, VotingSystemId = VotingSystemId });
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
        //get overall list
        [HttpGet]

        public async Task<IActionResult> get()
        {
            try
            {
                var cc = await IVotingSystemService.get();
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
      //get by the voting System
        [HttpGet("single")]

        public async Task<IActionResult> getSingle(int VotingSystemId)
        {
            try
            {
                var cc = await IVotingSystemService.getSingle(VotingSystemId);
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
        public async Task<IActionResult> update(VotingSystem votingSystem)
        {
            try
            {
                var cc = await IVotingSystemService.update(votingSystem);
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
        //to view the election result
        [HttpGet("Result")]

        public async Task<IActionResult> GetResule(int votingSystemId)
        {
            try
            {
                var cc = await IVotingSystemService.GetResule(votingSystemId);
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
