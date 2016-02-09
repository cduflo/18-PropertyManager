using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Models
{
    public class AddressesModel
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
    }
}