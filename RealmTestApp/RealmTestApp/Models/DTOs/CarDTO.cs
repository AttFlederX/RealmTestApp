using RealmTestApp.Models.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RealmTestApp.Models.DTOs
{
    public class CarDTO
    {
        public string CarId { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }

        public OwnerDTO Owner { get; set; }

        public string FullModelName => $"{Make} {Model}";

        public Car ToDbModel(Car targetCar, Owner carOwner) {

            targetCar.Make = this.Make;
            targetCar.Model = this.Model;
            targetCar.LicensePlate = this.LicensePlate;

            targetCar.Owner = carOwner;
            Debug.WriteLine($"Car {targetCar.Make} {targetCar.Model} owner set: {targetCar.Owner.FirstName} {targetCar.Owner.LastName}");

            return targetCar;
        }
    }
}
