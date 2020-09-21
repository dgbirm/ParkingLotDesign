using System;
using System.Collections.Generic;

namespace ParkingLotDesign {

    public class ParkingSpot {
        private static Dictionary<CarSize, HashSet<ParkingSpot>> allSpaces =
        new Dictionary<CarSize, HashSet<ParkingSpot>>();
        static ParkingSpot() {
            foreach( CarSize cs in Enum.GetValues(typeof(CarSize))) {
                allSpaces.Add(cs,new HashSet<ParkingSpot>());
            }
        }
        public bool IsOccupied { get; set; }
    
        public ParkingSpot(CarSize cs) {
            IsOccupied = false;
            ParkingSpot.allSpaces[cs].Add(this);
        }
        public bool ParkCar(Car c) {
            if (allSpaces[c.Size].Contains(this)) {
                if (!IsOccupied) {
                    IsOccupied = true;
                    return true;
                }
            }
            return false;
        }
        public bool UnParkCar() {
            if (IsOccupied) {
                IsOccupied = false;
                return true;
            }
            return false;
        }


    }
}