using java.util;


namespace CraftyServer.Core
{
    public class MobSpawnerTaiga : MobSpawnerBase
    {
        public MobSpawnerTaiga()
        {
        }

        public override WorldGenerator getRandomWorldGenForTrees(Random random)
        {
            if (random.nextInt(3) == 0)
            {
                return new WorldGenTaiga1();
            }
            else
            {
                return new WorldGenTaiga2();
            }
        }
    }
}