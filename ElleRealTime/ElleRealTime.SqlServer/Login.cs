using System.Collections;
using System.Data.Common;
using ElleRealTimeBaseDAO;
using ElleRealTimeBaseDAO.Interfaces;
using ElleRealTimeStd.Shared.Entities.Accounts;
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
    }
}
