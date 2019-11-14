using System;
using System.Collections.Generic;
using System.Text;
using ElleRealTime.Core.BO;
using ElleRealTimeStd.Shared.Test.Interfaces.Service;
using MagicOnion;
using MagicOnion.Server;

namespace ElleRealTime.Services
{
    public class LoginService : ServiceBase<ILoginService>, ILoginService
    {
        public async UnaryResult<int> CheckLogin(string username, string password)
        {
            return await new Login().CheckLogin(username, password);
        }
    }
}
