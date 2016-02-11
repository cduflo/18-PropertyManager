using PropertyManager.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PropertyManager.Infrastructure
{
    public class PropertyManagerDataContext : DbContext
    {
        public PropertyManagerDataContext() : base("PropertyManager")
        {
        }

        public IDbSet<Address> Addresses { get; set; }
        public IDbSet<Lease> Leases { get; set; }
        public IDbSet<Property> Properties { get; set; }
        public IDbSet<Tenant> Tenants { get; set; }
        public IDbSet<WorkOrder> WorkOrders { get; set; }

        public override int SaveChanges()
        {
            UpdateDates();
            return base.SaveChanges();
        }

        private void UpdateDates()
        {
            foreach (var change in ChangeTracker.Entries<WorkOrder>())
            {
                var values = change.CurrentValues;
                foreach (var name in values.PropertyNames)
                {
                    var value = values[name];
                    if (value is DateTime)
                    {
                        var date = (DateTime)value;
                        if (date < SqlDateTime.MinValue.Value)
                        {
                            values[name] = SqlDateTime.MinValue.Value;
                        }
                        else if (date > SqlDateTime.MaxValue.Value)
                        {
                            values[name] = SqlDateTime.MaxValue.Value;
                        }
                    }
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
            .HasMany(a => a.Properties)
            .WithRequired(p => p.Address)
            .HasForeignKey(p => p.AddressId);

            modelBuilder.Entity<Address>()
            .HasMany(a => a.Tenants)
            .WithRequired(t => t.Address)
            .HasForeignKey(t => t.AddressId)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Property>()
            .HasMany(p => p.Leases)
            .WithRequired(l => l.Property)
            .HasForeignKey(l => l.PropertyId);

            modelBuilder.Entity<Property>()
            .HasMany(p => p.WorkOrders)
            .WithRequired(w => w.Property)
            .HasForeignKey(w => w.PropertyId);

            modelBuilder.Entity<Tenant>()
            .HasMany(t => t.Leases)
            .WithRequired(l => l.Tenant)
            .HasForeignKey(l => l.TenantId);

            modelBuilder.Entity<Tenant>()
            .HasMany(t => t.WorkOrders)
            .WithRequired(w => w.Tenant)
            .HasForeignKey(w => w.TenantId);

        }
    }
}
