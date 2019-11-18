using System.Threading.Tasks;
using ElleRealTime.Core.BO;
using ElleRealTimeStd.Shared.Test.Interfaces.Service;
using MagicOnion.Server.Hubs;

namespace ElleRealTime.Services
{
    public class LoginService : StreamingHubBase<ILoginService, ILoginServiceReceiver>, ILoginService
    {
        IGroup room;

        public async Task LeaveAsync()
        {
            await room.RemoveAsync(this.Context);
        }

        public async Task<int> JoinAsync(string roomName, string username, string password)
        {
            Program.Logger.Info($"[LoginService] {username} has requested login.");

            (room) = await Group.AddAsync(roomName);

            var bo = new Login();
            int accountId = bo.CheckLogin(username, password);

            if (accountId > 0)
            {
                Program.Logger.Success($"[LoginService] {username} logged successfully with ID: {accountId}");
                //Broadcast(room).OnJoin(accountId);
            }
            else
                Program.Logger.Error($"[LoginService] {username} has sent wrong credentials!", false);

            return accountId;
        }
    }
}
