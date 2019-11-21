using System;
using System.Collections.Generic;
using System.Text;
using ElleFramework.Database.MVC;

namespace ElleRealTime.Shared.DBEntities.PlayersInfo
{
    public class PlayerInfo : View
    {
        public int AccountID { get; set; }
        public string Username { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float RotX { get; set; }
        public float RotY { get; set; }
        public float RotZ { get; set; }
        public float RotW { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int ExpToNextLevel { get; set; }
    }
}
