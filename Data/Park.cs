using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Park
    {
        [Key]
        public int ParkId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int ParkSize { get; set; }

        [Required]
        public int YearEstablished { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<Trail> Trails { get; set; }

        public bool IsDeleted { get; set; }
    }
}
