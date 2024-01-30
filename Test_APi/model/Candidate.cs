using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Test_APi.model
{
    public class Candidate
    {
        [Key]
        public int CandidateId { get; set; }

        public string Name { get; set; }

        [ForeignKey("Party")]
        public int PartyId { get; set; }

        public virtual Party Party { get; set; }

        [ForeignKey("state")]
        public int StateId { get; set; }
        public State State { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
