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
        [HttpPost]
       
        public async Task<IActionResult> insertSeat(List<int> candidateIds)
        {
            var cc = await IVotingSystemService.insert(new VotingSystem() { Candidates=await ICandidateService.get(candidateIds)});
            if (cc != null)
            {
                return Ok(cc);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("vote")]

        public async Task<IActionResult> insertvote(int voterId,int CandidateId,int VotingSystemId)
        {
            var cc = await IVotingSystemService.vote(new Vote() {VoteId=voterId,CandidateId=CandidateId,VotingSystemId= VotingSystemId });
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
      
        [HttpGet("single")]

        public async Task<IActionResult> getSingle(int VotingSystemId)
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
        [HttpPatch]
        public async Task<IActionResult> update(VotingSystem votingSystem)
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
        [HttpGet("Result")]

        public async Task<IActionResult> GetResule(int votingSystemId)
        {
            var cc = await IVotingSystemService.GetResule( votingSystemId);
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
