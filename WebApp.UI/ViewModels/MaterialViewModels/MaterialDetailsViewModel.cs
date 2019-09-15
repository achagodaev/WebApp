using WebApp.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.UI.ViewModels.MaterialViewModels
{
    public class MaterialDetailsViewModel : MaterialViewModel
    {
        public MaterialDetailsViewModel(Material material)
            : base(material)
        {

        }
    }
}