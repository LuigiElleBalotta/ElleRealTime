using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using MagicOnion;

namespace ElleRealTimeBaseDAO.Base
{
    public static class Login
    {
        public static int CheckLogin(ElleRealTimeDbDAO dao, string username, string hashedPassword, DbTransaction trans)
        {
            Hashtable prms = new Hashtable
            {
                { "@Username", username },
                { "@Password", hashedPassword }
            };

            //return dao.ExecuteScalar<int>("SELECT ID FROM users WHERE Username = @Username AND Password = @Password", prms, trans);
            return 1;
        }
    }
}
