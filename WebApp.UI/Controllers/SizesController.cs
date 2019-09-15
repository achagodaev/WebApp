using WebApp.UI.Models;
using WebApp.UI.ViewModels.SizeViewModels;
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
    public class SizesController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Index()
        {
            var sizes = await context.Sizes.OrderBy(s => s.Id).ToListAsync();
            var model = sizes.Select(s => new SizeViewModel(s));

            return View(model);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Size size = await context.Sizes.FindAsync(id);

            if (size == null)
            {
                return HttpNotFound();
            }

            SizeDetailsViewModel model = new SizeDetailsViewModel(size);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSizeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Size size = new Size
            {
                Name = model.Name
            };

            if (model.Name.Contains("/"))
            {
                size.Id = int.Parse(model.Name.Replace("/", ""));
            }
            else
            {
                if (context.Sizes.Any())
                {
                    size.Id = context.Sizes.Max(s => s.Id) + 1;
                }
                else
                {
                    size.Id = 1;
                }
            }

            context.Sizes.Add(size);

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

            Size size = await context.Sizes.FindAsync(id);

            if (size == null)
            {
                return HttpNotFound();
            }

            SizeViewModel model = new SizeViewModel(size);

            return View(model);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(SizeViewModel model)
        {
            Size size = await context.Sizes.FindAsync(model.Id);

            if (size == null)
            {
                return HttpNotFound();
            }

            context.Sizes.Remove(size);

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