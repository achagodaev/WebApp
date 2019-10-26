using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;

namespace WebApp.UI.ViewModels.ReportViewModels
{
    public class AddressesReportViewModel
    {
        public AddressesReportViewModel()
        {
            Orders = new Dictionary<int, string>();
            Products = new Dictionary<int, string>();
            Addresses = new Dictionary<int, string>();

            OrderProductAddresses = new List<OrderProductAddress>();
        }

        [Display(Name = "Грузополучатель")]
        public int SelectedAddressId { get; set; }

        public Dictionary<int, string> Orders { get; set; }

        public Dictionary<int, string> Products { get; set; }

        public Dictionary<int, string> Addresses { get; set; }

        public List<OrderProductAddress> OrderProductAddresses { get; set; }
    }
}