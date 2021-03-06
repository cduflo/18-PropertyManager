﻿using System;
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
    public class TenantsController : ApiController
    {
        private PropertyManagerDataContext db = new PropertyManagerDataContext();

        // GET: api/Tenants
        public IEnumerable<TenantsModel> GetTenants()
        {
            //return Mapper.Map<IEnumerable<TenantsModel>>(db.Tenants);
            return Mapper.Map<IEnumerable<TenantsModel>>(db.Tenants.Where(p => p.User.UserName == User.Identity.Name));
        }

        // GET: api/Tenants/5
        [ResponseType(typeof(TenantsModel))]
        public IHttpActionResult GetTenant(int id)
        {
            //Tenant tenant = db.Tenants.Find(id);
            Tenant tenant = db.Tenants.FirstOrDefault(p => p.User.UserName == User.Identity.Name && p.TenantId == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<TenantsModel>(tenant));
        }

        // GET: api/Tenants/5/leases
        [Route("api/tenants/{TenantId}/leases")]
        public IEnumerable<LeasesModel> GetLeasesForTenant(int TenantId)
        {
            var leases = db.Leases.Where(p => p.Property.User.UserName == User.Identity.Name && p.LeaseId == TenantId);
            return Mapper.Map<IEnumerable<LeasesModel>>(leases);
        }

        // GET: api/Tenants/5/workorders
        [Route("api/tenants/{TenantId}/workorders")]
        public IEnumerable<WorkOrdersModel> GetWorkOrdersForTenant(int TenantId)
        {
            var workOrders = db.WorkOrders.Where(p => p.Property.User.UserName == User.Identity.Name && p.WorkOrderId == TenantId); 
            return Mapper.Map<IEnumerable<WorkOrdersModel>>(workOrders);
        }

        // PUT: api/Tenants/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTenant(int id, TenantsModel tenant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tenant.TenantId)
            {
                return BadRequest();
            }


            //var dbTenants = db.Tenants.Find(id);
            var dbTenants = db.Tenants.FirstOrDefault(p => p.User.UserName == User.Identity.Name && p.TenantId == id);
            if (dbTenants == null)
            {
                return BadRequest();
            }

            dbTenants.Update(tenant);

            db.Entry(dbTenants).State = EntityState.Modified;


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TenantExists(id))
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

        // POST: api/Tenants
        [ResponseType(typeof(Tenant))]
        public IHttpActionResult PostTenant(Tenant tenant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var dbTenant = new Tenant(tenant);
            //dbTenant.User = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            //db.Tenants.Add(dbTenant);

            //db.SaveChanges();

            //tenant.TenantId = dbTenant.TenantId;


            tenant.User = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            db.Tenants.Add(tenant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tenant.TenantId }, tenant);
        }

        // DELETE: api/Tenants/5
        [ResponseType(typeof(TenantsModel))]
        public IHttpActionResult DeleteTenant(int id)
        {
            //Tenant tenant = db.Tenants.Find(id);
            Tenant tenant = db.Tenants.FirstOrDefault(p => p.User.UserName == User.Identity.Name && p.TenantId == id);
            if (tenant == null)
            {
                return NotFound();
            }

            db.Tenants.Remove(tenant);
            db.SaveChanges();

            return Ok(Mapper.Map<TenantsModel>(tenant));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TenantExists(int id)
        {
            return db.Tenants.Count(e => e.TenantId == id) > 0;
        }
    }
}