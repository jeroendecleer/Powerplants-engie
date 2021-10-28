using PowerplantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerplantAPI.Services.Calculators
{
    public interface ICalculateOptimalProductionPlan
    {
        List<PowerplantPower> GetOptimalProductionPlan(Payload payload);
    }

    public class CalculateOptimalProductionPlan : ICalculateOptimalProductionPlan
    {
        public List<PowerplantPower> GetOptimalProductionPlan(Payload payload)
        {
            //step 1 calculate the costs for all powerplants
            foreach (var powerplant in payload.Powerplants)
            {
                if (powerplant.Type == PowerplantTypes.Gasfired)
                {
                    powerplant.Cost = payload.Fuels.GasEuroPerMWh * powerplant.CalculateCostFactor();
                }
                else if (powerplant.Type == PowerplantTypes.Windturbine)
                {
                    powerplant.Cost = 0;
                    powerplant.Pmax = powerplant.Pmax * payload.Fuels.Wind / 100;
                }
                else if (powerplant.Type == PowerplantTypes.Turbojet)
                {
                    powerplant.Cost = payload.Fuels.KerosineEuroPerMWh * powerplant.CalculateCostFactor();
                }
            }

            List<Models.Powerplant> sortedPowerplants = payload.Powerplants.OrderBy(o => o.Cost).ToList();

            //step 2 determine production P per powerplant
            List<PowerplantPower> powerplantsWithPower = new List<PowerplantPower>();
            decimal currentLoad = 0;

            foreach (var sortedPowerplant in sortedPowerplants)
            {
                if (sortedPowerplant.Type == PowerplantTypes.Windturbine)
                {
                    if (currentLoad < payload.Load)
                    {
                        if ((currentLoad + sortedPowerplant.Pmax) <= payload.Load)
                        {
                            powerplantsWithPower.Add(new PowerplantPower(sortedPowerplant.Name, sortedPowerplant.Pmax));
                            currentLoad += sortedPowerplant.Pmax;
                        }
                        else
                        {
                            powerplantsWithPower.Add(new PowerplantPower(sortedPowerplant.Name, 0));
                        }
                    }
                    else
                    {
                        powerplantsWithPower.Add(new PowerplantPower(sortedPowerplant.Name, 0));
                    }
                }
                else if (sortedPowerplant.Type == PowerplantTypes.Gasfired || sortedPowerplant.Type == PowerplantTypes.Turbojet)
                {
                    if (currentLoad < payload.Load)
                    {
                        if (currentLoad + sortedPowerplant.Pmax < payload.Load)
                        {
                            powerplantsWithPower.Add(new PowerplantPower(sortedPowerplant.Name, sortedPowerplant.Pmax));
                            currentLoad += sortedPowerplant.Pmax;
                        }
                        else if ((currentLoad + sortedPowerplant.Pmax) > payload.Load)
                        {
                            var difference = (currentLoad + sortedPowerplant.Pmax) - payload.Load;
                            powerplantsWithPower.Add(new PowerplantPower(sortedPowerplant.Name, (sortedPowerplant.Pmax - difference)));
                            currentLoad += (sortedPowerplant.Pmax - difference);
                        }
                        else
                        {
                            powerplantsWithPower.Add(new PowerplantPower(sortedPowerplant.Name, 0));
                        }
                    }
                    else
                    {
                        powerplantsWithPower.Add(new PowerplantPower(sortedPowerplant.Name, 0));
                    }
                }
            }

            decimal totalPower = powerplantsWithPower.Sum(item => item.Power);
            if(payload.Load > totalPower)
            {
                throw new ArgumentException("De geproduceerde load kan niet voldoen aan de gevraagde load");
            }

            return powerplantsWithPower;
        }
    }
}