using Models.Trail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Park
{
    public class ParkDetail
    {
        [Display(Name = "Park ID")]
        public int ParkId { get; set; }

        public string Name { get; set; }

        [Display(Name = "Park Address")]
        public string Address { get; set; }

        [Display(Name = "Year Established")]
        public int YearEstablished { get; set; }

        public string Description { get; set; }

        [Display(Name = "Park Size (in acres)")]
        public int ParkSize { get; set; }


        [Display(Name = "Average Rating")]
        [DisplayFormat(DataFormatString = "{0:F}", ApplyFormatInEditMode = true)]
        public double AverageRating { get; set; }

        public ICollection<TrailListItem> Trails { get; set; }

    }
}
