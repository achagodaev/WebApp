using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;

namespace WebApp.UI.ViewModels.ReportViewModels
{
    public class ProductsReportViewModel
    {
        public ProductsReportViewModel()
        {
            Orders = new Dictionary<int, string>();
            Products = new Dictionary<int, string>();
            Addresses = new Dictionary<int, string>();

            OrderProducts = new List<OrderProduct>();
            OrderProductAddresses = new List<OrderProductAddress>();
        }

        [Display(Name = "Продукция")]
        public int SelectedProductId { get; set; }

        public Dictionary<int, string> Orders { get; set; }

        public Dictionary<int, string> Products { get; set; }

        public Dictionary<int, string> Addresses { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }

        public List<OrderProductAddress> OrderProductAddresses { get; set; }
    }
}