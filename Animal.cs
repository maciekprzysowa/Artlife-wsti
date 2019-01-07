using System;
using System.Linq;

namespace TestApp1
{
    public abstract class Animal
    {
        public Animal(Location location)
        {
            this.CurrentEnergy = Settings.InitialPreyEnergy;
            this.Location = location;
        }

        public double CurrentEnergy {get;set;}

        public double EnergyPerMovement {get;set;}

        public double EnergryForBirth {get;set;}

        private Location location;
        public Location Location
        {
            get => location;
            set
            {
                if (location != null)
                    location.Animals.Remove(this);

                value.Animals.Add(this);
                location = value;
            }
        }

        private void Move(Movement movement)
        {
            int newX = Location.PosX + movement.MoveX;

            if (newX >= Location.World.SizeX)
                newX -= Location.World.SizeX;

            if (newX < 0)
                newX += Location.World.SizeX;

            int newY = Location.PosY + movement.MoveY;

            if (newY >= Location.World.SizeY)
                newY -= Location.World.SizeY;

            if (newY < 0)
                newY += Location.World.SizeY;

            this.Location = this.Location.World.Locations.Single(l => l.PosX == newX && l.PosY == newY);
            this.CurrentEnergy -= this.EnergyPerMovement;
        }

        public abstract void BornNewAnimals();

        public abstract void Feed();

        public void MoveToBest()
        {
            this.Move(FindBestMovement());
        }

        public abstract Movement FindBestMovement();
    }
}