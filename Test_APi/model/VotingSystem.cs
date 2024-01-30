using System.ComponentModel.DataAnnotations;

namespace Test_APi.model
{
    public class VotingSystem
    {

        [Key]
        public int VotingSystemId { get; set; }
        public virtual ICollection<Candidate> Candidates { get; set; }
        public bool IsVotingOpen { get; set; }
    }
}
