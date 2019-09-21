using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;

namespace WebApp.UI.ViewModels.ReportViewModels
{
    public class MaterialsReportViewModel
    {
        public MaterialsReportViewModel()
        {
            Orders = new Dictionary<int, string>();
            Products = new Dictionary<int, string>();
            Materials = new Dictionary<int, string>();

            OrderProductMaterials = new List<OrderProductMaterial>();
        }

        [Display(Name = "Материал")]
        public int SelectedMaterialId { get; set; }

        public Dictionary<int, string> Orders { get; set; }

        public Dictionary<int, string> Products { get; set; }

        public Dictionary<int, string> Materials { get; set; }

        public List<OrderProductMaterial> OrderProductMaterials { get; set; }
    }
}