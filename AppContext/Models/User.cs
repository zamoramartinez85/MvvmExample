using System;
using System.Collections.Generic;
using System.Text;

namespace AppContext.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreationDateTime { get; set; }
        public List<Role> Roles { get; set; }
    }
}
