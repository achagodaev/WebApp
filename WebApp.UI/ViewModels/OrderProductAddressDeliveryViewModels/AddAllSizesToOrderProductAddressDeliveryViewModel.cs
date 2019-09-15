using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.ViewModels.OrderProductAddressProductionDateSizeViewModels;

namespace WebApp.UI.ViewModels.OrderProductAddressDeliveryViewModels
{
    [DisplayName("Добавление размерных данных")]
    public class AddAllSizesToOrderProductAddressDeliveryViewModel
    {
        public AddAllSizesToOrderProductAddressDeliveryViewModel()
        {
            OrderProductAddressProductionDateSizes = new List<OrderProductAddressProductionDateSizeViewModel>();
        }

        [Display(Name = "Поставка готовой продукции грузополучателя заказа")]
        public OrderProductAddressDeliveryViewModel OrderProductAddressDelivery { get; set; }

        [Display(Name = "Размерные данные")]
        public List<OrderProductAddressProductionDateSizeViewModel> OrderProductAddressProductionDateSizes { get; set; }
    }
}