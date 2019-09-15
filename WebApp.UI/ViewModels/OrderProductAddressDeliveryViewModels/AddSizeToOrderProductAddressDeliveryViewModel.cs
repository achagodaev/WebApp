using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.UI.ViewModels.OrderProductAddressDeliveryViewModels
{
    [DisplayName("Добавление размерных данных")]
    public class AddSizeToOrderProductAddressDeliveryViewModel
    {
        public AddSizeToOrderProductAddressDeliveryViewModel()
        {
            AvailableSizes = new List<SelectListItem>();
        }

        [Display(Name = "Поставка готовой продукции грузополучателя заказа")]
        public OrderProductAddressDeliveryViewModel OrderProductAddressDelivery { get; set; }

        [Required(ErrorMessage = "Выберите размер")]
        [Display(Name = "Размер")]
        public int SizeId { get; set; }

        [Required(ErrorMessage = "Введите количество")]
        [Display(Name = "Поставка. Количество")]
        public int DeliveryQuantity { get; set; }

        [Display(Name = "Приемка. Количество")]
        public int? AcceptanceQuantity { get; set; }

        public List<SelectListItem> AvailableSizes { get; set; }
    }
}