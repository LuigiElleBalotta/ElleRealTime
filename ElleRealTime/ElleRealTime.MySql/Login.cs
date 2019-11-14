﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using ElleRealTimeBaseDAO;
using ElleRealTimeBaseDAO.Interfaces;
using ElleRealTimeStd.Shared.Entities.Accounts;

namespace ElleRealTime.MySql
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
                                        "SELECT LAST_INSERT_ID();", prms, trans);

            return id;

        }
    }
}
