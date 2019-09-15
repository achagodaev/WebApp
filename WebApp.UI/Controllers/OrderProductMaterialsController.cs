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
using WebApp.UI.ViewModels.MaterialViewModels;
using WebApp.UI.ViewModels.OrderProductMaterialViewModels;
using WebApp.UI.ViewModels.OrderProductViewModels;

namespace WebApp.UI.Controllers
{
    public class OrderProductMaterialsController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Details(int? orderId, int? productId, int? materialId)
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

            OrderProductMaterial orderProductMaterial = await context.OrderProductMaterials
                .Include(opm => opm.OrderProduct.Order)
                .Include(opm => opm.OrderProduct.Product)
                .Include(opm => opm.Material)
                .Include(opm => opm.Supplier)
                .FirstOrDefaultAsync(opa => opa.OrderId == orderId 
                && opa.ProductId == productId
                && opa.MaterialId == materialId);

            if (orderProductMaterial == null)
            {
                return HttpNotFound();
            }

            OrderProductMaterialViewModel model = new OrderProductMaterialViewModel(orderProductMaterial);

            return View(model);
        }

        public async Task<ActionResult> Edit(int? orderId, int? productId, int? materialId)
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

            OrderProductMaterial orderProductMaterial = await context.OrderProductMaterials
                .Include(opm => opm.OrderProduct.Order)
                .Include(opm => opm.OrderProduct.Product)
                .Include(opm => opm.Material)
                .Include(opm => opm.Supplier)
                .FirstOrDefaultAsync(opa => opa.OrderId == orderId
                && opa.ProductId == productId
                && opa.MaterialId == materialId);

            if (orderProductMaterial == null)
            {
                return HttpNotFound();
            }

            EditOrderProductMaterialViewModel model = new EditOrderProductMaterialViewModel();

            model.OrderProduct = new OrderProductViewModel(orderProductMaterial.OrderProduct);
            model.Material = new MaterialViewModel(orderProductMaterial.Material);
            model.SupplierId = orderProductMaterial.SupplierId;
            model.Quantity = orderProductMaterial.Quantity;
            model.DeliveryDate = orderProductMaterial.DeliveryDate;
            model.Price = orderProductMaterial.Price;
            model.Rate = orderProductMaterial.Rate;
            model.AvailableSuppliers = GetAvailableSuppliers();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditOrderProductMaterialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableSuppliers = GetAvailableSuppliers(model.SupplierId);

                return View(model);
            }

            OrderProductMaterial orderProductMaterial = await context.OrderProductMaterials
                .FindAsync(model.OrderProduct.Order.Id, 
                model.OrderProduct.Product.Id, 
                model.Material.Id);

            if (orderProductMaterial == null)
            {
                return HttpNotFound();
            }

            orderProductMaterial.SupplierId = model.SupplierId;
            orderProductMaterial.Quantity = model.Quantity;
            orderProductMaterial.DeliveryDate = model.DeliveryDate;
            orderProductMaterial.Price = model.Price;
            orderProductMaterial.Rate = model.Rate;

            context.Entry(orderProductMaterial).State = EntityState.Modified;

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
                    productId = orderProductMaterial.ProductId,
                    materialId = orderProductMaterial.MaterialId
                });
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