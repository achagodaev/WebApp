using WebApp.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.UI.ViewModels.OrderViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {

        }

        public OrderViewModel(Order order)
        {
            Id = order.Id;
            Name = order.Name;
        }

        [Display(Name = "Идентификатор")]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }
}