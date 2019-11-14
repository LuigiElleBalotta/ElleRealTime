using System.Data.Common;
using ElleRealTimeBaseDAO;
using ElleRealTimeBaseDAO.Interfaces;
using MagicOnion;

namespace ElleRealTime.SqlServer
{
    public class Login : ElleRealTimeDbDAO, ILogin
    {
        public UnaryResult<int> CheckLogin(string username, string hashedPassword, DbTransaction trans)
        {
            return ElleRealTimeBaseDAO.Base.Login.CheckLogin(this, username, hashedPassword, trans);
        }
    }
}
