using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.OrderProductAddressDeliverySizeViewModels;
using WebApp.UI.ViewModels.OrderProductAddressDeliveryViewModels;
using WebApp.UI.ViewModels.SizeViewModels;

namespace WebApp.UI.Controllers
{
    public class OrderProductAddressDeliverySizesController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Details(int? orderId, int? productId, int? addressId, int? deliveryId, int? sizeId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (addressId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (deliveryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (sizeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProductAddressDeliverySize orderProductAddressDeliverySize = await context.OrderProductAddressDeliverySizes
                .Include(opads => opads.OrderProductAddressDelivery.OrderProductAddress.OrderProduct.Order)
                .Include(opads => opads.OrderProductAddressDelivery.OrderProductAddress.OrderProduct.Product)
                .Include(opads => opads.OrderProductAddressDelivery.OrderProductAddress.Address)
                .Include(opads => opads.Size)
                .FirstOrDefaultAsync(opads => opads.OrderId == orderId
                && opads.ProductId == productId
                && opads.AddressId == addressId
                && opads.DeliveryId == deliveryId
                && opads.SizeId == sizeId);

            if (orderProductAddressDeliverySize == null)
            {
                return HttpNotFound();
            }

            OrderProductAddressDeliverySizeViewModel model = new OrderProductAddressDeliverySizeViewModel(orderProductAddressDeliverySize);

            return View(model);
        }

        public async Task<ActionResult> Edit(int? orderId, int? productId, int? addressId, int? deliveryId, int? sizeId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (addressId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (deliveryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (sizeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProductAddressDeliverySize orderProductAddressDeliverySize = await context.OrderProductAddressDeliverySizes
                .Include(opads => opads.OrderProductAddressDelivery.OrderProductAddress.OrderProduct.Order)
                .Include(opads => opads.OrderProductAddressDelivery.OrderProductAddress.OrderProduct.Product)
                .Include(opads => opads.OrderProductAddressDelivery.OrderProductAddress.Address)
                .Include(opads => opads.Size)
                .FirstOrDefaultAsync(opads => opads.OrderId == orderId
                && opads.ProductId == productId
                && opads.AddressId == addressId
                && opads.DeliveryId == deliveryId
                && opads.SizeId == sizeId);

            if (orderProductAddressDeliverySize == null)
            {
                return HttpNotFound();
            }

            EditOrderProductAddressDeliverySizeViewModel model = new EditOrderProductAddressDeliverySizeViewModel();

            model.OrderProductAddressDelivery = new OrderProductAddressDeliveryViewModel(orderProductAddressDeliverySize.OrderProductAddressDelivery);
            model.Size = new SizeViewModel(orderProductAddressDeliverySize.Size);
            model.DeliveryQuantity = orderProductAddressDeliverySize.DeliveryQuantity;
            model.AcceptanceQuantity = orderProductAddressDeliverySize.AcceptanceQuantity;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditOrderProductAddressDeliverySizeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            OrderProductAddressDeliverySize orderProductAddressDeliverySize = await context.OrderProductAddressDeliverySizes
                .FindAsync(model.OrderProductAddressDelivery.OrderProductAddress.OrderProduct.Order.Id,
                model.OrderProductAddressDelivery.OrderProductAddress.OrderProduct.Product.Id,
                model.OrderProductAddressDelivery.OrderProductAddress.Address.Id,
                model.OrderProductAddressDelivery.DeliveryId,
                model.Size.Id);

            if (orderProductAddressDeliverySize == null)
            {
                return HttpNotFound();
            }

            orderProductAddressDeliverySize.DeliveryQuantity = model.DeliveryQuantity;
            orderProductAddressDeliverySize.AcceptanceQuantity = model.AcceptanceQuantity;

            context.Entry(orderProductAddressDeliverySize).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;

                while (ex != null)
                {
                    errorMessage = ex.Message;
                    ex = ex.InnerException;
                }

                ModelState.AddModelError("", errorMessage);

                return View(model);
            }

            return RedirectToAction(nameof(Details), 
                new
                {
                    orderId = orderProductAddressDeliverySize.OrderId,
                    productId = orderProductAddressDeliverySize.ProductId,
                    addressId = orderProductAddressDeliverySize.AddressId,
                    deliveryId = orderProductAddressDeliverySize.DeliveryId,
                    sizeId = orderProductAddressDeliverySize.SizeId
                });
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