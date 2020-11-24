using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViewHomeWork.ViewModel
{
    public class BasketVM
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        public double Price { get; set; }
        [Required, StringLength(100)]
        public string ImageName { get; set; }
        public int Count{ get; set; }
        public int DbCount { get; set; }
        public string Username { get; set; }

    }
}
