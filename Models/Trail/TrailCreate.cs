using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Trail;

namespace Models.Trail
{
    public class TrailCreate
    {
        [Required]
        public int ParkId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Please Enter at least one character.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field")]
        public string Name { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Please Enter at least one character.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field")]
        public string Length { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Please Enter at least two character.")]
        [MaxLength(8000, ErrorMessage = "There are too many characters in this field")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Trail is a Loop")]
        public IsALoop IsTrailALoop { get; set; }


        [Required]
        public Difficulty TrailDifficulty { get; set; }
    }
}
