using MagicOnion;

namespace ElleRealTimeStd.Shared.Test.Interfaces.Service
{
    public interface ILoginService : IService<ILoginService>
    {
        UnaryResult<int> CheckLogin(string username, string password);
    }
}
