using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.MaterialViewModels;

namespace WebApp.UI.ViewModels.OrderProductViewModels
{
    public class DeleteMaterialFromOrderProductViewModel
    {
        public DeleteMaterialFromOrderProductViewModel()
        {

        }

        public DeleteMaterialFromOrderProductViewModel(OrderProduct orderProduct, Material material)
        {
            OrderProduct = new OrderProductViewModel(orderProduct);
            Material = new MaterialViewModel(material);
        }

        [Display(Name = "Продукция заказа")]
        public OrderProductViewModel OrderProduct { get; set; }

        [Display(Name = "Материал")]
        public MaterialViewModel Material { get; set; }
    }
}