using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.UI.ViewModels.OrderViewModels
{
    public class CreateOrderViewModel
    {
        public CreateOrderViewModel()
        {
            AvailableCustomers = new List<SelectListItem>();
        }

        [Required(ErrorMessage = "Введите наименование")]
        [StringLength(50, ErrorMessage = "Значение должно содержать не более 50 символов")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Выберите заказчика")]
        [Display(Name = "Заказчик")]
        public int CustomerId { get; set; }

        public List<SelectListItem> AvailableCustomers { get; set; }
    }
}