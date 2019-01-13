using RealmTestApp.Models.Database;
using RealmTestApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTestApp.Contracts
{
    public interface IOwnerDataService
    {
        OwnerDTO GetOwnerById( string ownerId );
        IEnumerable<OwnerDTO> GetOwners();

        bool AddOwner( OwnerDTO newOwner );

        bool UpdateOwner( OwnerDTO updOwner );

        void DeleteOwner( OwnerDTO delOwner );
    }
}
