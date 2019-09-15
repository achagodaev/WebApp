using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.ViewModels.MaterialViewModels;
using WebApp.UI.ViewModels.OrderProductDeliveryViewModels;

namespace WebApp.UI.ViewModels.OrderProductDeliveryMaterialViewModels
{
    public class EditOrderProductDeliveryMaterialViewModel
    {
        [Display(Name = "Поставка продукции заказа")]
        public OrderProductDeliveryViewModel OrderProductDelivery { get; set; }

        [Display(Name = "Материал")]
        public MaterialViewModel Material { get; set; }

        [Required(ErrorMessage = "Введите количество")]
        [Display(Name = "Поставка. Количество")]
        public double DeliveryQuantity { get; set; }

        [Display(Name = "Приемка. Количество")]
        public double? AcceptanceQuantity { get; set; }
    }
}