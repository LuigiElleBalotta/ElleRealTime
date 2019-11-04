using ElleRealTime.Shared.Test.Entities.StreamingHub.Player;

namespace ElleRealTime.Shared.Test.Interfaces.StreamingHub
{
    public interface IGamingHubReceiver
    {
        void OnJoin(Player player);
        void OnLeave(Player player);
        void OnMove(Player player);
    }
}
