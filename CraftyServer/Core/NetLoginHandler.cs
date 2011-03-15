using CraftyServer.Server;
using java.lang;
using java.net;
using java.util;
using java.util.logging;

namespace CraftyServer.Core
{
    public class NetLoginHandler : NetHandler
    {
        public static Logger logger = Logger.getLogger("Minecraft");
        private static readonly Random rand = new Random();
        private readonly MinecraftServer mcServer;
        private Packet1Login field_9004_h;
        public bool finishedProcessing;
        private int loginTimer;
        public NetworkManager netManager;
        private string serverId;
        private string username;

        public NetLoginHandler(MinecraftServer minecraftserver, Socket socket, string s)
        {
            finishedProcessing = false;
            loginTimer = 0;
            username = null;
            field_9004_h = null;
            serverId = "";
            mcServer = minecraftserver;
            netManager = new NetworkManager(socket, s, this);
            netManager.chunkDataSendCounter = 0;
        }

        public void tryLogin()
        {
            if (field_9004_h != null)
            {
                doLogin(field_9004_h);
                field_9004_h = null;
            }
            if (loginTimer++ == 600)
            {
                kickUser("Took too long to log in");
            }
            else
            {
                netManager.processReadPackets();
            }
        }

        public void kickUser(string s)
        {
            try
            {
                logger.info(
                    (new StringBuilder()).append("Disconnecting ").append(getUserAndIPString()).append(": ").append(s).
                        toString());
                netManager.addToSendQueue(new Packet255KickDisconnect(s));
                netManager.serverShutdown();
                finishedProcessing = true;
            }
            catch (Exception exception)
            {
                exception.printStackTrace();
            }
        }

        public override void handleHandshake(Packet2Handshake packet2handshake)
        {
            if (mcServer.onlineMode)
            {
                serverId = Long.toHexString(rand.nextLong());
                netManager.addToSendQueue(new Packet2Handshake(serverId));
            }
            else
            {
                netManager.addToSendQueue(new Packet2Handshake("-"));
            }
        }

        public override void handleLogin(Packet1Login packet1login)
        {
            username = packet1login.username;
            if (packet1login.protocolVersion != 9)
            {
                if (packet1login.protocolVersion > 9)
                {
                    kickUser("Outdated server!");
                }
                else
                {
                    kickUser("Outdated client!");
                }
                return;
            }
            if (!mcServer.onlineMode)
            {
                doLogin(packet1login);
            }
            else
            {
                (new ThreadLoginVerifier(this, packet1login)).start();
            }
        }

        public void doLogin(Packet1Login packet1login)
        {
            EntityPlayerMP entityplayermp = mcServer.configManager.login(this, packet1login.username,
                                                                         packet1login.password);
            if (entityplayermp != null)
            {
                logger.info(
                    (new StringBuilder()).append(getUserAndIPString()).append(" logged in with entity id ").append(
                        entityplayermp.entityId).toString());
                ChunkCoordinates chunkcoordinates = mcServer.worldMngr.func_22078_l();
                var netserverhandler = new NetServerHandler(mcServer, netManager, entityplayermp);
                netserverhandler.sendPacket(new Packet1Login("", "", entityplayermp.entityId,
                                                             mcServer.worldMngr.func_22079_j(),
                                                             (byte) mcServer.worldMngr.worldProvider.worldType));
                netserverhandler.sendPacket(new Packet6SpawnPosition(chunkcoordinates.posX, chunkcoordinates.posY,
                                                                     chunkcoordinates.posZ));
                mcServer.configManager.sendPacketToAllPlayers(
                    new Packet3Chat(
                        (new StringBuilder()).append("§e").append(entityplayermp.username).append(" joined the game.").
                            toString()));
                mcServer.configManager.playerLoggedIn(entityplayermp);
                netserverhandler.teleportTo(entityplayermp.posX, entityplayermp.posY, entityplayermp.posZ,
                                            entityplayermp.rotationYaw, entityplayermp.rotationPitch);
                mcServer.networkServer.addPlayer(netserverhandler);
                netserverhandler.sendPacket(new Packet4UpdateTime(mcServer.worldMngr.getWorldTime()));
                entityplayermp.func_20057_k();
            }
            finishedProcessing = true;
        }

        public override void handleErrorMessage(string s, object[] aobj)
        {
            logger.info((new StringBuilder()).append(getUserAndIPString()).append(" lost connection").toString());
            finishedProcessing = true;
        }

        public override void registerPacket(Packet packet)
        {
            kickUser("Protocol error");
        }

        public string getUserAndIPString()
        {
            if (username != null)
            {
                return
                    (new StringBuilder()).append(username).append(" [").append(netManager.getRemoteAddress().toString())
                        .append("]").toString();
            }
            else
            {
                return netManager.getRemoteAddress().toString();
            }
        }

        public static string getServerId(NetLoginHandler netloginhandler)
        {
            return netloginhandler.serverId;
        }

        public static Packet1Login setLoginPacket(NetLoginHandler netloginhandler, Packet1Login packet1login)
        {
            return netloginhandler.field_9004_h = packet1login;
        }
    }
}