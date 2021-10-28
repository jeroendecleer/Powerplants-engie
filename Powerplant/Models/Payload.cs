using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PowerplantAPI.Models
{
    public class Payload
    {
        public Payload()
        {
            Powerplants = new List<Powerplant>();
        }

        [Required]
        [Min(1)]
        public decimal Load { get; set; }
        [Required]
        public Fuel Fuels {get; set;}
        [Required]
        public List<Powerplant> Powerplants { get; set; }
    }
}
