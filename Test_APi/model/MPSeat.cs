using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_APi.model
{
    public class MPSeat
    {
        [Key]
        public int MPSeatId { get; set; }
        [ForeignKey("state")]
        public int StateId { get; set; }
        public State State { get; set; }
        public int SeatNumber { get; set; }
    }
}
