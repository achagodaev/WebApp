using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.SizeViewModels;

namespace WebApp.UI.ViewModels.OrderProductAddressViewModels
{
    [DisplayName("Удаление размерных данных продукции")]
    public class DeleteSizeFromOrderProductAddressViewModel
    {
        public DeleteSizeFromOrderProductAddressViewModel()
        {

        }

        public DeleteSizeFromOrderProductAddressViewModel(OrderProductAddress orderProductAddress, Size size)
        {
            OrderProductAddress = new OrderProductAddressViewModel(orderProductAddress);
            Size = new SizeViewModel(size);
        }

        [Display(Name = "Грузополучатель продукции заказа")]
        public OrderProductAddressViewModel OrderProductAddress { get; set; }

        [Display(Name = "Размер")]
        public SizeViewModel Size { get; set; }
    }
}