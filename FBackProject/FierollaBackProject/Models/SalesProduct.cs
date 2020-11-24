using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViewHomeWork.Models
{
    public class SalesProduct
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public int ProductId{ get; set; }
        public int SalesId { get; set; }
        public virtual Product Product{ get; set; }
        public virtual Sales Sale { get; set; }
       
    }
}
