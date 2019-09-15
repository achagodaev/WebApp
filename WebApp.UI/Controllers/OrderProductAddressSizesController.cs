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
using WebApp.UI.ViewModels.OrderProductAddressSizeViewModels;
using WebApp.UI.ViewModels.OrderProductAddressViewModels;
using WebApp.UI.ViewModels.SizeViewModels;

namespace WebApp.UI.Controllers
{
    public class OrderProductAddressSizesController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Details(int? orderId, int? productId, int? addressId, int? sizeId)
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

            if (sizeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProductAddressSize orderProductAddressSize = await context.OrderProductAddressSizes
                .Include(opas => opas.OrderProductAddress.OrderProduct.Order)
                .Include(opas => opas.OrderProductAddress.OrderProduct.Product)
                .Include(opas => opas.OrderProductAddress.Address)
                .Include(opas => opas.Size)
                .FirstOrDefaultAsync(opas => opas.OrderId == orderId
                && opas.ProductId == productId
                && opas.AddressId == addressId 
                && opas.SizeId == sizeId);

            if (orderProductAddressSize == null)
            {
                return HttpNotFound();
            }

            OrderProductAddressSizeViewModel model = new OrderProductAddressSizeViewModel(orderProductAddressSize);

            return View(model);
        }

        public async Task<ActionResult> Edit(int? orderId, int? productId, int? addressId, int? sizeId)
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

            if (sizeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProductAddressSize orderProductAddressSize = await context.OrderProductAddressSizes
                .Include(opas => opas.OrderProductAddress.OrderProduct.Order)
                .Include(opas => opas.OrderProductAddress.OrderProduct.Product)
                .Include(opas => opas.OrderProductAddress.Address)
                .Include(opas => opas.Size)
                .FirstOrDefaultAsync(opas => opas.OrderId == orderId
                && opas.ProductId == productId
                && opas.AddressId == addressId
                && opas.SizeId == sizeId);

            if (orderProductAddressSize == null)
            {
                return HttpNotFound();
            }

            EditOrderProductAddressSizeViewModel model = new EditOrderProductAddressSizeViewModel();

            model.OrderProductAddress = new OrderProductAddressViewModel(orderProductAddressSize.OrderProductAddress);
            model.Size = new SizeViewModel(orderProductAddressSize.Size);
            model.Quantity = orderProductAddressSize.Quantity;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditOrderProductAddressSizeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            OrderProductAddressSize orderProductAddressSize = await context.OrderProductAddressSizes
                .FindAsync(model.OrderProductAddress.OrderProduct.Order.Id,
                model.OrderProductAddress.OrderProduct.Product.Id,
                model.OrderProductAddress.Address.Id, 
                model.Size.Id);

            if (orderProductAddressSize == null)
            {
                return HttpNotFound();
            }

            orderProductAddressSize.Quantity = model.Quantity;

            context.Entry(orderProductAddressSize).State = EntityState.Modified;

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
                    orderId = orderProductAddressSize.OrderId,
                    productId = orderProductAddressSize.ProductId,
                    addressId = orderProductAddressSize.AddressId,
                    sizeId = orderProductAddressSize.SizeId
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