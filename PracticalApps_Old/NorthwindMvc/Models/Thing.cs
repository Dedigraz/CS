using System.ComponentModel.DataAnnotations;

namespace NorthwindMvc.Models
{
    public class Thing
    {
        [Range(1,101)]
        public int? ID{get; set;}

        [Required]
        public string Color{get; set;}
    }
}