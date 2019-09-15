using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.UI.Models
{
    public class OrderProductAddressDelivery
    {
        public OrderProductAddressDelivery()
        {
            OrderProductAddressDeliverySizes = new HashSet<OrderProductAddressDeliverySize>();
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
        public int AddressId { get; set; }


        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeliveryId { get; set; }

        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }

        public string DeliveryImagePath { get; set; }


        public int? AcceptanceId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AcceptanceDate { get; set; }

        public string AcceptanceImagePath { get; set; }


        public virtual OrderProductAddress OrderProductAddress { get; set; }

        public virtual ICollection<OrderProductAddressDeliverySize> OrderProductAddressDeliverySizes { get; set; }
    }
}