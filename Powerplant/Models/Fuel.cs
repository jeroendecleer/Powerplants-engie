using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataAnnotationsExtensions;
using Newtonsoft.Json;

namespace PowerplantAPI.Models
{
    public class Fuel
    {
        public Fuel(decimal gasEuroPerMWh, decimal kerosineEuroPerMWh, decimal co2EuroPerTon, decimal wind)
        {
            GasEuroPerMWh = gasEuroPerMWh;
            KerosineEuroPerMWh = kerosineEuroPerMWh;
            Co2EuroPerTon = co2EuroPerTon;
            Wind = wind;
        }

        [JsonProperty(PropertyName = "gas(euro/MWh)")]
        [Required]
        [Min(0)]
        public decimal GasEuroPerMWh { get; set; }

        [JsonProperty(PropertyName = "kerosine(euro/MWh)")]
        [Required]
        [Min(0)]
        public decimal KerosineEuroPerMWh { get; set; }

        [JsonProperty(PropertyName = "co2(euro/ton)")]
        [Required]
        [Min(0)]
        public decimal Co2EuroPerTon { get; set; }

        [JsonProperty(PropertyName = "wind(%)")]
        [Required]
        [Min(0)]
        public decimal Wind { get; set; }


    }
}
