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
using WebApp.UI.ViewModels.OrderViewModels;
using WebApp.UI.ViewModels.OrderProductViewModels;
using WebApp.UI.ViewModels.ProductViewModels;
using WebApp.UI.ViewModels.OrderProductAddressViewModels;
using WebApp.UI.ViewModels.OrderProductMaterialViewModels;
using WebApp.UI.ViewModels.OrderProductDeliveryViewModels;

namespace WebApp.UI.Controllers
{
    public class OrderProductsController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Details(int? orderId, int? productId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProduct orderProduct = await context.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product)
                .Include(op => op.OrderProductAddresses)
                .Include(op => op.OrderProductMaterials)
                .Include(op => op.OrderProductDeliveries)
                .Include(op => op.OrderProductAddresses.Select(opa => opa.Address))
                .Include(op => op.OrderProductMaterials.Select(opm => opm.Material))
                .Include(op => op.OrderProductMaterials.Select(opm => opm.Supplier))
                .FirstOrDefaultAsync(op => op.OrderId == orderId 
                && op.ProductId == productId);

            if (orderProduct == null)
            {
                return HttpNotFound();
            }

            OrderProductDetailsViewModel model = new OrderProductDetailsViewModel(orderProduct);

            if (orderProduct.OrderProductAddresses.Any())
            {
                foreach (var orderAddressProduct in orderProduct.OrderProductAddresses)
                {
                    model.OrderProductAddresses.Add(new OrderProductAddressViewModel(orderAddressProduct));
                }
            }

            if (orderProduct.OrderProductMaterials.Any())
            {
                foreach (var orderAddressMaterial in orderProduct.OrderProductMaterials)
                {
                    model.OrderProductMaterials.Add(new OrderProductMaterialViewModel(orderAddressMaterial));
                }
            }

            if (orderProduct.OrderProductDeliveries.Any())
            {
                foreach (var orderProductDelivery in orderProduct.OrderProductDeliveries)
                {
                    model.OrderProductDeliveries.Add(new OrderProductDeliveryViewModel(orderProductDelivery));
                }
            }

            return View(model);
        }

        public async Task<ActionResult> Edit(int? orderId, int? productId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProduct orderProduct = await context.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product)
                .Include(op => op.OrderProductAddresses)
                .Include(op => op.OrderProductMaterials)
                .Include(op => op.OrderProductDeliveries)
                .Include(op => op.OrderProductAddresses.Select(opa => opa.Address))
                .Include(op => op.OrderProductMaterials.Select(opm => opm.Material))
                .Include(op => op.OrderProductMaterials.Select(opm => opm.Supplier))
                .FirstOrDefaultAsync(op => op.OrderId == orderId
                && op.ProductId == productId);

            if (orderProduct == null)
            {
                return HttpNotFound();
            }

            EditOrderProductViewModel model = new EditOrderProductViewModel();

            model.Order = new OrderViewModel(orderProduct.Order);
            model.Product = new ProductViewModel(orderProduct.Product);
            model.Price = orderProduct.Price;

            if (orderProduct.OrderProductAddresses.Any())
            {
                foreach (var orderAddressProduct in orderProduct.OrderProductAddresses)
                {
                    model.OrderProductAddresses.Add(new OrderProductAddressViewModel(orderAddressProduct));
                }
            }

            if (orderProduct.OrderProductMaterials.Any())
            {
                foreach (var orderAddressMaterial in orderProduct.OrderProductMaterials)
                {
                    model.OrderProductMaterials.Add(new OrderProductMaterialViewModel(orderAddressMaterial));
                }
            }

            if (orderProduct.OrderProductDeliveries.Any())
            {
                foreach (var orderProductDelivery in orderProduct.OrderProductDeliveries)
                {
                    model.OrderProductDeliveries.Add(new OrderProductDeliveryViewModel(orderProductDelivery));
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditOrderProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            OrderProduct orderProduct = await context.OrderProducts
                .FindAsync(model.Order.Id, model.Product.Id);

            if (orderProduct == null)
            {
                return HttpNotFound();
            }

            orderProduct.Price = model.Price;

            context.Entry(orderProduct).State = EntityState.Modified;

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
                    orderId = orderProduct.OrderId,
                    productId = orderProduct.ProductId
                });
        }

        public async Task<ActionResult> AddAddressToOrderProduct(int? orderId, int? productId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProduct orderProduct = await context.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product)
                .Include(op => op.OrderProductAddresses)
                .FirstOrDefaultAsync(oa => oa.OrderId == orderId
                && oa.ProductId == productId);

            if (orderProduct == null)
            {
                return HttpNotFound();
            }

            AddAddressToOrderProductViewModel model = new AddAddressToOrderProductViewModel();
            model.OrderProduct = new OrderProductViewModel(orderProduct);
            model.AvailableAddresses = GetAvailableAddresses(orderProduct);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAddressToOrderProduct(AddAddressToOrderProductViewModel model)
        {
            OrderProduct orderProduct = await context.OrderProducts
                .FindAsync(model.OrderProduct.Order.Id,
                model.OrderProduct.Product.Id);

            if (orderProduct == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                model.AvailableAddresses = GetAvailableAddresses(orderProduct, model.AddressId);

                return View(model);
            }

            Address address = await context.Addresses.FindAsync(model.AddressId);

            if (address == null)
            {
                return HttpNotFound();
            }

            context.OrderProductAddresses.Add(new OrderProductAddress
            {
                OrderProduct = orderProduct,
                Address = address,
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
                    orderId = orderProduct.OrderId,
                    productId = orderProduct.ProductId
                });
        }

        public async Task<ActionResult> DeleteAddressFromOrderProduct(int? orderId, int? productId, int? addressId)
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

            OrderProduct orderProduct = await context.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product)
                .FirstOrDefaultAsync(oa => oa.OrderId == orderId
                && oa.ProductId == productId);

            if (orderProduct == null)
            {
                return HttpNotFound();
            }

            Address address = await context.Addresses.FindAsync(addressId);

            if (address == null)
            {
                return HttpNotFound();
            }

            DeleteAddressFromOrderProductViewModel model = new DeleteAddressFromOrderProductViewModel(orderProduct, address);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAddressFromOrderProduct(DeleteAddressFromOrderProductViewModel model)
        {
            OrderProductAddress orderProductAddress = await context.OrderProductAddresses
                .FindAsync(model.OrderProduct.Order.Id,
                model.OrderProduct.Product.Id,
                model.Address.Id);

            if (orderProductAddress == null)
            {
                return HttpNotFound();
            }

            context.OrderProductAddresses.Remove(orderProductAddress);

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
                    productId = orderProductAddress.ProductId
                });
        }

        public async Task<ActionResult> AddMaterialToOrderProduct(int? orderId, int? productId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProduct orderProduct = await context.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product)
                .Include(op => op.OrderProductMaterials)
                .FirstOrDefaultAsync(op => op.OrderId == orderId
                && op.ProductId == productId);

            if (orderProduct == null)
            {
                return HttpNotFound();
            }

            AddMaterialToOrderProductViewModel model = new AddMaterialToOrderProductViewModel();
            model.OrderProduct = new OrderProductViewModel(orderProduct);
            model.AvailableMaterials = GetAvailableMaterials(orderProduct);
            model.AvailableSuppliers = GetAvailableSuppliers();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddMaterialToOrderProduct(AddMaterialToOrderProductViewModel model)
        {
            OrderProduct orderProduct = await context.OrderProducts
                .FindAsync(model.OrderProduct.Order.Id,
                model.OrderProduct.Product.Id);

            if (orderProduct == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                model.AvailableMaterials = GetAvailableMaterials(orderProduct, model.MaterialId);
                model.AvailableSuppliers = GetAvailableSuppliers(model.SupplierId);

                return View(model);
            }

            Material material = await context.Materials.FindAsync(model.MaterialId);

            if (material == null)
            {
                return HttpNotFound();
            }

            Supplier supplier = await context.Suppliers.FindAsync(model.SupplierId);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            context.OrderProductMaterials.Add(new OrderProductMaterial
            {
                OrderProduct = orderProduct,
                Material = material,
                Supplier = supplier,
                Quantity = model.Quantity,
                DeliveryDate = model.DeliveryDate,
                Price = model.Price,
                Rate = model.Rate
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
                    orderId = orderProduct.OrderId,
                    productId = orderProduct.ProductId
                });
        }

        public async Task<ActionResult> DeleteMaterialFromOrderProduct(int? orderId, int? productId, int? materialId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (materialId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProduct orderProduct = await context.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product)
                .FirstOrDefaultAsync(op => op.OrderId == orderId
                && op.ProductId == productId);

            if (orderProduct == null)
            {
                return HttpNotFound();
            }

            Material material = await context.Materials.FindAsync(materialId);

            if (material == null)
            {
                return HttpNotFound();
            }

            DeleteMaterialFromOrderProductViewModel model = new DeleteMaterialFromOrderProductViewModel(orderProduct, material);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteMaterialFromOrderProduct(DeleteMaterialFromOrderProductViewModel model)
        {
            OrderProductMaterial orderProductMaterial = await context.OrderProductMaterials
                .FindAsync(model.OrderProduct.Order.Id,
                model.OrderProduct.Product.Id,
                model.Material.Id);

            if (orderProductMaterial == null)
            {
                return HttpNotFound();
            }

            context.OrderProductMaterials.Remove(orderProductMaterial);

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
                    orderId = orderProductMaterial.OrderId,
                    productId = orderProductMaterial.ProductId
                });
        }

        public async Task<ActionResult> AddDeliveryToOrderProduct(int? orderId, int? productId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProduct orderProduct = await context.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product)
                .Include(op => op.OrderProductDeliveries)
                .FirstOrDefaultAsync(op => op.OrderId == orderId
                && op.ProductId == productId);

            if (orderProduct == null)
            {
                return HttpNotFound();
            }

            AddDeliveryToOrderProductViewModel model = new AddDeliveryToOrderProductViewModel();
            model.OrderProduct = new OrderProductViewModel(orderProduct);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddDeliveryToOrderProduct(AddDeliveryToOrderProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            OrderProduct orderProduct = await context.OrderProducts
                .Include(op => op.OrderProductDeliveries)
                .FirstOrDefaultAsync(op => op.OrderId == model.OrderProduct.Order.Id
                && op.ProductId == model.OrderProduct.Product.Id);

            if (orderProduct == null)
            {
                return HttpNotFound();
            }

            OrderProductDelivery orderProductDelivery = new OrderProductDelivery();
            orderProductDelivery.OrderProduct = orderProduct;
            orderProductDelivery.DeliveryId = model.DeliveryId; ;
            orderProductDelivery.DeliveryDate = model.DeliveryDate;
            orderProductDelivery.AcceptanceId = model.AcceptanceId;
            orderProductDelivery.AcceptanceDate = model.AcceptanceDate;

            context.OrderProductDeliveries.Add(orderProductDelivery);

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
                    orderId = orderProduct.OrderId,
                    productId = orderProduct.ProductId
                });
        }

        public async Task<ActionResult> DeleteDeliveryFromOrderProduct(int? orderId, int? productId, int? deliveryId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (deliveryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProduct orderProduct = await context.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Product)
                .FirstOrDefaultAsync(op => op.OrderId == orderId
                && op.ProductId == productId);

            if (orderProduct == null)
            {
                return HttpNotFound();
            }

            if (!context.OrderProductDeliveries.Any(opd => opd.DeliveryId == deliveryId))
            {
                return HttpNotFound();
            }

            DeleteDeliveryFromOrderProductViewModel model = new DeleteDeliveryFromOrderProductViewModel(orderProduct, deliveryId.Value);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteDeliveryFromOrderProduct(DeleteDeliveryFromOrderProductViewModel model)
        {
            OrderProductDelivery orderProductDelivery = await context.OrderProductDeliveries
                .FindAsync(model.OrderProduct.Order.Id,
                model.OrderProduct.Product.Id,
                model.DeliveryId);

            if (orderProductDelivery == null)
            {
                return HttpNotFound();
            }

            context.OrderProductDeliveries.Remove(orderProductDelivery);

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
                    orderId = orderProductDelivery.OrderId,
                    productId = orderProductDelivery.ProductId
                });
        }

        private List<SelectListItem> GetAvailableAddresses(OrderProduct orderProduct, int? addressId = null)
        {
            List<SelectListItem> availableAddresses = new List<SelectListItem>();

            if (context.Addresses.Any())
            {
                foreach (var address in context.Addresses.Include(a => a.OrderProductAddresses))
                {
                    OrderProductAddress orderProductAddress = address.OrderProductAddresses
                        .FirstOrDefault(opa => opa.OrderId == orderProduct.OrderId
                        && opa.ProductId == orderProduct.ProductId
                        && opa.AddressId == address.Id);

                    if (orderProductAddress == null)
                    {
                        availableAddresses.Add(new SelectListItem
                        {
                            Value = address.Id.ToString(),
                            Text = address.Name,
                            Selected = address.Id == addressId
                        });
                    }
                }
            }

            return availableAddresses;
        }

        private List<SelectListItem> GetAvailableMaterials(OrderProduct orderProduct, int? materialId = null)
        {
            List<SelectListItem> availableMaterials = new List<SelectListItem>();

            if (context.Materials.Any())
            {
                foreach (var material in context.Materials.Include(m => m.OrderProductMaterials))
                {
                    OrderProductMaterial orderProductMaterial = material.OrderProductMaterials
                        .FirstOrDefault(opm => opm.OrderId == orderProduct.OrderId
                        && opm.ProductId == orderProduct.ProductId
                        && opm.MaterialId == material.Id);

                    if (orderProductMaterial == null)
                    {
                        availableMaterials.Add(new SelectListItem
                        {
                            Value = material.Id.ToString(),
                            Text = material.Name,
                            Selected = material.Id == materialId
                        });
                    }
                }
            }

            return availableMaterials;
        }

        private List<SelectListItem> GetAvailableSuppliers(int? supplierId = null)
        {
            List<SelectListItem> availableSuppliers = new List<SelectListItem>();

            if (context.Suppliers.Any())
            {
                foreach (var supplier in context.Suppliers)
                {
                    availableSuppliers.Add(new SelectListItem
                    {
                        Value = supplier.Id.ToString(),
                        Text = supplier.Name,
                        Selected = supplier.Id == supplierId
                    });
                }
            }

            return availableSuppliers;
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