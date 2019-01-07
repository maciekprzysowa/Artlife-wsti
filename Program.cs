using System;
using System.Threading;
using System.Collections.Concurrent;
namespace TestApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int minAnimalCount = Settings.InitialPreyCount;
            int maxAnimalCount = Settings.InitialPreyCount;
            double minVegCount = Settings.InitialVegetation;
            double maxVegCount = Settings.InitialVegetation;

            var fileName = $"Result {DateTime.Now}.csv";

            World world = new World(Settings.WorldSizeX, Settings.WorldSizeY);
            world.SeedVegetation(Settings.InitialVegetation);
            world.SeedAnimals(Settings.InitialPreyCount);

            for (int i =0; i < 200; i++)
            {
                if (i == 100)
                    //world.InjectAnimals(40);
                    world.InjectVegetation(5000);

                Thread.Sleep(10);
                //world.MultiplyVegetation();
                world.SpreadVegetation();
                world.MoveAnimals();
                world.CalculateFeeding();
                world.CalculateBirths();
                world.CalculateDeaths();
                world.MultiplyVegetation();
                Console.Clear();
                DrawWorld(world);

                if (world.Animals.Count < minAnimalCount)
                    minAnimalCount = world.Animals.Count;

                if (world.Animals.Count > maxAnimalCount)
                    maxAnimalCount = world.Animals.Count;

                if (world.VegetationCount < minVegCount)
                    minVegCount = world.VegetationCount;

                if (world.VegetationCount > maxVegCount)
                    maxVegCount = world.VegetationCount;

                Console.WriteLine($"Vegetation: {world.VegetationCount}");
                Console.WriteLine($"Animals: {world.Animals.Count}");
                Console.WriteLine($"Min animals: {minAnimalCount}, max animals: {maxAnimalCount}");
                Console.WriteLine($"Min veg: {minVegCount}, max veg: {maxVegCount}");
                Console.WriteLine($"Tick: {i}");
                Console.WriteLine($"Path: {System.AppDomain.CurrentDomain.BaseDirectory}");

                //if (i % 10 == 0)
                //{
                    WriteToFile($"{world.VegetationCount};{world.Animals.Count}", fileName);
                //}
                
            }
            
            Console.Read();
        }

        public static void RunWorld()
        {
            
        }

        private static void DrawWorld(World world)
        {
            //Draw headers
            for (int x1 = -1; x1 < world.SizeX;x1++)
            {
                Console.Write(PadBoth(x1.ToString(), 7));
            }

            Console.WriteLine();

            for (int y = 0; y < world.SizeY; y++)
            {
                //Draw first column
                Console.Write(PadBoth(y.ToString(), 7));


                for (int x = 0; x < world.SizeX; x++)
                {
                    Console.Write(
                        PadBoth(
                            world[x,y].Animals.Count.ToString()
                            + '-'
                            + string.Format("{0:0}", world[x,y].VegetationCount)
                            , 7));
                }
                Console.WriteLine();
            }
        }

        // TODO: Move to extensions
        private static string PadBoth(string s, int len)
        {
             int spaces = len - s.Length;
            int padLeft = spaces/2 + s.Length;
            return s.PadLeft(padLeft).PadRight(len);
        }

        private static void WriteToFile(string row, string fileName)
        {
            var path = System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory),
                fileName);

            System.IO.File.AppendAllLines(path, new [] {row});

        }
    }
}
