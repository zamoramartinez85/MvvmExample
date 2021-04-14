using System;
using System.Collections.Generic;
using System.Text;

namespace AppContext.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string ErrorThrownAt { get; set; }
        public string StackErrorMessage { get; set; }
        public DateTime DateTime { get; set; }
    }
}
