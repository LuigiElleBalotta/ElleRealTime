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
        public string PrefabName { get; set; }
        [Key(1)]
        public Vector3 Position { get; set; }
        [Key(2)]
        public Quaternion Rotation { get; set; }
        [Key(3)]
        public int ID { get; set; }
        [Key(4)]
        public int Guid { get; set; }
    }
}

