using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;

namespace WebApp.UI.ViewModels.ReportViewModels
{
    public class OrderProductDeliveryMaterialsReportViewModel
    {
        public OrderProductDeliveryMaterialsReportViewModel()
        {
            Orders = new Dictionary<int, string>();
            Products = new Dictionary<int, string>();
            Materials = new Dictionary<int, string>();

            OrderProducts = new List<OrderProduct>();
        }

        [Display(Name = "Заказ")]
        public int SelectedOrderId { get; set; }

        [Display(Name = "Группировка")]
        public string SelectedGroupBy { get; set; }

        public Dictionary<int, string> Orders { get; set; }

        public Dictionary<int, string> Products { get; set; }

        public Dictionary<int, string> Materials { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}