using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;

namespace WebApp.UI.ViewModels.OrderProductAddressViewModels
{
    [DisplayName("Удаление поставки готовой продукции")]
    public class DeleteDeliveryFromOrderProductAddressViewModel
    {
        public DeleteDeliveryFromOrderProductAddressViewModel()
        {

        }

        public DeleteDeliveryFromOrderProductAddressViewModel(OrderProductAddress orderProductAddress, int deliveryId)
        {
            OrderProductAddress = new OrderProductAddressViewModel(orderProductAddress);
            DeliveryId = deliveryId;
        }

        [Display(Name = "Грузополучатель продукции заказа")]
        public OrderProductAddressViewModel OrderProductAddress { get; set; }

        [Display(Name = "Идентификатор поставки готовой продукции")]
        public int DeliveryId { get; set; }
    }
}