using PropertyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Domain
{
    public class Lease
    {
        public int LeaseId { get; set; }
        public int TenantId { get; set; }
        public int PropertyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Rent { get; set; }
        public RentFrequencies RentFrequency { get; set; }

        public virtual Property Property { get; set; }
        public virtual Tenant Tenant { get; set; }

        public void Update(LeasesModel l)
        {
            LeaseId = l.LeaseId;
            TenantId = l.TenantId;
            PropertyId = l.PropertyId;
            StartDate = l.StartDate;
            EndDate = l.EndDate;
            Rent = l.Rent;
            RentFrequency = (RentFrequencies)l.RentFrequency;
        }
    }

    public enum RentFrequencies
    {
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Quarterly = 4,
        Bianually = 5,
        Annually = 6
    }
}