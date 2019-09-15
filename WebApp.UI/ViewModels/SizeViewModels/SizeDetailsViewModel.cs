using WebApp.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.UI.ViewModels.SizeViewModels
{
    public class SizeDetailsViewModel : SizeViewModel
    {
        public SizeDetailsViewModel(Size size)
            : base(size)
        {

        }
    }
}