using System.ComponentModel.DataAnnotations;

namespace SchoolAppPart3._3.Models
{
    public class Work
    {
        [Key]
        public string WorkCode { get; set; }
        [Required]
        public string WorkDate { get; set; }
        [Required]
        public int WorkHours { get; set; }
    }
}
