using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.UI.ViewModels.OrderProductViewModels;

namespace WebApp.UI.ViewModels.OrderViewModels
{
    public class EditOrderViewModel
    {
        public EditOrderViewModel()
        {
            OrderProducts = new List<OrderProductViewModel>();

            AvailableCustomers = new List<SelectListItem>();
        }

        [Display(Name = "Идентификатор")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите наименование")]
        [Display(Name = "Наименование")]
        [StringLength(50, ErrorMessage = "Значение должно содержать не более 50 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Выберите заказчика")]
        [Display(Name = "Заказчик")]
        public int CustomerId { get; set; }

        [Display(Name = "Продукция заказа")]
        public List<OrderProductViewModel> OrderProducts { get; set; }

        public List<SelectListItem> AvailableCustomers { get; set; }
    }
}