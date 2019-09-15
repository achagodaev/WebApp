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
    public class OrderProductAddressDeliverySizesReportController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _OrderProductAddressDeliverySizes(int orderId = 0, string groupBy = "product")
        {
            OrderProductAddressDeliverySizesReportViewModel model = new OrderProductAddressDeliverySizesReportViewModel();

            model.SelectedOrderId = orderId;
            model.SelectedGroupBy = groupBy;

            IQueryable<OrderProduct> orderProducts = context.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product)
                .Include(op => op.OrderProductAddresses)
                .Include(op => op.OrderProductAddresses.Select(opa => opa.Address))
                .Include(op => op.OrderProductAddresses.Select(opa => opa.OrderProductAddressSizes))
                .Include(op => op.OrderProductAddresses.Select(opa => opa.OrderProductAddressSizes.Select(opas => opas.Size)))
                .Include(op => op.OrderProductAddresses.Select(opa => opa.OrderProductAddressDeliveries))
                .Include(op => op.OrderProductAddresses.Select(opa => opa.OrderProductAddressDeliveries.Select(opad => opad.OrderProductAddressDeliverySizes)))
                .Include(op => op.OrderProductAddresses.Select(opa => opa.OrderProductAddressDeliveries.Select(opad => opad.OrderProductAddressDeliverySizes.Select(opads => opads.Size))));

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

            if (context.Addresses.Any())
            {
                foreach (var address in context.Addresses)
                {
                    model.Addresses.Add(address.Id, address.Name);
                }
            }

            if (context.Sizes.Any())
            {
                foreach (var size in context.Sizes)
                {
                    model.Sizes.Add(size.Id, size.Name);
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