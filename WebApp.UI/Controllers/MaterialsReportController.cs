using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq.Expressions;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.ReportViewModels;
using System.Diagnostics;
using WebApp.UI.Repositories;

namespace WebApp.UI.Controllers
{
    public class MaterialsReportController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Materials(int orderId = 0, int productId = 0, int supplierId = 0, int materialId = 0)
        {
            MaterialsReportViewModel model = new MaterialsReportViewModel();

            model.SelectedOrderId = orderId;
            model.SelectedProductId = productId;
            model.SelectedSupplierId = supplierId;
            model.SelectedMaterialId = materialId;

            IQueryable<OrderProductMaterial> orderProductMaterials = context.OrderProductMaterials
                .Include(opm => opm.Material)
                .Include(opm => opm.Supplier)
                .Include(opm => opm.OrderProduct.Order)
                .Include(opm => opm.OrderProduct.Product)
                .Include(opm => opm.OrderProduct.OrderProductAddresses)
                .Include(opm => opm.OrderProduct.OrderProductAddresses.Select(opa => opa.OrderProductAddressSizes));

            if (orderId != 0)
            {
                orderProductMaterials = orderProductMaterials.Where(opm => opm.OrderId == orderId);
            }

            if (productId != 0)
            {
                orderProductMaterials = orderProductMaterials.Where(opm => opm.ProductId == productId);
            }

            if (supplierId != 0)
            {
                orderProductMaterials = orderProductMaterials.Where(opm => opm.SupplierId == supplierId);
            }

            if (materialId != 0)
            {
                orderProductMaterials = orderProductMaterials.Where(opm => opm.MaterialId == materialId);
            }

            if (orderProductMaterials.Any())
            {
                foreach (var orderProductMaterial in orderProductMaterials)
                {
                    model.OrderProductMaterials.Add(orderProductMaterial);
                }
            }

            model.Orders.Add(0, "Все");

            if (context.Orders.Any())
            {
                foreach (var order in context.Orders)
                {
                    model.Orders.Add(order.Id, order.Name);
                }
            }

            model.Products.Add(0, "Все");

            if (context.Products.Any())
            {
                foreach (var product in context.Products)
                {
                    model.Products.Add(product.Id, product.Name);
                }
            }

            model.Suppliers.Add(0, "Все");

            if (context.Suppliers.Any())
            {
                foreach (var supplier in context.Suppliers)
                {
                    model.Suppliers.Add(supplier.Id, supplier.Name);
                }
            }

            model.Materials.Add(0, "Все");

            if (context.Materials.Any())
            {
                foreach (var material in context.Materials)
                {
                    model.Materials.Add(material.Id, material.Name);
                }
            }

            return PartialView(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}