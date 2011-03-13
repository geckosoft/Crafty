using CraftyServer.Server;
using java.lang;

namespace CraftyServer.Core
{
    public class ThreadSleepForever : Thread
    {
        public ThreadSleepForever(MinecraftServer minecraftserver)
        {
            mc = minecraftserver;
//        
            setDaemon(true);
            start();
        }

        public override void run()
        {
            do
            {
                try
                {
                    Thread.sleep(0x7fffffffL);
                }
                catch (InterruptedException)
                {
                }
            } while (true);
        }

        private MinecraftServer mc; /* synthetic field */
    }
}