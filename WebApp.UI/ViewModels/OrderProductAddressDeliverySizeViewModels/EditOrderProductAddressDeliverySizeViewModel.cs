using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.UI.ViewModels.OrderProductAddressDeliveryViewModels;
using WebApp.UI.ViewModels.SizeViewModels;

namespace WebApp.UI.ViewModels.OrderProductAddressDeliverySizeViewModels
{
    public class EditOrderProductAddressDeliverySizeViewModel
    {
        public EditOrderProductAddressDeliverySizeViewModel()
        {
            AvailableSizes = new List<SelectListItem>();
        }

        [Display(Name = "Грузополучатель готовой продукции заказа")]
        public OrderProductAddressDeliveryViewModel OrderProductAddressDelivery { get; set; }

        [Display(Name = "Размер")]
        public SizeViewModel Size { get; set; }

        [Required(ErrorMessage = "Введите количество")]
        [Display(Name = "Поставка. Количество")]
        public int DeliveryQuantity { get; set; }

        [Display(Name = "Приемка. Количество")]
        public int? AcceptanceQuantity { get; set; }

        public List<SelectListItem> AvailableSizes { get; set; }
    }
}