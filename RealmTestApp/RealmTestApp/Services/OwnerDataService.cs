
using Realms;
using RealmTestApp.Contracts;
using RealmTestApp.Database;
using RealmTestApp.Models.Database;
using RealmTestApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Unity;

namespace RealmTestApp.Services
{
    public class OwnerDataService : IOwnerDataService {

        //Mapper _mapper;
        Realm _realmConnection;

        public OwnerDataService() {
            // var config = new MapperConfiguration( cfg => cfg.AddProfile<CarOwnerProfile>() );
            //_mapper = new Mapper(  );

            _realmConnection = Realm.GetInstance();
        }

        public OwnerDTO GetOwnerById( string ownerId ) {
            Owner result = _realmConnection.Find<Owner>( ownerId );
            if (result == null) { return null; }

            return result.ToDto();
        }

        public IEnumerable<OwnerDTO> GetOwners() {
            try {
                IEnumerable<Owner> owners = _realmConnection.All<Owner>();

                return owners.Select( ow => ow.ToDto() );
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool AddOwner( OwnerDTO newOwner ) {
            Owner result = null; 

            _realmConnection.Write(() => {
                result = _realmConnection.CreateObject(nameof(Owner), Guid.NewGuid().ToString() );
                result = newOwner.ToDbModel(result);

                result = _realmConnection.Add( result, update: true );
            } );

            if (result == null) { return false; }
            return true;
        }

        public bool UpdateOwner( OwnerDTO updOwner ) {
            Owner ownerInDb = _realmConnection.Find<Owner>( updOwner.OwnerId );

            if (ownerInDb == null) { return false; }

            _realmConnection.Write( () => {
                _realmConnection.Add(updOwner.ToDbModel( ownerInDb ), update: true);
            } );

            return true;
        }

        public void DeleteOwner( OwnerDTO delOwner ) {
            Owner ownerInDb = _realmConnection.Find<Owner>( delOwner.OwnerId );

            _realmConnection.Write( () => {
                _realmConnection.Remove(ownerInDb);
            } );
        }
    }
}
