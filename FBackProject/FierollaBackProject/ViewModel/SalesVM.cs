using PartialViewHomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViewHomeWork.ViewModel
{
    public class SalesVM
    {
        public int Id{ get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public List<SalesProduct>SaleProducts { get; set; }

    }
}
