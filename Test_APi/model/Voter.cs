using System.ComponentModel.DataAnnotations;

namespace Test_APi.model
{
    public class Voter
    {
        [Key]
        public int VoterId { get; set; }

        public string VoterIdNumber { get; set; }
        public string password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }

        [Required]
        public bool Approved { get; set; } = false;
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
