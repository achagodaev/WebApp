using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.OrderViewModels;
using WebApp.UI.ViewModels.OrderProductViewModels;

namespace WebApp.UI.Controllers
{
    public class OrdersController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public async Task<ActionResult> Index()
        {
            var orders = await context.Orders.ToListAsync();
            var model = orders.Select(o => new OrderViewModel(o));

            return View(model);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = await context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts.Select(op => op.Product))
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            OrderDetailsViewModel model = new OrderDetailsViewModel(order);

            if (order.OrderProducts.Any())
            {
                foreach (var orderProduct in order.OrderProducts.OrderBy(op => op.Product.Name))
                {
                    model.OrderProducts.Add(new OrderProductViewModel(orderProduct));
                }
            }

            return View(model);
        }

        public ActionResult Create()
        {
            CreateOrderViewModel model = new CreateOrderViewModel();
            model.AvailableCustomers = GetAvailableCustomers();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableCustomers = GetAvailableCustomers(model.CustomerId);

                return View(model);
            }

            Order order = new Order();

            if (context.Orders.Any())
            {
                order.Id = context.Orders.Max(o => o.Id) + 1;
            }
            else
            {
                order.Id = 1;
            }

            order.Name = model.Name;
            order.CustomerId = model.CustomerId;

            context.Orders.Add(order);

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

            Order order = await context.Orders
                .Include(o => o.OrderProducts.Select(op => op.Product))
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            EditOrderViewModel model = new EditOrderViewModel();

            model.Id = order.Id;
            model.Name = order.Name;
            model.CustomerId = order.CustomerId;
            model.AvailableCustomers = GetAvailableCustomers();

            if (order.OrderProducts.Any())
            {
                foreach (var orderProduct in order.OrderProducts.OrderBy(op => op.Product.Name))
                {
                    model.OrderProducts.Add(new OrderProductViewModel(orderProduct));
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableCustomers = GetAvailableCustomers(model.CustomerId);

                return View(model);
            }

            Order order = await context.Orders.FindAsync(model.Id);

            if (order == null)
            {
                return HttpNotFound();
            }

            order.Name = model.Name;
            order.CustomerId = model.CustomerId;

            context.Entry(order).State = EntityState.Modified;

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

            return RedirectToAction(nameof(Details), new { id = order.Id });
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = await context.Orders.FindAsync(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            OrderViewModel model = new OrderViewModel(order);

            return View(model);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(OrderViewModel model)
        {
            Order order = await context.Orders.FindAsync(model.Id);

            if (order == null)
            {
                return HttpNotFound();
            }

            context.Orders.Remove(order);

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

        public async Task<ActionResult> AddProductToOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = await context.Orders
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            AddProductToOrderViewModel model = new AddProductToOrderViewModel();

            model.Order = new OrderViewModel(order);
            model.AvailableProducts = GetAvailableProducts(order);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddProductToOrder(AddProductToOrderViewModel model)
        {
            Order order = await context.Orders.FindAsync(model.Order.Id);

            if (order == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                model.AvailableProducts = GetAvailableProducts(order, model.ProductId);

                return View(model);
            }

            Product product = await context.Products.FindAsync(model.ProductId);

            if (product == null)
            {
                return HttpNotFound();
            }

            context.OrderProducts.Add(new OrderProduct
            {
                Order = order,
                Product = product,
                Price = model.Price
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

            return RedirectToAction(nameof(Details), new { id = order.Id });
        }

        public async Task<ActionResult> DeleteProductFromOrder(int? orderId, int? productId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = await context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return HttpNotFound();
            }

            Product product = await context.Products.FindAsync(productId);

            if (product == null)
            {
                return HttpNotFound();
            }

            DeleteProductFromOrderViewModel model = new DeleteProductFromOrderViewModel(order, product);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteProductFromOrder(DeleteProductFromOrderViewModel model)
        {
            OrderProduct orderProduct = await context.OrderProducts
                .FindAsync(model.Order.Id, model.Product.Id);

            if (orderProduct == null)
            {
                return HttpNotFound();
            }

            context.OrderProducts.Remove(orderProduct);

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

            return RedirectToAction(nameof(Details), new { id = orderProduct.OrderId });
        }

        private List<SelectListItem> GetAvailableCustomers(int? customerId = null)
        {
            List<SelectListItem> availableCustomers = new List<SelectListItem>();

            if (context.Customers.Any())
            {
                foreach (var customer in context.Customers)
                {
                    availableCustomers.Add(new SelectListItem
                    {
                        Value = customer.Id.ToString(),
                        Text = customer.Name,
                        Selected = customer.Id == customerId
                    });
                }
            }

            return availableCustomers;
        }

        private List<SelectListItem> GetAvailableProducts(Order order, int? productId = null)
        {
            List<SelectListItem> availableProducts = new List<SelectListItem>();

            if (context.Products.Any())
            {
                foreach (var product in context.Products.Include(p => p.OrderProducts))
                {
                    OrderProduct orderProduct = order.OrderProducts
                        .FirstOrDefault(op => op.OrderId == order.Id
                        && op.ProductId == product.Id);

                    if (orderProduct == null)
                    {
                        availableProducts.Add(new SelectListItem
                        {
                            Value = product.Id.ToString(),
                            Text = product.Name,
                            Selected = product.Id == productId
                        });
                    }
                }
            }

            return availableProducts;
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