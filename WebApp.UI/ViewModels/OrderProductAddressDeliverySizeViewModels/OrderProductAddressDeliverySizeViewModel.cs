using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.OrderProductAddressDeliveryViewModels;
using WebApp.UI.ViewModels.OrderProductAddressViewModels;
using WebApp.UI.ViewModels.SizeViewModels;

namespace WebApp.UI.ViewModels.OrderProductAddressDeliverySizeViewModels
{
    public class OrderProductAddressDeliverySizeViewModel
    {
        public OrderProductAddressDeliverySizeViewModel()
        {

        }

        public OrderProductAddressDeliverySizeViewModel(OrderProductAddressDeliverySize orderProductAddressDeliverySize)
        {
            OrderProductAddressDelivery = new OrderProductAddressDeliveryViewModel(orderProductAddressDeliverySize.OrderProductAddressDelivery);
            Size = new SizeViewModel(orderProductAddressDeliverySize.Size);
            DeliveryQuantity = orderProductAddressDeliverySize.DeliveryQuantity;
            AcceptanceQuantity = orderProductAddressDeliverySize.AcceptanceQuantity;
        }

        [Display(Name = "Поставка готовой продукции грузополучателя заказа")]
        public OrderProductAddressDeliveryViewModel OrderProductAddressDelivery { get; set; }

        [Display(Name = "Размер")]
        public SizeViewModel Size { get; set; }

        [Display(Name = "Поставка. Количество")]
        public int DeliveryQuantity { get; set; }

        [Display(Name = "Приемка. Количество")]
        public int? AcceptanceQuantity { get; set; }
    }
}