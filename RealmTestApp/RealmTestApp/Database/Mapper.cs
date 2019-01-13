using RealmTestApp.Contracts;
using RealmTestApp.Models.Database;
using RealmTestApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealmTestApp.Database
{
    public class Mapper
    {
        //private IOwnerDataService _ownerDataService;
        //private ICarDataService _carDataService;

        public Mapper() {
            //_ownerDataService = ownerDataService;
            //_carDataService = carDataService;
        }


        public TTo Map<TFrom, TTo>(TFrom from) 
            where TFrom: class
            where TTo : class {

            TTo res = null;

            if (typeof(TFrom) == typeof(Owner) && typeof(TTo) == typeof(OwnerDTO)) {
                var result = new OwnerDTO();
                var origin = from as Owner;

                result.OwnerId = origin.OwnerId;
                result.FirstName = origin.FirstName;
                result.LastName = origin.LastName;

                result.Cars = origin.Cars.Select( c => Map<Car, CarDTO>( c ) ).ToList();
                foreach (var c in result.Cars) {
                    c.Owner = result;
                }

                res = result as TTo;

            //} else if (typeof( TFrom ) == typeof( OwnerDTO ) && typeof( TTo ) == typeof( Owner )) {
            //    var origin = from as OwnerDTO;
            //    var result = _ownerDataService.GetOwnerById(origin.OwnerId);

            //    result.FirstName = origin.FirstName;
            //    result.LastName = origin.LastName;

            //    res = result as TTo;

            } else if (typeof( TFrom ) == typeof( Car ) && typeof( TTo ) == typeof( CarDTO )) {
                var result = new CarDTO();
                var origin = from as Car;

                result.CarId = origin.CarId;
                result.Make = origin.Make;
                result.Model = origin.Model;

                res = result as TTo;

            //} else if (typeof( TFrom ) == typeof( CarDTO ) && typeof( TTo ) == typeof( Car )) {
            //    var origin = from as CarDTO;
            //    var result = _carDataService.GetCarById( origin.CarId );

            //    result.Make = origin.Make;
            //    result.Model = origin.Model;

            //    result.Owner = _ownerDataService.GetOwnerById(origin.Owner.OwnerId);

            //    res = result as TTo;

            }

            return res;
        }
    }
}
