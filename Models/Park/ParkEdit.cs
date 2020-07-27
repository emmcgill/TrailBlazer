using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Park
{
    public class ParkEdit
    {
        public int ParkId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Please Enter at least two characters")]
        [MaxLength(500, ErrorMessage = "There are too many characters in this field")]
        public string Name { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Please Enter at least two characters")]
        [MaxLength(500, ErrorMessage = "There are too many characters in this field")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Year Established")]
        [Range(1776, 2020, ErrorMessage = "Year established must be between 1776 and 2020")]
        public int YearEstablished { get; set; }

        [Required]
        [Range(0, 14000000, ErrorMessage = "There are too many characters in this field")]
        public int ParkSize { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Please Enter at least two characters")]
        [MaxLength(8000, ErrorMessage = "There are too many characters in this field")]
        public string Description { get; set; }
    }
}
