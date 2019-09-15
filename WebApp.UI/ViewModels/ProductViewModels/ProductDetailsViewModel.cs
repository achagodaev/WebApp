using WebApp.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.UI.ViewModels.ProductViewModels
{
    public class ProductDetailsViewModel : ProductViewModel
    {
        public ProductDetailsViewModel(Product product)
            : base(product)
        {
            Description = product.Description;
        }

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}