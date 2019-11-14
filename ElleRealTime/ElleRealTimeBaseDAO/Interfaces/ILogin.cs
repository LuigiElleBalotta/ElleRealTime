using System.Data.Common;
using MagicOnion;

namespace ElleRealTimeBaseDAO.Interfaces
{
    public interface ILogin : ITransactions
    {
        int CheckLogin(string username, string hashedPassword, DbTransaction trans);
    }
}
