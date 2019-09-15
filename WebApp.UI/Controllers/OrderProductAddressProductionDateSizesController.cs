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
using WebApp.UI.ViewModels.OrderProductAddressProductionDateSizeViewModels;
using WebApp.UI.ViewModels.OrderProductAddressProductionDateViewModels;
using WebApp.UI.ViewModels.SizeViewModels;

namespace WebApp.UI.Controllers
{
    public class OrderProductAddressProductionDateSizesController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Details(int? orderId, int? productId, int? addressId, DateTime? productionDate, int? sizeId)
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

            if (productionDate == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (sizeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProductAddressProductionDateSize orderProductAddressProductionDateSize = await context.OrderProductAddressProductionDateSizes
                .Include(opapds => opapds.OrderProductAddressProductionDate.OrderProductAddress.OrderProduct.Order)
                .Include(opapds => opapds.OrderProductAddressProductionDate.OrderProductAddress.OrderProduct.Product)
                .Include(opapds => opapds.OrderProductAddressProductionDate.OrderProductAddress.Address)
                .Include(opapds => opapds.Size)
                .FirstOrDefaultAsync(opapds => opapds.OrderId == orderId
                && opapds.ProductId == productId
                && opapds.AddressId == addressId 
                && opapds.ProductionDate == productionDate
                && opapds.SizeId == sizeId);

            if (orderProductAddressProductionDateSize == null)
            {
                return HttpNotFound();
            }

            OrderProductAddressProductionDateSizeViewModel model = new OrderProductAddressProductionDateSizeViewModel(orderProductAddressProductionDateSize);

            return View(model);
        }

        public async Task<ActionResult> Edit(int? orderId, int? productId, int? addressId, DateTime? productionDate, int? sizeId)
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

            if (productionDate == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (sizeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProductAddressProductionDateSize orderProductAddressProductionDateSize = await context.OrderProductAddressProductionDateSizes
                .Include(opapds => opapds.OrderProductAddressProductionDate.OrderProductAddress.OrderProduct.Order)
                .Include(opapds => opapds.OrderProductAddressProductionDate.OrderProductAddress.OrderProduct.Product)
                .Include(opapds => opapds.OrderProductAddressProductionDate.OrderProductAddress.Address)
                .Include(opapds => opapds.Size)
                .FirstOrDefaultAsync(opapds => opapds.OrderId == orderId
                && opapds.ProductId == productId
                && opapds.AddressId == addressId
                && opapds.ProductionDate == productionDate
                && opapds.SizeId == sizeId);

            if (orderProductAddressProductionDateSize == null)
            {
                return HttpNotFound();
            }

            EditOrderProductAddressProductionDateSizeViewModel model = new EditOrderProductAddressProductionDateSizeViewModel();

            model.OrderProductAddressProductionDate = new OrderProductAddressProductionDateViewModel(orderProductAddressProductionDateSize.OrderProductAddressProductionDate);
            model.Size = new SizeViewModel(orderProductAddressProductionDateSize.Size);
            model.Quantity = orderProductAddressProductionDateSize.Quantity;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditOrderProductAddressProductionDateSizeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            OrderProductAddressProductionDateSize orderProductAddressProductionDateSize = await context.OrderProductAddressProductionDateSizes
                .FindAsync(model.OrderProductAddressProductionDate.OrderProductAddress.OrderProduct.Order.Id,
                model.OrderProductAddressProductionDate.OrderProductAddress.OrderProduct.Product.Id,
                model.OrderProductAddressProductionDate.OrderProductAddress.Address.Id,
                model.OrderProductAddressProductionDate.ProductionDate,
                model.Size.Id);

            if (orderProductAddressProductionDateSize == null)
            {
                return HttpNotFound();
            }

            orderProductAddressProductionDateSize.Quantity = model.Quantity;

            context.Entry(orderProductAddressProductionDateSize).State = EntityState.Modified;

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
                    orderId = orderProductAddressProductionDateSize.OrderId,
                    productId = orderProductAddressProductionDateSize.ProductId,
                    addressId = orderProductAddressProductionDateSize.AddressId,
                    productionDate = orderProductAddressProductionDateSize.ProductionDate,
                    sizeId = orderProductAddressProductionDateSize.SizeId
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