using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;

namespace WebApp.UI.ViewModels.ReportViewModels
{
    public class MaterialAcceptancesReportViewModel
    {
        public MaterialAcceptancesReportViewModel()
        {
            Orders = new Dictionary<int, string>();
            Products = new Dictionary<int, string>();
            Suppliers = new Dictionary<int, string>();
            Materials = new Dictionary<int, string>();
            Deliveries = new Dictionary<int, string>();
            Acceptances = new Dictionary<int, string>();

            OrderProductDeliveryMaterials = new List<OrderProductDeliveryMaterial>();
        }

        [Display(Name = "Заказ")]
        public int SelectedOrderId { get; set; }

        [Display(Name = "Продукция")]
        public int SelectedProductId { get; set; }

        [Display(Name = "Поставщик")]
        public int SelectedSupplierId { get; set; }

        [Display(Name = "Материал")]
        public int SelectedMaterialId { get; set; }

        public Dictionary<int, string> Orders { get; set; }

        public Dictionary<int, string> Products { get; set; }

        public Dictionary<int, string> Suppliers { get; set; }

        public Dictionary<int, string> Materials { get; set; }

        public Dictionary<int, string> Deliveries { get; set; }

        public Dictionary<int, string> Acceptances { get; set; }

        public List<OrderProductDeliveryMaterial> OrderProductDeliveryMaterials { get; set; }
    }
}