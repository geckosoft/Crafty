using CraftyServer.Server;
using java.util;
using java.util.logging;
using java.lang;

namespace CraftyServer.Core
{
    public class ConsoleCommandHandler
    {
        public ConsoleCommandHandler(MinecraftServer minecraftserver)
        {
            minecraftServer = minecraftserver;
        }

        public void handleCommand(ServerCommand servercommand)
        {
            string s = servercommand.command;
            ICommandListener icommandlistener = servercommand.commandListener;
            string s1 = icommandlistener.getUsername();
            WorldServer worldserver = minecraftServer.worldMngr;
            ServerConfigurationManager serverconfigurationmanager = minecraftServer.configManager;
            if (s.ToLower().StartsWith("help") || s.ToLower().StartsWith("?"))
            {
                showHelp(icommandlistener);
            }
            else if (s.ToLower().StartsWith("list"))
            {
                icommandlistener.log(
                    (new StringBuilder()).append("Connected players: ").append(
                        serverconfigurationmanager.getPlayerList()).toString());
            }
            else if (s.ToLower().StartsWith("stop"))
            {
                func_22115_a(s1, "Stopping the server..");
                minecraftServer.initiateShutdown();
            }
            else if (s.ToLower().StartsWith("save-all"))
            {
                func_22115_a(s1, "Forcing save..");
                worldserver.saveWorld(true, null);
                func_22115_a(s1, "Save complete.");
            }
            else if (s.ToLower().StartsWith("save-off"))
            {
                func_22115_a(s1, "Disabling level saving..");
                worldserver.levelSaving = true;
            }
            else if (s.ToLower().StartsWith("save-on"))
            {
                func_22115_a(s1, "Enabling level saving..");
                worldserver.levelSaving = false;
            }
            else if (s.ToLower().StartsWith("op "))
            {
                string s2 = s.Substring(s.IndexOf(" ")).Trim();
                serverconfigurationmanager.opPlayer(s2);
                func_22115_a(s1, (new StringBuilder()).append("Opping ").append(s2).toString());
                serverconfigurationmanager.sendChatMessageToPlayer(s2, "§eYou are now op!");
            }
            else if (s.ToLower().StartsWith("deop "))
            {
                string s3 = s.Substring(s.IndexOf(" ")).Trim();
                serverconfigurationmanager.deopPlayer(s3);
                serverconfigurationmanager.sendChatMessageToPlayer(s3, "§eYou are no longer op!");
                func_22115_a(s1, (new StringBuilder()).append("De-opping ").append(s3).toString());
            }
            else if (s.ToLower().StartsWith("ban-ip "))
            {
                string s4 = s.Substring(s.IndexOf(" ")).Trim();
                serverconfigurationmanager.banIP(s4);
                func_22115_a(s1, (new StringBuilder()).append("Banning ip ").append(s4).toString());
            }
            else if (s.ToLower().StartsWith("pardon-ip "))
            {
                string s5 = s.Substring(s.IndexOf(" ")).Trim();
                serverconfigurationmanager.pardonIP(s5);
                func_22115_a(s1, (new StringBuilder()).append("Pardoning ip ").append(s5).toString());
            }
            else if (s.ToLower().StartsWith("ban "))
            {
                string s6 = s.Substring(s.IndexOf(" ")).Trim();
                serverconfigurationmanager.banPlayer(s6);
                func_22115_a(s1, (new StringBuilder()).append("Banning ").append(s6).toString());
                EntityPlayerMP entityplayermp = serverconfigurationmanager.getPlayerEntity(s6);
                if (entityplayermp != null)
                {
                    entityplayermp.playerNetServerHandler.kickPlayer("Banned by admin");
                }
            }
            else if (s.ToLower().StartsWith("pardon "))
            {
                string s7 = s.Substring(s.IndexOf(" ")).Trim();
                serverconfigurationmanager.pardonPlayer(s7);
                func_22115_a(s1, (new StringBuilder()).append("Pardoning ").append(s7).toString());
            }
            else if (s.ToLower().StartsWith("kick "))
            {
                string s8 = s.Substring(s.IndexOf(" ")).Trim();
                EntityPlayerMP entityplayermp1 = null;
                for (int i = 0; i < serverconfigurationmanager.playerEntities.size(); i++)
                {
                    EntityPlayerMP entityplayermp5 = (EntityPlayerMP) serverconfigurationmanager.playerEntities.get(i);
                    if (entityplayermp5.username.ToLowerInvariant() == s8.ToLowerInvariant())
                    {
                        entityplayermp1 = entityplayermp5;
                    }
                }

                if (entityplayermp1 != null)
                {
                    entityplayermp1.playerNetServerHandler.kickPlayer("Kicked by admin");
                    func_22115_a(s1,
                                 (new StringBuilder()).append("Kicking ").append(entityplayermp1.username).toString());
                }
                else
                {
                    icommandlistener.log(
                        (new StringBuilder()).append("Can't find user ").append(s8).append(". No kick.").toString());
                }
            }
            else if (s.ToLower().StartsWith("tp "))
            {
                string[] ask = s.Split(' ');
                if (ask.Length == 3)
                {
                    EntityPlayerMP entityplayermp2 = serverconfigurationmanager.getPlayerEntity(ask[1]);
                    EntityPlayerMP entityplayermp3 = serverconfigurationmanager.getPlayerEntity(ask[2]);
                    if (entityplayermp2 == null)
                    {
                        icommandlistener.log(
                            (new StringBuilder()).append("Can't find user ").append(ask[1]).append(". No tp.").toString());
                    }
                    else if (entityplayermp3 == null)
                    {
                        icommandlistener.log(
                            (new StringBuilder()).append("Can't find user ").append(ask[2]).append(". No tp.").toString());
                    }
                    else
                    {
                        entityplayermp2.playerNetServerHandler.teleportTo(entityplayermp3.posX, entityplayermp3.posY,
                                                                          entityplayermp3.posZ,
                                                                          entityplayermp3.rotationYaw,
                                                                          entityplayermp3.rotationPitch);
                        func_22115_a(s1,
                                     (new StringBuilder()).append("Teleporting ").append(ask[1]).append(" to ").append(
                                         ask[2]).append(".").toString());
                    }
                }
                else
                {
                    icommandlistener.log("Syntax error, please provice a source and a target.");
                }
            }
            else if (s.ToLower().StartsWith("give "))
            {
                string[] as1 = s.Split(' ');
                if (as1.Length != 3 && as1.Length != 4)
                {
                    return;
                }
                string s9 = as1[1];
                EntityPlayerMP entityplayermp4 = serverconfigurationmanager.getPlayerEntity(s9);
                if (entityplayermp4 != null)
                {
                    try
                    {
                        int k = java.lang.Integer.parseInt(as1[2]);
                        if (Item.itemsList[k] != null)
                        {
                            func_22115_a(s1,
                                         (new StringBuilder()).append("Giving ").append(entityplayermp4.username).append
                                             (" some ").append(k).toString());
                            int l = 1;
                            if (as1.Length > 3)
                            {
                                l = tryParse(as1[3], 1);
                            }
                            if (l < 1)
                            {
                                l = 1;
                            }
                            if (l > 64)
                            {
                                l = 64;
                            }
                            entityplayermp4.dropPlayerItem(new ItemStack(k, l, 0));
                        }
                        else
                        {
                            icommandlistener.log(
                                (new StringBuilder()).append("There's no item with id ").append(k).toString());
                        }
                    }
                    catch (NumberFormatException numberformatexception1)
                    {
                        icommandlistener.log(
                            (new StringBuilder()).append("There's no item with id ").append(as1[2]).toString());
                    }
                }
                else
                {
                    icommandlistener.log((new StringBuilder()).append("Can't find user ").append(s9).toString());
                }
            }
            else if (s.ToLower().StartsWith("time "))
            {
                string[] as2 = s.Split(' ');
                if (as2.Length != 3)
                {
                    return;
                }
                string s10 = as2[1];
                try
                {
                    int j = java.lang.Integer.parseInt(as2[2]);
                    if ("add".Equals(s10, System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        worldserver.func_22076_a(worldserver.getWorldTime() + (long) j);
                        func_22115_a(s1, (new StringBuilder()).append("Added ").append(j).append(" to time").toString());
                    }
                    else if ("set".Equals(s10, System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        worldserver.func_22076_a(j);
                        func_22115_a(s1, (new StringBuilder()).append("Set time to ").append(j).toString());
                    }
                    else
                    {
                        icommandlistener.log("Unknown method, use either \"add\" or \"set\"");
                    }
                }
                catch (NumberFormatException numberformatexception)
                {
                    icommandlistener.log(
                        (new StringBuilder()).append("Unable to convert time value, ").append(as2[2]).toString());
                }
            }
            else if (s.ToLower().StartsWith("say "))
            {
                s = s.Substring(s.IndexOf(" ")).Trim();
                minecraftLogger.info((new StringBuilder()).append("[").append(s1).append("] ").append(s).toString());
                serverconfigurationmanager.sendPacketToAllPlayers(
                    new Packet3Chat((new StringBuilder()).append("§d[Server] ").append(s).toString()));
            }
            else if (s.ToLower().StartsWith("tell "))
            {
                string[] as3 = s.Split(' ');
                if (as3.Length >= 3)
                {
                    s = s.Substring(s.IndexOf(" ")).Trim();
                    s = s.Substring(s.IndexOf(" ")).Trim();
                    minecraftLogger.info(
                        (new StringBuilder()).append("[").append(s1).append("->").append(as3[1]).append("] ").append(s).
                            toString());
                    s = (new StringBuilder()).append("§7").append(s1).append(" whispers ").append(s).toString();
                    minecraftLogger.info(s);
                    if (!serverconfigurationmanager.sendPacketToPlayer(as3[1], new Packet3Chat(s)))
                    {
                        icommandlistener.log("There's no player by that name online.");
                    }
                }
            }
            else if (s.ToLower().StartsWith("whitelist "))
            {
                func_22113_a(s1, s, icommandlistener);
            }
            else
            {
                minecraftLogger.info("Unknown console command. Type \"help\" for help.");
            }
        }

        private void func_22113_a(string s, string s1, ICommandListener icommandlistener)
        {
            string[] ask = s1.Split(' ');
            if (ask.Length < 2)
            {
                return;
            }
            string s2 = ask[1].ToLower();
            if ("on".Equals(s2))
            {
                func_22115_a(s, "Turned on white-listing");
                minecraftServer.propertyManagerObj.func_22118_b("white-list", true);
            }
            else if ("off".Equals(s2))
            {
                func_22115_a(s, "Turned off white-listing");
                minecraftServer.propertyManagerObj.func_22118_b("white-list", false);
            }
            else if ("list".Equals(s2))
            {
                Set set = minecraftServer.configManager.func_22167_e();
                string s5 = "";
                for (Iterator iterator = set.iterator(); iterator.hasNext();)
                {
                    string s6 = (string) iterator.next();
                    s5 = (new StringBuilder()).append(s5).append(s6).append(" ").toString();
                }

                icommandlistener.log((new StringBuilder()).append("White-listed players: ").append(s5).toString());
            }
            else if ("add".Equals(s2) && ask.Length == 3)
            {
                string s3 = ask[2].ToLower();
                minecraftServer.configManager.func_22169_k(s3);
                func_22115_a(s, (new StringBuilder()).append("Added ").append(s3).append(" to white-list").toString());
            }
            else if ("remove".Equals(s2) && ask.Length == 3)
            {
                string s4 = ask[2].ToLower();
                minecraftServer.configManager.func_22170_l(s4);
                func_22115_a(s,
                             (new StringBuilder()).append("Removed ").append(s4).append(" from white-list").toString());
            }
            else if ("reload".Equals(s2))
            {
                minecraftServer.configManager.func_22171_f();
                func_22115_a(s, "Reloaded white-list from file");
            }
        }

        private void showHelp(ICommandListener icommandlistener)
        {
            icommandlistener.log("Console commands:");
            icommandlistener.log("   help  or  ?               shows this message");
            icommandlistener.log("   kick <player>             removes a player from the server");
            icommandlistener.log("   ban <player>              bans a player from the server");
            icommandlistener.log("   pardon <player>           pardons a banned player so that they can connect again");
            icommandlistener.log("   ban-ip <ip>               bans an IP address from the server");
            icommandlistener.log(
                "   pardon-ip <ip>            pardons a banned IP address so that they can connect again");
            icommandlistener.log("   op <player>               turns a player into an op");
            icommandlistener.log("   deop <player>             removes op status from a player");
            icommandlistener.log("   tp <player1> <player2>    moves one player to the same location as another player");
            icommandlistener.log("   give <player> <id> [num]  gives a player a resource");
            icommandlistener.log("   tell <player> <message>   sends a private message to a player");
            icommandlistener.log("   stop                      gracefully stops the server");
            icommandlistener.log("   save-all                  forces a server-wide level save");
            icommandlistener.log("   save-off                  disables terrain saving (useful for backup scripts)");
            icommandlistener.log("   save-on                   re-enables terrain saving");
            icommandlistener.log("   list                      lists all currently connected players");
            icommandlistener.log("   say <message>             broadcasts a message to all players");
            icommandlistener.log("   time <add|set> <amount>   adds to or sets the world time (0-24000)");
        }

        private void func_22115_a(string s, string s1)
        {
            string s2 = (new StringBuilder()).append(s).append(": ").append(s1).toString();
            minecraftServer.configManager.sendChatMessageToAllPlayers(
                (new StringBuilder()).append("§7(").append(s2).append(")").toString());
            minecraftLogger.info(s2);
        }

        private int tryParse(string s, int i)
        {
            try
            {
                return java.lang.Integer.parseInt(s);
            }
            catch (NumberFormatException numberformatexception)
            {
                return i;
            }
        }

        private static Logger minecraftLogger = Logger.getLogger("Minecraft");
        private MinecraftServer minecraftServer;
    }
}