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
using WebApp.UI.ViewModels.AddressViewModels;
using WebApp.UI.ViewModels.OrderProductAddressDeliveryViewModels;
using WebApp.UI.ViewModels.OrderProductAddressProductionDateViewModels;
using WebApp.UI.ViewModels.OrderProductAddressSizeViewModels;
using WebApp.UI.ViewModels.OrderProductAddressViewModels;
using WebApp.UI.ViewModels.OrderProductViewModels;

namespace WebApp.UI.Controllers
{
    public class OrderProductAddressesController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Details(int? orderId, int? productId, int? addressId)
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

            OrderProductAddress orderProductAddress = await context.OrderProductAddresses
                .Include(opa => opa.OrderProduct.Order)
                .Include(opa => opa.OrderProduct.Product)
                .Include(opa => opa.Address)
                .Include(opa => opa.OrderProductAddressSizes)
                .Include(opa => opa.OrderProductAddressSizes.Select(opas => opas.Size))
                .Include(opa => opa.OrderProductAddressProductionDates)
                .Include(opa => opa.OrderProductAddressDeliveries)
                .FirstOrDefaultAsync(opa => opa.OrderId == orderId 
                && opa.ProductId == productId
                && opa.AddressId == addressId);

            if (orderProductAddress == null)
            {
                return HttpNotFound();
            }

            OrderProductAddressViewModel model = new OrderProductAddressViewModel(orderProductAddress);

            if (orderProductAddress.OrderProductAddressSizes.Any())
            {
                foreach (var orderProductAddressSize in orderProductAddress.OrderProductAddressSizes)
                {
                    model.OrderProductAddressSizes.Add(new OrderProductAddressSizeViewModel(orderProductAddressSize));
                }
            }

            if (orderProductAddress.OrderProductAddressProductionDates.Any())
            {
                foreach (var orderProductAddressProductionDate in orderProductAddress.OrderProductAddressProductionDates)
                {
                    model.OrderProductAddressProductionDates.Add(new OrderProductAddressProductionDateViewModel(orderProductAddressProductionDate));
                }
            }

            if (orderProductAddress.OrderProductAddressDeliveries.Any())
            {
                foreach (var orderProductAddressDelivery in orderProductAddress.OrderProductAddressDeliveries)
                {
                    model.OrderProductAddressDeliveries.Add(new OrderProductAddressDeliveryViewModel(orderProductAddressDelivery));
                }
            }

            return View(model);
        }

        public async Task<ActionResult> Edit(int? orderId, int? productId, int? addressId)
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

            OrderProductAddress orderProductAddress = await context.OrderProductAddresses
                .Include(opa => opa.OrderProduct.Order)
                .Include(opa => opa.OrderProduct.Product)
                .Include(opa => opa.Address)
                .Include(opa => opa.OrderProductAddressSizes)
                .Include(opa => opa.OrderProductAddressSizes.Select(opas => opas.Size))
                .Include(opa => opa.OrderProductAddressProductionDates)
                .Include(opa => opa.OrderProductAddressDeliveries)
                .FirstOrDefaultAsync(opa => opa.OrderId == orderId
                && opa.ProductId == productId
                && opa.AddressId == addressId);

            if (orderProductAddress == null)
            {
                return HttpNotFound();
            }

            EditOrderProductAddressViewModel model = new EditOrderProductAddressViewModel();

            model.OrderProduct = new OrderProductViewModel(orderProductAddress.OrderProduct);
            model.Address = new AddressViewModel(orderProductAddress.Address);
            model.DeliveryDate = orderProductAddress.DeliveryDate;

            if (orderProductAddress.OrderProductAddressSizes.Any())
            {
                foreach (var orderProductAddressSize in orderProductAddress.OrderProductAddressSizes)
                {
                    model.OrderProductAddressSizes.Add(new OrderProductAddressSizeViewModel(orderProductAddressSize));
                }
            }

            if (orderProductAddress.OrderProductAddressProductionDates.Any())
            {
                foreach (var orderProductAddressProductionDate in orderProductAddress.OrderProductAddressProductionDates)
                {
                    model.OrderProductAddressProductionDates.Add(new OrderProductAddressProductionDateViewModel(orderProductAddressProductionDate));
                }
            }

            if (orderProductAddress.OrderProductAddressDeliveries.Any())
            {
                foreach (var orderProductAddressDelivery in orderProductAddress.OrderProductAddressDeliveries)
                {
                    model.OrderProductAddressDeliveries.Add(new OrderProductAddressDeliveryViewModel(orderProductAddressDelivery));
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditOrderProductAddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            OrderProductAddress orderProductAddress = await context.OrderProductAddresses
                .FindAsync(model.OrderProduct.Order.Id, 
                model.OrderProduct.Product.Id, 
                model.Address.Id);

            if (orderProductAddress == null)
            {
                return HttpNotFound();
            }

            orderProductAddress.DeliveryDate = model.DeliveryDate;

            context.Entry(orderProductAddress).State = EntityState.Modified;

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
                    orderId = orderProductAddress.OrderId,
                    productId = orderProductAddress.ProductId,
                    addressId = orderProductAddress.AddressId
                });
        }

        public async Task<ActionResult> AddSizeToOrderProductAddress(int? orderId, int? productId, int? addressId)
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

            OrderProductAddress orderProductAddress = await context.OrderProductAddresses
                .Include(opa => opa.OrderProduct.Order)
                .Include(opa => opa.OrderProduct.Product)
                .Include(opa => opa.Address)
                .FirstOrDefaultAsync(opa => opa.OrderId == orderId
                && opa.ProductId == productId
                && opa.AddressId == addressId);

            if (orderProductAddress == null)
            {
                return HttpNotFound();
            }

            AddSizeToOrderProductAddressViewModel model = new AddSizeToOrderProductAddressViewModel();

            model.OrderProductAddress = new OrderProductAddressViewModel(orderProductAddress);
            model.AvailableSizes = GetAvailableSizes(orderProductAddress);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddSizeToOrderProductAddress(AddSizeToOrderProductAddressViewModel model)
        {
            OrderProductAddress orderProductAddress = await context.OrderProductAddresses
                .FindAsync(model.OrderProductAddress.OrderProduct.Order.Id,
                model.OrderProductAddress.OrderProduct.Product.Id,
                model.OrderProductAddress.Address.Id);

            if (orderProductAddress == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                model.AvailableSizes = GetAvailableSizes(orderProductAddress, model.SizeId);

                return View(model);
            }

            Size size = await context.Sizes.FindAsync(model.SizeId);

            if (size == null)
            {
                return HttpNotFound();
            }

            context.OrderProductAddressSizes.Add(new OrderProductAddressSize
            {
                OrderProductAddress = orderProductAddress,
                Size = size,
                Quantity = model.Quantity
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
                    orderId = orderProductAddress.OrderId,
                    productId = orderProductAddress.ProductId,
                    addressId = orderProductAddress.AddressId
                });
        }

        public async Task<ActionResult> DeleteSizeFromOrderProductAddress(int? orderId, int? productId, int? addressId, int? sizeId)
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

            OrderProductAddress orderProductAddress = await context.OrderProductAddresses
                 .Include(opa => opa.OrderProduct.Order)
                 .Include(opa => opa.OrderProduct.Product)
                 .Include(opa => opa.Address)
                 .FirstOrDefaultAsync(opa => opa.OrderId == orderId
                 && opa.ProductId == productId
                 && opa.AddressId == addressId);

            if (orderProductAddress == null)
            {
                return HttpNotFound();
            }

            Size size = await context.Sizes.FindAsync(sizeId);

            if (size == null)
            {
                return HttpNotFound();
            }

            DeleteSizeFromOrderProductAddressViewModel model = new DeleteSizeFromOrderProductAddressViewModel(orderProductAddress, size);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSizeFromOrderProductAddress(DeleteSizeFromOrderProductAddressViewModel model)
        {
            OrderProductAddressSize orderProductAddressSize = await context.OrderProductAddressSizes
                .FindAsync(model.OrderProductAddress.OrderProduct.Order.Id,
                model.OrderProductAddress.OrderProduct.Product.Id,
                model.OrderProductAddress.Address.Id,
                model.Size.Id);

            if (orderProductAddressSize == null)
            {
                return HttpNotFound();
            }

            context.OrderProductAddressSizes.Remove(orderProductAddressSize);

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
                    addressId = orderProductAddressSize.AddressId
                });
        }

        public async Task<ActionResult> AddProductionDateToOrderProductAddress(int? orderId, int? productId, int? addressId)
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

            OrderProductAddress orderProductAddress = await context.OrderProductAddresses
                .Include(opa => opa.OrderProduct.Order)
                .Include(opa => opa.OrderProduct.Product)
                .Include(opa => opa.Address)
                .FirstOrDefaultAsync(opa => opa.OrderId == orderId
                && opa.ProductId == productId
                && opa.AddressId == addressId);

            if (orderProductAddress == null)
            {
                return HttpNotFound();
            }

            AddProductionDateToOrderProductAddressViewModel model = new AddProductionDateToOrderProductAddressViewModel();

            model.OrderProductAddress = new OrderProductAddressViewModel(orderProductAddress);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddProductionDateToOrderProductAddress(AddProductionDateToOrderProductAddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            OrderProductAddress orderProductAddress = await context.OrderProductAddresses
                .FindAsync(model.OrderProductAddress.OrderProduct.Order.Id,
                model.OrderProductAddress.OrderProduct.Product.Id,
                model.OrderProductAddress.Address.Id);

            if (orderProductAddress == null)
            {
                return HttpNotFound();
            }

            context.OrderProductAddressProductionDates.Add(new OrderProductAddressProductionDate
            {
                OrderProductAddress = orderProductAddress,
                ProductionDate = model.ProductionDate
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
                    orderId = orderProductAddress.OrderId,
                    productId = orderProductAddress.ProductId,
                    addressId = orderProductAddress.AddressId
                });
        }

        public async Task<ActionResult> DeleteProductionDateFromOrderProductAddress(int? orderId, int? productId, int? addressId, DateTime? productionDate)
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

            OrderProductAddress orderProductAddress = await context.OrderProductAddresses
                 .Include(opa => opa.OrderProduct.Order)
                 .Include(opa => opa.OrderProduct.Product)
                 .Include(opa => opa.Address)
                 .FirstOrDefaultAsync(opa => opa.OrderId == orderId
                 && opa.ProductId == productId
                 && opa.AddressId == addressId);

            if (orderProductAddress == null)
            {
                return HttpNotFound();
            }

            DeleteProductionDateFromOrderProductAddressViewModel model = new DeleteProductionDateFromOrderProductAddressViewModel(orderProductAddress, productionDate.Value);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteProductionDateFromOrderProductAddress(DeleteProductionDateFromOrderProductAddressViewModel model)
        {
            OrderProductAddressProductionDate orderProductAddressProductionDate = await context.OrderProductAddressProductionDates
                .FindAsync(model.OrderProductAddress.OrderProduct.Order.Id,
                model.OrderProductAddress.OrderProduct.Product.Id,
                model.OrderProductAddress.Address.Id,
                model.ProductionDate);

            if (orderProductAddressProductionDate == null)
            {
                return HttpNotFound();
            }

            context.OrderProductAddressProductionDates.Remove(orderProductAddressProductionDate);

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
                    orderId = orderProductAddressProductionDate.OrderId,
                    productId = orderProductAddressProductionDate.ProductId,
                    addressId = orderProductAddressProductionDate.AddressId
                });
        }

        public async Task<ActionResult> AddDeliveryToOrderProductAddress(int? orderId, int? productId, int? addressId)
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

            OrderProductAddress orderProductAddress = await context.OrderProductAddresses
                .Include(opa => opa.OrderProduct.Order)
                .Include(opa => opa.OrderProduct.Product)
                .Include(opa => opa.Address)
                .FirstOrDefaultAsync(opa => opa.OrderId == orderId
                && opa.ProductId == productId
                && opa.AddressId == addressId);

            if (orderProductAddress == null)
            {
                return HttpNotFound();
            }

            AddDeliveryToOrderProductAddressViewModel model = new AddDeliveryToOrderProductAddressViewModel();

            model.OrderProductAddress = new OrderProductAddressViewModel(orderProductAddress);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddDeliveryToOrderProductAddress(AddDeliveryToOrderProductAddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            OrderProductAddress orderProductAddress = await context.OrderProductAddresses
                .FindAsync(model.OrderProductAddress.OrderProduct.Order.Id,
                model.OrderProductAddress.OrderProduct.Product.Id,
                model.OrderProductAddress.Address.Id);

            if (orderProductAddress == null)
            {
                return HttpNotFound();
            }

            context.OrderProductAddressDeliveries.Add(new OrderProductAddressDelivery
            {
                OrderProductAddress = orderProductAddress,
                DeliveryId = model.DeliveryId,
                DeliveryDate = model.DeliveryDate
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
                    orderId = orderProductAddress.OrderId,
                    productId = orderProductAddress.ProductId,
                    addressId = orderProductAddress.AddressId
                });
        }

        public async Task<ActionResult> DeleteDeliveryFromOrderProductAddress(int? orderId, int? productId, int? addressId, int? deliveryId)
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

            OrderProductAddress orderProductAddress = await context.OrderProductAddresses
                 .Include(opa => opa.OrderProduct.Order)
                 .Include(opa => opa.OrderProduct.Product)
                 .Include(opa => opa.Address)
                 .FirstOrDefaultAsync(opa => opa.OrderId == orderId
                 && opa.ProductId == productId
                 && opa.AddressId == addressId);

            if (orderProductAddress == null)
            {
                return HttpNotFound();
            }

            DeleteDeliveryFromOrderProductAddressViewModel model = new DeleteDeliveryFromOrderProductAddressViewModel(orderProductAddress, deliveryId.Value);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteDeliveryFromOrderProductAddress(DeleteDeliveryFromOrderProductAddressViewModel model)
        {
            OrderProductAddressDelivery orderProductAddressDelivery = await context.OrderProductAddressDeliveries
                .FindAsync(model.OrderProductAddress.OrderProduct.Order.Id,
                model.OrderProductAddress.OrderProduct.Product.Id,
                model.OrderProductAddress.Address.Id,
                model.DeliveryId);

            if (orderProductAddressDelivery == null)
            {
                return HttpNotFound();
            }

            context.OrderProductAddressDeliveries.Remove(orderProductAddressDelivery);

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
                    addressId = orderProductAddressDelivery.AddressId
                });
        }

        private List<SelectListItem> GetAvailableSizes(OrderProductAddress orderProductAddress, int? sizeId = null)
        {
            List<SelectListItem> availableSizes = new List<SelectListItem>();

            if (context.Sizes.Any())
            {
                foreach (var size in context.Sizes.Include(s => s.OrderProductAddressSizes))
                {
                    OrderProductAddressSize orderProductAddressSize = size.OrderProductAddressSizes
                        .FirstOrDefault(opas => opas.OrderId == orderProductAddress.OrderId
                        && opas.ProductId == orderProductAddress.ProductId
                        && opas.AddressId == orderProductAddress.AddressId
                        && opas.SizeId == size.Id);

                    if (orderProductAddressSize == null)
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