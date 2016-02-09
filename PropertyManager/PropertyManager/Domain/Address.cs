using PropertyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Domain
{
    public partial class Address
    {
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostCode { get; set; }
        public bool International { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<Tenant> Tenants { get; set; }


        public void Update(AddressesModel a)
        {
            Address1 = a.Address1;
            Address2 = a.Address2;
            Address3 = a.Address3;
            Address4 = a.Address4;
            Address5 = a.Address5;
            City = a.City;
            Region = a.Region;
            PostCode = a.PostCode;
            International = a.International;
        }

        public Address() { }

        public Address(AddressesModel model)
        {
            this.Update(model);
        }

    }
}