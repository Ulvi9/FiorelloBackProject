using PartialViewHomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViewHomeWork.ProductViewModel
{
    public class ProductModel
    {
        public int Id { get; set; }     
        public string Title { get; set; }
        public double Price { get; set; }       
        public string ImageName { get; set; }      
        public  Category Category { get; set; }
    }
}
