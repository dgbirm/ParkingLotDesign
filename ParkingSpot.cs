using System;
using System.Collections.Generic;

namespace ParkingLotDesign {

    public class ParkingSpot {
        public static Dictionary<CarSize, List<ParkingSpot>> EmptySpaces =
        new Dictionary<CarSize, List<ParkingSpot>>();
        static ParkingSpot() {
            foreach (CarSize cs in Enum.GetValues(typeof(CarSize))) {
                EmptySpaces.Add(cs, new List<ParkingSpot>());
            }
        }
        public bool IsOccupied { get; set; }
        public CarSize Size { get; }

        public ParkingSpot(CarSize cs) {
            IsOccupied = false;
            this.Size = cs;
            ParkingSpot.EmptySpaces[cs].Add(this);
        }
        public bool ParkCar(Car c) {
            if (EmptySpaces[c.Size].Contains(this)) {
                if (!IsOccupied) { //double check
                    IsOccupied = true;
                    EmptySpaces[c.Size].Remove(this);
                    return true;
                }
            }
            return false;
        }
        public bool UnParkCar() {
            if (IsOccupied) { //double check
                IsOccupied = false;
                EmptySpaces[this.Size].Add(this);
                return true;
            }
            return false;
        }


    }
}