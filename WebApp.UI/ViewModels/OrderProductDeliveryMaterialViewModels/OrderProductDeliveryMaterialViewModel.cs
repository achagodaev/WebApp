using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.MaterialViewModels;
using WebApp.UI.ViewModels.OrderProductDeliveryViewModels;

namespace WebApp.UI.ViewModels.OrderProductDeliveryMaterialViewModels
{
    public class OrderProductDeliveryMaterialViewModel
    {
        public OrderProductDeliveryMaterialViewModel()
        {

        }

        public OrderProductDeliveryMaterialViewModel(OrderProductDeliveryMaterial orderProductDeliveryMaterial)
        {
            OrderProductDelivery = new OrderProductDeliveryViewModel(orderProductDeliveryMaterial.OrderProductDelivery);
            Material = new MaterialViewModel(orderProductDeliveryMaterial.Material);
            DeliveryQuantity = orderProductDeliveryMaterial.DeliveryQuantity;
            AcceptanceQuantity = orderProductDeliveryMaterial.AcceptanceQuantity;
        }

        [Display(Name = "Поставка продукции заказа")]
        public OrderProductDeliveryViewModel OrderProductDelivery { get; set; }

        [Display(Name = "Материал")]
        public MaterialViewModel Material { get; set; }

        [Display(Name = "Поставка. Количество")]
        public double DeliveryQuantity { get; set; }

        [Display(Name = "Приемка. Количество")]
        public double? AcceptanceQuantity { get; set; }

    }
}