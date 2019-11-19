using System;
using System.Collections.Generic;
using System.Text;
using ElleFramework.Database;
using ElleRealTime.Shared;
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

            CreatureTemplateFilter filter = new CreatureTemplateFilter{ PrefabNameAll = prefabName };
            CreatureTemplate[] creaturesTemplate = dao.GetCreaturesTemplate(filter, null);
            CreatureTemplate creatureTemplate = null;
            if (creaturesTemplate == null || (creaturesTemplate != null && creaturesTemplate.Length == 0))
            {
                Program.Logger.Info($"[{DateTime.Now.ToString(Constants.DATETIME_FORMAT)}] Prefab name \"{prefabName}\" does not exists in creature_template table. I'm going to add it.");
                creatureTemplate = new CreatureTemplate{ Name = prefabName, PrefabName = prefabName };
                dao.InsertCreatureTemplate(creatureTemplate, null);
            }
            else
            {
                Program.Logger.Info($"[{DateTime.Now.ToString(Constants.DATETIME_FORMAT)}] Found {prefabName} inside creature_template table.");
                creatureTemplate = creaturesTemplate[0];
            }
            dao.InsertSpawnCreature(creatureTemplate.ID, player, null);
        }

        public Creature[] GetCreatures()
        {
            ICreatures dao = DAOFactory.Create<ICreatures>();
            return dao.GetCreatures();
        }
    }
}
