using CraftyServer.Server;

namespace CraftyServer.Core
{
    public class ThreadServerApplication : java.lang.Thread
    {
        public ThreadServerApplication(string s, MinecraftServer minecraftserver)
            : base(s)
        {
            mcServer = minecraftserver;
        }

        public override void run()
        {
            mcServer.run();
        }

        private MinecraftServer mcServer; /* synthetic field */
    }
}