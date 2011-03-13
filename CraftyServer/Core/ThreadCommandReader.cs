using CraftyServer.Server;
using java.io;
using java.lang;

namespace CraftyServer.Core
{
    public class ThreadCommandReader : Thread
    {
        public ThreadCommandReader(MinecraftServer minecraftserver)
        {
            mcServer = minecraftserver;
//        
        }

        public override void run()
        {
            BufferedReader bufferedreader = new BufferedReader(new InputStreamReader(java.lang.System.@in));
            string s = null;
            try
            {
                while (!mcServer.serverStopped && MinecraftServer.isServerRunning(mcServer) &&
                       (s = bufferedreader.readLine()) != null)
                {
                    mcServer.addCommand(s, mcServer);
                }
            }
            catch (IOException ioexception)
            {
                ioexception.printStackTrace();
            }
        }

        private MinecraftServer mcServer; /* synthetic field */
    }
}