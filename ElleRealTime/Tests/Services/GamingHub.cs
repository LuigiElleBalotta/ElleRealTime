using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player;
using ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub;
using MagicOnion.Server.Hubs;

namespace ElleRealTime.Tests.Services
{
    public class GamingHub : StreamingHubBase<IGamingHub, IGamingHubReceiver>, IGamingHub
    {
        // this class is instantiated per connected so fields are cache area of connection.
        IGroup room;
        Player self;
        IInMemoryStorage<Player> storage;

        public async Task<Player[]> JoinAsync(string roomName, string userName, Vector3 position, Quaternion rotation)
        {
            self = new Player
            {
                Name = userName,
                Position = position,
                Rotation = rotation
            };

            (room, storage) = await Group.AddAsync(roomName, self);

            Broadcast(room).OnJoin(self);

            return storage.AllValues.ToArray();
        }

        public async Task LeaveAsync()
        {
            await room.RemoveAsync(this.Context);
            Broadcast(room).OnLeave(self);
        }

        public async Task MoveAsync(Vector3 position, Quaternion rotation)
        {
            self.Position = position;
            self.Rotation = rotation;

            Broadcast(room).OnMove(self);
        }

    }
}
