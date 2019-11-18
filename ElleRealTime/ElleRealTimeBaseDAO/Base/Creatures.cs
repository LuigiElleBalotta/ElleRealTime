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
            return "SELECT * FROM creatures";
        }

        public static void InsertSpawnCreature(ElleRealTimeDbDAO dao, string prefabName, Player player, DbTransaction trans)
        {
            Hashtable prms = new Hashtable
            {
                { $"@{nameof(Creature.PrefabName)}", prefabName },
                { $"@{nameof(Creature.PosX)}", player.Position.x },
                { $"@{nameof(Creature.PosY)}", player.Position.y },
                { $"@{nameof(Creature.PosZ)}", player.Position.z },
                { $"@{nameof(Creature.RotX)}", player.Rotation.x },
                { $"@{nameof(Creature.RotY)}", player.Rotation.y },
                { $"@{nameof(Creature.RotZ)}", player.Rotation.z },
            };

            dao.ExecuteNonQuery("INSERT INTO creatures(PrefabName, PosX, PosY, PosZ, RotX, RotY, RotZ) VALUES ( " +
                                $" @{nameof(Creature.PrefabName)}, " +
                                $" @{nameof(Creature.PosX)}, " +
                                $" @{nameof(Creature.PosY)}, " +
                                $" @{nameof(Creature.PosZ)}, " +
                                $" @{nameof(Creature.RotX)}, " +
                                $" @{nameof(Creature.RotY)}, " +
                                $" @{nameof(Creature.RotZ)} " +
                                $") ", prms, trans);
        }
    }
}
