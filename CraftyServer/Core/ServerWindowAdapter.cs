using CraftyServer.Server;
using java.awt.@event;
using java.lang;

namespace CraftyServer.Core
{
    public class ServerWindowAdapter : WindowAdapter
    {
        public ServerWindowAdapter(MinecraftServer minecraftserver)
        {
            mcServer = minecraftserver;
//        
        }

        public override void windowClosing(java.awt.@event.WindowEvent windowevent)
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

        private MinecraftServer mcServer; /* synthetic field */
    }
}