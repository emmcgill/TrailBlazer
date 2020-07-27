using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Review.TrailReview
{
    public class TrailReviewDetail
    {
        public int ReviewId { get; set; }

        public int TrailId { get; set; }

        public int Score { get; set; }

        public string Title { get; set; }


        public string Comment { get; set; }

        [Display(Name = "Date of Visit")]
        [DataType(DataType.Date)]
        public DateTime VisitDate { get; set; }
    }
}
