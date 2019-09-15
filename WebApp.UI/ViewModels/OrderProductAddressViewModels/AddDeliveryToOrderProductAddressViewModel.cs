using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.UI.ViewModels.OrderProductAddressViewModels
{
    [DisplayName("Добавление поставки готовой продукции")]
    public class AddDeliveryToOrderProductAddressViewModel
    {
        [Display(Name = "Грузополучатель продукции заказа")]
        public OrderProductAddressViewModel OrderProductAddress { get; set; }

        [Required(ErrorMessage = "Введите идентификатор")]
        [Display(Name = "Поставки готовой продукции. Идентификатор")]
        public int DeliveryId { get; set; }

        [Required(ErrorMessage = "Введите дату")]
        [DataType(DataType.Date)]
        [Display(Name = "Поставки готовой продукции. Дата")]
        public DateTime DeliveryDate { get; set; }
    }
}