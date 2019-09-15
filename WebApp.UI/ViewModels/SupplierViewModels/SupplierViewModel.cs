using WebApp.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.UI.ViewModels.SupplierViewModels
{
    public class SupplierViewModel
    {
        public SupplierViewModel()
        {

        }

        public SupplierViewModel(Supplier supplier)
        {
            Id = supplier.Id;
            Name = supplier.Name;
        }

        [Display(Name = "Идентификатор")]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }
}