using System;
using System.Collections.Generic;
using System.Text;
using ElleFramework.Database.MVC;
using ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures;
using UnityEngine;

namespace ElleRealTime.Shared.DBEntities.Creatures
{
    public class Creature : View
    {
        public int ID { get; set; }
        public int Guid { get; set; }
        public string PrefabName { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float RotX { get; set; }
        public float RotY { get; set; }
        public float RotZ { get; set; }

        public static CreatureUnity[] ToUnity(Creature[] creatures)
        {
            List<CreatureUnity> ret = new List<CreatureUnity>();
            foreach (Creature creature in creatures)
            {
                CreatureUnity c = new CreatureUnity
                {
                    ID = creature.ID,
                    Guid = creature.Guid,
                    PrefabName = creature.PrefabName,
                    Position = new Vector3( creature.PosX, creature.PosY, creature.PosZ ),
                    Rotation = new Quaternion( creature.RotX, creature.RotY, creature.RotZ, 0)
                };
                ret.Add(c);
            }

            return ret.ToArray();
        }
    }
}
