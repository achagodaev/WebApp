using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.UI.ViewModels.MaterialViewModels;
using WebApp.UI.ViewModels.OrderProductViewModels;

namespace WebApp.UI.ViewModels.OrderProductMaterialViewModels
{
    public class EditOrderProductMaterialViewModel
    {
        public EditOrderProductMaterialViewModel()
        {
            AvailableSuppliers = new List<SelectListItem>();
        }

        [Display(Name = "Продукция заказа")]
        public OrderProductViewModel OrderProduct { get; set; }

        [Display(Name = "Материал")]
        public MaterialViewModel Material { get; set; }

        [Required(ErrorMessage = "Выберите поставщика")]
        [Display(Name = "Поставщик")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Введите количество")]
        [Display(Name = "Количество")]
        public double Quantity { get; set; }

        [Required(ErrorMessage = "Введите дату")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата поставки")]
        public DateTime DeliveryDate { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Введите норму расхода")]
        [Display(Name = "Норма расхода")]
        public double Rate { get; set; }

        public List<SelectListItem> AvailableSuppliers { get; set; }
    }
}