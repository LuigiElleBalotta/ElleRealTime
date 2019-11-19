using System;
using System.Collections.Generic;
using System.Text;
using ElleFramework.Database.MVC;
using ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures;

namespace ElleRealTime.Shared.DBEntities.Creatures
{
    public class CreatureTemplate : View
    {
        public int ID { get; set; }
        public string PrefabName { get; set; }
        public string Name { get; set; }

        public Creature[] Creatures { get; set; }

        public static CreatureTemplateUnity[] ToUnity(CreatureTemplate[] creaturesTemplate)
        {
            List<CreatureTemplateUnity> ret = new List<CreatureTemplateUnity>();
            foreach (CreatureTemplate creatureTemplate in creaturesTemplate)
            {
                CreatureTemplateUnity c = new CreatureTemplateUnity
                {
                    ID = creatureTemplate.ID,
                    Name = creatureTemplate.Name,
                    PrefabName = creatureTemplate.PrefabName
                };

                if (creatureTemplate.Creatures != null && creatureTemplate.Creatures.Length > 0)
                {
                    c.Creatures = Creature.ToUnity(creatureTemplate.Creatures);
                }

                ret.Add(c);
            }

            return ret.ToArray();
        }
    }
}
