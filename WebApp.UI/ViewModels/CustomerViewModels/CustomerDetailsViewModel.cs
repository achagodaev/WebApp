using WebApp.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.UI.ViewModels.CustomerViewModels
{
    public class CustomerDetailsViewModel : CustomerViewModel
    {
        public CustomerDetailsViewModel(Customer customer)
            : base(customer)
        {

        }
    }
}