using java.util;

namespace CraftyServer.Core
{
    public class WorldGenPumpkin : WorldGenerator
    {
        public override bool generate(World world, Random random, int i, int j, int k)
        {
            for (int l = 0; l < 64; l++)
            {
                int i1 = (i + random.nextInt(8)) - random.nextInt(8);
                int j1 = (j + random.nextInt(4)) - random.nextInt(4);
                int k1 = (k + random.nextInt(8)) - random.nextInt(8);
                if (world.isAirBlock(i1, j1, k1) && world.getBlockId(i1, j1 - 1, k1) == Block.grass.blockID &&
                    Block.pumpkin.canPlaceBlockAt(world, i1, j1, k1))
                {
                    world.setBlockAndMetadata(i1, j1, k1, Block.pumpkin.blockID, random.nextInt(4));
                }
            }

            return true;
        }
    }
}