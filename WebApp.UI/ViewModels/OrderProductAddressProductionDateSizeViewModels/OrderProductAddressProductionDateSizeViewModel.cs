using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.OrderProductAddressProductionDateViewModels;
using WebApp.UI.ViewModels.SizeViewModels;

namespace WebApp.UI.ViewModels.OrderProductAddressProductionDateSizeViewModels
{
    public class OrderProductAddressProductionDateSizeViewModel
    {
        public OrderProductAddressProductionDateSizeViewModel()
        {

        }

        public OrderProductAddressProductionDateSizeViewModel(OrderProductAddressProductionDateSize orderProductAddressProductionDateSize)
        {
            OrderProductAddressProductionDate = new OrderProductAddressProductionDateViewModel(orderProductAddressProductionDateSize.OrderProductAddressProductionDate);
            Size = new SizeViewModel(orderProductAddressProductionDateSize.Size);
            Quantity = orderProductAddressProductionDateSize.Quantity;
        }

        [Display(Name = "Дата производства продукции грузополучателя заказа")]
        public OrderProductAddressProductionDateViewModel OrderProductAddressProductionDate { get; set; }

        [Display(Name = "Размер")]
        public SizeViewModel Size { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }
    }
}