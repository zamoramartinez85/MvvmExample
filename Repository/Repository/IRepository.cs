using AppContext.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public interface IRepository
    {
        #region User

        User CreateUser(User user);
        bool DeleteUser(User user);
        User GetUserByName(string username);
        User GetUserById(int id);
        List<User> GetUsersList();
        User UpdateUser(User user);
        #endregion

        #region ErrorLog
        bool InsertErrorLog(ErrorLog errorLog);
        #endregion

    }
}
