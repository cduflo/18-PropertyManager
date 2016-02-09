using PropertyManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Models
{
    public class LeasesModel
    {
        public int LeaseId { get; set; }
        public int TenantId { get; set; }
        public int PropertyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Rent { get; set; }
        public RentFrequencies RentFrequency { get; set; }
    }

}