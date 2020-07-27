using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Trail
    {
        [Key]
        public int TrailId { get; set; }

        [ForeignKey("Park")]
        public int ParkId { get; set; }
        public virtual Park Park { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Length { get; set; }

        [Required]
        public Difficulty TrailDifficulty { get; set; }

        public enum Difficulty
        {
            Easy,
            Moderate,
            Strenuous
        }

        [Required]
        public IsALoop IsTrailALoop { get; set; }

        public enum IsALoop
        {
            No,
            Yes
        }

        [Required]
        public string Description { get; set; }

        public double AverageRating { get; set; }

        public virtual ICollection<TrailReview> Ratings { get; set; } = new List<TrailReview>();

        public bool IsDeleted { get; set; }
    }
}
