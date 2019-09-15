using WebApp.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.UI.ViewModels.SupplierViewModels
{
    public class SupplierDetailsViewModel : SupplierViewModel
    {
        public SupplierDetailsViewModel(Supplier supplier)
            : base(supplier)
        {

        }
    }
}