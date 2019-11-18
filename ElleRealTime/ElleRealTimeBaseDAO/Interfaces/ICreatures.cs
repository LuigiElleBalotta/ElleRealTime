﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using ElleRealTime.Shared.DBEntities.Creatures;
using ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player;

namespace ElleRealTimeBaseDAO.Interfaces
{
    public interface ICreatures : ITransactions
    {
        Creature[] GetCreatures();
        void InsertSpawnCreature(string prefabName, Player player, DbTransaction trans);
    }
}
