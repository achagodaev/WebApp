using WebApp.UI.Models;
using WebApp.UI.ViewModels.AddressViewModels;
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
    public class AddressesController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Index()
        {
            var addresses = await context.Addresses.OrderBy(a => a.Name).ToListAsync();
            var model = addresses.Select(a => new AddressViewModel(a));

            return View(model);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Address address = await context.Addresses.FindAsync(id);

            if (address == null)
            {
                return HttpNotFound();
            }

            AddressDetailsViewModel model = new AddressDetailsViewModel(address);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Address address = new Address
            {
                Name = model.Name
            };

            if (context.Addresses.Any())
            {
                address.Id = context.Addresses.Max(t => t.Id) + 1;
            }
            else
            {
                address.Id = 1;
            }

            context.Addresses.Add(address);

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

            Address address = await context.Addresses.FindAsync(id);

            if (address == null)
            {
                return HttpNotFound();
            }

            AddressViewModel model = new AddressViewModel(address);

            return View(model);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(AddressViewModel model)
        {
            Address address = await context.Addresses.FindAsync(model.Id);

            if (address == null)
            {
                return HttpNotFound();
            }

            context.Addresses.Remove(address);

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

            Address address = await context.Addresses.FindAsync(id);

            if (address == null)
            {
                return HttpNotFound();
            }

            EditAddressViewModel model = new EditAddressViewModel
            {
                Id = address.Id,
                Name = address.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditAddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Address address = await context.Addresses.FindAsync(model.Id);

            if (address == null)
            {
                return HttpNotFound();
            }

            address.Name = model.Name;

            context.Entry(address).State = EntityState.Modified;

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