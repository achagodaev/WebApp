using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.UI.ViewModels.OrderProductDeliveryMaterialViewModels;
using WebApp.UI.ViewModels.OrderProductViewModels;

namespace WebApp.UI.ViewModels.OrderProductDeliveryViewModels
{
    public class EditOrderProductDeliveryViewModel
    {
        public EditOrderProductDeliveryViewModel()
        {
            OrderProductDeliveryMaterials = new List<OrderProductDeliveryMaterialViewModel>();
        }

        [Display(Name = "Продукция заказа")]
        public OrderProductViewModel OrderProduct { get; set; }

        [Display(Name = "Поставка. Идентификатор")]
        public int DeliveryId { get; set; }

        [Required(ErrorMessage = "Введите дату")]
        [DataType(DataType.Date)]
        [Display(Name = "Поставка. Дата")]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Поставка. Расположение изображения")]
        public string DeliveryImagePath { get; set; }

        [Display(Name = "Приемка. Идентификатор")]
        public int? AcceptanceId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Приемка. Дата")]
        public DateTime? AcceptanceDate { get; set; }

        [Display(Name = "Приемка. Расположение изображения")]
        public string AcceptanceImagePath { get; set; }

        [Display(Name = "Материалы")]
        public List<OrderProductDeliveryMaterialViewModel> OrderProductDeliveryMaterials { get; set; }
    }
}