using WebApp.UI.Models;
using WebApp.UI.ViewModels.SupplierViewModels;
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
    public class SuppliersController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Index()
        {
            var suppliers = await context.Suppliers.OrderBy(s => s.Name).ToListAsync();
            var model = suppliers.Select(a => new SupplierViewModel(a));

            return View(model);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Supplier supplier = await context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            SupplierDetailsViewModel model = new SupplierDetailsViewModel(supplier);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSupplierViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Supplier supplier = new Supplier
            {
                Name = model.Name
            };

            if (context.Suppliers.Any())
            {
                supplier.Id = context.Suppliers.Max(s => s.Id) + 1;
            }
            else
            {
                supplier.Id = 1;
            }

            context.Suppliers.Add(supplier);

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

            Supplier supplier = await context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            SupplierViewModel model = new SupplierViewModel(supplier);

            return View(model);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(SupplierViewModel model)
        {
            Supplier supplier = await context.Suppliers.FindAsync(model.Id);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            context.Suppliers.Remove(supplier);

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

            Supplier supplier = await context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            EditSupplierViewModel model = new EditSupplierViewModel
            {
                Id = supplier.Id,
                Name = supplier.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditSupplierViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Supplier supplier = await context.Suppliers.FindAsync(model.Id);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            supplier.Name = model.Name;

            context.Entry(supplier).State = EntityState.Modified;

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