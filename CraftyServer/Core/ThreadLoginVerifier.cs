using java.io;
using java.lang;
using java.net;

namespace CraftyServer.Core
{
    public class ThreadLoginVerifier : Thread
    {
        private readonly NetLoginHandler loginHandler; /* synthetic field */
        private readonly Packet1Login loginPacket; /* synthetic field */

        public ThreadLoginVerifier(NetLoginHandler netloginhandler, Packet1Login packet1login)
        {
            loginHandler = netloginhandler;
            loginPacket = packet1login;
//        
        }

        public override void run()
        {
            try
            {
                string s = NetLoginHandler.getServerId(loginHandler);
                var url =
                    new URL(
                        (new StringBuilder()).append("http://www.minecraft.net/game/checkserver.jsp?user=").append(
                            loginPacket.username).append("&serverId=").append(s).toString());
                var bufferedreader = new BufferedReader(new InputStreamReader(url.openStream()));
                string s1 = bufferedreader.readLine();
                bufferedreader.close();
                if (s1.Equals("YES"))
                {
                    NetLoginHandler.setLoginPacket(loginHandler, loginPacket);
                }
                else
                {
                    loginHandler.kickUser("Failed to verify username!");
                }
            }
            catch (Exception exception)
            {
                exception.printStackTrace();
            }
        }
    }
}