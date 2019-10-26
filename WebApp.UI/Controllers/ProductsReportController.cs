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
    public class ProductsReportController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Products(int productId = 0)
        {
            ProductsReportViewModel model = new ProductsReportViewModel();

            model.SelectedProductId = productId;

            IQueryable<OrderProduct> orderProducts = context.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product);

            IQueryable<OrderProductAddress> orderProductAddresses = context.OrderProductAddresses
                .Include(opa => opa.Address)
                .Include(opa => opa.OrderProduct.Order)
                .Include(opa => opa.OrderProduct.Product)
                .Include(opa => opa.OrderProductAddressSizes);

            if (productId != 0)
            {
                orderProducts = orderProducts.Where(op => op.ProductId == productId);
                orderProductAddresses = orderProductAddresses.Where(opa => opa.ProductId == productId);

                if (orderProducts.Any())
                {
                    foreach (var orderProduct in orderProducts)
                    {
                        model.OrderProducts.Add(orderProduct);
                    }
                }

                if (orderProductAddresses.Any())
                {
                    foreach (var orderProductAddress in orderProductAddresses)
                    {
                        model.OrderProductAddresses.Add(orderProductAddress);
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