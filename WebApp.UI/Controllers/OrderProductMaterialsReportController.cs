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
    public class OrderProductMaterialsReportController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _OrderProductMaterials(int orderId = 0, string sortColumn = "name")
        {
            OrderProductMaterialsReportViewModel model = new OrderProductMaterialsReportViewModel();

            model.SelectedOrderId = orderId;
            model.SelectedSortColumn = sortColumn;

            IQueryable<OrderProduct> orderProducts = context.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product)
                .Include(op => op.OrderProductAddresses)
                .Include(op => op.OrderProductAddresses.Select(opa => opa.OrderProductAddressSizes))
                .Include(op => op.OrderProductMaterials)
                .Include(op => op.OrderProductMaterials.Select(opm => opm.Supplier))
                .Include(op => op.OrderProductMaterials.Select(opm => opm.OrderProductDeliveryMaterials))
                .Include(op => op.OrderProductDeliveries);

            if (orderId != 0)
            {
                orderProducts = orderProducts.Where(oaps => oaps.OrderId == orderId);

                if (orderProducts.Any())
                {
                    foreach (var orderProduct in orderProducts)
                    {
                        model.OrderProducts.Add(orderProduct);
                    }
                }
            }

            if (context.Orders.Any())
            {
                foreach (var order in context.Orders)
                {
                    model.Orders.Add(order.Id, order.Name);
                }
            }

            if (context.Products.Any())
            {
                foreach (var product in context.Products)
                {
                    model.Products.Add(product.Id, product.Name);
                }
            }

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