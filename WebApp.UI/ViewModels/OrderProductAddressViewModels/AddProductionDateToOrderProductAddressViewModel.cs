using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.UI.ViewModels.OrderProductAddressViewModels
{
    [DisplayName("Добавление даты производства продукции")]
    public class AddProductionDateToOrderProductAddressViewModel
    {
        [Display(Name = "Грузополучатель продукции заказа")]
        public OrderProductAddressViewModel OrderProductAddress { get; set; }

        [Required(ErrorMessage = "Введите дату")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата производства")]
        public DateTime ProductionDate { get; set; }
    }
}