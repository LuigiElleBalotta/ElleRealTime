using System.Collections;
using System.Collections.Generic;
using ElleFramework.Database.MVC;
using UnityEngine;

namespace ElleRealTimeStd.Shared.Entities.Accounts
{
    public class Account : View
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
