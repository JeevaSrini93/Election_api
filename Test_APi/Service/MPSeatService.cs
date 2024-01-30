using Microsoft.EntityFrameworkCore;
using System.IO;
using Test_APi.model;

namespace Test_APi.Service
{
    public class MpSeatService:IMPService
    {
        private readonly ElectionManagerContext managerContext;
        public MpSeatService(ElectionManagerContext electionManager)
        {
            this.managerContext= electionManager;
        }

        public async Task<List<MPSeat>> get()
        {
            return await this.managerContext.MPSeats.ToListAsync();
        }

        public async Task<MPSeat?> insert(MPSeat mpSeat)
        {
            await this.managerContext.MPSeats.AddAsync(mpSeat);
            await this.managerContext.SaveChangesAsync();
            return await managerContext.MPSeats.FirstOrDefaultAsync(ob => ob.MPSeatId == mpSeat.MPSeatId);
        }

        public async Task<MPSeat?> update(int mpSeatId,int seats)
        {
            var vv = await managerContext.MPSeats.FirstOrDefaultAsync(ob => ob.MPSeatId == mpSeatId);
            if (vv == null)
                return null;
            vv.SeatNumber = seats;
            await this.managerContext.SaveChangesAsync();
            return await managerContext.MPSeats.FirstOrDefaultAsync(ob => ob.MPSeatId == mpSeatId);
        }
    }

    public interface IMPService
    {
        Task<MPSeat?> update(int mpSeatId, int seats);
        Task<MPSeat?> insert(MPSeat mpSeat);
        Task<List<MPSeat>> get();

    }
}
