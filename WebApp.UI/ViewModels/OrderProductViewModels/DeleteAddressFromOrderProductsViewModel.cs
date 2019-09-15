using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.AddressViewModels;

namespace WebApp.UI.ViewModels.OrderProductViewModels
{
    public class DeleteAddressFromOrderProductViewModel
    {
        public DeleteAddressFromOrderProductViewModel()
        {

        }

        public DeleteAddressFromOrderProductViewModel(OrderProduct orderProduct, Address address)
        {
            OrderProduct = new OrderProductViewModel(orderProduct);
            Address = new AddressViewModel(address);
        }

        [Display(Name = "Продукция заказа")]
        public OrderProductViewModel OrderProduct { get; set; }

        [Display(Name = "Грузополучатель")]
        public AddressViewModel Address { get; set; }
    }
}