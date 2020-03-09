using System.ComponentModel.DataAnnotations;

namespace MVCTEST.Models
{
    public class pvqviewmodel
    {

        public int Q_ID { get; set; }
        public string Question { get; set; }
        [Required(ErrorMessage = "User answer is required")]
        public string Useranswer { get; set; }
    }
}