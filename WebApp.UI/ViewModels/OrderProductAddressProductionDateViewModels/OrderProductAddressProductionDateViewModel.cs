using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.OrderProductAddressProductionDateSizeViewModels;
using WebApp.UI.ViewModels.OrderProductAddressViewModels;

namespace WebApp.UI.ViewModels.OrderProductAddressProductionDateViewModels
{
    public class OrderProductAddressProductionDateViewModel
    {
        public OrderProductAddressProductionDateViewModel()
        {

        }

        public OrderProductAddressProductionDateViewModel(OrderProductAddressProductionDate orderProductAddressProductionDate)
        {
            OrderProductAddress = new OrderProductAddressViewModel(orderProductAddressProductionDate.OrderProductAddress);
            ProductionDate = orderProductAddressProductionDate.ProductionDate;

            OrderProductAddressProductionDateSizes = new List<OrderProductAddressProductionDateSizeViewModel>();
        }

        [Display(Name = "Грузополучатель продукции заказа")]
        public OrderProductAddressViewModel OrderProductAddress { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата производства")]
        public DateTime ProductionDate { get; set; }

        [Display(Name = "Готовая продукция. Размерные данные")]
        public List<OrderProductAddressProductionDateSizeViewModel> OrderProductAddressProductionDateSizes { get; set; }
    }
}