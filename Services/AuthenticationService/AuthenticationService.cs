using AppContext.Models;
using Microsoft.AspNet.Identity;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository repository;
        private readonly IPasswordHasher passwordHasher;

        public AuthenticationService(IRepository repository, IPasswordHasher passwordHasher)
        {
            this.repository = repository;
            this.passwordHasher = passwordHasher;
        }

        public User Login(string username, string password)
        {
            User selectedUser = repository.GetUserByName(username);

            if(selectedUser != null)
            {
                PasswordVerificationResult passwordResult = passwordHasher.VerifyHashedPassword(selectedUser.Password, password);
                if (passwordResult != PasswordVerificationResult.Success)
                    selectedUser = null;
            }
            return selectedUser;
        }

        public bool Register(string username, string password, string confirmPassword)
        {
            bool IsRegistred = false;

            if (password.Equals(confirmPassword))
            {
                User newUser = new User()
                {
                    Username = username,
                    Password = passwordHasher.HashPassword(password),
                    CreationDateTime = DateTime.Now
                };
                repository.CreateUser(newUser);
            }
            return IsRegistred;
        }
    }
}
