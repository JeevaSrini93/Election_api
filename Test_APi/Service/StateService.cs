using Microsoft.EntityFrameworkCore;
using Test_APi.model;

namespace Test_APi.Service
{
    
    public class StateService : IStateService
    {
        private  ElectionManagerContext managerContext;
        public StateService(ElectionManagerContext electionManager)
        {
            this.managerContext = electionManager;
        }
        public async Task<State> insert(State state)
        {
            await this.managerContext.states.AddAsync(state);
            await this.managerContext.SaveChangesAsync();
            return await managerContext.states.FirstOrDefaultAsync(ob => ob.id == state.id);
        }
        public async Task<List<State>> get()
        {
            return await managerContext.states.ToListAsync();
        }
    }

    public interface IStateService
    {
        Task<State> insert(State state);
        Task<List<State>> get();
    }

}
