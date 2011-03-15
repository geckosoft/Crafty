using CraftyServer.Server;
using java.lang;

namespace CraftyServer.Core
{
    public class ThreadServerApplication : Thread
    {
        private readonly MinecraftServer mcServer; /* synthetic field */

        public ThreadServerApplication(string s, MinecraftServer minecraftserver)
            : base(s)
        {
            mcServer = minecraftserver;
        }

        public override void run()
        {
            mcServer.run();
        }
    }
}