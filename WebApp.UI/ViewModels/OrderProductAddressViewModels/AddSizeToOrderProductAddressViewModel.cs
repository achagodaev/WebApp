using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.UI.ViewModels.OrderProductAddressViewModels
{
    [DisplayName("Добавление размерных данных продукции")]
    public class AddSizeToOrderProductAddressViewModel
    {
        public AddSizeToOrderProductAddressViewModel()
        {
            AvailableSizes = new List<SelectListItem>();
        }

        [Display(Name = "Грузополучатель продукции заказа")]
        public OrderProductAddressViewModel OrderProductAddress { get; set; }

        [Required(ErrorMessage = "Выберите размер")]
        [Display(Name = "Размер")]
        public int SizeId { get; set; }

        [Required(ErrorMessage = "Введите количество")]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        public List<SelectListItem> AvailableSizes { get; set; }
    }
}