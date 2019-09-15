using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.OrderProductAddressViewModels;
using WebApp.UI.ViewModels.SizeViewModels;

namespace WebApp.UI.ViewModels.OrderProductAddressSizeViewModels
{
    public class OrderProductAddressSizeViewModel
    {
        public OrderProductAddressSizeViewModel()
        {

        }

        public OrderProductAddressSizeViewModel(OrderProductAddressSize orderProductAddressSize)
        {
            OrderProductAddress = new OrderProductAddressViewModel(orderProductAddressSize.OrderProductAddress);
            Size = new SizeViewModel(orderProductAddressSize.Size);
            Quantity = orderProductAddressSize.Quantity;
        }

        [Display(Name = "Грузополучатель продукции заказа")]
        public OrderProductAddressViewModel OrderProductAddress { get; set; }

        [Display(Name = "Размер")]
        public SizeViewModel Size { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }
    }
}