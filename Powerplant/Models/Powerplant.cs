using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PowerplantAPI.Models
{
    public class Powerplant
    {
        public Powerplant(string name, string type, decimal efficiency, decimal pmin, decimal pmax)
        {
            Name = name;
            Type = type;
            Efficiency = efficiency;
            Pmin = pmin;
            Pmax = pmax;
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Min(0)]
        public decimal Efficiency { get; set; }
        [Required]
        [Min(0)]
        public decimal Pmin { get; set; }
        [Required]
        [Min(0)]
        public decimal Pmax { get; set; }
        public decimal Cost { get; set; }
        public decimal Production { get; set; }

        public decimal CalculateCostFactor()
        {
            return 100 / Efficiency;
        }
    }
}
