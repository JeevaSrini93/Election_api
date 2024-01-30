using Microsoft.EntityFrameworkCore;
using Test_APi.model;

namespace Test_APi.Service
{
    public class VotingSystemService : IVotingSystemService
    {
        private readonly ElectionManagerContext managerContext;
        public VotingSystemService(ElectionManagerContext electionManager)
        {
            this.managerContext = electionManager;
        }

        public async Task<List<VotingSystem>> get()
        {
            return await managerContext.VotingSystems.ToListAsync();
        }

        public async Task<List<Resule>> GetResule(int votingSystemId)
        {
            var query = from v in managerContext.Votes
                        where v.VotingSystemId == votingSystemId
                        group v by v.CandidateId into g
                        join c in managerContext.Candidates on g.Key equals c.CandidateId
                        join p in managerContext.Parties on c.PartyId equals p.PartyId
                        select new Resule
                        {
                            CandidateId = g.Key,
                            VoteCount = g.Count(),
                            Candidate = c,
                            Party = p
                        };
          
            return await query.ToListAsync<Resule>();       
        }

        public async Task<VotingSystem?> getSingle(int VotingSystemId)
        {
            return await this.managerContext.VotingSystems.SingleOrDefaultAsync(ob => ob.VotingSystemId == VotingSystemId);
        }

        public async Task<VotingSystem?> insert(VotingSystem votingSystem)
        {
            await this.managerContext.VotingSystems.AddAsync(votingSystem);
            await this.managerContext.SaveChangesAsync();
            return await managerContext.VotingSystems.FirstOrDefaultAsync(ob => ob.VotingSystemId == votingSystem.VotingSystemId);
        }

        public async Task<VotingSystem?> update(VotingSystem votingSystem)
        {
            var vv = await managerContext.VotingSystems.FirstOrDefaultAsync(ob => ob.VotingSystemId == votingSystem.VotingSystemId);
            if (vv == null)
                return null;
            vv.IsVotingOpen = votingSystem.IsVotingOpen;
            vv.Candidates = votingSystem.Candidates;    
            await this.managerContext.SaveChangesAsync();
            return await this.managerContext.VotingSystems.SingleOrDefaultAsync(ob => ob.VotingSystemId == vv.VotingSystemId);
        }

        public async Task<Vote?> vote(Vote vote)
        {
            if(await managerContext.Voters.CountAsync(ob => ob.VoterId == vote.VoterId)==0 || await managerContext.Candidates.CountAsync(ob => ob.CandidateId == vote.CandidateId) == 0)
                return null;
            if(await managerContext.Votes.CountAsync(ob=>ob.VoterId== vote.VoterId && ob.CandidateId==vote.CandidateId && ob.VotingSystemId==vote.VotingSystemId) >0)
            {
                return null;
            }
            await managerContext.Votes.AddAsync(vote);
            await managerContext.SaveChangesAsync();
            return vote;
        }
    }
    public interface IVotingSystemService
    {
        Task<VotingSystem?> insert(VotingSystem state);
        Task<List<VotingSystem>> get();
        Task<VotingSystem?> getSingle(int VotingSystemId);
        Task<VotingSystem?> update(VotingSystem votingSystem);

        Task<List<Resule>> GetResule(int votingSystemId);
        Task<Vote?> vote(Vote vote);
    }

    public class Resule
    {
        public int CandidateId { get; set; }
        public Party Party { get; set; }
        public int VoteCount { get; set; }
        public Candidate Candidate { get; set; } 
    }
}
