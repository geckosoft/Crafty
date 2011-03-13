using java.lang;

namespace CraftyServer.Core
{
    public class MinecraftException : RuntimeException
    {
        public MinecraftException(string s)
            : base(s)
        {
        }
    }
}