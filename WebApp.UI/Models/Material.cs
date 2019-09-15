using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.UI.Models
{
    public class Material
    {
        public Material()
        {
            OrderProductMaterials = new HashSet<OrderProductMaterial>();
            OrderProductDeliveryMaterials = new HashSet<OrderProductDeliveryMaterial>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<OrderProductMaterial> OrderProductMaterials { get; set; }

        public virtual ICollection<OrderProductDeliveryMaterial> OrderProductDeliveryMaterials { get; set; }
    }
}