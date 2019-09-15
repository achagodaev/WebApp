using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.UI.Controllers
{
    public class FilesController : Controller
    {
        public FileContentResult GetImage(string path)
        {
            if (System.IO.File.Exists(path))
            {
                return File(System.IO.File.ReadAllBytes(path), "application/octet-stream");
            }
            else
            {
                return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}