using java.util;


namespace CraftyServer.Core
{
    public abstract class WorldGenerator
    {
        public WorldGenerator()
        {
        }

        public abstract bool generate(World world, Random random, int i, int j, int k);

        public virtual void func_420_a(double d, double d1, double d2)
        {
        }
    }
}