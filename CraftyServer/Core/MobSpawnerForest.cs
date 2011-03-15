using java.util;

namespace CraftyServer.Core
{
    public class MobSpawnerForest : MobSpawnerBase
    {
        public override WorldGenerator getRandomWorldGenForTrees(Random random)
        {
            if (random.nextInt(5) == 0)
            {
                return new WorldGenForest();
            }
            if (random.nextInt(3) == 0)
            {
                return new WorldGenBigTree();
            }
            else
            {
                return new WorldGenTrees();
            }
        }
    }
}