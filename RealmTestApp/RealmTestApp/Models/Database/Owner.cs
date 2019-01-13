using Realms;
using RealmTestApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealmTestApp.Models.Database
{
    public class Owner : RealmObject
    {
        [PrimaryKey]
        public string OwnerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Backlink(nameof(Car.Owner))]
        public IQueryable<Car> Cars { get; }

        public OwnerDTO ToDto() {
            var result = new OwnerDTO();

            result.OwnerId = this.OwnerId;
            result.FirstName = this.FirstName;
            result.LastName = this.LastName;

            IEnumerable<Car> cars = this.Cars;
            result.Cars = new List<CarDTO>(cars.Select( c => c.ToDto() ));
            foreach (var c in result.Cars) {
                c.Owner = result;
            }

            return result;
        }
    }
}
