using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.UI.ViewModels.SizeViewModels
{
    public class CreateSizeViewModel
    {
        [Required(ErrorMessage = "Введите наименование")]
        [Display(Name = "Наименование")]
        [StringLength(10, ErrorMessage = "Значение должно содержать не более 10 символов")]
        public string Name { get; set; }
    }
}