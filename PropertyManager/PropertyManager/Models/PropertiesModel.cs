using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Models
{
    public class PropertiesModel
    {
        public int PropertyId { get; set; }
        public int? AddressId { get; set; }
        public string PropertyName { get; set; }
        public int? SquareFeet { get; set; }
        public int? NumberOfBedrooms { get; set; }
        public float? NumberOfBathrooms { get; set; }
        public int? NumberofVehicles { get; set; }
        public AddressesModel Address { get; set; }
    }
}