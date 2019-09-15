using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.ViewModels.OrderProductAddressSizeViewModels;

namespace WebApp.UI.ViewModels.OrderProductAddressProductionDateViewModels
{
    [DisplayName("Добавление всех размерных данных продукции")]
    public class AddAllSizesToOrderProductAddressProductionDateViewModel
    {
        public AddAllSizesToOrderProductAddressProductionDateViewModel()
        {
            OrderProductAddressSizes = new List<OrderProductAddressSizeViewModel>();
        }

        [Display(Name = "Дата производства продукции грузополучателя заказа")]
        public OrderProductAddressProductionDateViewModel OrderProductAddressProductionDate { get; set; }

        [Display(Name = "Размерные данные")]
        public List<OrderProductAddressSizeViewModel> OrderProductAddressSizes { get; set; }
    }
}