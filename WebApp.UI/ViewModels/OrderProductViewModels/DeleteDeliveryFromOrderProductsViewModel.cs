using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.AddressViewModels;

namespace WebApp.UI.ViewModels.OrderProductViewModels
{
    [DisplayName("Удаление поставки")]
    public class DeleteDeliveryFromOrderProductViewModel
    {
        public DeleteDeliveryFromOrderProductViewModel()
        {

        }

        public DeleteDeliveryFromOrderProductViewModel(OrderProduct orderProduct, int deliveryId)
        {
            OrderProduct = new OrderProductViewModel(orderProduct);
            DeliveryId = deliveryId;
        }

        [Display(Name = "Продукция заказа")]
        public OrderProductViewModel OrderProduct { get; set; }

        [Display(Name = "Идентификатор поставки")]
        public int DeliveryId { get; set; }
    }
}