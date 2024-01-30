using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Test_APi.model
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }

        [ForeignKey("Voter")]
        public int VoterId { get; set; }

        public virtual Voter Voter { get; set; }

        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }

        [ForeignKey("VotingSystem")]
        public int VotingSystemId { get; set; }
        public virtual VotingSystem  VotingSystem { get; set; }




    }
}
