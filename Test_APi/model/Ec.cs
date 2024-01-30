using System.ComponentModel.DataAnnotations;

namespace Test_APi.model
{
    public class Ec
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String userName { get; set; }
        public string password { get; set; }
    }
}
