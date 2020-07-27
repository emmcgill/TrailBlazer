using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Review.TrailReview
{
    public class TrailReviewEdit
    {
        public int ReviewId { get; set; }

        public int TrailId { get; set; }

        public int MyProperty { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be whole number from 1 to 5")]
        public int Score { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Please Enter at least two characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field")]
        public string Title { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Please Enter at least two characters.")]
        [MaxLength(8000, ErrorMessage = "There are too many characters in this field")]
        public string Comment { get; set; }

        [Display(Name = "Date of Visit")]
        [DataType(DataType.Date)]
        public DateTime VisitDate { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
