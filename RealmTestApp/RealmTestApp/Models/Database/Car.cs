using Realms;
using RealmTestApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTestApp.Models.Database
{
    public class Car : RealmObject
    {
        [PrimaryKey]
        public string CarId { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }

        public Owner Owner { get; set; }

        public CarDTO ToDto() {
            var result = new CarDTO();

            result.CarId = this.CarId;
            result.Make = this.Make;
            result.Model = this.Model;
            result.LicensePlate = this.LicensePlate;

            return result;
        }
    }
}
