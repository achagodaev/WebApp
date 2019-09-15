using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.UI.ViewModels.OrderProductAddressViewModels;
using WebApp.UI.ViewModels.SizeViewModels;

namespace WebApp.UI.ViewModels.OrderProductAddressSizeViewModels
{
    public class EditOrderProductAddressSizeViewModel
    {
        public EditOrderProductAddressSizeViewModel()
        {
            AvailableSizes = new List<SelectListItem>();
        }

        [Display(Name = "Грузополучатель продукции заказа")]
        public OrderProductAddressViewModel OrderProductAddress { get; set; }

        [Display(Name = "Размер")]
        public SizeViewModel Size { get; set; }

        [Required(ErrorMessage = "Введите количество")]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        public List<SelectListItem> AvailableSizes { get; set; }
    }
}