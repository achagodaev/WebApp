using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.SizeViewModels;

namespace WebApp.UI.ViewModels.OrderProductAddressProductionDateViewModels
{
    [DisplayName("Удаление размерных данных")]
    public class DeleteSizeFromOrderProductAddressProductionDateViewModel
    {
        public DeleteSizeFromOrderProductAddressProductionDateViewModel()
        {

        }

        public DeleteSizeFromOrderProductAddressProductionDateViewModel(OrderProductAddressProductionDate orderProductAddressProductionDate, Size size)
        {
            OrderProductAddressProductionDate = new OrderProductAddressProductionDateViewModel(orderProductAddressProductionDate);
            Size = new SizeViewModel(size);
        }

        [Display(Name = "Дата производства продукции грузополучателя заказа")]
        public OrderProductAddressProductionDateViewModel OrderProductAddressProductionDate { get; set; }

        [Required(ErrorMessage = "Введите размер")]
        [Display(Name = "Размер")]
        public SizeViewModel Size { get; set; }
    }
}