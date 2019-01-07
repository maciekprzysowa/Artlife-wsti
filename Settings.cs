using System;
using System.Collections.Generic;

namespace TestApp1
{
    public static class Settings
    {
        public const int WorldSizeX = 10;
        public const int WorldSizeY = 10;
        public const int InitialVegetation = 300;
        public const double VegetationMultiplication = 0.08;
        public const double MaxVegetationInCell = 100;
        public const int InitialPreyCount = 10;
        public const int InitialPreyEnergy = 15;
        public const int MaxVegetationPerFeeding = 3;
        public const double PreyEnergyPerMovement = 2;
        public const int PreyEneryForBirth = 25;
        public const double FeedingMultiplier = 1.7;

/*
        public const int WorldSizeX = 10;
        public const int WorldSizeY = 10;
        public const int InitialVegetation = 300;
        public const double VegetationMultiplication = 0.05;
        public const double MaxVegetationInCell = 25;
        public const double MaxVegetation = WorldSizeX * WorldSizeY * MaxVegetationInCell;
        public const int InitialPreyCount = 10;
        public const int InitialPreyEnergy = 15;
        public const int MaxVegetationPerFeeding = 3;
        public const double PreyEnergyPerMovement = 1.5;

        public const int PreyEneryForBirth = 25;

        public const double FeedingMultiplier = 1;

 */

        /*
        public const int WorldSizeX = 15;
        public const int WorldSizeY = 15;
        public const int InitialVegetation = 500;
        public const double VegetationMultiplication = 0.05;
        public const double MaxVegetationInCell = 200;
        public const double MaxVegetation = WorldSizeX * WorldSizeY * MaxVegetationInCell;
        public const int InitialPreyCount = 10;
        public const int InitialPreyEnergy = 6;
        public const int MaxVegetationPerFeeding = 3;
        public const double PreyEnergyPerMovement = 2;
        public const int PreyEneryForBirth = 20;
        public const double FeedingMultiplier = 1;
         */

        public static List<Movement> AvailableMovements
        {
            get
            {
                return new List<Movement>()
                {
                    new Movement("Left", -1, 0),
                    new Movement("Top", 0, -1),
                    new Movement("Right", 1, 0),
                    new Movement("Bottom", 0, 1),

                    new Movement("Top-Left", -1, -1),
                    new Movement("Top-Right", 1, -1),
                    new Movement("Bottom-Left", -1, 1),
                    new Movement("Bottom-Right", 1, 1),
                };
            }
        }
    }
}