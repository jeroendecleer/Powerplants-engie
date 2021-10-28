using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerplantAPI.Models
{
    public class PowerplantPower
    {
        public PowerplantPower(string name, decimal power)
        {
            Name = name;
            Power = power;
        }

        public string Name { get; set; }
        [JsonProperty(PropertyName = "p")]
        public decimal Power { get; set; }
    }
}
