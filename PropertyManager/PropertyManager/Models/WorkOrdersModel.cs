using PropertyManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Models
{
    public class WorkOrdersModel
    {
        public int WorkOrderId { get; set; }
        public int PropertyId { get; set; }
        public int? TenantId { get; set; }
        public string Description { get; set; }
        public DateTime OpenedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public Priorities Priority { get; set; }
        public TenantsModel Tenant { get; set; }
        public PropertiesModel Property { get; set; }
    }

}