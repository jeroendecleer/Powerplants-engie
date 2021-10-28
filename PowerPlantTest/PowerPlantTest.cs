using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerplantAPI.Models;
using PowerplantAPI.Services.Calculators;
using System.Collections.Generic;
using System.Linq;

namespace PowerPlantTest
{
    [TestClass]
    public class PowerPlantTest
    {
        private readonly ICalculateOptimalProductionPlan _calculateOptimalProductionPlan;

        public PowerPlantTest()
        {
            _calculateOptimalProductionPlan = new CalculateOptimalProductionPlan();
        }
        private Payload CreatePayload1()
        {
            var payload = new Payload();
            payload.Load = 480;
            payload.Fuels = new Fuel(13.4m, 50.8m, 20m, 60m);
            payload.Powerplants = InitializePowerPlants();
            return payload;
        }

        private Payload CreatePayload2()
        {
            var payload = new Payload();
            payload.Load = 480;
            payload.Fuels = new Fuel(13.4m,50.8m,20m,0m);
            payload.Powerplants = InitializePowerPlants();
            return payload;
        }

        private Payload CreatePayload3()
        {
            var payload = new Payload();
            payload.Load = 910;
            payload.Fuels = new Fuel(13.4m, 50.8m, 20m, 60m);
            payload.Powerplants = InitializePowerPlants();
            return payload;
        }

        private List<PowerplantAPI.Models.Powerplant> InitializePowerPlants()
        {
            List<PowerplantAPI.Models.Powerplant> powerplants = new List<PowerplantAPI.Models.Powerplant>();
            powerplants.Add(new PowerplantAPI.Models.Powerplant("gasfiredbig1", "gasfired", 0.53m, 100m, 460m));
            powerplants.Add(new PowerplantAPI.Models.Powerplant("gasfiredbig2", "gasfired", 0.53m, 100m, 460m));
            powerplants.Add(new PowerplantAPI.Models.Powerplant("gasfiredsomewhatsmaller", "gasfired", 0.37m, 40m, 210m));
            powerplants.Add(new PowerplantAPI.Models.Powerplant("tj1", "turbojet", 0.3m, 0m, 16m));
            powerplants.Add(new PowerplantAPI.Models.Powerplant("windpark1", "windturbine", 1m, 0m, 150m));
            powerplants.Add(new PowerplantAPI.Models.Powerplant("windpark2", "windturbine", 1m, 0m, 36m));
            return powerplants;
        }

        private decimal CalculateTotalPower(List<PowerplantPower> powerplantProductions)
        {
            return powerplantProductions.Sum(item => item.Power);
        }

        [TestMethod]
        public void CalculateOptimalProductionTest1()
        {
            Payload payload1 = CreatePayload1();
            var powerplantProductions = _calculateOptimalProductionPlan.GetOptimalProductionPlan(payload1);
            Assert.IsNotNull(powerplantProductions);
            Assert.AreEqual(90, powerplantProductions[0].Power);
            Assert.AreEqual(21.6m, powerplantProductions[1].Power);
            Assert.AreEqual(368.4m, powerplantProductions[2].Power);
            Assert.AreEqual(0, powerplantProductions[3].Power);
            Assert.AreEqual(0, powerplantProductions[4].Power);
            Assert.AreEqual(0, powerplantProductions[5].Power);
            Assert.AreEqual(payload1.Load, CalculateTotalPower(powerplantProductions));
        }

        [TestMethod]
        public void CalculateOptimalProductionTest2()
        {
            Payload payload2 = CreatePayload2();
            var powerplantProductions = _calculateOptimalProductionPlan.GetOptimalProductionPlan(payload2);
            Assert.IsNotNull(powerplantProductions);
            Assert.AreEqual(0, powerplantProductions[0].Power);
            Assert.AreEqual(0, powerplantProductions[1].Power);
            Assert.AreEqual(460, powerplantProductions[2].Power);
            Assert.AreEqual(20, powerplantProductions[3].Power);
            Assert.AreEqual(0, powerplantProductions[4].Power);
            Assert.AreEqual(0, powerplantProductions[5].Power);
            Assert.AreEqual(payload2.Load, CalculateTotalPower(powerplantProductions));
        }

        [TestMethod]
        public void CalculateOptimalProductionTest3()
        {
            Payload payload3 = CreatePayload3();
            var powerplantProductions = _calculateOptimalProductionPlan.GetOptimalProductionPlan(payload3);
            Assert.IsNotNull(powerplantProductions);
            Assert.AreEqual(90, powerplantProductions[0].Power);
            Assert.AreEqual(21.6m, powerplantProductions[1].Power);
            Assert.AreEqual(460, powerplantProductions[2].Power);
            Assert.AreEqual(338.4m, powerplantProductions[3].Power);
            Assert.AreEqual(0, powerplantProductions[4].Power);
            Assert.AreEqual(0, powerplantProductions[5].Power);
            Assert.AreEqual(payload3.Load, CalculateTotalPower(powerplantProductions));
        }
    }
}
