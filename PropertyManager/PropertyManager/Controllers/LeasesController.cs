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
using AutoMapper;
using PropertyManager.Models;

namespace PropertyManager.Controllers
{
    [Authorize]
    public class LeasesController : ApiController
    {
        private PropertyManagerDataContext db = new PropertyManagerDataContext();

        // GET: api/Leases
        public IEnumerable<LeasesModel> GetLeases()
        {
            //return Mapper.Map<IEnumerable<LeasesModel>>(db.Leases);
            return Mapper.Map<IEnumerable<LeasesModel>>(db.Leases.Where(p => p.Property.User.UserName == User.Identity.Name));
        }

        // GET: api/Leases/5
        [ResponseType(typeof(LeasesModel))]
        public IHttpActionResult GetLease(int id)
        {
            //Lease lease = db.Leases.Find(id);
            Lease lease = db.Leases.FirstOrDefault(p => p.Property.User.UserName == User.Identity.Name && p.LeaseId == id);
            if (lease == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<LeasesModel>(lease));
        }

        // PUT: api/Leases/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLease(int id, LeasesModel lease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lease.LeaseId)
            {
                return BadRequest();
            }

            //var dbLease = db.Leases.Find(id);
            var dbLease = db.Leases.FirstOrDefault(p => p.Property.User.UserName == User.Identity.Name && p.LeaseId == id);
            if (dbLease == null)
            {
                return BadRequest();
            }

            dbLease.Update(lease);

            db.Entry(dbLease).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaseExists(id))
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

        // POST: api/Leases
        [ResponseType(typeof(Lease))]
        public IHttpActionResult PostLease(LeasesModel lease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbLease = new Lease(lease);

            db.Leases.Add(dbLease);

            db.SaveChanges();

            lease.LeaseId = dbLease.LeaseId;

            return CreatedAtRoute("DefaultApi", new { id = lease.LeaseId }, lease);
        }

        // DELETE: api/Leases/5
        [ResponseType(typeof(LeasesModel))]
        public IHttpActionResult DeleteLease(int id)
        {
           // Lease lease = db.Leases.Find(id);
            Lease lease = db.Leases.FirstOrDefault(p => p.Property.User.UserName == User.Identity.Name && p.LeaseId == id);
            if (lease == null)
            {
                return NotFound();
            }

            db.Leases.Remove(lease);
            db.SaveChanges();

            return Ok(Mapper.Map<LeasesModel>(lease));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeaseExists(int id)
        {
            return db.Leases.Count(e => e.LeaseId == id) > 0;
        }
    }
}