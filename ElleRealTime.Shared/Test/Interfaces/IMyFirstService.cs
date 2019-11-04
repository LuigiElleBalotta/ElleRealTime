using MagicOnion;

namespace ElleRealTime.Shared.Test.Interfaces
{
    public interface IMyFirstService : IService<IMyFirstService>
    {
        UnaryResult<int> SumAsync(int x, int y);
    }
}
