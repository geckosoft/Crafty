using CraftyServer.Server;
using java.lang;

namespace CraftyServer.Core
{
    public class ConvertProgressUpdater
        : IProgressUpdate
    {
        public ConvertProgressUpdater(MinecraftServer minecraftserver)
        {
            field_22072_a = minecraftserver;
            field_22071_b = java.lang.System.currentTimeMillis();
        }

        public void func_438_a(string s)
        {
        }

        public void setLoadingProgress(int i)
        {
            if (java.lang.System.currentTimeMillis() - field_22071_b >= 1000L)
            {
                field_22071_b = java.lang.System.currentTimeMillis();
                MinecraftServer.logger.info(
                    (new StringBuilder()).append("Converting... ").append(i).append("%").toString());
            }
        }

        public void displayLoadingString(string s)
        {
        }

        private long field_22071_b;
        private MinecraftServer field_22072_a; /* synthetic field */
    }
}