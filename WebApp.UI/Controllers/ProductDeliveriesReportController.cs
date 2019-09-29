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
    public class ProductDeliveriesReportController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _ProductDeliveries(int orderId = 0, int productId = 0, int addressId = 0)
        {
            ProductDeliveriesReportViewModel model = new ProductDeliveriesReportViewModel();

            model.SelectedOrderId = orderId;
            model.SelectedProductId = productId;
            model.SelectedAddressId = addressId;

            IQueryable<OrderProductAddressDeliverySize> orderProductAddressDeliverySizes = context.OrderProductAddressDeliverySizes
                .Include(opads => opads.Size)
                .Include(opads => opads.OrderProductAddressDelivery.OrderProductAddress.OrderProduct.Order)
                .Include(opads => opads.OrderProductAddressDelivery.OrderProductAddress.OrderProduct.Product)
                .Include(opads => opads.OrderProductAddressDelivery.OrderProductAddress.Address)
                .Include(opads => opads.OrderProductAddressDelivery.OrderProductAddress.OrderProduct.OrderProductAddresses)
                .Include(opads => opads.OrderProductAddressDelivery.OrderProductAddress.OrderProduct.OrderProductAddresses.Select(opa => opa.OrderProductAddressSizes))
                .Include(opads => opads.OrderProductAddressDelivery.OrderProductAddress.OrderProduct.OrderProductAddresses.Select(opa => opa.OrderProductAddressSizes.Select(opas => opas.Size)));

            if (orderId != 0)
            {
                orderProductAddressDeliverySizes = orderProductAddressDeliverySizes.Where(opads => opads.OrderId == orderId);
            }

            if (productId != 0)
            {
                orderProductAddressDeliverySizes = orderProductAddressDeliverySizes.Where(opads => opads.ProductId == productId);
            }

            if (addressId != 0)
            {
                orderProductAddressDeliverySizes = orderProductAddressDeliverySizes.Where(opads => opads.AddressId == addressId);
            }

            if (orderProductAddressDeliverySizes.Any())
            {
                foreach (var orderProductAddressDeliverySize in orderProductAddressDeliverySizes)
                {
                    model.OrderProductAddressDeliverySizes.Add(orderProductAddressDeliverySize);
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

            model.Addresses.Add(0, "Все");

            if (context.Addresses.Any())
            {
                foreach (var address in context.Addresses)
                {
                    model.Addresses.Add(address.Id, address.Name);
                }
            }

            if (context.OrderProductAddressDeliveries.Any())
            {
                foreach (var orderProductAddressDelivery in context.OrderProductAddressDeliveries)
                {
                    model.Deliveries.Add(int.Parse($"{orderProductAddressDelivery.OrderId}{orderProductAddressDelivery.ProductId}{orderProductAddressDelivery.AddressId}{orderProductAddressDelivery.DeliveryId}"), $"Накладная {orderProductAddressDelivery.DeliveryId} от {orderProductAddressDelivery.DeliveryDate.ToShortDateString()}");
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