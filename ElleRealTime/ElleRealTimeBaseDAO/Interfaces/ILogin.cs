using System.Data.Common;
using ElleRealTime.Shared.DBEntities.Accounts;
using MagicOnion;

namespace ElleRealTimeBaseDAO.Interfaces
{
    public interface ILogin : ITransactions
    {
        int CheckLogin(string username, string hashedPassword, DbTransaction trans);
        int CreateAccount(string username, string hashedPassword, DbTransaction trans);
        void ModifyPassword(string username, string hashedPassword, DbTransaction trans);
        Account[] GetAccountsInfo(AccountsFilter filter, DbTransaction trans);
    }
}
