using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViewHomeWork.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string AppUserId { get; set; }
        public double Total { get; set; }
        public virtual AppUser AppUser { get; set; }
        public ICollection<SalesProduct> SaleProducts { get; set; }
    }
}
