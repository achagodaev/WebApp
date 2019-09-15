using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.UI.Models
{
    public class OrderProductAddress
    {
        public OrderProductAddress()
        {
            OrderProductAddressSizes = new HashSet<OrderProductAddressSize>();
            OrderProductAddressProductionDates = new HashSet<OrderProductAddressProductionDate>();
            OrderProductAddressDeliveries = new HashSet<OrderProductAddressDelivery>();
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

        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }

        public virtual OrderProduct OrderProduct { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<OrderProductAddressSize> OrderProductAddressSizes { get; set; }

        public virtual ICollection<OrderProductAddressProductionDate> OrderProductAddressProductionDates { get; set; }

        public virtual ICollection<OrderProductAddressDelivery> OrderProductAddressDeliveries { get; set; }
    }
}