using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElleRealTime.Core.BO;
using ElleRealTime.Core.BO.World;
using ElleRealTime.Shared;
using ElleRealTime.Shared.DBEntities.Accounts;
using ElleRealTime.Shared.DBEntities.Creatures;
using ElleRealTime.Shared.DBEntities.PlayersInfo;
using ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures;
using ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player;
using ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub;
using MagicOnion.Server.Hubs;
using UnityEngine;

namespace ElleRealTime.Tests.Services
{
    public class GamingHub : StreamingHubBase<IGamingHub, IGamingHubReceiver>, IGamingHub
    {
        private static Vector3 DefaultVector3 = new Vector3(11.13949f, 4.719501f, -116.671f);
        private static Quaternion DefaultQuaternion = new Quaternion(0, 0, 0, 0);

        // this class is instantiated per connected so fields are cache area of connection.
        IGroup room;
        Player self;
        IInMemoryStorage<Player> storage;
        private static List<Player> players = new List<Player>();
        private GamingHub _instance;
        public static GamingHub Instance;

        public GamingHub()
        {
            Instance = this;
        }

        public static Player[] GetOnlinePlayers()
        {
            return players.ToArray();
        }

        public async Task<Player[]> JoinAsync(string roomName, int accountId)
        {
            var bo = new Players();
            var result = bo.GetPlayerInfo(new PlayersInfoFilter {AccountID = accountId});
            string accountName = "";
            if (result != null && result.Length == 1)
            {
                PlayerInfo playerInfo = result[0];
                accountName = playerInfo.Username;
                self = new Player
                {
                    ID = playerInfo.AccountID,
                    Name = playerInfo.Username,
                    Position = new Vector3(playerInfo.PosX, playerInfo.PosY, playerInfo.PosZ),
                    Rotation = new Quaternion(playerInfo.RotX, playerInfo.RotY, playerInfo.RotZ, 0)
                };
            }
            else
            {
                AccountsFilter filter = new AccountsFilter {ID = accountId};
                Account account = new Login().GetAccountsInfo(filter)[0];
                accountName = account.Username;
                self = new Player
                {
                    ID = account.ID,
                    Name = account.Username,
                    Position = DefaultVector3,
                    Rotation = DefaultQuaternion
                };
            }

            (room, storage) = await Group.AddAsync(roomName, self);

            //Send to already connected players that a new player has joined.
            Broadcast(room).OnJoin(self);

            Program.Logger.Info($"[GamingHub] \"{accountName}\" joined the room: \"{roomName}\"");
            players.Add(self);
            return storage.AllValues.ToArray();
        }

        public async Task LeaveAsync()
        {
            players.Remove(players.Where(x => x.ID == self.ID).ToArray()[0]);
            await room.RemoveAsync(this.Context);
            Program.Logger.Info($"[GamingHub] \"{self.Name}\" leaves the room.");
            Broadcast(room).OnLeave(self);
        }

        public async Task MoveAsync(Vector3 position, Quaternion rotation)
        {
            self.Position = position;
            self.Rotation = rotation;

            //Program.Logger.Info($"=================================={DateTime.Now.ToString(Constants.DATETIME_FORMAT)}==============================");
            Program.Logger.Info($"[GamingHub] \"{self.Name}\" is moving!");
            //Program.Logger.Info($"Vector3: x={position.x}, y={position.y}, z={position.z} ");
            //Program.Logger.Info($"Quaternion: x={rotation.x}, y={rotation.y}, z={rotation.z}, w={rotation.w}");
           // Program.Logger.Info($"===================================================================================");

            Broadcast(room).OnMove(self);
        }

        public async Task SendAnimStateAsync(int state)
        {
            Broadcast(room).OnAnimStateChange(self.ID, state);
        }

        public async Task SavePlayerAsync()
        {
            Program.Logger.Info($"[GamingHub] Player \"{self.Name}\" requested to save his info.");
            var bo = new Players();

            PlayerInfo playerInfo = new PlayerInfo
            {
                PosX = self.Position.x,
                PosY = self.Position.y,
                PosZ = self.Position.z,

                RotX = self.Rotation.x,
                RotY = self.Rotation.y,
                RotZ = self.Rotation.z,

                AccountID = self.ID
            };

            bo.SavePlayerInfo(playerInfo);
            Program.Logger.Success($"[GamingHub] Saved player(\"{self.Name}\") info.");

            BroadcastToSelf(room).OnPlayerInfoSaved();
        }

        public async Task QueryCreaturesAsync()
        {
            Program.Logger.Info($"[GamingHub] Player \"{self.Name}\" queried creatures.");
            var bo = new Creatures();

            Creature[] creatures = bo.GetCreatures();
            CreatureUnity[] ret = Creature.ToUnity(creatures);
            Broadcast(room).OnQueriedCreatures(ret);
        }
    }
}
