using System;
using System.Collections.Generic;
using System.Linq;
using TestApp1;


namespace TestApp1
{
    public class World
    {
        public World(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Locations = new List<Location>();

            for (int x = 0; x < SizeX; x++)
            {
                for(int y = 0; y < SizeY; y++)
                {
                    Locations.Add(new Location(this, x, y));
                }
            }
        }

        public Location this[int x, int y] =>
            Locations.Single(l => l.PosX == x && l.PosY == y);
        
        public int SizeX {get;set;}
        public int SizeY {get;set;}

        public List<Location> Locations { get; set; }

        public List<Animal> Animals => Locations.SelectMany(l => l.Animals).ToList();

        public double VegetationCount => Locations.Sum(l => l.VegetationCount);

        public void SeedVegetation(int newVegetationCount)
        {
            int locationCount = Locations.Count;
            
            while (this.VegetationCount < newVegetationCount)
            {
                Locations[RandomGenerator.GetRandom(0, Locations.Count)].VegetationCount += (newVegetationCount / 5);
            }
        }

        public void InjectVegetation(int vegeCount)
        {
            int injectedCount = 0;
            while (injectedCount < vegeCount)
            {
                Locations[RandomGenerator.GetRandom(0, Locations.Count)].VegetationCount += 10;
                injectedCount += 10;
            }
            
        }

        public void InjectAnimals(int preyCount)
        {
            while(preyCount > 0)
            {
                Location loc = Locations[RandomGenerator.GetRandom(0, Locations.Count)];
                new Prey(loc);
                preyCount--;
            }
        }

        public void MultiplyVegetation()
        {
            var tempLocations = this.Locations;

            for (int x = 0; x < SizeX; x++)
            {
                for(int y = 0; y < SizeY; y++)
                {
                    double currVegeCount = this[x,y].VegetationCount;

                    double vegeToAdd = currVegeCount * Settings.VegetationMultiplication;

                    Location tempLocation = tempLocations.Single(l => l.PosX == x && l.PosY == y);

                    var neighbouringLocations = Settings.AvailableMovements
                                .Select(m => m.GetNext(tempLocation))
                                .OrderBy(l => l.VegetationCount);

                    foreach (Location loc in neighbouringLocations)
                    {
                        double maxToAdd = Settings.MaxVegetationInCell - loc.VegetationCount;
                        double toAdd;
                        if (maxToAdd > vegeToAdd)
                            toAdd = vegeToAdd;
                        else
                            toAdd = maxToAdd;

                        loc.VegetationCount += toAdd;
                        vegeToAdd -= toAdd;
                    }
                }
            }

            this.Locations = tempLocations;
        }

        public void SpreadVegetation()
        {
            int newVegetationCount = (int)(this.VegetationCount + this.VegetationCount * Settings.VegetationMultiplication);
            SeedVegetation(newVegetationCount);
        }

        public void SeedAnimals(int preyCount)
        {
            while(this.Animals.Count < preyCount)
            {
                Location loc = Locations[RandomGenerator.GetRandom(0, Locations.Count)];
                new Prey(loc);
            }
        }

        public void MoveAnimals()
        {
            var temp = this.Animals.ToList();

            foreach (Animal animal in temp)
            {
                animal.MoveToBest();
            }
        }

        public void CalculateDeaths()
        {
            var temp = this.Animals.ToList();
            
            foreach (Animal animal in temp)
            {
                if (animal.CurrentEnergy <= 0)
                    animal.Location.Animals.Remove(animal);
            }
        }

        public void CalculateFeeding()
        {
            var temp = this.Animals.ToList();
            
            foreach (Animal animal in temp)
            {
                animal.Feed();
            }
        }

        public void CalculateBirths()
        {
            var temp = this.Animals.ToList();
            
            foreach (Animal animal in temp)
            {
                animal.BornNewAnimals();
            }
        }
    }
}
