using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViewHomeWork.ViewModel
{
    public class UserVm
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool IsActivated { get; set; }
        public string Role { get; set; }
        public List<string>Roles { get; set; }
    }
}
