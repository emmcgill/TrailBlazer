using Models.Review.TrailReview;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Trail;

namespace Models.Trail
{
    public class TrailDetail
    {
        [Display(Name = "Trail ID")]
        public int TrailId { get; set; }

        [Display(Name = "Park Id")]
        public int ParkId { get; set; }

        [Display(Name = "Trail Name")]
        public string Name { get; set; }

        [Display(Name = "Trail Length")]
        public string Length { get; set; }

        [Display(Name = "Average Rating")]
        [DisplayFormat(DataFormatString = "{0:F}", ApplyFormatInEditMode = true)]
        public double AverageRating { get; set; }

        [Display(Name = "Loop")]
        public IsALoop IsTrailALoop { get; set; }

        [Display(Name = "Trail Difficulty")]
        public Difficulty TrailDifficulty { get; set; }

        public string Description { get; set; }

        public ICollection<TrailReviewListItem> Reviews { get; set; }
    }
}
