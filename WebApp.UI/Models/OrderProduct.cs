using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.UI.Models
{
    public class OrderProduct
    {
        public OrderProduct()
        {
            OrderProductAddresses = new HashSet<OrderProductAddress>();
            OrderProductMaterials = new HashSet<OrderProductMaterial>();
            OrderProductDeliveries = new HashSet<OrderProductDelivery>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        public virtual ICollection<OrderProductAddress> OrderProductAddresses { get; set; }

        public virtual ICollection<OrderProductMaterial> OrderProductMaterials { get; set; }

        public virtual ICollection<OrderProductDelivery> OrderProductDeliveries { get; set; }
    }
}