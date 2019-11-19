using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using ElleRealTime.Shared.DBEntities.Creatures;
using ElleRealTimeBaseDAO;
using ElleRealTimeBaseDAO.Interfaces;
using ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player;

namespace ElleRealTime.SqlServer
{
    public class Creatures : ElleRealTimeDbDAO, ICreatures
    {
        public Creature[] GetCreatures()
        {

            return ExecuteViewArray<Creature>(ElleRealTimeBaseDAO.Base.Creatures.GetBaseQueryCreatures(),
                new Hashtable(), null);
        }

        public void InsertSpawnCreature(int creatureId, Player player, DbTransaction trans)
        {
            ElleRealTimeBaseDAO.Base.Creatures.InsertSpawnCreature(this, creatureId, player, trans);
        }

        public CreatureTemplate[] GetCreaturesTemplate(CreatureTemplateFilter filter, DbTransaction trans)
        {
            Hashtable prms = new Hashtable
            {
                { "@FirstRow", filter.FirstRow },
                { "@LastRow", filter.LastRow },
            };

            return ExecuteViewArray<CreatureTemplate>("SELECT * " +
                                                "FROM ( " +
                                                "    SELECT MZ.*, " +
                                                "			 ROW_NUMBER() OVER (" + filter.OrderByCondition() + ") AS rn " +
                                                "    FROM ( " + ElleRealTimeBaseDAO.Base.Creatures.GetBaseQueryCreatureTemplate(filter, prms) + " ) MZ " +
                                                ") NumMZ " +
                                                "WHERE rn BETWEEN @FirstRow AND @LastRow",
                prms, trans);
        }

        public void InsertCreatureTemplate(CreatureTemplate creatureTemplate, DbTransaction trans)
        {
            Hashtable prms = new Hashtable
            {
                { $@"{nameof(CreatureTemplate.Name)}", creatureTemplate.Name },
                { $@"{nameof(CreatureTemplate.PrefabName)}", creatureTemplate.PrefabName },
            };

            creatureTemplate.ID = ExecuteScalar<int>("INSERT INTO creatures_template(Name, PrefabName) VALUES (" +
                                                     $" @{nameof(CreatureTemplate.Name)}, " +
                                                     $" @{nameof(CreatureTemplate.PrefabName)} " +
                                                     $"); SELECT SCOPE_IDENTITY();", prms, trans);
        }
    }
}
