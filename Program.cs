using System;
using System.Threading;
using System.Collections.Generic;

namespace ParkingLotDesign {
    class Program {
        //initialize
        private static Mutex mut = new Mutex();
        private static Random r = new Random();
        const int SMALL = 0;
        const int MEDIUM = 1;
        const int LARGE = 2;
        static void Main() {

            // make the parking spaces
            for (int i = 0; i < 20; i++) {
                int rand = r.Next(3);
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
            Console.WriteLine();

            //Create Cars as threads
            for (int i = 0; i < 100; i++) {
                Car c = new Car((CarSize)r.Next(3));
                PLotController plc = new PLotController();
                plc.Car = c;
                Thread cThread = new Thread(new ThreadStart(plc.UseParkingLot));
                cThread.Name = String.Format("{0}", i + 1);
                cThread.Start();
            }
        }

    }

    class PLotController {

        public Car Car { get; set; }
        public void UseParkingLot() {

            List<ParkingSpot> eLst = ParkingSpot.EmptySpaces[Car.Size];
            ParkingSpot chosenSpot = null;

            while (true) {
                Console.WriteLine("Car {0} is looking for a place to park",
                                  Thread.CurrentThread.Name);
                Monitor.Enter(eLst);
                try {
                    Console.WriteLine("System is checking for a parking spot for car {0}",
                                      Thread.CurrentThread.Name);
                    if (eLst.Count > 0) {
                        chosenSpot = eLst[0];
                        chosenSpot.ParkCar(Car);
                        Console.WriteLine("Car {0} is {1} and now parked in a {2} spot",
                            Thread.CurrentThread.Name, Car.Size, chosenSpot.Size);
                        break;
                    } else {
                        Console.WriteLine("Car {0} will wait for a spot to open up",
                                        Thread.CurrentThread.Name);
                        Monitor.Wait(eLst);
                    }
                } finally {
                    Monitor.Exit(eLst);
                }
            }

            Thread.Sleep(new Random().Next(10000)); //doing stuff wile car is parked

            //If the rest of the threads are waiting,
            //there should be no issue exiting the the lot
            //... this still rubs me the wrong way for reasons
            //i cant quite put my hands on... maybe there should
            //be another synchronization primitive that is managed
            //as cars leave the lot
            Monitor.Enter(eLst);
            try {
                chosenSpot.UnParkCar();
                Console.WriteLine("Car {0} is leaving the lot",
                                    Thread.CurrentThread.Name);
                Monitor.Pulse(eLst);
            } finally {
                Monitor.Exit(eLst);
            }
        }
    }
}
