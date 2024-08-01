using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAppPart3._3.Models
{
    public class VMLogin
    {
       
        [Key]
        public string Module { get; set; }
        public string Date { get; set; }
    }
}
