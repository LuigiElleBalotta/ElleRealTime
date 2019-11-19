using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures
{
    [MessagePackObject]
    public class CreatureTemplateUnity
    {
        [Key(0)]
        public int ID { get; set; }
        [Key(1)]
        public string PrefabName { get; set; }
        [Key(2)]
        public string Name { get; set; }
        [Key(3)]
        public CreatureUnity[] Creatures { get; set; }
    }
}
