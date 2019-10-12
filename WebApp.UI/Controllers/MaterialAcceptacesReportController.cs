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
    public class MaterialAcceptancesReportController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _MaterialAcceptances(int orderId = 0, int productId = 0, int supplierId = 0, int materialId = 0)
        {
            MaterialAcceptancesReportViewModel model = new MaterialAcceptancesReportViewModel();

            model.SelectedOrderId = orderId;
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

            if (orderId != 0)
            {
                orderProductDeliveryMaterials = orderProductDeliveryMaterials.Where(opdm => opdm.OrderId == orderId);
            }

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

            if (context.OrderProductDeliveries.Any())
            {
                foreach (var orderProductDelivery in context.OrderProductDeliveries)
                {
                    model.Deliveries.Add(int.Parse($"{orderProductDelivery.OrderId}{orderProductDelivery.ProductId}{orderProductDelivery.DeliveryId}"), $"Накладная {orderProductDelivery.DeliveryId} от {orderProductDelivery.DeliveryDate.ToShortDateString()}");
                }
            }

            if (context.OrderProductDeliveries.Any())
            {
                foreach (var orderProductDelivery in context.OrderProductDeliveries)
                {
                    if (orderProductDelivery.AcceptanceId.HasValue && orderProductDelivery.AcceptanceDate.HasValue)
                    {
                        if (!model.Acceptances.ContainsKey(int.Parse($"{orderProductDelivery.OrderId}{orderProductDelivery.ProductId}{orderProductDelivery.AcceptanceId}")))
                        {
                            model.Acceptances.Add(int.Parse($"{orderProductDelivery.OrderId}{orderProductDelivery.ProductId}{orderProductDelivery.AcceptanceId}"), $"Акт приемки {orderProductDelivery.AcceptanceId} от {orderProductDelivery.AcceptanceDate.Value.ToShortDateString()}");
                        }
                    }
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