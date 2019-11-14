using System;
using System.Collections.Generic;
using System.Text;
using ElleFramework.Database;
using ElleRealTimeBaseDAO.Interfaces;
using MagicOnion;

namespace ElleRealTime.Core.BO
{
    public class Login
    {
        /// <summary>
        /// Returns the ID of the account if login has success.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int CheckLogin(string username, string password)
        {
            ILogin dao = DAOFactory.Create<ILogin>();

            var hashedPassword = Shared.BO.Utils.GenerateHashPassword(username, password);
            return dao.CheckLogin(username, hashedPassword, null);
        }

        public static int CreateAccount(string username, string password)
        {
            ILogin dao = DAOFactory.Create<ILogin>();
            string hashedPassword = Shared.BO.Utils.GenerateHashPassword(username, password);
            return dao.CreateAccount(username, hashedPassword, null);
        }
    }
}
