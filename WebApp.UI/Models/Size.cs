using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.UI.Models
{
    public class Size
    {
        public Size()
        {
            OrderProductAddressSizes = new HashSet<OrderProductAddressSize>();
            OrderProductAddressProductionDateSizes = new HashSet<OrderProductAddressProductionDateSize>();
            OrderProductAddressDeliverySizes = new HashSet<OrderProductAddressDeliverySize>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<OrderProductAddressSize> OrderProductAddressSizes { get; set; }

        public virtual ICollection<OrderProductAddressProductionDateSize> OrderProductAddressProductionDateSizes { get; set; }

        public virtual ICollection<OrderProductAddressDeliverySize> OrderProductAddressDeliverySizes { get; set; }
    }
}