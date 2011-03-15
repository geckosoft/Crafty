using CraftyServer.Server;
using java.io;
using java.lang;

namespace CraftyServer.Core
{
    public class ThreadCommandReader : Thread
    {
        private readonly MinecraftServer mcServer; /* synthetic field */

        public ThreadCommandReader(MinecraftServer minecraftserver)
        {
            mcServer = minecraftserver;
//        
        }

        public override void run()
        {
            var bufferedreader = new BufferedReader(new InputStreamReader(java.lang.System.@in));
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
    }
}