using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Trail;

namespace Models.Trail
{
    public class TrailListItem
    {
        [Display(Name = "Trail Id")]
        public int TrailId { get; set; }

        [Display(Name = "Park")]
        public string ParkName { get; set; }

        [Display(Name = "Park Id")]
        public int ParkId { get; set; }

        public string Name { get; set; }

        [Display(Name = "Trail Length (in miles)")]
        public string Length { get; set; }

        [Display(Name = "Difficulty")]
        public Difficulty TrailDifficulty { get; set; }
    }
}
