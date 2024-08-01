using System.ComponentModel.DataAnnotations;

namespace SchoolAppPart3._3.Models
{
    public class Modules
    {
        [Key]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int NoCredits { get; set; }
        [Required]
        public int Hours { get; set; }
        

       
    }
}
