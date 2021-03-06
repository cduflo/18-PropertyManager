﻿using PropertyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Domain
{
    
    public class Tenant
    {
        public Tenant(TenantsModel tenant)
        {
            this.Update(tenant);
        }

        public Tenant()
        { }

        public int TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AddressId { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public string UserId { get; set; }

        public virtual Address Address { get; set; }
        public virtual PropertyManagerUser User { get; set; }

        public virtual ICollection<Lease> Leases { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }

        public void Update(TenantsModel t)
        {
            FirstName = t.FirstName;
            LastName = t.LastName;
            AddressId = t.AddressId;
            Telephone = t.Telephone;
            EmailAddress = t.EmailAddress;
            Address.Update(t.Address);
        }
    }
}