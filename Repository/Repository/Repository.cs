using AppContext.DAL;
using AppContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repository
{
    public class Repository : IRepository
    {
        private readonly Context context;
        private readonly ILogger logger;

        public Repository(Context context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
        }

        #region User
        public User CreateUser(User user)
        {
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception at Repository.CreateUser: {ex}");
                InsertErrorLog(new ErrorLog()
                {
                    DateTime = DateTime.Now,
                    StackErrorMessage = ex.ToString(),
                    ErrorThrownAt = "Repository.CreateUser"
                });
                user = null;
            }
            return user;
        }

        public bool DeleteUser(User user)
        {
            bool isDeleted = false;

            try
            {
                context.Users.Remove(user);
                context.SaveChanges();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception at Repository.DeleteUser: {ex}");
                InsertErrorLog(new ErrorLog()
                {
                    DateTime = DateTime.Now,
                    StackErrorMessage = ex.ToString(),
                    ErrorThrownAt = "Repository.DeleteUser"
                });
            } 
            return isDeleted;
        }

        public User GetUserById(int id)
        {
            return context.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public User GetUserByName(string username)
        {
            return context.Users.Where(x => x.Username == username).FirstOrDefault();
        }

        public List<User> GetUsersList()
        {
            return context.Users.ToList();
        }

        public User UpdateUser(User user)
        {
            try
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception at Repository.UpdateUser: {ex}");
                InsertErrorLog(new ErrorLog()
                {
                    DateTime = DateTime.Now,
                    StackErrorMessage = ex.ToString(),
                    ErrorThrownAt = "Repository.UpdateUser"
                });
                user = null;
            }
            return user;
        }
        #endregion

        #region ErrorLog
        public bool InsertErrorLog(ErrorLog errorLog)
        {
            bool isInserted = false;

            try
            {
                context.ErrorLogs.Add(errorLog);
                context.SaveChanges();
                isInserted = true;
            }
            catch(Exception ex)
            {
                logger.LogError($"Exception at Repository.InsertErrorLog: {ex}");
            }
            return isInserted;
        }
        #endregion
    }
}
