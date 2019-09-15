using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;

namespace WebApp.UI.ViewModels.OrderProductAddressViewModels
{
    [DisplayName("Удаление даты производства продукции")]
    public class DeleteProductionDateFromOrderProductAddressViewModel
    {
        public DeleteProductionDateFromOrderProductAddressViewModel()
        {

        }

        public DeleteProductionDateFromOrderProductAddressViewModel(OrderProductAddress orderProductAddress, DateTime productionDate)
        {
            OrderProductAddress = new OrderProductAddressViewModel(orderProductAddress);
            ProductionDate = productionDate;
        }

        [Display(Name = "Грузополучатель продукции заказа")]
        public OrderProductAddressViewModel OrderProductAddress { get; set; }

        [Display(Name = "Дата производства")]
        public DateTime ProductionDate { get; set; }
    }
}