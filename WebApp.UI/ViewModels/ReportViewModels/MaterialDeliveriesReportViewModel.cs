﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;

namespace WebApp.UI.ViewModels.ReportViewModels
{
    public class MaterialDeliveriesReportViewModel
    {
        public MaterialDeliveriesReportViewModel()
        {
            Orders = new Dictionary<int, string>();
            Products = new Dictionary<int, string>();
            Suppliers = new Dictionary<int, string>();
            Materials = new Dictionary<int, string>();

            OrderProductDeliveryMaterials = new List<OrderProductDeliveryMaterial>();
        }

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

        public List<OrderProductDeliveryMaterial> OrderProductDeliveryMaterials { get; set; }
    }
}