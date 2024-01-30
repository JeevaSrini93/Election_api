using Microsoft.EntityFrameworkCore;
using Test_APi.model;

namespace Test_APi.Service
{
    public class VoterService : IVoterService
    {
        private readonly ElectionManagerContext managerContext;
        public VoterService(ElectionManagerContext electionManager)
        {
            this.managerContext = electionManager;
        }

        public async Task<List<Voter>> get()
        {
            return await managerContext.Voters.ToListAsync();
        }

        public async Task<Voter?> approve(int VoterId, bool approve = false)
        {
            var voter = await managerContext.Voters.FirstOrDefaultAsync(ob => ob.VoterId == VoterId);
            if (voter == null)
                return null;
            voter.Approved = approve;
            await this.managerContext.SaveChangesAsync();
            return voter;
        }
        public async Task<Voter?> insert(Voter voter)
        {
            voter.password = HashPassword.hashPassword(voter.password);

            if (await this.managerContext.Voters.CountAsync(ob => ob.VoterIdNumber == voter.VoterIdNumber) > 0)
                return null;
            await this.managerContext.Voters.AddAsync(voter);
            await this.managerContext.SaveChangesAsync();
            return await managerContext.Voters.FirstOrDefaultAsync(ob => ob.VoterId == voter.VoterId);
        }

        public async Task<Voter?> getSingle(string VoterIdNumber, string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(VoterIdNumber))
                return null;
            var voter = await managerContext.Voters.FirstOrDefaultAsync(ob => ob.VoterIdNumber == VoterIdNumber);
            if (voter == null)
                return null;
            if (HashPassword.VerifyPassword(voter.password, password) == false)
                return null;
            else
                return voter;
        }
    }
    public interface IVoterService
    {
        Task<Voter?> insert(Voter state);
        Task<List<Voter>> get();
        Task<Voter?> approve(int VoterId, bool approve = false);
        Task<Voter?> getSingle(string VoterIdNumber, string password);
    
    }




}
