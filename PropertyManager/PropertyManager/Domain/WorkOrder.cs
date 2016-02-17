using PropertyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Domain
{
    public class WorkOrder
    {
        public WorkOrder(WorkOrdersModel workOrder)
        {
            this.Update(workOrder);
        }

        public WorkOrder()
        { }

        public int WorkOrderId { get; set; }
        public int PropertyId { get; set; }
        public int? TenantId { get; set; }
        public string Description { get; set; }
        public DateTime OpenedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public Priorities Priority { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual Property Property { get; set; }

        public void Update(WorkOrdersModel w)
        {
            PropertyId = w.PropertyId;
            TenantId = w.TenantId;
            Description = w.Description;
            OpenedDate = w.OpenedDate;
            ClosedDate = w.ClosedDate;
            Priority = (Priorities)w.Priority;
        }
    }

    public enum Priorities
    {
        Critical = 1,
        Urgent = 2,
        High = 3,
        Medium = 4,
        Low = 5
    }

}
