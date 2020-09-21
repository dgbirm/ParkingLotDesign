using System;

namespace ParkingLotDesign
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialize
            Random r = new Random();
            const int SMALL = 0;
            const int MEDIUM = 1;
            const int LARGE = 2;

            for (int i = 0; i < 20; i++ ) {
                int rand = r.Next(0,3);
                switch (rand) {
                    case SMALL:
                        new ParkingSpot(CarSize.Small);
                        Console.WriteLine("A new small spot has been created!");
                        break;
                    case MEDIUM:
                        new ParkingSpot(CarSize.Medium);
                        Console.WriteLine("A new medium spot has been created!");
                        break;
                    case LARGE:
                        new ParkingSpot(CarSize.Large);
                        Console.WriteLine("A new large spot has been created!");
                        break;
                }
            }


            //Controller
            
        }
    }
}
