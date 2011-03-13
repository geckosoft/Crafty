using CraftyServer.Server;
using java.io;
using java.net;
using java.lang;
using java.util;

namespace CraftyServer.Core
{
    public class NetworkAcceptThread : Thread
    {
        public NetworkAcceptThread(NetworkListenThread networklistenthread, string s, MinecraftServer minecraftserver)
            : base(s)
        {
            field_985_b = networklistenthread;
            mcServer = minecraftserver;
        }

        public override void run()
        {
            HashMap hashmap = new HashMap();
            do
            {
                if (!field_985_b.field_973_b)
                {
                    break;
                }
                try
                {
                    Socket socket = NetworkListenThread.func_713_a(field_985_b).accept();
                    if (socket != null)
                    {
                        InetAddress inetaddress = socket.getInetAddress();
                        if (hashmap.containsKey(inetaddress) && !"127.0.0.1".Equals(inetaddress.getHostAddress()) &&
                            java.lang.System.currentTimeMillis() - ((Long) hashmap.get(inetaddress)).longValue() < 5000L)
                        {
                            hashmap.put(inetaddress, Long.valueOf(java.lang.System.currentTimeMillis()));
                            socket.close();
                        }
                        else
                        {
                            hashmap.put(inetaddress, Long.valueOf(java.lang.System.currentTimeMillis()));
                            NetLoginHandler netloginhandler = new NetLoginHandler(mcServer, socket,
                                                                                  (new StringBuilder()).append(
                                                                                      "Connection #").append(
                                                                                          NetworkListenThread.func_712_b
                                                                                              (field_985_b)).toString());
                            NetworkListenThread.func_716_a(field_985_b, netloginhandler);
                        }
                    }
                }
                catch (IOException ioexception)
                {
                    ioexception.printStackTrace();
                }
            } while (true);
        }

        private MinecraftServer mcServer; /* synthetic field */
        private NetworkListenThread field_985_b; /* synthetic field */
    }
}