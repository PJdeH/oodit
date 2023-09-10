using System.ComponentModel.DataAnnotations;

namespace NumberSelector.Models
{
    public class Input
    {
        [Required(ErrorMessage = "[x,y,..,.....] is required and only integers")]
        public string Values { get; set; }
    }

    public class TotalNumbers
    {
        public int Value { get; set; }
        public int Count { get; set; }
    }
}
