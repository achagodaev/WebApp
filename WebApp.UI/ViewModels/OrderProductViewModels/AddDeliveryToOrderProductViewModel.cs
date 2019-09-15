using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.UI.ViewModels.OrderProductViewModels
{
    [DisplayName("Добавление поставки")]
    public class AddDeliveryToOrderProductViewModel
    {
        [Display(Name = "Продукция заказа")]
        public OrderProductViewModel OrderProduct { get; set; }

        [Required(ErrorMessage = "Введите идентификатор")]
        [Display(Name = "Идентификатор поставки")]
        public int DeliveryId { get; set; }

        [Required(ErrorMessage = "Введите дату")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата поставки")]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Идентификатор приемки")]
        public int? AcceptanceId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата приемки")]
        public DateTime? AcceptanceDate { get; set; }
    }
}