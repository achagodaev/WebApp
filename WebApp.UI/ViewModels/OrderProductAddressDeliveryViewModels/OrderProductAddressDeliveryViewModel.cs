using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.OrderProductAddressDeliverySizeViewModels;
using WebApp.UI.ViewModels.OrderProductAddressViewModels;
using WebApp.UI.ViewModels.SizeViewModels;

namespace WebApp.UI.ViewModels.OrderProductAddressDeliveryViewModels
{
    public class OrderProductAddressDeliveryViewModel
    {
        public OrderProductAddressDeliveryViewModel()
        {

        }

        public OrderProductAddressDeliveryViewModel(OrderProductAddressDelivery orderProductAddressDelivery)
        {
            OrderProductAddress = new OrderProductAddressViewModel(orderProductAddressDelivery.OrderProductAddress);
            DeliveryId = orderProductAddressDelivery.DeliveryId;
            DeliveryDate = orderProductAddressDelivery.DeliveryDate;
            DeliveryImagePath = orderProductAddressDelivery.DeliveryImagePath;
            AcceptanceId = orderProductAddressDelivery.AcceptanceId;
            AcceptanceDate = orderProductAddressDelivery.AcceptanceDate;
            AcceptanceImagePath = orderProductAddressDelivery.AcceptanceImagePath;

            OrderProductAddressDeliverySizes = new List<OrderProductAddressDeliverySizeViewModel>();
        }

        [Display(Name = "Грузополучатель продукции заказа")]
        public OrderProductAddressViewModel OrderProductAddress { get; set; }


        [Display(Name = "Поставка готовой продукции. Идентификатор")]
        public int DeliveryId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Поставка готовой продукции. Дата")]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Поставка готовой продукции. Расположение изображения")]
        public string DeliveryImagePath { get; set; }


        [Display(Name = "Приемка готовой продукции. Идентификатор")]
        public int? AcceptanceId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Приемка готовой продукции. Дата")]
        public DateTime? AcceptanceDate { get; set; }

        [Display(Name = "Приемка готовой продукции. Расположение изображения")]
        public string AcceptanceImagePath { get; set; }


        [Display(Name = "Готовая продукция. Размерные данные")]
        public List<OrderProductAddressDeliverySizeViewModel> OrderProductAddressDeliverySizes { get; set; }
    }
}