using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.OrderProductAddressViewModels;
using WebApp.UI.ViewModels.OrderProductDeliveryViewModels;
using WebApp.UI.ViewModels.OrderProductMaterialViewModels;
using WebApp.UI.ViewModels.SupplierViewModels;

namespace WebApp.UI.ViewModels.OrderProductViewModels
{
    public class OrderProductDetailsViewModel : OrderProductViewModel
    {
        public OrderProductDetailsViewModel(OrderProduct orderProduct)
            : base(orderProduct)
        {
            OrderProductAddresses = new List<OrderProductAddressViewModel>();
            OrderProductMaterials = new List<OrderProductMaterialViewModel>();
            OrderProductDeliveries = new List<OrderProductDeliveryViewModel>();
        }

        [Display(Name = "Грузополучатели")]
        public List<OrderProductAddressViewModel> OrderProductAddresses { get; set; }

        [Display(Name = "Материалы")]
        public List<OrderProductMaterialViewModel> OrderProductMaterials { get; set; }

        [Display(Name = "Материалы. Поставки")]
        public List<OrderProductDeliveryViewModel> OrderProductDeliveries { get; set; }
    }
}