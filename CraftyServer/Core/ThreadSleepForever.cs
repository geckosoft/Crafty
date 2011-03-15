using CraftyServer.Server;
using java.lang;

namespace CraftyServer.Core
{
    public class ThreadSleepForever : Thread
    {
        private MinecraftServer mc; /* synthetic field */

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
                    sleep(0x7fffffffL);
                }
                catch (InterruptedException)
                {
                }
            } while (true);
        }
    }
}