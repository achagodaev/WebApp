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
using WebApp.UI.ViewModels.OrderProductAddressProductionDateSizeViewModels;
using WebApp.UI.ViewModels.OrderProductAddressViewModels;

namespace WebApp.UI.Controllers
{
    public class OrderProductAddressDeliveriesController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Details(int? orderId, int? productId, int? addressId, int? deliveryId)
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

            OrderProductAddressDelivery orderProductAddressDelivery = await context.OrderProductAddressDeliveries
                .Include(opad => opad.OrderProductAddress.OrderProduct.Order)
                .Include(opad => opad.OrderProductAddress.OrderProduct.Product)
                .Include(opad => opad.OrderProductAddress.Address)
                .Include(opad => opad.OrderProductAddressDeliverySizes)
                .Include(opad => opad.OrderProductAddressDeliverySizes.Select(opads => opads.Size))
                .FirstOrDefaultAsync(opad => opad.OrderId == orderId
                && opad.ProductId == productId
                && opad.AddressId == addressId 
                && opad.DeliveryId == deliveryId);

            if (orderProductAddressDelivery == null)
            {
                return HttpNotFound();
            }

            OrderProductAddressDeliveryViewModel model = new OrderProductAddressDeliveryViewModel(orderProductAddressDelivery);

            if (orderProductAddressDelivery.OrderProductAddressDeliverySizes.Any())
            {
                foreach (var orderProductAddressDeliverySize in orderProductAddressDelivery.OrderProductAddressDeliverySizes)
                {
                    model.OrderProductAddressDeliverySizes.Add(new OrderProductAddressDeliverySizeViewModel(orderProductAddressDeliverySize));
                }
            }

            return View(model);
        }

        public async Task<ActionResult> Edit(int? orderId, int? productId, int? addressId, int? deliveryId)
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

            OrderProductAddressDelivery orderProductAddressDelivery = await context.OrderProductAddressDeliveries
                .Include(opad => opad.OrderProductAddress.OrderProduct.Order)
                .Include(opad => opad.OrderProductAddress.OrderProduct.Product)
                .Include(opad => opad.OrderProductAddress.Address)
                .Include(opad => opad.OrderProductAddressDeliverySizes)
                .Include(opad => opad.OrderProductAddressDeliverySizes.Select(opads => opads.Size))
                .FirstOrDefaultAsync(opad => opad.OrderId == orderId
                && opad.ProductId == productId
                && opad.AddressId == addressId
                && opad.DeliveryId == deliveryId);

            if (orderProductAddressDelivery == null)
            {
                return HttpNotFound();
            }

            EditOrderProductAddressDeliveryViewModel model = new EditOrderProductAddressDeliveryViewModel();

            model.OrderProductAddress = new OrderProductAddressViewModel(orderProductAddressDelivery.OrderProductAddress);
            model.DeliveryId = orderProductAddressDelivery.DeliveryId;
            model.DeliveryDate = orderProductAddressDelivery.DeliveryDate;
            model.DeliveryImagePath = orderProductAddressDelivery.DeliveryImagePath;
            model.AcceptanceId = orderProductAddressDelivery.AcceptanceId;
            model.AcceptanceDate = orderProductAddressDelivery.AcceptanceDate;
            model.AcceptanceImagePath = orderProductAddressDelivery.AcceptanceImagePath;

            if (orderProductAddressDelivery.OrderProductAddressDeliverySizes.Any())
            {
                foreach (var orderProductAddressDeliverySize in orderProductAddressDelivery.OrderProductAddressDeliverySizes)
                {
                    model.OrderProductAddressDeliverySizes.Add(new OrderProductAddressDeliverySizeViewModel(orderProductAddressDeliverySize));
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditOrderProductAddressDeliveryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            OrderProductAddressDelivery orderProductAddressDelivery = await context.OrderProductAddressDeliveries
                .FindAsync(model.OrderProductAddress.OrderProduct.Order.Id,
                model.OrderProductAddress.OrderProduct.Product.Id,
                model.OrderProductAddress.Address.Id, 
                model.DeliveryId);

            if (orderProductAddressDelivery == null)
            {
                return HttpNotFound();
            }

            orderProductAddressDelivery.DeliveryDate = model.DeliveryDate;
            orderProductAddressDelivery.DeliveryImagePath = model.DeliveryImagePath;
            orderProductAddressDelivery.AcceptanceId = model.AcceptanceId;
            orderProductAddressDelivery.AcceptanceDate = model.AcceptanceDate;
            orderProductAddressDelivery.AcceptanceImagePath = model.AcceptanceImagePath;

            context.Entry(orderProductAddressDelivery).State = EntityState.Modified;

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
                    orderId = orderProductAddressDelivery.OrderId,
                    productId = orderProductAddressDelivery.ProductId,
                    addressId = orderProductAddressDelivery.AddressId,
                    deliveryId = orderProductAddressDelivery.DeliveryId
                });
        }

        public async Task<ActionResult> AddSizeToOrderProductAddressDelivery(int? orderId, int? productId, int? addressId, int? deliveryId)
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

            OrderProductAddressDelivery orderProductAddressDelivery = await context.OrderProductAddressDeliveries
                .Include(opad => opad.OrderProductAddress.OrderProduct.Order)
                .Include(opad => opad.OrderProductAddress.OrderProduct.Product)
                .Include(opad => opad.OrderProductAddress.Address)
                .FirstOrDefaultAsync(opad => opad.OrderId == orderId
                && opad.ProductId == productId
                && opad.AddressId == addressId
                && opad.DeliveryId == deliveryId);

            if (orderProductAddressDelivery == null)
            {
                return HttpNotFound();
            }

            AddSizeToOrderProductAddressDeliveryViewModel model = new AddSizeToOrderProductAddressDeliveryViewModel();

            model.OrderProductAddressDelivery = new OrderProductAddressDeliveryViewModel(orderProductAddressDelivery);
            model.AvailableSizes = GetAvailableSizes(orderProductAddressDelivery);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddSizeToOrderProductAddressDelivery(AddSizeToOrderProductAddressDeliveryViewModel model)
        {
            OrderProductAddressDelivery orderProductAddressDelivery = await context.OrderProductAddressDeliveries
                .FindAsync(model.OrderProductAddressDelivery.OrderProductAddress.OrderProduct.Order.Id,
                model.OrderProductAddressDelivery.OrderProductAddress.OrderProduct.Product.Id,
                model.OrderProductAddressDelivery.OrderProductAddress.Address.Id,
                model.OrderProductAddressDelivery.DeliveryId);

            if (orderProductAddressDelivery == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                model.AvailableSizes = GetAvailableSizes(orderProductAddressDelivery, model.SizeId);

                return View(model);
            }

            Size size = await context.Sizes.FindAsync(model.SizeId);

            if (size == null)
            {
                return HttpNotFound();
            }

            context.OrderProductAddressDeliverySizes.Add(new OrderProductAddressDeliverySize
            {
                OrderProductAddressDelivery = orderProductAddressDelivery,
                Size = size,
                DeliveryQuantity = model.DeliveryQuantity,
                AcceptanceQuantity = model.AcceptanceQuantity
            });

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
                    orderId = orderProductAddressDelivery.OrderId,
                    productId = orderProductAddressDelivery.ProductId,
                    addressId = orderProductAddressDelivery.AddressId,
                    deliveryId = orderProductAddressDelivery.DeliveryId
                });
        }

        public async Task<ActionResult> DeleteSizeFromOrderProductAddressDelivery(int? orderId, int? productId, int? addressId, int? deliveryId, int? sizeId)
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

            OrderProductAddressDelivery orderProductAddressDelivery = await context.OrderProductAddressDeliveries
                .Include(opad => opad.OrderProductAddress.OrderProduct.Order)
                .Include(opad => opad.OrderProductAddress.OrderProduct.Product)
                .Include(opad => opad.OrderProductAddress.Address)
                .FirstOrDefaultAsync(opad => opad.OrderId == orderId
                && opad.ProductId == productId
                && opad.AddressId == addressId
                && opad.DeliveryId == deliveryId);

            if (orderProductAddressDelivery == null)
            {
                return HttpNotFound();
            }

            Size size = await context.Sizes.FindAsync(sizeId);

            if (size == null)
            {
                return HttpNotFound();
            }

            DeleteSizeFromOrderProductAddressDeliveryViewModel model = new DeleteSizeFromOrderProductAddressDeliveryViewModel(orderProductAddressDelivery, size);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSizeFromOrderProductAddressDelivery(DeleteSizeFromOrderProductAddressDeliveryViewModel model)
        {
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

            context.OrderProductAddressDeliverySizes.Remove(orderProductAddressDeliverySize);

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
                    deliveryId = orderProductAddressDeliverySize.DeliveryId
                });
        }

        public async Task<ActionResult> AddAllSizesToOrderProductAddressDelivery(int? orderId, int? productId, int? addressId, int? deliveryId)
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

            OrderProductAddressDelivery orderProductAddressDelivery = await context.OrderProductAddressDeliveries
                .Include(opad => opad.OrderProductAddress.OrderProduct.Order)
                .Include(opad => opad.OrderProductAddress.OrderProduct.Product)
                .Include(opad => opad.OrderProductAddress.Address)
                .FirstOrDefaultAsync(opad => opad.OrderId == orderId
                && opad.ProductId == productId
                && opad.AddressId == addressId
                && opad.DeliveryId == deliveryId);

            if (orderProductAddressDelivery == null)
            {
                return HttpNotFound();
            }

            AddAllSizesToOrderProductAddressDeliveryViewModel model = new AddAllSizesToOrderProductAddressDeliveryViewModel();

            model.OrderProductAddressDelivery = new OrderProductAddressDeliveryViewModel(orderProductAddressDelivery);
            model.OrderProductAddressProductionDateSizes = GetOrderProductAddressProductionDateSizeViewModels(orderProductAddressDelivery);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAllSizesToOrderProductAddressDelivery(AddAllSizesToOrderProductAddressDeliveryViewModel model)
        {
            OrderProductAddressDelivery orderProductAddressDelivery = await context.OrderProductAddressDeliveries
                .Include(opad => opad.OrderProductAddress.OrderProduct.Order)
                .Include(opad => opad.OrderProductAddress.OrderProduct.Product)
                .Include(opad => opad.OrderProductAddress.Address)
                .Include(opad => opad.OrderProductAddressDeliverySizes)
                .FirstOrDefaultAsync(opad => opad.OrderId == model.OrderProductAddressDelivery.OrderProductAddress.OrderProduct.Order.Id
                && opad.ProductId == model.OrderProductAddressDelivery.OrderProductAddress.OrderProduct.Product.Id
                && opad.AddressId == model.OrderProductAddressDelivery.OrderProductAddress.Address.Id
                && opad.DeliveryId == model.OrderProductAddressDelivery.DeliveryId);

            if (orderProductAddressDelivery == null)
            {
                return HttpNotFound();
            }

            if (orderProductAddressDelivery.OrderProductAddressDeliverySizes.Any())
            {
                ModelState.AddModelError("", "Поставка готовой продукции грузополучателя заказа уже содержит размерные данные");
            }

            if (!ModelState.IsValid)
            {
                model.OrderProductAddressProductionDateSizes = GetOrderProductAddressProductionDateSizeViewModels(orderProductAddressDelivery);

                return View(model);
            }

            foreach (var orderProductAddressDeliverySizeViewModel in GetOrderProductAddressProductionDateSizeViewModels(orderProductAddressDelivery))
            {
                Size size = context.Sizes.Find(orderProductAddressDeliverySizeViewModel.Size.Id);

                if (size != null)
                {
                    context.OrderProductAddressDeliverySizes.Add(new OrderProductAddressDeliverySize
                    {
                        OrderProductAddressDelivery = orderProductAddressDelivery,
                        Size = size,
                        DeliveryQuantity = orderProductAddressDeliverySizeViewModel.Quantity
                    });
                }
            }

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
                    orderId = orderProductAddressDelivery.OrderId,
                    productId = orderProductAddressDelivery.ProductId,
                    addressId = orderProductAddressDelivery.AddressId,
                    deliveryId = orderProductAddressDelivery.DeliveryId
                });
        }

        public async Task<ActionResult> AddAllSizesToOrderProductAddressAcceptance(int? orderId, int? productId, int? addressId, int? deliveryId)
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

            OrderProductAddressDelivery orderProductAddressDelivery = await context.OrderProductAddressDeliveries
                .Include(opad => opad.OrderProductAddress.OrderProduct.Order)
                .Include(opad => opad.OrderProductAddress.OrderProduct.Product)
                .Include(opad => opad.OrderProductAddress.Address)
                .Include(opad => opad.OrderProductAddressDeliverySizes)
                .Include(opad => opad.OrderProductAddressDeliverySizes.Select(opads => opads.Size))
                .FirstOrDefaultAsync(opad => opad.OrderId == orderId
                && opad.ProductId == productId
                && opad.AddressId == addressId
                && opad.DeliveryId == deliveryId);

            if (orderProductAddressDelivery == null)
            {
                return HttpNotFound();
            }

            OrderProductAddressDeliveryViewModel model = new OrderProductAddressDeliveryViewModel(orderProductAddressDelivery);

            if (orderProductAddressDelivery.OrderProductAddressDeliverySizes.Any())
            {
                foreach (var orderProductAddressDeliverySize in orderProductAddressDelivery.OrderProductAddressDeliverySizes)
                {
                    model.OrderProductAddressDeliverySizes.Add(new OrderProductAddressDeliverySizeViewModel(orderProductAddressDeliverySize));
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAllSizesToOrderProductAddressAcceptance(OrderProductAddressDeliveryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            OrderProductAddressDelivery orderProductAddressDelivery = await context.OrderProductAddressDeliveries
                .Include(opad => opad.OrderProductAddress.OrderProduct.Order)
                .Include(opad => opad.OrderProductAddress.OrderProduct.Product)
                .Include(opad => opad.OrderProductAddress.Address)
                .Include(opad => opad.OrderProductAddressDeliverySizes)
                .FirstOrDefaultAsync(opad => opad.OrderId == model.OrderProductAddress.OrderProduct.Order.Id
                && opad.ProductId == model.OrderProductAddress.OrderProduct.Product.Id
                && opad.AddressId == model.OrderProductAddress.Address.Id
                && opad.DeliveryId == model.DeliveryId);

            if (orderProductAddressDelivery == null)
            {
                return HttpNotFound();
            }

            foreach (var orderProductAddressDeliverySize in orderProductAddressDelivery.OrderProductAddressDeliverySizes)
            {
                orderProductAddressDeliverySize.AcceptanceQuantity = orderProductAddressDeliverySize.DeliveryQuantity;

                context.Entry(orderProductAddressDeliverySize).State = EntityState.Modified;
            }

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
                    orderId = orderProductAddressDelivery.OrderId,
                    productId = orderProductAddressDelivery.ProductId,
                    addressId = orderProductAddressDelivery.AddressId,
                    deliveryId = orderProductAddressDelivery.DeliveryId
                });
        }

        private List<SelectListItem> GetAvailableSizes(OrderProductAddressDelivery orderProductAddressDelivery, int? sizeId = null)
        {
            List<SelectListItem> availableSizes = new List<SelectListItem>();

            if (context.Sizes.Any())
            {
                foreach (var size in context.Sizes
                    .Include(s => s.OrderProductAddressProductionDateSizes)
                    .Include(s => s.OrderProductAddressDeliverySizes))
                {
                    var orderProductAddressProductionDateSizes = size.OrderProductAddressProductionDateSizes
                        .Where(opapds => opapds.OrderId == orderProductAddressDelivery.OrderId
                        && opapds.ProductId == orderProductAddressDelivery.ProductId
                        && opapds.AddressId == orderProductAddressDelivery.AddressId
                        && opapds.SizeId == size.Id)
                        .GroupBy(opapds => opapds.SizeId);

                    OrderProductAddressDeliverySize orderProductAddressDeliverySize = size.OrderProductAddressDeliverySizes
                        .FirstOrDefault(opads => opads.OrderId == orderProductAddressDelivery.OrderId
                        && opads.ProductId == orderProductAddressDelivery.ProductId
                        && opads.AddressId == orderProductAddressDelivery.AddressId
                        && opads.DeliveryId == orderProductAddressDelivery.DeliveryId
                        && opads.SizeId == size.Id);


                    if (orderProductAddressProductionDateSizes.Any() && orderProductAddressDeliverySize == null)
                    {
                        availableSizes.Add(new SelectListItem
                        {
                            Value = size.Id.ToString(),
                            Text = size.Name,
                            Selected = size.Id == sizeId
                        });
                    }
                }
            }

            return availableSizes;
        }

        private List<OrderProductAddressProductionDateSizeViewModel> GetOrderProductAddressProductionDateSizeViewModels(OrderProductAddressDelivery orderProductAddressDelivery)
        {
            List<OrderProductAddressProductionDateSizeViewModel> orderProductAddressProductionDateSizeViewModels = new List<OrderProductAddressProductionDateSizeViewModel>();

            if (context.OrderProductAddressProductionDateSizes.Any())
            {
                foreach (var orderProductAddressProductionDateSize in context.OrderProductAddressProductionDateSizes
                    .Include(opapds => opapds.OrderProductAddressProductionDate.OrderProductAddress.OrderProduct.Order)
                    .Include(opapds => opapds.OrderProductAddressProductionDate.OrderProductAddress.OrderProduct.Product)
                    .Include(opapds => opapds.OrderProductAddressProductionDate.OrderProductAddress.Address)
                    .Include(opapds => opapds.Size)
                    .Where(opapds => opapds.OrderId == orderProductAddressDelivery.OrderId
                    && opapds.ProductId == orderProductAddressDelivery.ProductId
                    && opapds.AddressId == orderProductAddressDelivery.AddressId))
                {
                    orderProductAddressProductionDateSizeViewModels.Add(new OrderProductAddressProductionDateSizeViewModel(orderProductAddressProductionDateSize));
                }
            }

            return orderProductAddressProductionDateSizeViewModels;
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