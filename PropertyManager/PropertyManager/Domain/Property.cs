﻿using PropertyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Domain
{
    public class Property
    {
        public int PropertyId { get; set; }
        public int? AddressId { get; set; }
        public string PropertyName { get; set; }
        public int? SquareFeet { get; set; }
        public int? NumberOfBedrooms { get; set; }
        public float? NumberOfBathrooms { get; set; }
        public int? NumberofVehicles { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Lease> Leases { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }

        public void Update(PropertiesModel p)
        {
            AddressId = p.AddressId;
            PropertyName = p.PropertyName;
            SquareFeet = p.SquareFeet;
            NumberOfBathrooms = p.NumberOfBathrooms;
            NumberOfBedrooms = p.NumberOfBedrooms;
            NumberofVehicles = p.NumberofVehicles;
        }
    }
}