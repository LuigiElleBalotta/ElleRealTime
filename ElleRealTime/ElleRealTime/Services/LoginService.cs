using System.Threading.Tasks;
using ElleRealTime.Core.BO;
using ElleRealTimeStd.Shared.Test.Interfaces.Service;
using MagicOnion.Server.Hubs;

namespace ElleRealTime.Services
{
    public class LoginService : StreamingHubBase<ILoginService, ILoginServiceReceiver>, ILoginService
    {
        public async Task LeaveAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> JoinAsync(string username, string password)
        {
            Program.Logger.Info($"Received {username} & {password}");
            var bo = new Login();
            int accountId = bo.CheckLogin(username, password);
            return accountId;
        }
    }
}
