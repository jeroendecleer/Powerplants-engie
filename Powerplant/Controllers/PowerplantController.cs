using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PowerplantAPI.Models;
using PowerplantAPI.Services.Calculators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerplantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PowerplantController : ControllerBase
    {
        private readonly ICalculateOptimalProductionPlan ICalculateOptimalProductionPlan;

        public PowerplantController(ICalculateOptimalProductionPlan _ICalculateOptimalProductionPlan)
        {
            ICalculateOptimalProductionPlan = _ICalculateOptimalProductionPlan;
        }

        [HttpPost("/productionplan")]
        public List<PowerplantPower> GetProductionPlan([FromBody] Payload payload)
        {
            return ICalculateOptimalProductionPlan.GetOptimalProductionPlan(payload);
        }
    }
}
