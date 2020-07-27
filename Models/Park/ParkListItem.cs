using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Park
{
    public class ParkListItem
    {
        [Display(Name = "Park Id")]
        public int ParkId { get; set; }

        public string Name { get; set; }

        [Display(Name = "Location")]
        public string Address { get; set; }

        [Display(Name = "Size (in acres)")]
        public int ParkSize { get; set; }
    }
}
