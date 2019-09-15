using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.UI.Models
{
    public class OrderProductMaterial
    {
        public OrderProductMaterial()
        {
            OrderProductDeliveryMaterials = new HashSet<OrderProductDeliveryMaterial>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaterialId { get; set; }

        public int SupplierId { get; set; }

        public double Quantity { get; set; }

        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }

        public decimal Price { get; set; }

        public double Rate { get; set; }

        public virtual OrderProduct OrderProduct { get; set; }

        public virtual Material Material { get; set; }

        public Supplier Supplier { get; set; }

        public virtual ICollection<OrderProductDeliveryMaterial> OrderProductDeliveryMaterials { get; set; }
    }
}