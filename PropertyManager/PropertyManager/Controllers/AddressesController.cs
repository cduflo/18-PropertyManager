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
    public class AddressesController : ApiController
    {
        private PropertyManagerDataContext db = new PropertyManagerDataContext();

        // GET: api/Addresses
        public IEnumerable<AddressesModel> GetAddresses()
        {
            return Mapper.Map<IEnumerable<AddressesModel>>(db.Addresses);
        }

        // GET: api/Addresses/5
        [ResponseType(typeof(AddressesModel))]
        public IHttpActionResult GetAddress(int id)
        {
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<AddressesModel>(address));
        }

        // GET: api/Addresses/5/tenants
        [Route("api/addresses/{AddressId}/tenants")]
        public IEnumerable<TenantsModel> GetTenantsForAddresses(int AddressId)
        {
            var tenants = db.Tenants.Where(m => m.AddressId == AddressId);
            return Mapper.Map<IEnumerable<TenantsModel>>(tenants);
        }

        // GET: api/Addresses/5/properties
        [Route("api/addresses/{AddressId}/properties")]
        public IEnumerable<PropertiesModel> GetPropertiesForAddresses(int AddressId)
        {
            var properties = db.Properties.Where(m => m.AddressId == AddressId);
            return Mapper.Map<IEnumerable<PropertiesModel>>(properties);
        }


        // PUT: api/Addresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddress(int id, AddressesModel address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.AddressID)
            {
                return BadRequest();
            }

            var dbAddress = db.Addresses.Find(id);
            dbAddress.Update(address);

            db.Entry(dbAddress).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        [ResponseType(typeof(AddressesModel))]
        public IHttpActionResult PostAddress(AddressesModel address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var add = new Address(address);

            db.Addresses.Add(add);
            db.SaveChanges();

            address.AddressID = add.AddressID;
            
            return CreatedAtRoute("DefaultApi", new { id = address.AddressID }, address);
        }

        // DELETE: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult DeleteAddress(int id)
        {
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return NotFound();
            }

            db.Addresses.Remove(address);
            db.SaveChanges();

            return Ok(address);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddressExists(int id)
        {
            return db.Addresses.Count(e => e.AddressID == id) > 0;
        }
    }
}