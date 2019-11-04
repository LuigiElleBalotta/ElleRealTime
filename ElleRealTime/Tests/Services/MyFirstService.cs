using System;
using System.Collections.Generic;
using System.Text;
using ElleRealTime.Shared.Test;
using ElleRealTime.Shared.Test.Interfaces;
using ElleRealTime.Shared.Test.Interfaces.Service;
using MagicOnion;
using MagicOnion.Server;

namespace ElleRealTime.Tests.Services
{
    public class MyFirstService : ServiceBase<IMyFirstService>, IMyFirstService
    {
        public async UnaryResult<int> SumAsync(int x, int y)
        {
            Logger.Debug($"Received: {x}, {y}");

            return x + y;
        }
    }
}
