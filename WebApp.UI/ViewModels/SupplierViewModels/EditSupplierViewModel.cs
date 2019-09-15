using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.UI.ViewModels.SupplierViewModels
{
    public class EditSupplierViewModel
    {
        [Display(Name = "Идентификатор")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите наименование")]
        [Display(Name = "Наименование")]
        [StringLength(50, ErrorMessage = "Значение должно содержать не более 50 символов")]
        public string Name { get; set; }
    }
}