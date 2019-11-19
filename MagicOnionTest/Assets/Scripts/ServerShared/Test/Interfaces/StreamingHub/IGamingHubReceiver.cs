using ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures;
using ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player;

namespace ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub
{
    public interface IGamingHubReceiver
    {
        void OnJoin(Player player);
        void OnLeave(Player player);
        void OnMove(Player player);
        void OnAnimStateChange(int playerId, int state);
        void OnPlayerInfoSaved();
        void OnQueriedCreatures(CreatureUnity[] creatures);
        void OnChatMessage(string playerName, string text);
    }
}
