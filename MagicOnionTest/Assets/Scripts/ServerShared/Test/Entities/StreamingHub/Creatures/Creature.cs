using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures
{
    [MessagePackObject]
    public class CreatureUnity
    {
        [Key(0)]
        public Vector3 Position { get; set; }
        [Key(1)]
        public Quaternion Rotation { get; set; }
        [Key(2)]
        public int CreatureID { get; set; }
        [Key(3)]
        public int Guid { get; set; }
        [Key(4)]
        public string PrefabName { get; set; }
    }
}

