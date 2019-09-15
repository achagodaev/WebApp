using WebApp.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.UI.ViewModels.AddressViewModels
{
    public class AddressDetailsViewModel : AddressViewModel
    {
        public AddressDetailsViewModel(Address address)
            : base(address)
        {

        }
    }
}