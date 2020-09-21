

namespace ParkingLotDesign {
    
    public class Car {
        public CarSize Size{ get; set; }

        public Car(CarSize cs) {
            this.Size = cs;
        }
    }
}