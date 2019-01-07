using System;
using System.Collections.Generic;
using TestApp1;

namespace TestApp1
{
    public class Location
    {
        public Location(World world, int posX, int posY, int vegetationCount = 0)
        {
            PosX = posX;
            PosY = posY;
            VegetationCount = vegetationCount;
            World = world;
            Animals = new List<Animal>();
        }

        public World World {get;set;}

        public int PosX {get;set;}
        public int PosY {get;set;}

        public double VegetationCount {get;set;}

        public List<Animal> Animals {get;set;}
    }
}