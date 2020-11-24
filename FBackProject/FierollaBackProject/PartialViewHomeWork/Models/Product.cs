using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViewHomeWork.Models
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        public double Price { get; set; }
        [Required, StringLength(100)]
        public string ImageName { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
