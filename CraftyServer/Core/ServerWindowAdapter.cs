using CraftyServer.Server;
using java.awt.@event;
using java.lang;

namespace CraftyServer.Core
{
    public class ServerWindowAdapter : WindowAdapter
    {
        private readonly MinecraftServer mcServer; /* synthetic field */

        public ServerWindowAdapter(MinecraftServer minecraftserver)
        {
            mcServer = minecraftserver;
//        
        }

        public override void windowClosing(WindowEvent windowevent)
        {
            mcServer.initiateShutdown();
            while (!mcServer.serverStopped)
            {
                try
                {
                    Thread.sleep(100L);
                }
                catch (InterruptedException interruptedexception)
                {
                    interruptedexception.printStackTrace();
                }
            }
            java.lang.System.exit(0);
        }
    }
}