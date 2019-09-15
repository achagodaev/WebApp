using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.UI.ViewModels.OrderProductDeliveryViewModels
{
    [DisplayName("Добавление материала")]
    public class AddMaterialToOrderProductDeliveryViewModel
    {
        public AddMaterialToOrderProductDeliveryViewModel()
        {
            AvailableMaterials = new List<SelectListItem>();
        }

        [Display(Name = "Поставка материалов продукции заказа")]
        public OrderProductDeliveryViewModel OrderProductDelivery { get; set; }

        [Required(ErrorMessage = "Выберите материал")]
        [Display(Name = "Материал")]
        public int MaterialId { get; set; }

        [Required(ErrorMessage = "Введите количество")]
        [Display(Name = "Поставка. Количество")]
        public double DeliveryQuantity { get; set; }

        [Display(Name = "Приемка. Количество")]
        public double? AcceptanceQuantity { get; set; }

        public List<SelectListItem> AvailableMaterials { get; set; }
    }
}