using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.UI.Models
{
    public class OrderProductAddressProductionDate
    {
        public OrderProductAddressProductionDate()
        {
            OrderProductAddressProductionDateSizes = new HashSet<OrderProductAddressProductionDateSize>();
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
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        public virtual OrderProductAddress OrderProductAddress { get; set; }

        public virtual ICollection<OrderProductAddressProductionDateSize> OrderProductAddressProductionDateSizes { get; set; }
    }
}