using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.OrderViewModels;
using WebApp.UI.ViewModels.ProductViewModels;

namespace WebApp.UI.ViewModels.OrderProductViewModels
{
    public class OrderProductViewModel
    {
        public OrderProductViewModel()
        {

        }

        public OrderProductViewModel(OrderProduct orderProduct)
        {
            Order = new OrderViewModel(orderProduct.Order);
            Product = new ProductViewModel(orderProduct.Product);

            Price = orderProduct.Price;
        }

        [Display(Name = "Заказ")]
        public OrderViewModel Order { get; set; }

        [Display(Name = "Продукция")]
        public ProductViewModel Product { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}