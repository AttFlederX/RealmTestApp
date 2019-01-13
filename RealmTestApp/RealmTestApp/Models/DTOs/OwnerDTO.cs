using RealmTestApp.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTestApp.Models.DTOs
{
    public class OwnerDTO
    {
        public string OwnerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<CarDTO> Cars { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public Owner ToDbModel(Owner targetOwner) {
            //if (this.OwnerId == targetOwner.OwnerId) {
                targetOwner.FirstName = this.FirstName;
                targetOwner.LastName = this.LastName;
            //}

            return targetOwner;
        }
    }
}
