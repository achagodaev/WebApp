using WebApp.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.ViewModels.CustomerViewModels;
using WebApp.UI.ViewModels.OrderProductViewModels;

namespace WebApp.UI.ViewModels.OrderViewModels
{
    public class OrderDetailsViewModel : OrderViewModel
    {
        public OrderDetailsViewModel(Order order)
            : base(order)
        {
            Customer = new CustomerViewModel(order.Customer);

            OrderProducts = new List<OrderProductViewModel>();
        }

        [Display(Name = "Заказчик")]
        public CustomerViewModel Customer { get; set; }

        [Display(Name = "Продукция заказа")]
        public List<OrderProductViewModel> OrderProducts { get; set; }
    }
}