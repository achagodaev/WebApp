using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.UI.ViewModels.ProductViewModels
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "Введите наименование")]
        [Display(Name = "Наименование")]
        [StringLength(100, ErrorMessage = "Значение должно содержать не более 100 символов")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [StringLength(100, ErrorMessage = "Значение должно содержать не более 100 символов")]
        public string Description { get; set; }
    }
}