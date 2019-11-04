using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ElleRealTime.Shared.Test.Entities.StreamingHub.Player;
using MagicOnion;

namespace ElleRealTime.Shared.Test.Interfaces.StreamingHub
{
    public interface IGamingHub : IStreamingHub<IGamingHub, IGamingHubReceiver>
    {
        Task<Player[]> JoinAsync(string roomName, string userName, Vector3 position, Quaternion rotation);
        Task LeaveAsync();
        Task MoveAsync(Vector3 position, Quaternion rotation);
    }
}
