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
using WebApp.UI.ViewModels.OrderProductAddressSizeViewModels;
using WebApp.UI.ViewModels.OrderProductAddressViewModels;

namespace WebApp.UI.Controllers
{
    public class OrderProductAddressProductionDatesController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Details(int? orderId, int? productId, int? addressId, DateTime? productionDate)
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

            if(productionDate == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProductAddressProductionDate orderProductAddressProductionDate = await context.OrderProductAddressProductionDates
                .Include(opapd => opapd.OrderProductAddress.OrderProduct.Order)
                .Include(opapd => opapd.OrderProductAddress.OrderProduct.Product)
                .Include(opapd => opapd.OrderProductAddress.Address)
                .Include(opapd => opapd.OrderProductAddressProductionDateSizes)
                .Include(opapd => opapd.OrderProductAddressProductionDateSizes.Select(opapds => opapds.Size))
                .FirstOrDefaultAsync(opapd => opapd.OrderId == orderId 
                && opapd.ProductId == productId
                && opapd.AddressId == addressId
                && opapd.ProductionDate == productionDate);

            if (orderProductAddressProductionDate == null)
            {
                return HttpNotFound();
            }

            OrderProductAddressProductionDateViewModel model = new OrderProductAddressProductionDateViewModel(orderProductAddressProductionDate);

            if (orderProductAddressProductionDate.OrderProductAddressProductionDateSizes.Any())
            {
                foreach (var orderProductAddressProductionDateSize in orderProductAddressProductionDate.OrderProductAddressProductionDateSizes)
                {
                    model.OrderProductAddressProductionDateSizes.Add(new OrderProductAddressProductionDateSizeViewModel(orderProductAddressProductionDateSize));
                }
            }

            return View(model);
        }

        public async Task<ActionResult> Edit(int? orderId, int? productId, int? addressId, DateTime? productionDate)
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

            OrderProductAddressProductionDate orderProductAddressProductionDate = await context.OrderProductAddressProductionDates
                .Include(opapd => opapd.OrderProductAddress.OrderProduct.Order)
                .Include(opapd => opapd.OrderProductAddress.OrderProduct.Product)
                .Include(opapd => opapd.OrderProductAddress.Address)
                .Include(opapd => opapd.OrderProductAddressProductionDateSizes)
                .Include(opapd => opapd.OrderProductAddressProductionDateSizes.Select(opapds => opapds.Size))
                .FirstOrDefaultAsync(opapd => opapd.OrderId == orderId
                && opapd.ProductId == productId
                && opapd.AddressId == addressId
                && opapd.ProductionDate == productionDate);

            if (orderProductAddressProductionDate == null)
            {
                return HttpNotFound();
            }

            EditOrderProductAddressProductionDateViewModel model = new EditOrderProductAddressProductionDateViewModel();

            model.OrderProductAddress = new OrderProductAddressViewModel(orderProductAddressProductionDate.OrderProductAddress);
            model.ProductionDate = orderProductAddressProductionDate.ProductionDate;

            if (orderProductAddressProductionDate.OrderProductAddressProductionDateSizes.Any())
            {
                foreach (var orderProductAddressProductionDateSize in orderProductAddressProductionDate.OrderProductAddressProductionDateSizes)
                {
                    model.OrderProductAddressProductionDateSizes.Add(new OrderProductAddressProductionDateSizeViewModel(orderProductAddressProductionDateSize));
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditOrderProductAddressProductionDateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            OrderProductAddressProductionDate orderProductAddressProductionDate = await context.OrderProductAddressProductionDates
                .FindAsync(model.OrderProductAddress.OrderProduct.Order.Id, 
                model.OrderProductAddress.OrderProduct.Product.Id, 
                model.OrderProductAddress.Address.Id,
                model.ProductionDate);

            if (orderProductAddressProductionDate == null)
            {
                return HttpNotFound();
            }

            context.Entry(orderProductAddressProductionDate).State = EntityState.Modified;

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
                    addressId = orderProductAddressProductionDate.AddressId,
                    productionDate = orderProductAddressProductionDate.ProductionDate
                });
        }

        public async Task<ActionResult> AddSizeToOrderProductAddressProductionDate(int? orderId, int? productId, int? addressId, DateTime? productionDate)
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

            OrderProductAddressProductionDate orderProductAddressProductionDate = await context.OrderProductAddressProductionDates
                .Include(opapd => opapd.OrderProductAddress.OrderProduct.Order)
                .Include(opapd => opapd.OrderProductAddress.OrderProduct.Product)
                .Include(opapd => opapd.OrderProductAddress.Address)
                .FirstOrDefaultAsync(opapd => opapd.OrderId == orderId
                && opapd.ProductId == productId
                && opapd.AddressId == addressId
                && opapd.ProductionDate == productionDate);

            if (orderProductAddressProductionDate == null)
            {
                return HttpNotFound();
            }

            AddSizeToOrderProductAddressProductionDateViewModel model = new AddSizeToOrderProductAddressProductionDateViewModel();

            model.OrderProductAddressProductionDate = new OrderProductAddressProductionDateViewModel(orderProductAddressProductionDate);
            model.AvailableSizes = GetAvailableSizes(orderProductAddressProductionDate);
                
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddSizeToOrderProductAddressProductionDate(AddSizeToOrderProductAddressProductionDateViewModel model)
        {
            OrderProductAddressProductionDate orderProductAddressProductionDate = await context.OrderProductAddressProductionDates
                .FindAsync(model.OrderProductAddressProductionDate.OrderProductAddress.OrderProduct.Order.Id,
                model.OrderProductAddressProductionDate.OrderProductAddress.OrderProduct.Product.Id,
                model.OrderProductAddressProductionDate.OrderProductAddress.Address.Id,
                model.OrderProductAddressProductionDate.ProductionDate);

            if (orderProductAddressProductionDate == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                model.AvailableSizes = GetAvailableSizes(orderProductAddressProductionDate, model.SizeId);

                return View(model);
            }

            Size size = await context.Sizes.FindAsync(model.SizeId);

            if (size == null)
            {
                return HttpNotFound();
            }

            context.OrderProductAddressProductionDateSizes.Add(new OrderProductAddressProductionDateSize
            {
                OrderProductAddressProductionDate = orderProductAddressProductionDate,
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
                    orderId = orderProductAddressProductionDate.OrderId,
                    productId = orderProductAddressProductionDate.ProductId,
                    addressId = orderProductAddressProductionDate.AddressId,
                    productionDate = orderProductAddressProductionDate.ProductionDate
                });
        }

        public async Task<ActionResult> DeleteSizeFromOrderProductAddressProductionDate(int? orderId, int? productId, int? addressId, DateTime? productionDate, int? sizeId)
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

            OrderProductAddressProductionDate orderProductAddressProductionDate = await context.OrderProductAddressProductionDates
                .Include(opapd => opapd.OrderProductAddress.OrderProduct.Order)
                .Include(opapd => opapd.OrderProductAddress.OrderProduct.Product)
                .Include(opapd => opapd.OrderProductAddress.Address)
                .FirstOrDefaultAsync(opapd => opapd.OrderId == orderId
                && opapd.ProductId == productId
                && opapd.AddressId == addressId
                && opapd.ProductionDate == productionDate);

            if (orderProductAddressProductionDate == null)
            {
                return HttpNotFound();
            }

            Size size = await context.Sizes.FindAsync(sizeId);

            if (size == null)
            {
                return HttpNotFound();
            }

            DeleteSizeFromOrderProductAddressProductionDateViewModel model = new DeleteSizeFromOrderProductAddressProductionDateViewModel(orderProductAddressProductionDate, size);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSizeFromOrderProductAddressProductionDate(DeleteSizeFromOrderProductAddressProductionDateViewModel model)
        {
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

            context.OrderProductAddressProductionDateSizes.Remove(orderProductAddressProductionDateSize);

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
                    productionDate = orderProductAddressProductionDateSize.ProductionDate
                });
        }

        public async Task<ActionResult> AddAllSizesToOrderProductAddressProductionDate(int? orderId, int? productId, int? addressId, DateTime? productionDate)
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

            OrderProductAddressProductionDate orderProductAddressProductionDate = await context.OrderProductAddressProductionDates
                .Include(opapd => opapd.OrderProductAddress.OrderProduct.Order)
                .Include(opapd => opapd.OrderProductAddress.OrderProduct.Product)
                .Include(opapd => opapd.OrderProductAddress.Address)
                .FirstOrDefaultAsync(opapd => opapd.OrderId == orderId
                && opapd.ProductId == productId
                && opapd.AddressId == addressId
                && opapd.ProductionDate == productionDate);

            if (orderProductAddressProductionDate == null)
            {
                return HttpNotFound();
            }

            AddAllSizesToOrderProductAddressProductionDateViewModel model = new AddAllSizesToOrderProductAddressProductionDateViewModel();

            model.OrderProductAddressProductionDate = new OrderProductAddressProductionDateViewModel(orderProductAddressProductionDate);
            model.OrderProductAddressSizes = GetOrderProductAddressSizeViewModels(orderProductAddressProductionDate);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAllSizesToOrderProductAddressProductionDate(AddAllSizesToOrderProductAddressProductionDateViewModel model)
        {
            OrderProductAddressProductionDate orderProductAddressProductionDate = await context.OrderProductAddressProductionDates
                .Include(opapd => opapd.OrderProductAddress.OrderProduct.Order)
                .Include(opapd => opapd.OrderProductAddress.OrderProduct.Product)
                .Include(opapd => opapd.OrderProductAddress.Address)
                .Include(opapd => opapd.OrderProductAddressProductionDateSizes)
                .FirstOrDefaultAsync(opapd => opapd.OrderId == model.OrderProductAddressProductionDate.OrderProductAddress.OrderProduct.Order.Id
                && opapd.ProductId == model.OrderProductAddressProductionDate.OrderProductAddress.OrderProduct.Product.Id
                && opapd.AddressId == model.OrderProductAddressProductionDate.OrderProductAddress.Address.Id
                && opapd.ProductionDate == model.OrderProductAddressProductionDate.ProductionDate);

            if (orderProductAddressProductionDate == null)
            {
                return HttpNotFound();
            }

            if (orderProductAddressProductionDate.OrderProductAddressProductionDateSizes.Any())
            {
                ModelState.AddModelError("", "Дата производства продукции грузополучателя заказа уже содержит размерные данные");
            }

            if (!ModelState.IsValid)
            {
                model.OrderProductAddressSizes = GetOrderProductAddressSizeViewModels(orderProductAddressProductionDate);

                return View(model);
            }

            foreach (var orderProductAddressSizeViewModel in GetOrderProductAddressSizeViewModels(orderProductAddressProductionDate))
            {
                Size size = context.Sizes.Find(orderProductAddressSizeViewModel.Size.Id);

                if (size != null)
                {
                    context.OrderProductAddressProductionDateSizes.Add(new OrderProductAddressProductionDateSize
                    {
                        OrderProductAddressProductionDate = orderProductAddressProductionDate,
                        Size = size,
                        Quantity = orderProductAddressSizeViewModel.Quantity
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
                    orderId = orderProductAddressProductionDate.OrderId,
                    productId = orderProductAddressProductionDate.ProductId,
                    addressId = orderProductAddressProductionDate.AddressId,
                    productionDate = orderProductAddressProductionDate.ProductionDate
                });
        }

        private List<SelectListItem> GetAvailableSizes(OrderProductAddressProductionDate orderProductAddressProductionDate, int? sizeId = null)
        {
            List<SelectListItem> availableSizes = new List<SelectListItem>();

            if (context.Sizes.Any())
            {
                foreach (var size in context.Sizes
                    .Include(s => s.OrderProductAddressSizes)
                    .Include(s => s.OrderProductAddressProductionDateSizes))
                {
                    OrderProductAddressSize orderProductAddressSize = size.OrderProductAddressSizes
                        .FirstOrDefault(opas => opas.OrderId == orderProductAddressProductionDate.OrderId
                        && opas.ProductId == orderProductAddressProductionDate.ProductId
                        && opas.AddressId == orderProductAddressProductionDate.AddressId
                        && opas.SizeId == size.Id);

                    OrderProductAddressProductionDateSize orderProductAddressProductionDateSize = size.OrderProductAddressProductionDateSizes
                        .FirstOrDefault(opapds => opapds.OrderId == orderProductAddressProductionDate.OrderId
                        && opapds.ProductId == orderProductAddressProductionDate.ProductId
                        && opapds.AddressId == orderProductAddressProductionDate.AddressId
                        && opapds.ProductionDate == orderProductAddressProductionDate.ProductionDate
                        && opapds.SizeId == size.Id);

                    if (orderProductAddressSize != null && orderProductAddressProductionDateSize == null)
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

        private List<OrderProductAddressSizeViewModel> GetOrderProductAddressSizeViewModels(OrderProductAddressProductionDate orderProductAddressProductionDate)
        {
            List<OrderProductAddressSizeViewModel> orderProductAddressSizeViewModels = new List<OrderProductAddressSizeViewModel>();

            if (context.OrderProductAddressSizes.Any())
            {
                foreach (var orderProductAddressSize in context.OrderProductAddressSizes
                    .Include(opas => opas.OrderProductAddress.OrderProduct.Order)
                    .Include(opas => opas.OrderProductAddress.OrderProduct.Product)
                    .Include(opas => opas.OrderProductAddress.Address)
                    .Include(opas => opas.Size)
                    .Where(opas => opas.OrderId == orderProductAddressProductionDate.OrderId
                    && opas.ProductId == orderProductAddressProductionDate.ProductId
                    && opas.AddressId == orderProductAddressProductionDate.AddressId))
                {
                    orderProductAddressSizeViewModels.Add(new OrderProductAddressSizeViewModel(orderProductAddressSize));
                }
            }

            return orderProductAddressSizeViewModels;
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