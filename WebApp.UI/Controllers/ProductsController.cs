using WebApp.UI.Models;
using WebApp.UI.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApp.UI.Controllers
{
    public class ProductsController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Index()
        {
            var products = await context.Products.OrderBy(p => p.Name).ToListAsync();
            var model = products.Select(p => new ProductViewModel(p));

            return View(model);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = await context.Products.FindAsync(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            ProductDetailsViewModel model = new ProductDetailsViewModel(product);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Product product = new Product
            {
                Name = model.Name,
                Description = model.Description
            };

            if (context.Products.Any())
            {
                product.Id = context.Products.Max(t => t.Id) + 1;
            }
            else
            {
                product.Id = 1;
            }

            context.Products.Add(product);

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

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = await context.Products.FindAsync(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            ProductViewModel model = new ProductViewModel(product);

            return View(model);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(ProductViewModel model)
        {
            Product product = await context.Products.FindAsync(model.Id);

            if (product == null)
            {
                return HttpNotFound();
            }

            context.Products.Remove(product);

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

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = await context.Products.FindAsync(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            EditProductViewModel model = new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Product product = await context.Products.FindAsync(model.Id);

            if (product == null)
            {
                return HttpNotFound();
            }

            product.Name = model.Name;
            product.Description = model.Description;

            context.Entry(product).State = EntityState.Modified;

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

            return RedirectToAction(nameof(Index));
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