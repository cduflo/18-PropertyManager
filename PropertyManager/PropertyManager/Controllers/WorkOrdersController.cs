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
    public class WorkOrdersController : ApiController
    {
        private PropertyManagerDataContext db = new PropertyManagerDataContext();

        // GET: api/WorkOrders
        public IEnumerable<WorkOrdersModel> GetWorkOrders()
        {
            return Mapper.Map<IEnumerable<WorkOrdersModel>>(db.WorkOrders);
        }

        // GET: api/WorkOrders/5
        [ResponseType(typeof(WorkOrdersModel))]
        public IHttpActionResult GetWorkOrder(int id)
        {
            WorkOrder workOrder = db.WorkOrders.Find(id);
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


            var dbWorkOrders = db.WorkOrders.Find(id);
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
        public IHttpActionResult PostWorkOrder(WorkOrder workOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkOrders.Add(workOrder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = workOrder.WorkOrderId }, workOrder);
        }

        // DELETE: api/WorkOrders/5
        [ResponseType(typeof(WorkOrdersModel))]
        public IHttpActionResult DeleteWorkOrder(int id)
        {
            WorkOrder workOrder = db.WorkOrders.Find(id);
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