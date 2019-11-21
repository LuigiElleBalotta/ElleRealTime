using MessagePack;
using UnityEngine;

namespace ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player
{
    [MessagePackObject]
    public class Player
    {
        [Key(0)]
        public string Name { get; set; }
        [Key(1)]
        public Vector3 Position { get; set; }
        [Key(2)]
        public Quaternion Rotation { get; set; }
        [Key(3)]
        public int ID { get; set; }
        [Key(4)]
        public int Health { get; set; }
        [Key(5)]
        public int MaxHealth { get; set; }
        [Key(6)]
        public int Damage { get; set; }
        [Key(7)]
        public int Level { get; set; }
        [Key(8)]
        public int Experience { get; set; }
        [Key(9)]
        public int ExpToNextLevel { get; set; }
    }
}
