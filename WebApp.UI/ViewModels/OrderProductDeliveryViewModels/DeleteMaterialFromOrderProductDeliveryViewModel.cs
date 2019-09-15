using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.MaterialViewModels;

namespace WebApp.UI.ViewModels.OrderProductDeliveryViewModels
{
    [DisplayName("Удаление материала")]
    public class DeleteMaterialFromOrderProductDeliveryViewModel
    {
        public DeleteMaterialFromOrderProductDeliveryViewModel()
        {

        }

        public DeleteMaterialFromOrderProductDeliveryViewModel(OrderProductDelivery orderProductDelivery, Material material)
        {
            OrderProductDelivery = new OrderProductDeliveryViewModel(orderProductDelivery);
            Material = new MaterialViewModel(material); 
        }

        [Display(Name = "Поставка материалов продукции заказа")]
        public OrderProductDeliveryViewModel OrderProductDelivery { get; set; }

        [Display(Name = "Материал")]
        public MaterialViewModel Material { get; set; }
    }
}