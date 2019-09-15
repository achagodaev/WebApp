using WebApp.UI.Models;
using WebApp.UI.ViewModels.MaterialViewModels;
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
    public class MaterialsController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Index()
        {
            var materials = await context.Materials.OrderBy(m => m.Name).ToListAsync();
            var model = materials.Select(m => new MaterialViewModel(m));

            return View(model);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Material material = await context.Materials.FindAsync(id);

            if (material == null)
            {
                return HttpNotFound();
            }

            MaterialDetailsViewModel model = new MaterialDetailsViewModel(material);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateMaterialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Material material = new Material
            {
                Name = model.Name
            };

            if (context.Materials.Any())
            {
                material.Id = context.Materials.Max(t => t.Id) + 1;
            }
            else
            {
                material.Id = 1;
            }

            context.Materials.Add(material);

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

            Material material = await context.Materials.FindAsync(id);

            if (material == null)
            {
                return HttpNotFound();
            }

            MaterialViewModel model = new MaterialViewModel(material);

            return View(model);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(MaterialViewModel model)
        {
            Material material = await context.Materials.FindAsync(model.Id);

            if (material == null)
            {
                return HttpNotFound();
            }

            context.Materials.Remove(material);

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

            Material material = await context.Materials.FindAsync(id);

            if (material == null)
            {
                return HttpNotFound();
            }

            EditMaterialViewModel model = new EditMaterialViewModel
            {
                Id = material.Id,
                Name = material.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditMaterialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Material material = await context.Materials.FindAsync(model.Id);

            if (material == null)
            {
                return HttpNotFound();
            }

            material.Name = model.Name;

            context.Entry(material).State = EntityState.Modified;

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