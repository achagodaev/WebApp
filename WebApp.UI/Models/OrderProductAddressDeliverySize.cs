using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.UI.Models
{
    public class OrderProductAddressDeliverySize
    {
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
        public int AddressId { get; set; }

        [Key]
        [Column(Order = 3)]
        public int DeliveryId { get; set; }

        [Key]
        [Column(Order = 4)]
        public int SizeId { get; set; }

        public int DeliveryQuantity { get; set; }

        public int? AcceptanceQuantity { get; set; }

        public virtual Size Size { get; set; }

        public virtual OrderProductAddressDelivery OrderProductAddressDelivery { get; set; }
    }
}