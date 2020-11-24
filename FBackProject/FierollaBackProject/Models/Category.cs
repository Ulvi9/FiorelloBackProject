using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViewHomeWork.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="bosh qoyma"), MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "bosh qoyma"), MaxLength(200)]
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
