using System;
using System.Linq;

namespace TestApp1
{
    public class Prey : Animal
    {
        public Prey(Location location) : base(location)
        {
            this.EnergyPerMovement = Settings.PreyEnergyPerMovement;
            this.EnergryForBirth = Settings.PreyEneryForBirth;
        }

        public override void BornNewAnimals()
        {
            if (this.CurrentEnergy > this.EnergryForBirth)
            {
                new Prey(this.Location);
                this.CurrentEnergy = Settings.InitialPreyEnergy;
            }
        }

        public override void Feed()
        {
            if (this.Location.VegetationCount <= 0)
                return;

            double countToEat =
                Settings.MaxVegetationPerFeeding > this.Location.VegetationCount
                ? this.Location.VegetationCount
                : Settings.MaxVegetationPerFeeding;

            this.Location.VegetationCount -= countToEat;

            this.CurrentEnergy += countToEat * Settings.FeedingMultiplier;
        }

        public override Movement FindBestMovement()
        {
            // TODO: Calculate base movement based on vegetation
            
            return Settings.AvailableMovements
                .OrderBy(m => m.GetNext(this.Location).Animals.Count)
                .ThenByDescending(m => m.GetNext(this.Location).VegetationCount)
                
                .FirstOrDefault();
                
            return Settings.AvailableMovements[RandomGenerator.GetRandom(0, Settings.AvailableMovements.Count)];
        }
    }
}