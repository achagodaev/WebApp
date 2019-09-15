using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.UI.Models
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext()
        {
            Configuration.LazyLoadingEnabled = false;
            Database.Log = log => Debug.WriteLine(log);
        }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Material> Materials { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Size> Sizes { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<OrderProduct> OrderProducts { get; set; }

        public virtual DbSet<OrderProductAddress> OrderProductAddresses { get; set; }

        public virtual DbSet<OrderProductDelivery> OrderProductDeliveries { get; set; }

        public virtual DbSet<OrderProductMaterial> OrderProductMaterials { get; set; }

        public virtual DbSet<OrderProductAddressSize> OrderProductAddressSizes { get; set; }

        public virtual DbSet<OrderProductAddressProductionDate> OrderProductAddressProductionDates { get; set; }

        public virtual DbSet<OrderProductAddressDelivery> OrderProductAddressDeliveries { get; set; }

        public virtual DbSet<OrderProductDeliveryMaterial> OrderProductDeliveryMaterials { get; set; }

        public virtual DbSet<OrderProductAddressProductionDateSize> OrderProductAddressProductionDateSizes { get; set; }

        public virtual DbSet<OrderProductAddressDeliverySize> OrderProductAddressDeliverySizes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProductMaterial>()
                .HasMany(opm => opm.OrderProductDeliveryMaterials)
                .WithRequired(opdm => opdm.OrderProductMaterial)
                .HasForeignKey(opdm => new { opdm.OrderId, opdm.ProductId, opdm.MaterialId })
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {


            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {


            return base.SaveChangesAsync();
        }
    }
}