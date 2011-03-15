using CraftyServer.Server;
using java.lang;
using java.net;
using java.util;
using java.util.logging;

namespace CraftyServer.Core
{
    public class NetworkListenThread
    {
        public static Logger logger = Logger.getLogger("Minecraft");
        private readonly Thread networkAcceptThread;
        private readonly ArrayList pendingConnections;
        private readonly ArrayList playerList;
        private readonly ServerSocket serverSocket;
        public volatile bool field_973_b;
        private int field_977_f;
        public MinecraftServer mcServer;

        public NetworkListenThread(MinecraftServer minecraftserver, InetAddress inetaddress, int i)
        {
            field_973_b = false;
            field_977_f = 0;
            pendingConnections = new ArrayList();
            playerList = new ArrayList();
            mcServer = minecraftserver;
            serverSocket = new ServerSocket(i, 0, inetaddress);
            serverSocket.setPerformancePreferences(0, 2, 1);
            field_973_b = true;
            networkAcceptThread = new NetworkAcceptThread(this, "Listen thread", minecraftserver);
            networkAcceptThread.start();
        }

        public void addPlayer(NetServerHandler netserverhandler)
        {
            playerList.add(netserverhandler);
        }

        private void func_717_a(NetLoginHandler netloginhandler)
        {
            if (netloginhandler == null)
            {
                throw new IllegalArgumentException("Got null pendingconnection!");
            }
            else
            {
                pendingConnections.add(netloginhandler);
                return;
            }
        }

        public void func_715_a()
        {
            for (int i = 0; i < pendingConnections.size(); i++)
            {
                var netloginhandler = (NetLoginHandler) pendingConnections.get(i);
                try
                {
                    netloginhandler.tryLogin();
                }
                catch (Exception exception)
                {
                    netloginhandler.kickUser("Internal server error");
                    logger.log(Level.WARNING,
                               (new StringBuilder()).append("Failed to handle packet: ").append(exception).toString(),
                               exception);
                }
                if (netloginhandler.finishedProcessing)
                {
                    pendingConnections.remove(i--);
                }
            }

            for (int j = 0; j < playerList.size(); j++)
            {
                var netserverhandler = (NetServerHandler) playerList.get(j);
                try
                {
                    netserverhandler.handlePackets();
                }
                catch (Exception exception1)
                {
                    logger.log(Level.WARNING,
                               (new StringBuilder()).append("Failed to handle packet: ").append(exception1).toString(),
                               exception1);
                    netserverhandler.kickPlayer("Internal server error");
                }
                if (netserverhandler.connectionClosed)
                {
                    playerList.remove(j--);
                }
            }
        }

        public static ServerSocket func_713_a(NetworkListenThread networklistenthread)
        {
            return networklistenthread.serverSocket;
        }

        public static int func_712_b(NetworkListenThread networklistenthread)
        {
            return networklistenthread.field_977_f++;
        }

        public static void func_716_a(NetworkListenThread networklistenthread, NetLoginHandler netloginhandler)
        {
            networklistenthread.func_717_a(netloginhandler);
        }
    }
}