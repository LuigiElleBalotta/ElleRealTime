﻿using System.Collections;
using System.Data.Common;
using ElleRealTime.Shared.DBEntities.Accounts;
using ElleRealTimeBaseDAO;
using ElleRealTimeBaseDAO.Interfaces;
using MagicOnion;

namespace ElleRealTime.SqlServer
{
    public class Login : ElleRealTimeDbDAO, ILogin
    {
        public int CheckLogin(string username, string hashedPassword, DbTransaction trans)
        {
            return ElleRealTimeBaseDAO.Base.Login.CheckLogin(this, username, hashedPassword, trans);
        }

        public int CreateAccount(string username, string hashedPassword, DbTransaction trans)
        {
            Hashtable prms = new Hashtable
            {
                { $"@{nameof(Account.Username)}", username },
                { $"@{nameof(Account.Password)}", hashedPassword },
            };
            int id = ExecuteScalar<int>("INSERT INTO accounts( Username, Password ) VALUES ( " +
                                                   $" @{nameof(Account.Username)}, " +
                                                   $" @{nameof(Account.Password)} " +
                                                   "); " +
                                                   "SELECT SCOPE_IDENTITY();", prms, trans);

            return id;

        }

        public void ModifyPassword(string username, string hashedPassword, DbTransaction trans)
        {
            ElleRealTimeBaseDAO.Base.Login.ModifyPassword(this, username, hashedPassword, trans);
        }

        public Account[] GetAccountsInfo(AccountsFilter filter, DbTransaction trans)
        {
            Hashtable prms = new Hashtable
            {
                { "@FirstRow", filter.FirstRow },
                { "@LastRow", filter.LastRow },
            };

            return ExecuteViewArray<Account>("SELECT * " +
                                                "FROM ( " +
                                                "    SELECT MZ.*, " +
                                                "			 ROW_NUMBER() OVER (" + filter.OrderByCondition() + ") AS rn " +
                                                "    FROM ( " + ElleRealTimeBaseDAO.Base.Login.GetBaseQueryAccounts(filter, prms) + " ) MZ " +
                                                ") NumMZ " +
                                                "WHERE rn BETWEEN @FirstRow AND @LastRow",
                prms, trans);
        }
    }
}
