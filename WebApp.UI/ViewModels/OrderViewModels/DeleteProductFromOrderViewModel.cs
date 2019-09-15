using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.ProductViewModels;

namespace WebApp.UI.ViewModels.OrderViewModels
{
    public class DeleteProductFromOrderViewModel
    {
        public DeleteProductFromOrderViewModel()
        {

        }

        public DeleteProductFromOrderViewModel(Order order, Product product)
        {
            Order = new OrderViewModel(order);
            Product = new ProductViewModel(product);
        }

        [Display(Name = "Заказ")]
        public OrderViewModel Order { get; set; }

        [Display(Name = "Продукция")]
        public ProductViewModel Product { get; set; }
    }
}