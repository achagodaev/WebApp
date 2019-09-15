using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.UI.ViewModels.OrderProductAddressProductionDateViewModels;
using WebApp.UI.ViewModels.SizeViewModels;

namespace WebApp.UI.ViewModels.OrderProductAddressProductionDateSizeViewModels
{
    public class EditOrderProductAddressProductionDateSizeViewModel
    {
        public EditOrderProductAddressProductionDateSizeViewModel()
        {
            AvailableSizes = new List<SelectListItem>();
        }

        [Display(Name = "Дата производства продукции грузополучателя заказа")]
        public OrderProductAddressProductionDateViewModel OrderProductAddressProductionDate { get; set; }

        [Display(Name = "Размер")]
        public SizeViewModel Size { get; set; }

        [Required(ErrorMessage = "Введите количество")]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        public List<SelectListItem> AvailableSizes { get; set; }
    }
}