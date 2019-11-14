using System.Data.Common;
using MagicOnion;

namespace ElleRealTimeBaseDAO.Interfaces
{
    public interface ILogin : ITransactions
    {
        UnaryResult<int> CheckLogin(string username, string hashedPassword, DbTransaction trans);
    }
}
