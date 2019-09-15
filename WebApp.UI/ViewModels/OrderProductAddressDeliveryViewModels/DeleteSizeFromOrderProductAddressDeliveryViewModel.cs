using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.SizeViewModels;

namespace WebApp.UI.ViewModels.OrderProductAddressDeliveryViewModels
{
    [DisplayName("Удаление размерных данных")]
    public class DeleteSizeFromOrderProductAddressDeliveryViewModel
    {
        public DeleteSizeFromOrderProductAddressDeliveryViewModel()
        {

        }

        public DeleteSizeFromOrderProductAddressDeliveryViewModel(OrderProductAddressDelivery orderProductAddressDelivery, Size size)
        {
            OrderProductAddressDelivery = new OrderProductAddressDeliveryViewModel(orderProductAddressDelivery);
            Size = new SizeViewModel(size);
        }

        [Display(Name = "Поставка готовой продукции грузополучателя заказа")]
        public OrderProductAddressDeliveryViewModel OrderProductAddressDelivery { get; set; }

        [Display(Name = "Размер")]
        public SizeViewModel Size { get; set; }
    }
}