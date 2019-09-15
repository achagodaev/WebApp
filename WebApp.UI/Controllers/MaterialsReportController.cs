using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq.Expressions;
using WebApp.UI.Models;
using WebApp.UI.ViewModels.ReportViewModels;
using System.Diagnostics;
using WebApp.UI.Repositories;

namespace WebApp.UI.Controllers
{
    public class MaterialsReportController : Controller
    {
        private WebAppDbContext context = new WebAppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Materials(int materialId = 0, string sortColumn = "name")
        {
            MaterialsReportViewModel model = new MaterialsReportViewModel();

            model.SelectedMaterialId = materialId;
            model.SelectedSortColumn = sortColumn;

            IQueryable<OrderProductMaterial> orderProductMaterials = context.OrderProductMaterials
                .Include(opm => opm.OrderProduct.Order)
                .Include(opm => opm.OrderProduct.Product)
                .Include(opm => opm.OrderProduct.OrderProductAddresses)
                .Include(opm => opm.OrderProduct.OrderProductAddresses.Select(opa => opa.OrderProductAddressSizes))
                .Include(opm => opm.Material)
                .Include(opm => opm.Supplier);

            if (materialId != 0)
            {
                orderProductMaterials = orderProductMaterials.Where(opm => opm.MaterialId == materialId);

                if (orderProductMaterials.Any())
                {
                    foreach (var orderProductMaterial in orderProductMaterials)
                    {
                        model.OrderProductMaterials.Add(orderProductMaterial);
                    }
                }
            }

            if (context.Orders.Any())
            {
                foreach (var order in context.Orders)
                {
                    model.Orders.Add(order.Id, order.Name);
                }
            }

            if (context.Products.Any())
            {
                foreach (var product in context.Products)
                {
                    model.Products.Add(product.Id, product.Name);
                }
            }

            if (context.Materials.Any())
            {
                foreach (var material in context.Materials)
                {
                    model.Materials.Add(material.Id, material.Name);
                }
            }

            return PartialView(model);
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