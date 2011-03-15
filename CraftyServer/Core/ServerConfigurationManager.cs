using System;
using CraftyServer.Server;
using java.io;
using java.lang;
using java.util;
using java.util.logging;
using Exception = java.lang.Exception;

namespace CraftyServer.Core
{
    public class ServerConfigurationManager
    {
        public static Logger logger = Logger.getLogger("Minecraft");
        private readonly Set bannedIPs;
        private readonly Set bannedPlayers;
        private readonly File bannedPlayersFile;
        private readonly File ipBanFile;
        private readonly int maxPlayers;
        private readonly MinecraftServer mcServer;
        private readonly File opFile;
        private readonly Set ops;
        private readonly PlayerManager playerManagerObj;
        private readonly bool whiteListEnforced;
        private readonly Set whiteListedIPs;
        private readonly File whitelistPlayersFile;
        public List playerEntities;
        private IPlayerFileData playerNBTManagerObj;

        public ServerConfigurationManager(MinecraftServer minecraftserver)
        {
            playerEntities = new ArrayList();
            bannedPlayers = new HashSet();
            bannedIPs = new HashSet();
            ops = new HashSet();
            whiteListedIPs = new HashSet();
            mcServer = minecraftserver;
            bannedPlayersFile = minecraftserver.getFile("banned-players.txt");
            ipBanFile = minecraftserver.getFile("banned-ips.txt");
            opFile = minecraftserver.getFile("ops.txt");
            whitelistPlayersFile = minecraftserver.getFile("white-list.txt");
            playerManagerObj = new PlayerManager(minecraftserver);
            maxPlayers = minecraftserver.propertyManagerObj.getIntProperty("max-players", 20);
            whiteListEnforced = minecraftserver.propertyManagerObj.getBooleanProperty("white-list", false);
            readBannedPlayers();
            loadBannedList();
            loadOps();
            loadWhiteList();
            writeBannedPlayers();
            saveBannedList();
            saveOps();
            saveWhiteList();
        }

        public void setPlayerManager(WorldServer worldserver)
        {
            playerNBTManagerObj = worldserver.func_22075_m().func_22090_d();
        }

        public int func_640_a()
        {
            return playerManagerObj.func_542_b();
        }

        public void playerLoggedIn(EntityPlayerMP entityplayermp)
        {
            playerEntities.add(entityplayermp);
            playerNBTManagerObj.readPlayerData(entityplayermp);
            mcServer.worldMngr.field_20911_y.loadChunk((int) entityplayermp.posX >> 4, (int) entityplayermp.posZ >> 4);
            for (;
                mcServer.worldMngr.getCollidingBoundingBoxes(entityplayermp, entityplayermp.boundingBox).size() != 0;
                entityplayermp.setPosition(entityplayermp.posX, entityplayermp.posY + 1.0D, entityplayermp.posZ))
            {
            }
            mcServer.worldMngr.entityJoinedWorld(entityplayermp);
            playerManagerObj.addPlayer(entityplayermp);
        }

        public void func_613_b(EntityPlayerMP entityplayermp)
        {
            playerManagerObj.func_543_c(entityplayermp);
        }

        public void playerLoggedOut(EntityPlayerMP entityplayermp)
        {
            playerNBTManagerObj.writePlayerData(entityplayermp);
            mcServer.worldMngr.func_22085_d(entityplayermp);
            playerEntities.remove(entityplayermp);
            playerManagerObj.removePlayer(entityplayermp);
        }

        public EntityPlayerMP login(NetLoginHandler netloginhandler, string s, string s1)
        {
            if (bannedPlayers.contains(s.Trim().ToLower()))
            {
                netloginhandler.kickUser("You are banned from this server!");
                return null;
            }
            if (!isAllowedToLogin(s))
            {
                netloginhandler.kickUser("You are not white-listed on this server!");
                return null;
            }
            string s2 = netloginhandler.netManager.getRemoteAddress().toString();
            s2 = s2.Substring(s2.IndexOf("/") + 1);
            s2 = s2.Substring(0, s2.IndexOf(":"));
            if (bannedIPs.contains(s2))
            {
                netloginhandler.kickUser("Your IP address is banned from this server!");
                return null;
            }
            if (playerEntities.size() >= maxPlayers)
            {
                netloginhandler.kickUser("The server is full!");
                return null;
            }
            for (int i = 0; i < playerEntities.size(); i++)
            {
                var entityplayermp = (EntityPlayerMP) playerEntities.get(i);
                if (entityplayermp.username.ToLowerInvariant() == s.ToLowerInvariant())
                {
                    entityplayermp.playerNetServerHandler.kickPlayer("You logged in from another location");
                }
            }

            return new EntityPlayerMP(mcServer, mcServer.worldMngr, s, new ItemInWorldManager(mcServer.worldMngr));
        }

        public EntityPlayerMP recreatePlayerEntity(EntityPlayerMP entityplayermp)
        {
            mcServer.entityTracker.removeTrackedPlayerSymmetric(entityplayermp);
            mcServer.entityTracker.untrackEntity(entityplayermp);
            playerManagerObj.removePlayer(entityplayermp);
            playerEntities.remove(entityplayermp);
            mcServer.worldMngr.func_22073_e(entityplayermp);
            var entityplayermp1 = new EntityPlayerMP(mcServer, mcServer.worldMngr, entityplayermp.username,
                                                     new ItemInWorldManager(mcServer.worldMngr));
            entityplayermp1.entityId = entityplayermp.entityId;
            entityplayermp1.playerNetServerHandler = entityplayermp.playerNetServerHandler;
            mcServer.worldMngr.field_20911_y.loadChunk((int) entityplayermp1.posX >> 4, (int) entityplayermp1.posZ >> 4);
            for (;
                mcServer.worldMngr.getCollidingBoundingBoxes(entityplayermp1, entityplayermp1.boundingBox).size() != 0;
                entityplayermp1.setPosition(entityplayermp1.posX, entityplayermp1.posY + 1.0D, entityplayermp1.posZ))
            {
            }
            entityplayermp1.playerNetServerHandler.sendPacket(new Packet9());
            entityplayermp1.playerNetServerHandler.teleportTo(entityplayermp1.posX, entityplayermp1.posY,
                                                              entityplayermp1.posZ, entityplayermp1.rotationYaw,
                                                              entityplayermp1.rotationPitch);
            playerManagerObj.addPlayer(entityplayermp1);
            mcServer.worldMngr.entityJoinedWorld(entityplayermp1);
            playerEntities.add(entityplayermp1);
            entityplayermp1.func_20057_k();
            entityplayermp1.func_22068_s();
            return entityplayermp1;
        }

        public void func_637_b()
        {
            playerManagerObj.func_538_a();
        }

        public void func_622_a(int i, int j, int k)
        {
            playerManagerObj.func_535_a(i, j, k);
        }

        public void sendPacketToAllPlayers(Packet packet)
        {
            for (int i = 0; i < playerEntities.size(); i++)
            {
                var entityplayermp = (EntityPlayerMP) playerEntities.get(i);
                entityplayermp.playerNetServerHandler.sendPacket(packet);
            }
        }

        public string getPlayerList()
        {
            string s = "";
            for (int i = 0; i < playerEntities.size(); i++)
            {
                if (i > 0)
                {
                    s = (new StringBuilder()).append(s).append(", ").toString();
                }
                s = (new StringBuilder()).append(s).append(((EntityPlayerMP) playerEntities.get(i)).username).toString();
            }

            return s;
        }

        public void banPlayer(string s)
        {
            bannedPlayers.add(s.ToLower());
            writeBannedPlayers();
        }

        public void pardonPlayer(string s)
        {
            bannedPlayers.remove(s.ToLower());
            writeBannedPlayers();
        }

        private void readBannedPlayers()
        {
            try
            {
                bannedPlayers.clear();
                var bufferedreader = new BufferedReader(new FileReader(bannedPlayersFile));
                for (string s = ""; (s = bufferedreader.readLine()) != null;)
                {
                    bannedPlayers.add(s.Trim().ToLower());
                }

                bufferedreader.close();
            }
            catch (Exception exception)
            {
                logger.warning((new StringBuilder()).append("Failed to load ban list: ").append(exception).toString());
            }
        }

        private void writeBannedPlayers()
        {
            try
            {
                var printwriter = new PrintWriter(new FileWriter(bannedPlayersFile, false));
                string s;
                for (Iterator iterator = bannedPlayers.iterator(); iterator.hasNext(); printwriter.println(s))
                {
                    s = (string) iterator.next();
                }

                printwriter.close();
            }
            catch (Exception exception)
            {
                logger.warning((new StringBuilder()).append("Failed to save ban list: ").append(exception).toString());
            }
        }

        public void banIP(string s)
        {
            bannedIPs.add(s.ToLower());
            saveBannedList();
        }

        public void pardonIP(string s)
        {
            bannedIPs.remove(s.ToLower());
            saveBannedList();
        }

        private void loadBannedList()
        {
            try
            {
                bannedIPs.clear();
                var bufferedreader = new BufferedReader(new FileReader(ipBanFile));
                for (string s = ""; (s = bufferedreader.readLine()) != null;)
                {
                    bannedIPs.add(s.Trim().ToLower());
                }

                bufferedreader.close();
            }
            catch (Exception exception)
            {
                logger.warning((new StringBuilder()).append("Failed to load ip ban list: ").append(exception).toString());
            }
        }

        private void saveBannedList()
        {
            try
            {
                var printwriter = new PrintWriter(new FileWriter(ipBanFile, false));
                string s;
                for (Iterator iterator = bannedIPs.iterator(); iterator.hasNext(); printwriter.println(s))
                {
                    s = (string) iterator.next();
                }

                printwriter.close();
            }
            catch (Exception exception)
            {
                logger.warning((new StringBuilder()).append("Failed to save ip ban list: ").append(exception).toString());
            }
        }

        public void opPlayer(string s)
        {
            ops.add(s.ToLower());
            saveOps();
        }

        public void deopPlayer(string s)
        {
            ops.remove(s.ToLower());
            saveOps();
        }

        private void loadOps()
        {
            try
            {
                ops.clear();
                var bufferedreader = new BufferedReader(new FileReader(opFile));
                for (string s = ""; (s = bufferedreader.readLine()) != null;)
                {
                    ops.add(s.Trim().ToLower());
                }

                bufferedreader.close();
            }
            catch (Exception exception)
            {
                logger.warning((new StringBuilder()).append("Failed to load ip ban list: ").append(exception).toString());
            }
        }

        private void saveOps()
        {
            try
            {
                var printwriter = new PrintWriter(new FileWriter(opFile, false));
                string s;
                for (Iterator iterator = ops.iterator(); iterator.hasNext(); printwriter.println(s))
                {
                    s = (string) iterator.next();
                }

                printwriter.close();
            }
            catch (Exception exception)
            {
                logger.warning((new StringBuilder()).append("Failed to save ip ban list: ").append(exception).toString());
            }
        }

        private void loadWhiteList()
        {
            try
            {
                whiteListedIPs.clear();
                var bufferedreader = new BufferedReader(new FileReader(whitelistPlayersFile));
                for (string s = ""; (s = bufferedreader.readLine()) != null;)
                {
                    whiteListedIPs.add(s.Trim().ToLower());
                }

                bufferedreader.close();
            }
            catch (Exception exception)
            {
                logger.warning((new StringBuilder()).append("Failed to load white-list: ").append(exception).toString());
            }
        }

        private void saveWhiteList()
        {
            try
            {
                var printwriter = new PrintWriter(new FileWriter(whitelistPlayersFile, false));
                string s;
                for (Iterator iterator = whiteListedIPs.iterator(); iterator.hasNext(); printwriter.println(s))
                {
                    s = (string) iterator.next();
                }

                printwriter.close();
            }
            catch (Exception exception)
            {
                logger.warning((new StringBuilder()).append("Failed to save white-list: ").append(exception).toString());
            }
        }

        public bool isAllowedToLogin(string s)
        {
            s = s.Trim().ToLower();
            return !whiteListEnforced || ops.contains(s) || whiteListedIPs.contains(s);
        }

        public bool isOp(string s)
        {
            return ops.contains(s.Trim().ToLower());
        }

        public EntityPlayerMP getPlayerEntity(string s)
        {
            for (int i = 0; i < playerEntities.size(); i++)
            {
                var entityplayermp = (EntityPlayerMP) playerEntities.get(i);
                if (entityplayermp.username.Equals(s, StringComparison.InvariantCultureIgnoreCase))
                {
                    return entityplayermp;
                }
            }

            return null;
        }

        public void sendChatMessageToPlayer(string s, string s1)
        {
            EntityPlayerMP entityplayermp = getPlayerEntity(s);
            if (entityplayermp != null)
            {
                entityplayermp.playerNetServerHandler.sendPacket(new Packet3Chat(s1));
            }
        }

        public void func_12022_a(double d, double d1, double d2, double d3, Packet packet)
        {
            for (int i = 0; i < playerEntities.size(); i++)
            {
                var entityplayermp = (EntityPlayerMP) playerEntities.get(i);
                double d4 = d - entityplayermp.posX;
                double d5 = d1 - entityplayermp.posY;
                double d6 = d2 - entityplayermp.posZ;
                if (d4*d4 + d5*d5 + d6*d6 < d3*d3)
                {
                    entityplayermp.playerNetServerHandler.sendPacket(packet);
                }
            }
        }

        public void sendChatMessageToAllPlayers(string s)
        {
            var packet3chat = new Packet3Chat(s);
            for (int i = 0; i < playerEntities.size(); i++)
            {
                var entityplayermp = (EntityPlayerMP) playerEntities.get(i);
                if (isOp(entityplayermp.username))
                {
                    entityplayermp.playerNetServerHandler.sendPacket(packet3chat);
                }
            }
        }

        public bool sendPacketToPlayer(string s, Packet packet)
        {
            EntityPlayerMP entityplayermp = getPlayerEntity(s);
            if (entityplayermp != null)
            {
                entityplayermp.playerNetServerHandler.sendPacket(packet);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void savePlayerStates()
        {
            for (int i = 0; i < playerEntities.size(); i++)
            {
                playerNBTManagerObj.writePlayerData((EntityPlayer) playerEntities.get(i));
            }
        }

        public void sentTileEntityToPlayer(int i, int j, int k, TileEntity tileentity)
        {
        }

        public void func_22169_k(string s)
        {
            whiteListedIPs.add(s);
            saveWhiteList();
        }

        public void func_22170_l(string s)
        {
            whiteListedIPs.remove(s);
            saveWhiteList();
        }

        public Set func_22167_e()
        {
            return whiteListedIPs;
        }

        public void func_22171_f()
        {
            loadWhiteList();
        }
    }
}