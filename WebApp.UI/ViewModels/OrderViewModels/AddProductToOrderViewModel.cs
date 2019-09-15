using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.UI.ViewModels.OrderViewModels
{
    public class AddProductToOrderViewModel
    {
        public AddProductToOrderViewModel()
        {
            AvailableProducts = new List<SelectListItem>();
        }

        [Display(Name = "Заказ")]
        public OrderViewModel Order { get; set; }

        [Required(ErrorMessage = "Выберите продукцию")]
        [Display(Name = "Продукция")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        public List<SelectListItem> AvailableProducts { get; set; }
    }
}