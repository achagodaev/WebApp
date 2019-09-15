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
using WebApp.UI.ViewModels.OrderProductDeliveryMaterialViewModels;
using WebApp.UI.ViewModels.OrderProductDeliveryViewModels;
using WebApp.UI.ViewModels.OrderProductViewModels;

namespace WebApp.UI.Controllers
{
    public class OrderProductDeliveriesController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Details(int? orderId, int? productId, int? deliveryId)
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

            OrderProductDelivery orderProductDelivery = await context.OrderProductDeliveries
                .Include(opd => opd.OrderProduct.Order)
                .Include(opd => opd.OrderProduct.Product)
                .Include(opd => opd.OrderProductDeliveryMaterials.Select(opdm => opdm.Material))
                .FirstOrDefaultAsync(opd => opd.OrderId == orderId 
                && opd.ProductId == productId
                && opd.DeliveryId == deliveryId);

            if (orderProductDelivery == null)
            {
                return HttpNotFound();
            }

            OrderProductDeliveryViewModel model = new OrderProductDeliveryViewModel(orderProductDelivery);

            if (orderProductDelivery.OrderProductDeliveryMaterials.Any())
            {
                foreach (var orderProductlDeliveryMaterial in orderProductDelivery.OrderProductDeliveryMaterials)
                {
                    model.OrderProductDeliveryMaterials.Add(new OrderProductDeliveryMaterialViewModel(orderProductlDeliveryMaterial));
                }
            }

            return View(model);
        } 

        public async Task<ActionResult> Edit(int? orderId, int? productId, int? deliveryId)
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

            OrderProductDelivery orderProductDelivery = await context.OrderProductDeliveries
                .Include(opd => opd.OrderProduct.Order)
                .Include(opd => opd.OrderProduct.Product)
                .Include(opd => opd.OrderProductDeliveryMaterials.Select(opdm => opdm.Material))
                .FirstOrDefaultAsync(opd => opd.OrderId == orderId
                && opd.ProductId == productId
                && opd.DeliveryId == deliveryId);

            if (orderProductDelivery == null)
            {
                return HttpNotFound();
            }

            EditOrderProductDeliveryViewModel model = new EditOrderProductDeliveryViewModel();

            model.OrderProduct = new OrderProductViewModel(orderProductDelivery.OrderProduct);
            model.DeliveryId = orderProductDelivery.DeliveryId;
            model.DeliveryDate = orderProductDelivery.DeliveryDate;
            model.DeliveryImagePath = orderProductDelivery.DeliveryImagePath;
            model.AcceptanceId = orderProductDelivery.AcceptanceId;
            model.AcceptanceDate = orderProductDelivery.AcceptanceDate;
            model.AcceptanceImagePath = orderProductDelivery.AcceptanceImagePath;

            if (orderProductDelivery.OrderProductDeliveryMaterials.Any())
            {
                foreach (var orderProductlDeliveryMaterial in orderProductDelivery.OrderProductDeliveryMaterials)
                {
                    model.OrderProductDeliveryMaterials.Add(new OrderProductDeliveryMaterialViewModel(orderProductlDeliveryMaterial));
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditOrderProductDeliveryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            OrderProductDelivery orderProductDelivery = await context.OrderProductDeliveries
                .FindAsync(model.OrderProduct.Order.Id, 
                model.OrderProduct.Product.Id, 
                model.DeliveryId);

            if (orderProductDelivery == null)
            {
                return HttpNotFound();
            }

            orderProductDelivery.DeliveryDate = model.DeliveryDate;
            orderProductDelivery.DeliveryImagePath = model.DeliveryImagePath;
            orderProductDelivery.AcceptanceId = model.AcceptanceId;
            orderProductDelivery.AcceptanceDate = model.AcceptanceDate;
            orderProductDelivery.AcceptanceImagePath = model.AcceptanceImagePath;

            context.Entry(orderProductDelivery).State = EntityState.Modified;

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
                    productId = orderProductDelivery.ProductId,
                    deliveryId = orderProductDelivery.DeliveryId
                });
        }

        public async Task<ActionResult> AddMaterialToOrderProductDelivery(int? orderId, int? productId, int? deliveryId)
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

            OrderProductDelivery orderProductDelivery = await context.OrderProductDeliveries
                .Include(opd => opd.OrderProduct.Order)
                .Include(opd => opd.OrderProduct.Product)
                .FirstOrDefaultAsync(opa => opa.OrderId == orderId
                && opa.ProductId == productId
                && opa.DeliveryId == deliveryId);

            if (orderProductDelivery == null)
            {
                return HttpNotFound();
            }

            AddMaterialToOrderProductDeliveryViewModel model = new AddMaterialToOrderProductDeliveryViewModel();

            model.OrderProductDelivery = new OrderProductDeliveryViewModel(orderProductDelivery);
            model.AvailableMaterials = GetAvailableMaterials(orderProductDelivery);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddMaterialToOrderProductDelivery(AddMaterialToOrderProductDeliveryViewModel model)
        {
            OrderProductDelivery orderProductDelivery = await context.OrderProductDeliveries
                .FindAsync(model.OrderProductDelivery.OrderProduct.Order.Id,
                model.OrderProductDelivery.OrderProduct.Product.Id,
                model.OrderProductDelivery.DeliveryId);

            if (orderProductDelivery == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                model.AvailableMaterials = GetAvailableMaterials(orderProductDelivery, model.MaterialId);

                return View(model);
            }

            Material material = await context.Materials.FindAsync(model.MaterialId);

            if (material == null)
            {
                return HttpNotFound();
            }

            context.OrderProductDeliveryMaterials.Add(new OrderProductDeliveryMaterial
            {
                OrderProductDelivery = orderProductDelivery,
                Material = material,
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
                    orderId = orderProductDelivery.OrderId,
                    productId = orderProductDelivery.ProductId,
                    deliveryId = orderProductDelivery.DeliveryId
                });
        }

        public async Task<ActionResult> DeleteMaterialFromOrderProductDelivery(int? orderId, int? productId, int? deliveryId, int? materialId)
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

            OrderProductDelivery orderProductDelivery = await context.OrderProductDeliveries
                 .Include(opd => opd.OrderProduct.Order)
                 .Include(opd => opd.OrderProduct.Product)
                 .Include(opd => opd.OrderProductDeliveryMaterials)
                 .FirstOrDefaultAsync(opd => opd.OrderId == orderId
                 && opd.ProductId == productId
                 && opd.DeliveryId == deliveryId);

            if (orderProductDelivery == null)
            {
                return HttpNotFound();
            }

            Material material = await context.Materials.FindAsync(materialId);

            if (material == null)
            {
                return HttpNotFound();
            }

            DeleteMaterialFromOrderProductDeliveryViewModel model = new DeleteMaterialFromOrderProductDeliveryViewModel(orderProductDelivery, material);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteMaterialFromOrderProductDelivery(DeleteMaterialFromOrderProductDeliveryViewModel model)
        {
            OrderProductDeliveryMaterial orderProductDeliveryMaterial = await context.OrderProductDeliveryMaterials
                .FindAsync(model.OrderProductDelivery.OrderProduct.Order.Id,
                model.OrderProductDelivery.OrderProduct.Product.Id,
                model.OrderProductDelivery.DeliveryId,
                model.Material.Id);

            if (orderProductDeliveryMaterial == null)
            {
                return HttpNotFound();
            }

            context.OrderProductDeliveryMaterials.Remove(orderProductDeliveryMaterial);

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
                    deliveryId = orderProductDeliveryMaterial.DeliveryId
                });
        }

        private List<SelectListItem> GetAvailableMaterials(OrderProductDelivery orderProductDelivery, int? materialId = null)
        {
            List<SelectListItem> availableMaterials = new List<SelectListItem>();

            if (context.Materials.Any())
            {
                foreach (var material in context.Materials.Include(m => m.OrderProductDeliveryMaterials))
                {
                    OrderProductDeliveryMaterial orderProductDeliveryMaterial = material.OrderProductDeliveryMaterials
                        .FirstOrDefault(opdm => opdm.OrderId == orderProductDelivery.OrderId
                        && opdm.ProductId == orderProductDelivery.ProductId
                        && opdm.DeliveryId == orderProductDelivery.DeliveryId
                        && opdm.MaterialId == material.Id);

                    if (orderProductDeliveryMaterial == null)
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