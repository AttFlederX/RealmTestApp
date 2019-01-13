using RealmTestApp.Models.Database;
using RealmTestApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTestApp.Contracts
{
    public interface ICarDataService
    {
        CarDTO GetCarById( string carId );
        IEnumerable<CarDTO> GetCars();

        bool AddCar( CarDTO newCar );

        bool UpdateCar( CarDTO updCar );

        void DeleteCar( CarDTO delCar );
    }
}
