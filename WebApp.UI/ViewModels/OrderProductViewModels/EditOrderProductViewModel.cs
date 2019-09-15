using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.ViewModels.OrderProductAddressViewModels;
using WebApp.UI.ViewModels.OrderProductDeliveryViewModels;
using WebApp.UI.ViewModels.OrderProductMaterialViewModels;
using WebApp.UI.ViewModels.OrderViewModels;
using WebApp.UI.ViewModels.ProductViewModels;

namespace WebApp.UI.ViewModels.OrderProductViewModels
{
    public class EditOrderProductViewModel
    {
        public EditOrderProductViewModel()
        {
            OrderProductAddresses = new List<OrderProductAddressViewModel>();
            OrderProductMaterials = new List<OrderProductMaterialViewModel>();
            OrderProductDeliveries = new List<OrderProductDeliveryViewModel>();
        }

        [Display(Name = "Заказ")]
        public OrderViewModel Order { get; set; }

        [Display(Name = "Продукция")]
        public ProductViewModel Product { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Грузополучатели заказа")]
        public List<OrderProductAddressViewModel> OrderProductAddresses { get; set; }

        [Display(Name = "Материалы")]
        public List<OrderProductMaterialViewModel> OrderProductMaterials { get; set; }

        [Display(Name = "Материалы. Поставки")]
        public List<OrderProductDeliveryViewModel> OrderProductDeliveries { get; set; }
    }
}