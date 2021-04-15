using System;
using System.Collections.Generic;
using System.Text;

namespace AppContext.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Role> Roles { get; set; }
    }
}
