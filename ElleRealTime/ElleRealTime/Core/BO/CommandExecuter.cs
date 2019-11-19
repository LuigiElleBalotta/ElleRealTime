using System;
using System.Collections.Generic;
using System.Text;
using ElleRealTime.Core.BO.World;
using ElleRealTime.Shared.DBEntities.PlayersInfo;
using ElleRealTime.Tests.Services;

namespace ElleRealTime.Core.BO
{
    public static class CommandExecuter
    {
        public static void Execute(string command)
        {
            if (command.StartsWith(".createaccount"))
            {
                try
                {
                    string[] parameters = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    if (parameters.Length == 3)
                    {
                        int id = Login.CreateAccount(parameters[1], parameters[2]);
                        if (id > 0)
                        {
                            Program.Logger.Success($"Successfully created the account \"{parameters[1]}\" with ID: {id}!");
                        }
                        else
                        {
                            Program.Logger.Error("An error occurred while creating the account!");
                        }
                    }
                    else
                    {
                        Program.Logger.Error("Syntax error: .createaccount {username} {password}");
                    }
                }
                catch (Exception ex)
                {
                    Program.Logger.Error(ex.InnerException?.Message ?? ex.Message, true);
                }
            }
            else if (command.StartsWith(".modifypassword"))
            {
                try
                {
                    string[] parameters = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    if (parameters.Length == 3)
                    {
                        Login.ModifyPassword(parameters[1], parameters[2]);
                        Program.Logger.Success($"Successfully modified the account \"{parameters[1]}\" with a new password!");
                    }
                    else
                    {
                        Program.Logger.Error("Syntax error: .modifypassword {username} {newpassword}");
                    }
                }
                catch (Exception ex)
                {
                    Program.Logger.Error(ex.InnerException?.Message ?? ex.Message, true);
                }
            }
            else if (command.StartsWith(".getplayerinfo"))
            {
                try
                {
                    string[] parameters = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    if (parameters.Length == 2)
                    {
                        int accountId;
                        if (int.TryParse(parameters[1], out accountId))
                        {
                            var bo = new Players();
                            var playerInfo = bo.GetPlayerInfo(new PlayersInfoFilter { AccountID = accountId });
                            if (playerInfo != null && playerInfo.Length > 0)
                            {
                                Program.Logger.Success($"Position X: {playerInfo[0].PosX}");
                                Program.Logger.Success($"Position Y: {playerInfo[0].PosY}");
                                Program.Logger.Success($"Position Z: {playerInfo[0].PosZ}");
                                Program.Logger.Success($"Rotation X: {playerInfo[0].RotX}");
                                Program.Logger.Success($"Rotation Y: {playerInfo[0].RotY}");
                                Program.Logger.Success($"Rotation Z: {playerInfo[0].RotZ}");
                            }
                            else
                            {
                                Program.Logger.Error($"No account with id={accountId} have data stored.");
                            }
                        }
                        else
                        {
                            Program.Logger.Error($"Invalid account id: {parameters[1]}");
                        }
                    }
                    else
                    {
                        Program.Logger.Error("Syntax error: .getplayerinfo {AccountID}");
                    }
                }
                catch (Exception ex)
                {
                    Program.Logger.Error(ex.InnerException?.Message ?? ex.Message, true);
                }
            }
            else if (command.StartsWith(".clearconsole"))
            {
                Console.Clear();
            }
            else if (command.StartsWith(".spawn")) //Debug purpose only
            {
                string[] parameters = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (parameters.Length == 2)
                {
                    var bo = new Creatures();
                    string prefabName = parameters[1];
                    bo.InsertSpawnCreature(prefabName);
                    GamingHub.Instance.QueryCreaturesAsync();
                }
                else
                {
                    Program.Logger.Error("Syntax error: .spawn {prefabName}");
                }
            }
        }
    }
}
