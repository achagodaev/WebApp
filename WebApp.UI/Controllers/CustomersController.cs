using WebApp.UI.Models;
using WebApp.UI.ViewModels.CustomerViewModels;
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
    public class CustomersController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Index()
        {
            var customers = await context.Customers.OrderBy(c => c.Name).ToListAsync();
            var model = customers.Select(c => new CustomerViewModel(c));

            return View(model);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = await context.Customers.FindAsync(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            CustomerDetailsViewModel model = new CustomerDetailsViewModel(customer);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Customer customer = new Customer
            {
                Name = model.Name
            };

            if (context.Customers.Any())
            {
                customer.Id = context.Customers.Max(t => t.Id) + 1;
            }
            else
            {
                customer.Id = 1;
            }

            context.Customers.Add(customer);

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

            Customer customer = await context.Customers.FindAsync(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            CustomerViewModel model = new CustomerViewModel(customer);

            return View(model);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(CustomerViewModel model)
        {
            Customer customer = await context.Customers.FindAsync(model.Id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            context.Customers.Remove(customer);

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

            Customer customer = await context.Customers.FindAsync(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            EditCustomerViewModel model = new EditCustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditCustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Customer customer = await context.Customers.FindAsync(model.Id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            customer.Name = model.Name;

            context.Entry(customer).State = EntityState.Modified;

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