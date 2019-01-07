using System;
using System.Linq;

namespace TestApp1
{
    public class Movement
    {
        public Movement(string name, int x, int y)
        {
            Name = name;
            MoveX = x;
            MoveY = y;
        }

        public Location GetNext(Location location)
        {
            int newX = location.PosX + this.MoveX;

            if (newX >= location.World.SizeX)
                newX -= location.World.SizeX;

            if (newX < 0)
                newX += location.World.SizeX;

            int newY = location.PosY + this.MoveY;

            if (newY >= location.World.SizeY)
                newY -= location.World.SizeY;

            if (newY < 0)
                newY += location.World.SizeY;

            return location.World.Locations.Single(l => l.PosX == newX && l.PosY == newY);
        }

        public string Name {get;set;}

        public int MoveX {get;set;}
        public int MoveY {get;set;}
    }
}