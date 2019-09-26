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
    public class MaterialDeliveriesReportController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _MaterialDeliveries(int productId = 0, int supplierId = 0, int materialId = 0)
        {
            MaterialDeliveriesReportViewModel model = new MaterialDeliveriesReportViewModel();

            model.SelectedProductId = productId;
            model.SelectedSupplierId = supplierId;
            model.SelectedMaterialId = materialId;

            IQueryable<OrderProductDeliveryMaterial> orderProductDeliveryMaterials = context.OrderProductDeliveryMaterials
                .Include(opdm => opdm.Material)
                .Include(opdm => opdm.OrderProductDelivery.OrderProduct.Order)
                .Include(opdm => opdm.OrderProductDelivery.OrderProduct.Product)
                .Include(opdm => opdm.OrderProductDelivery.OrderProduct.Order)
                .Include(opdm => opdm.OrderProductDelivery.OrderProduct.Product)
                .Include(opdm => opdm.OrderProductDelivery.OrderProduct.OrderProductAddresses)
                .Include(opdm => opdm.OrderProductDelivery.OrderProduct.OrderProductAddresses.Select(opa => opa.OrderProductAddressSizes))
                .Include(opdm => opdm.OrderProductDelivery.OrderProduct.OrderProductMaterials)
                .Include(opdm => opdm.OrderProductDelivery.OrderProduct.OrderProductMaterials.Select(opm => opm.OrderProductDeliveryMaterials))
                .Include(opdm => opdm.OrderProductDelivery.OrderProduct.OrderProductDeliveries)
                .Include(opdm => opdm.OrderProductMaterial.Material)
                .Include(opdm => opdm.OrderProductMaterial.Supplier);

            if (productId != 0)
            {
                orderProductDeliveryMaterials = orderProductDeliveryMaterials.Where(opdm => opdm.ProductId == productId);
            }

            if (supplierId != 0)
            {
                orderProductDeliveryMaterials = orderProductDeliveryMaterials.Where(opdm => opdm.OrderProductMaterial.SupplierId == supplierId);
            }

            if (materialId != 0)
            {
                orderProductDeliveryMaterials = orderProductDeliveryMaterials.Where(opdm => opdm.MaterialId == materialId);
            }

            if (orderProductDeliveryMaterials.Any())
            {
                foreach (var orderProductDeliveryMaterial in orderProductDeliveryMaterials)
                {
                    model.OrderProductDeliveryMaterials.Add(orderProductDeliveryMaterial);
                }
            }

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