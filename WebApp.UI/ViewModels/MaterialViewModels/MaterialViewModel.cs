using WebApp.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.UI.ViewModels.MaterialViewModels
{
    public class MaterialViewModel
    {
        public MaterialViewModel()
        {

        }

        public MaterialViewModel(Material material)
        {
            Id = material.Id;
            Name = material.Name;
        }

        [Display(Name = "Идентификатор")]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }
}