using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using ElleRealTime.Shared.DBEntities.PlayersInfo;

namespace ElleRealTimeBaseDAO.Base.World
{
    public static class Players
    {
        public static void UpdatePlayerInfo(ElleRealTimeDbDAO dao, PlayerInfo playerInfo, DbTransaction trans)
        {
            Hashtable prms = new Hashtable
            {
                { $"@{nameof(PlayerInfo.AccountID)}", playerInfo.AccountID },
                { $"@{nameof(PlayerInfo.PosX)}", playerInfo.PosX },
                { $"@{nameof(PlayerInfo.PosY)}", playerInfo.PosY },
                { $"@{nameof(PlayerInfo.PosZ)}", playerInfo.PosZ },
                { $"@{nameof(PlayerInfo.RotX)}", playerInfo.RotX },
                { $"@{nameof(PlayerInfo.RotY)}", playerInfo.RotY },
                { $"@{nameof(PlayerInfo.RotZ)}", playerInfo.RotZ },
                { $"@{nameof(PlayerInfo.RotW)}", playerInfo.RotW },
                { $"@{nameof(PlayerInfo.Health)}", playerInfo.Health },
                { $"@{nameof(PlayerInfo.MaxHealth)}", playerInfo.MaxHealth },
                { $"@{nameof(PlayerInfo.Damage)}", playerInfo.Damage },
                { $"@{nameof(PlayerInfo.Level)}", playerInfo.Level },
                { $"@{nameof(PlayerInfo.Experience)}", playerInfo.Experience },
                { $"@{nameof(PlayerInfo.ExpToNextLevel)}", playerInfo.ExpToNextLevel },
            };

            dao.ExecuteNonQuery("UPDATE players_info " +
                                $"SET PosX = @{nameof(PlayerInfo.PosX)}, " +
                                $"    PosY = @{nameof(PlayerInfo.PosY)}, " +
                                $"    PosZ = @{nameof(PlayerInfo.PosZ)}, " +
                                $"    RotX = @{nameof(PlayerInfo.RotX)}, " +
                                $"    RotY = @{nameof(PlayerInfo.RotY)}, " +
                                $"    RotZ = @{nameof(PlayerInfo.RotZ)}, " +
                                $"    RotW = @{nameof(PlayerInfo.RotW)}, " +
                                $"    Health = @{nameof(PlayerInfo.Health)}, " +
                                $"    MaxHealth = @{nameof(PlayerInfo.MaxHealth)}, " +
                                $"    Damage = @{nameof(PlayerInfo.Damage)}, " +
                                $"    Level = @{nameof(PlayerInfo.Level)}, " +
                                $"    Experience = @{nameof(PlayerInfo.Experience)}, " +
                                $"    ExpToNextLevel = @{nameof(PlayerInfo.ExpToNextLevel)} " +
                                $"WHERE AccountID = @{nameof(PlayerInfo.AccountID)}", prms, trans);
        }

        public static void InsertPlayerInfo(ElleRealTimeDbDAO dao, PlayerInfo playerInfo, DbTransaction trans)
        {
            Hashtable prms = new Hashtable
            {
                { $"@{nameof(PlayerInfo.AccountID)}", playerInfo.AccountID },
                { $"@{nameof(PlayerInfo.PosX)}", playerInfo.PosX },
                { $"@{nameof(PlayerInfo.PosY)}", playerInfo.PosY },
                { $"@{nameof(PlayerInfo.PosZ)}", playerInfo.PosZ },
                { $"@{nameof(PlayerInfo.RotX)}", playerInfo.RotX },
                { $"@{nameof(PlayerInfo.RotY)}", playerInfo.RotY },
                { $"@{nameof(PlayerInfo.RotZ)}", playerInfo.RotZ },
                { $"@{nameof(PlayerInfo.RotW)}", playerInfo.RotW },
                { $"@{nameof(PlayerInfo.Health)}", playerInfo.Health },
                { $"@{nameof(PlayerInfo.MaxHealth)}", playerInfo.MaxHealth },
                { $"@{nameof(PlayerInfo.Damage)}", playerInfo.Damage },
                { $"@{nameof(PlayerInfo.Level)}", playerInfo.Level },
                { $"@{nameof(PlayerInfo.Experience)}", playerInfo.Experience },
                { $"@{nameof(PlayerInfo.ExpToNextLevel)}", playerInfo.ExpToNextLevel },
            };

            dao.ExecuteNonQuery("INSERT INTO players_info( AccountID, PosX, PosY, PosZ, RotX, RotY, RotZ, RotW, Health, MaxHealth, Damage, Level, Experience, ExpToNextLevel ) VALUES ( " +
                            $" @{nameof(PlayerInfo.AccountID)}, " +
                            $" @{nameof(PlayerInfo.PosX)}, " +
                            $" @{nameof(PlayerInfo.PosY)}, " +
                            $" @{nameof(PlayerInfo.PosZ)}, " +
                            $" @{nameof(PlayerInfo.RotX)}, " +
                            $" @{nameof(PlayerInfo.RotY)}, " +
                            $" @{nameof(PlayerInfo.RotZ)}, " +
                            $" @{nameof(PlayerInfo.RotW)}, " +
                            $" @{nameof(PlayerInfo.Health)}, " +
                            $" @{nameof(PlayerInfo.MaxHealth)}, " +
                            $" @{nameof(PlayerInfo.Damage)}, " +
                            $" @{nameof(PlayerInfo.Level)}, " +
                            $" @{nameof(PlayerInfo.Experience)}, " +
                            $" @{nameof(PlayerInfo.ExpToNextLevel)} " +
                            "); ", prms, trans);
        }

        public static string GetBaseQueryPlayersInfo(PlayersInfoFilter filter, Hashtable prms)
        {
            return "SELECT PI.*, A.Username " +
                   "FROM players_info PI " +
                   "JOIN accounts A ON A.ID = PI.AccountID " +
                   filter.WhereCondition(prms);
        }
    }
}
