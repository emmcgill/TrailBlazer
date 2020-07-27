using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Score { get; set; }


        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }


        public DateTimeOffset? ModifiedUtc { get; set; }

        public bool IsDeleted { get; set; }
    }

    //PARK REVIEW
    public class ParkReview : Review
    {
        [ForeignKey(nameof(Park))]
        public int ParkId { get; set; }
        public virtual Park Park { get; set; }

    }

    //TRAIL REVIEW
    public class TrailReview : Review
    {
        [ForeignKey(nameof(Trail))]
        public int TrailId { get; set; }
        public virtual Trail Trail { get; set; }
    }
}
