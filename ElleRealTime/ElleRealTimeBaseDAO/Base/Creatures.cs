using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using ElleRealTime.Shared.DBEntities.Creatures;
using ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player;

namespace ElleRealTimeBaseDAO.Base
{
    public static class Creatures
    {
        public static string GetBaseQueryCreatures()
        {
            return "SELECT C.*, CT.PrefabName " +
                   "FROM creatures C " +
                   "JOIN creatures_template CT ON CT.ID = C.CreatureID ";
        }

        public static void InsertSpawnCreature(ElleRealTimeDbDAO dao, int creatureId, Player player, DbTransaction trans)
        {
            Hashtable prms = new Hashtable
            {
                { $"@{nameof(Creature.CreatureID)}", creatureId },
                { $"@{nameof(Creature.PosX)}", player.Position.x },
                { $"@{nameof(Creature.PosY)}", player.Position.y },
                { $"@{nameof(Creature.PosZ)}", player.Position.z },
                { $"@{nameof(Creature.RotX)}", player.Rotation.x },
                { $"@{nameof(Creature.RotY)}", player.Rotation.y },
                { $"@{nameof(Creature.RotZ)}", player.Rotation.z },
            };

            dao.ExecuteNonQuery("INSERT INTO creatures(CreatureID, PosX, PosY, PosZ, RotX, RotY, RotZ) VALUES ( " +
                                $" @{nameof(Creature.CreatureID)}, " +
                                $" @{nameof(Creature.PosX)}, " +
                                $" @{nameof(Creature.PosY)}, " +
                                $" @{nameof(Creature.PosZ)}, " +
                                $" @{nameof(Creature.RotX)}, " +
                                $" @{nameof(Creature.RotY)}, " +
                                $" @{nameof(Creature.RotZ)} " +
                                $") ", prms, trans);
        }

        public static string GetBaseQueryCreatureTemplate(CreatureTemplateFilter filter, Hashtable prms)
        {
            return "SELECT * FROM creatures_template CT " +
                   filter.WhereCondition(prms);
        }
    }
}
