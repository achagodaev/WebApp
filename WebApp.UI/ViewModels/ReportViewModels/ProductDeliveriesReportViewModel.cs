using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;

namespace WebApp.UI.ViewModels.ReportViewModels
{
    public class ProductDeliveriesReportViewModel
    {
        public ProductDeliveriesReportViewModel()
        {
            Orders = new Dictionary<int, string>();
            Products = new Dictionary<int, string>();
            Addresses = new Dictionary<int, string>();

            OrderProductDeliveries = new List<OrderProductDelivery>();
            OrderProductAddressDeliverySizes = new List<OrderProductAddressDeliverySize>();
        }

        [Display(Name = "Заказ")]
        public int SelectedOrderId { get; set; }

        [Display(Name = "Продукция")]
        public int SelectedProductId { get; set; }

        [Display(Name = "Грузополучатель")]
        public int SelectedAddressId { get; set; }

        public Dictionary<int, string> Orders { get; set; }

        public Dictionary<int, string> Products { get; set; }

        public Dictionary<int, string> Addresses { get; set; }

        public List<OrderProductDelivery> OrderProductDeliveries { get; set; }

        public List<OrderProductAddressDeliverySize> OrderProductAddressDeliverySizes { get; set; }
    }
}