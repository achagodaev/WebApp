using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.ReportViewModels;

namespace WebApp.UI.Controllers
{
    public class OrderProductAddressesReportController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _OrderProductAddresses(int orderId = 0, string groupBy = "product", string sortColumn = "name")
        {
            OrderProductAddressesReportViewModel model = new OrderProductAddressesReportViewModel();

            model.SelectedOrderId = orderId;
            model.SelectedGroupBy = groupBy;

            IQueryable<OrderProduct> orderProducts = context.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product)
                .Include(op => op.OrderProductAddresses.Select(opa => opa.Address))
                .Include(op => op.OrderProductAddresses.Select(opa => opa.OrderProductAddressSizes))
                .Include(op => op.OrderProductAddresses.Select(opa => opa.OrderProductAddressSizes.Select(opas => opas.Size))); 

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