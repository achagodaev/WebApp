using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.AddressViewModels;
using WebApp.UI.ViewModels.OrderProductAddressDeliveryViewModels;
using WebApp.UI.ViewModels.OrderProductAddressProductionDateViewModels;
using WebApp.UI.ViewModels.OrderProductAddressSizeViewModels;
using WebApp.UI.ViewModels.OrderProductViewModels;

namespace WebApp.UI.ViewModels.OrderProductAddressViewModels
{
    [DisplayName("Грузополучатель продукции заказа. Подробная информация")]
    public class OrderProductAddressViewModel
    {
        public OrderProductAddressViewModel()
        {

        }

        public OrderProductAddressViewModel(OrderProductAddress orderProductAddress)
        {
            OrderProduct = new OrderProductViewModel(orderProductAddress.OrderProduct);
            Address = new AddressViewModel(orderProductAddress.Address);

            OrderProductAddressSizes = new List<OrderProductAddressSizeViewModel>();
            OrderProductAddressProductionDates = new List<OrderProductAddressProductionDateViewModel>();

            DeliveryDate = orderProductAddress.DeliveryDate;
            OrderProductAddressDeliveries = new List<OrderProductAddressDeliveryViewModel>();
        }

        [Display(Name = "Продукция заказа")]
        public OrderProductViewModel OrderProduct { get; set; }

        [Display(Name = "Грузополучатель")]
        public AddressViewModel Address { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Готовая продукция. Дата поставки")]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Продукция. Размерные данные")]
        public List<OrderProductAddressSizeViewModel> OrderProductAddressSizes { get; set; }

        [Display(Name = "Продукция. Производство")]
        public List<OrderProductAddressProductionDateViewModel> OrderProductAddressProductionDates { get; set; }

        [Display(Name = "Готовая продукция. Поставки")]
        public List<OrderProductAddressDeliveryViewModel> OrderProductAddressDeliveries { get; set; }
    }
}