using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.MaterialViewModels;
using WebApp.UI.ViewModels.OrderProductViewModels;
using WebApp.UI.ViewModels.SupplierViewModels;

namespace WebApp.UI.ViewModels.OrderProductMaterialViewModels
{
    public class OrderProductMaterialViewModel
    {
        public OrderProductMaterialViewModel()
        {

        }

        public OrderProductMaterialViewModel(OrderProductMaterial orderProductMaterial)
        {
            OrderProduct = new OrderProductViewModel(orderProductMaterial.OrderProduct);
            Material = new MaterialViewModel(orderProductMaterial.Material);
            Supplier = new SupplierViewModel(orderProductMaterial.Supplier);
            Quantity = orderProductMaterial.Quantity;
            DeliveryDate = orderProductMaterial.DeliveryDate;
            Price = orderProductMaterial.Price;
            Rate = orderProductMaterial.Rate;
        }

        [Display(Name = "Продукция заказа")]
        public OrderProductViewModel OrderProduct { get; set; }

        [Display(Name = "Материал")]
        public MaterialViewModel Material { get; set; }

        [Display(Name = "Поставщик")]
        public SupplierViewModel Supplier { get; set; }

        [Display(Name = "Количество")]
        public double Quantity { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата поставки")]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Норма расхода")]
        public double Rate { get; set; }
    }
}