
using Realms;
using RealmTestApp.Contracts;
using RealmTestApp.Database;
using RealmTestApp.Models.Database;
using RealmTestApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Unity;

namespace RealmTestApp.Services
{
    public class CarDataService : ICarDataService {

        //private Mapper _mapper;
        private Realm _realmConnection;

        public CarDataService() {
            //var config = new MapperConfiguration( cfg => cfg.AddProfile<CarOwnerProfile>() );
            //_mapper = new Mapper();
            _realmConnection = Realm.GetInstance();
        }

        public CarDTO GetCarById( string carId ) {
            Car result = _realmConnection.Find<Car>( carId );
            if (result == null) { return null; }

            return result.ToDto();
        }

        public IEnumerable<CarDTO> GetCars() {
            IEnumerable<Car> cars = _realmConnection.All<Car>();

            return cars.Select(c => c.ToDto());                 
        }

        public bool AddCar( CarDTO newCar ) {
            Car result = null;
            Owner newCarOwner = _realmConnection.Find<Owner>(newCar.Owner.OwnerId);

            _realmConnection.Write( () => {
                newCar.CarId = Guid.NewGuid().ToString();
                result = _realmConnection.CreateObject( nameof( Car ), newCar.CarId );
                result = newCar.ToDbModel( result, newCarOwner );

                result = _realmConnection.Add( result, update: true );
                Debug.WriteLine( $"Car {result.Make} {result.Model} added to db" );
            } );

            if (result == null) { return false; }
            return true;
        }

        public bool UpdateCar( CarDTO updCar ) {
            Car carInDb = _realmConnection.Find<Car>( updCar.CarId );
            Owner updCarOwner = _realmConnection.Find<Owner>( updCar.Owner.OwnerId );

            if (carInDb == null) { return false; }

            _realmConnection.Write( () => {
                _realmConnection.Add(updCar.ToDbModel(carInDb, updCarOwner), update: true);
            } ); 

            return true;
        }

        public void DeleteCar( CarDTO delCar ) {
            Car carInDb = _realmConnection.Find<Car>( delCar.CarId );

            _realmConnection.Write( () => {
                _realmConnection.Remove( carInDb );
            } );
        }
    }
}
