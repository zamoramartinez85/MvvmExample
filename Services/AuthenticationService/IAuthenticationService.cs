using AppContext.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        User Login(string username, string password);
        bool Register(string username, string password, string confirmPassword);
    }
}
