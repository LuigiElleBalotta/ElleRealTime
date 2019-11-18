using System;
using System.Collections.Generic;
using System.Text;
using ElleFramework.Database;
using ElleRealTime.Shared.DBEntities.Creatures;
using ElleRealTime.Tests.Services;
using ElleRealTimeBaseDAO.Interfaces;
using ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player;

namespace ElleRealTime.Core.BO.World
{
    public class Creatures
    {
        public void InsertSpawnCreature(string prefabName)
        {
            Player player = GamingHub.GetOnlinePlayers()[0];
            ICreatures dao = DAOFactory.Create<ICreatures>();
            dao.InsertSpawnCreature(prefabName, player, null);
        }

        public Creature[] GetCreatures()
        {
            ICreatures dao = DAOFactory.Create<ICreatures>();
            return dao.GetCreatures();
        }
    }
}
