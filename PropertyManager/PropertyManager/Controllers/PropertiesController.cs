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
using System.Data.Entity.Migrations.Model;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Runtime.Remoting.Contexts;

namespace PropertyManager.Controllers
{


    [Authorize]
    public class PropertiesController : ApiController
    {
        private PropertyManagerDataContext db = new PropertyManagerDataContext();


        // GET: api/Properties
        public IEnumerable<PropertiesModel> GetProperties()
        {

            //return Mapper.Map<IEnumerable<PropertiesModel>>(db.Properties);
            return Mapper.Map<IEnumerable<PropertiesModel>>(db.Properties.Where(p => p.User.UserName == User.Identity.Name));
        }

        // GET: api/Properties/5
        [ResponseType(typeof(PropertiesModel))]
        public IHttpActionResult GetProperty(int id)
        {
            //Property property = db.Properties.Find(id);
            Property property = db.Properties.FirstOrDefault(p => p.User.UserName == User.Identity.Name && p.PropertyId == id);

            if (property == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PropertiesModel>(property));
        }

        // GET: api/Properties/5/workorders
        [Route("api/properties/{PropertyId}/workorders")]
        public IEnumerable<WorkOrdersModel> GetWorkOrdersForProperty(int PropertyId)
        {
            var workOrders = db.WorkOrders.Where(p => p.Property.User.UserName == User.Identity.Name && p.WorkOrderId == PropertyId);
            return Mapper.Map<IEnumerable<WorkOrdersModel>>(workOrders);
        }

        // GET: api/Properties/5/leases
        [Route("api/properties/{PropertyId}/leases")]
        public IEnumerable<LeasesModel> GetLeasesForProperty(int PropertyId)
        {
            var leases = db.Leases.Where(p => p.Property.User.UserName == User.Identity.Name && p.LeaseId == PropertyId);
            return Mapper.Map<IEnumerable<LeasesModel>>(leases);
        }


        // PUT: api/Properties/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProperty(int id, PropertiesModel property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != property.PropertyId)
            {
                return BadRequest();
            }


            //var dbProperty = db.Properties.Find(id);
            Property dbProperty = db.Properties.FirstOrDefault(p => p.User.UserName == User.Identity.Name && p.PropertyId == id);
            if (dbProperty == null)
            {
                return BadRequest();
            }

            dbProperty.Update(property);

            db.Entry(dbProperty).State = EntityState.Modified;


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
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

        // POST: api/Properties
        [ResponseType(typeof(Property))]
        public IHttpActionResult PostProperty(Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var dbProperty = new Property(property);
            //dbProperty.User = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            //db.Properties.Add(dbProperty);

            //db.SaveChanges();

            //property.PropertyId = dbProperty.PropertyId;

            property.User = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            db.Properties.Add(property);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = property.PropertyId }, property);
        }

        // DELETE: api/Properties/5
        [ResponseType(typeof(PropertiesModel))]
        public IHttpActionResult DeleteProperty(int id)
        {
            //Property property = db.Properties.Find(id);
            Property property = db.Properties.FirstOrDefault(p => p.User.UserName == User.Identity.Name && p.PropertyId == id);
            if (property == null)
            {
                return NotFound();
            }

            db.Properties.Remove(property);
            db.SaveChanges();

            return Ok(Mapper.Map<PropertiesModel>(property));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyExists(int id)
        {
            return db.Properties.Count(e => e.PropertyId == id) > 0;
        }
    }
}