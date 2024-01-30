using System.ComponentModel.DataAnnotations;

namespace Test_APi.model
{
    public class Party
    {
        [Key]
        public int PartyId { get; set; }

        public string Name { get; set; }
        public string Symbol { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}
