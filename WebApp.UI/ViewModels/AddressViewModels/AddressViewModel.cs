using WebApp.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.UI.ViewModels.AddressViewModels
{
    public class AddressViewModel
    {
        public AddressViewModel()
        {

        }

        public AddressViewModel(Address address)
        {
            Id = address.Id;
            Name = address.Name;
        }

        [Display(Name = "Идентификатор")]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }
}