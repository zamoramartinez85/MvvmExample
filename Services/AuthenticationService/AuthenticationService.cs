using AppContext.Models;
using Microsoft.AspNetCore.Identity;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository repository;
        private readonly IPasswordHasher<User> passwordHasher;

        public AuthenticationService(IRepository repository, IPasswordHasher<User> passwordHasher)
        {
            this.repository = repository;
            this.passwordHasher = passwordHasher;
        }

        public User Login(string username, string password)
        {
            User selectedUser = repository.GetUserByName(username);

            if (selectedUser != null)
            {
                PasswordVerificationResult passwordResult = passwordHasher.VerifyHashedPassword(selectedUser, selectedUser.Password, password);
                if (passwordResult != PasswordVerificationResult.Success)
                    selectedUser = null;
            }

            return selectedUser;
        }

        public bool Register(string username, string password, string confirmPassword)
        {
            bool isRegistred = false;

            if (password.Equals(confirmPassword))
            {
                User newUser = new User()
                {
                    Username = username,                    
                    CreationDateTime = DateTime.Now
                };

                newUser.Password = passwordHasher.HashPassword(newUser, password);
                repository.CreateUser(newUser);
            }

            return isRegistred;
        }
    }
}
