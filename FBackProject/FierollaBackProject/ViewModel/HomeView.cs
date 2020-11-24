using PartialViewHomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViewHomeWork.ViewModel
{
    public class HomeView
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Slider> sliders { get; set; }
        //public IEnumerable<Prodsliuct> Products { get; set; }
    }
}
