using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.UI.ViewModels.MaterialViewModels
{
    public class CreateMaterialViewModel
    {
        [Required(ErrorMessage = "Введите наименование")]
        [Display(Name = "Наименование")]
        [StringLength(100, ErrorMessage = "Значение должно содержать не более 100 символов")]
        public string Name { get; set; }
    }
}