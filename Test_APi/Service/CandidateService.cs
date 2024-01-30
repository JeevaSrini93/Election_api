using Microsoft.EntityFrameworkCore;
using Test_APi.model;

namespace Test_APi.Service
{
    public class CandidateService : ICandidateService
    {
        private readonly ElectionManagerContext managerContext;
        public CandidateService(ElectionManagerContext electionManager)
        {
            this.managerContext = electionManager;
        }
        public async Task<List<Candidate>> get(List<int> candatedIds)
        {
            return await managerContext.Candidates.Where(ob=> candatedIds.Contains(ob.CandidateId)).ToListAsync();
        }

        public async Task<List<Candidate>> get()
        {
            return await managerContext.Candidates.ToListAsync();
        }

        public async Task<Candidate?> insert(Candidate candiadate)
        {
            await this.managerContext.Candidates.AddAsync(candiadate);
            await this.managerContext.SaveChangesAsync();
            return await managerContext.Candidates.FirstOrDefaultAsync(ob => ob.CandidateId == candiadate.CandidateId);
        }
    }
    public interface ICandidateService
    {
        Task<Candidate?> insert(Candidate candiadate);
        Task<List<Candidate>> get();
        Task<List<Candidate>> get(List<int> candatedIds);

    }

}
