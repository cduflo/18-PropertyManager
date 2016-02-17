using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PropertyManager.Domain;
using PropertyManager.Infrastructure;
using PropertyManager.Models;
using AutoMapper;

namespace PropertyManager.Controllers
{
    [Authorize]
    public class WorkOrdersController : ApiController
    {
        private PropertyManagerDataContext db = new PropertyManagerDataContext();

        // GET: api/WorkOrders
        public IEnumerable<WorkOrdersModel> GetWorkOrders()
        {
           // return Mapper.Map<IEnumerable<WorkOrdersModel>>(db.WorkOrders);
            return Mapper.Map<IEnumerable<WorkOrdersModel>>(db.WorkOrders.Where(p => p.Property.User.UserName == User.Identity.Name));
        }

        // GET: api/WorkOrders/5
        [ResponseType(typeof(WorkOrdersModel))]
        public IHttpActionResult GetWorkOrder(int id)
        {
            //WorkOrder workOrder = db.WorkOrders.Find(id);
            WorkOrder workOrder = db.WorkOrders.FirstOrDefault(p => p.Property.User.UserName == User.Identity.Name && p.WorkOrderId == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<WorkOrdersModel>(workOrder));
        }

        // PUT: api/WorkOrders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkOrder(int id, WorkOrdersModel workOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workOrder.WorkOrderId)
            {
                return BadRequest();
            }


           // var dbWorkOrders = db.WorkOrders.Find(id);

            var dbWorkOrders = db.WorkOrders.FirstOrDefault(p => p.Property.User.UserName == User.Identity.Name && p.WorkOrderId == id);
            if (dbWorkOrders == null)
            {
                return BadRequest();
            }

            dbWorkOrders.Update(workOrder);

            db.Entry(dbWorkOrders).State = EntityState.Modified;


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkOrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/WorkOrders
        [ResponseType(typeof(WorkOrder))]
        public IHttpActionResult PostWorkOrder(WorkOrdersModel workOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbWorkOrder = new WorkOrder(workOrder);

            db.WorkOrders.Add(dbWorkOrder);

            db.SaveChanges();

            workOrder.WorkOrderId = dbWorkOrder.WorkOrderId;

            return CreatedAtRoute("DefaultApi", new { id = workOrder.WorkOrderId }, workOrder);
        }

        // DELETE: api/WorkOrders/5
        [ResponseType(typeof(WorkOrdersModel))]
        public IHttpActionResult DeleteWorkOrder(int id)
        {
            //WorkOrder workOrder = db.WorkOrders.Find(id);
            WorkOrder workOrder = db.WorkOrders.FirstOrDefault(p => p.Property.User.UserName == User.Identity.Name && p.WorkOrderId == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            db.WorkOrders.Remove(workOrder);
            db.SaveChanges();

            return Ok(Mapper.Map<WorkOrdersModel>(workOrder));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkOrderExists(int id)
        {
            return db.WorkOrders.Count(e => e.WorkOrderId == id) > 0;
        }
    }
}