﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using ElleFramework.Database;
using ElleRealTime.Shared.DBEntities.Accounts;
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
            int idaccount = -1;
            idaccount = dao.CheckLogin(username, hashedPassword, null);
            return idaccount;
        }

        public static int CreateAccount(string username, string password)
        {
            ILogin dao = DAOFactory.Create<ILogin>();
            string hashedPassword = Shared.BO.Utils.GenerateHashPassword(username, password);
            return dao.CreateAccount(username, hashedPassword, null);
        }

        public static void ModifyPassword(string username, string password)
        {
            ILogin dao = DAOFactory.Create<ILogin>();
            string hashedPassword = Shared.BO.Utils.GenerateHashPassword(username, password);
            dao.ModifyPassword(username, hashedPassword, null);
        }

        public Account[] GetAccountsInfo(AccountsFilter filter)
        {
            return GetAccountsInfo(filter, null);
        }

        internal Account[] GetAccountsInfo(AccountsFilter filter, DbTransaction trans)
        {
            ILogin dao = DAOFactory.Create<ILogin>();
            return dao.GetAccountsInfo(filter, trans);
        }
    }
}
