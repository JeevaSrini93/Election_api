using Microsoft.EntityFrameworkCore;
using Test_APi.model;

namespace Test_APi.ServiceMpSeatService
{
    public class PartyService : IPartyService
    {
        private readonly ElectionManagerContext managerContext;
        public PartyService(ElectionManagerContext electionManager)
        {
            this.managerContext = electionManager;
        }

        public async Task<List<Party>> get()
        {
            return await this.managerContext.Parties.ToListAsync();
        }

        public async Task<Party> insert(Party party)
        {
            await this.managerContext.Parties.AddAsync(party);
            await this.managerContext.SaveChangesAsync();
            return await managerContext.Parties.FirstOrDefaultAsync(ob => ob.PartyId == party.PartyId);
        }
    }

    public interface IPartyService
    {
        Task<Party> insert(Party state);
        Task<List<Party>> get();

    }

}
