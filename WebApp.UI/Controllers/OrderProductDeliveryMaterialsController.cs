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
using WebApp.UI.ViewModels.OrderProductDeliveryMaterialViewModels;
using WebApp.UI.ViewModels.OrderProductDeliveryViewModels;

namespace WebApp.UI.Controllers
{
    public class OrderProductDeliveryMaterialsController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Details(int? orderId, int? productId, int? deliveryId, int? materialId)
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

            if (materialId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProductDeliveryMaterial orderProductDeliveryMaterial = await context.OrderProductDeliveryMaterials
                .Include(opdm => opdm.OrderProductDelivery.OrderProduct.Order)
                .Include(opdm => opdm.OrderProductDelivery.OrderProduct.Product)
                .Include(opdm => opdm.Material)
                .FirstOrDefaultAsync(opdm => opdm.OrderId == orderId
                && opdm.ProductId == productId
                && opdm.DeliveryId == deliveryId 
                && opdm.MaterialId == materialId);

            if (orderProductDeliveryMaterial == null)
            {
                return HttpNotFound();
            }

            OrderProductDeliveryMaterialViewModel model = new OrderProductDeliveryMaterialViewModel(orderProductDeliveryMaterial);

            return View(model);
        }

        public async Task<ActionResult> Edit(int? orderId, int? productId, int? deliveryId, int? materialId)
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

            if (materialId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderProductDeliveryMaterial orderProductDeliveryMaterial = await context.OrderProductDeliveryMaterials
                .Include(opdm => opdm.OrderProductDelivery.OrderProduct.Order)
                .Include(opdm => opdm.OrderProductDelivery.OrderProduct.Product)
                .Include(opdm => opdm.Material)
                .FirstOrDefaultAsync(opdm => opdm.OrderId == orderId
                && opdm.ProductId == productId
                && opdm.DeliveryId == deliveryId
                && opdm.MaterialId == materialId);

            if (orderProductDeliveryMaterial == null)
            {
                return HttpNotFound();
            }

            EditOrderProductDeliveryMaterialViewModel model = new EditOrderProductDeliveryMaterialViewModel();

            model.OrderProductDelivery = new OrderProductDeliveryViewModel(orderProductDeliveryMaterial.OrderProductDelivery);
            model.Material = new MaterialViewModel(orderProductDeliveryMaterial.Material);
            model.DeliveryQuantity = orderProductDeliveryMaterial.DeliveryQuantity;
            model.AcceptanceQuantity = orderProductDeliveryMaterial.AcceptanceQuantity;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditOrderProductDeliveryMaterialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            OrderProductDeliveryMaterial orderProductDeliveryMaterial = await context.OrderProductDeliveryMaterials
                .FindAsync(model.OrderProductDelivery.OrderProduct.Order.Id,
                model.OrderProductDelivery.OrderProduct.Product.Id,
                model.OrderProductDelivery.DeliveryId, 
                model.Material.Id);

            if (orderProductDeliveryMaterial == null)
            {
                return HttpNotFound();
            }

            orderProductDeliveryMaterial.DeliveryQuantity = model.DeliveryQuantity;
            orderProductDeliveryMaterial.AcceptanceQuantity = model.AcceptanceQuantity;

            context.Entry(orderProductDeliveryMaterial).State = EntityState.Modified;

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
                    orderId = orderProductDeliveryMaterial.OrderId,
                    productId = orderProductDeliveryMaterial.ProductId,
                    deliveryId = orderProductDeliveryMaterial.DeliveryId,
                    materialId = orderProductDeliveryMaterial.MaterialId
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