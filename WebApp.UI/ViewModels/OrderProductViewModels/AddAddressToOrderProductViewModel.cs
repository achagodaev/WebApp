using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.UI.ViewModels.OrderProductViewModels;

namespace WebApp.UI.ViewModels.OrderProductViewModels
{
    public class AddAddressToOrderProductViewModel
    {
        public AddAddressToOrderProductViewModel()
        {
            AvailableAddresses = new List<SelectListItem>();
        }

        [Display(Name = "Продукция заказа")]
        public OrderProductViewModel OrderProduct { get; set; }

        [Required(ErrorMessage = "Выберите грузополучателя")]
        [Display(Name = "Грузополучатель")]
        public int AddressId { get; set; }

        [Required(ErrorMessage = "Введите дату")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата поставки")]
        public DateTime DeliveryDate { get; set; }

        public List<SelectListItem> AvailableAddresses { get; set; }
    }
}