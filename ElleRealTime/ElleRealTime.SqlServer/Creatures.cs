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

        public void InsertSpawnCreature(string prefabName, Player player, DbTransaction trans)
        {
            ElleRealTimeBaseDAO.Base.Creatures.InsertSpawnCreature(this, prefabName, player, trans);
        }
    }
}
