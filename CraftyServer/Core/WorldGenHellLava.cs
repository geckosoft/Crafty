using java.util;

namespace CraftyServer.Core
{
    public class WorldGenHellLava : WorldGenerator
    {
        private readonly int field_4250_a;

        public WorldGenHellLava(int i)
        {
            field_4250_a = i;
        }

        public override bool generate(World world, Random random, int i, int j, int k)
        {
            if (world.getBlockId(i, j + 1, k) != Block.bloodStone.blockID)
            {
                return false;
            }
            if (world.getBlockId(i, j, k) != 0 && world.getBlockId(i, j, k) != Block.bloodStone.blockID)
            {
                return false;
            }
            int l = 0;
            if (world.getBlockId(i - 1, j, k) == Block.bloodStone.blockID)
            {
                l++;
            }
            if (world.getBlockId(i + 1, j, k) == Block.bloodStone.blockID)
            {
                l++;
            }
            if (world.getBlockId(i, j, k - 1) == Block.bloodStone.blockID)
            {
                l++;
            }
            if (world.getBlockId(i, j, k + 1) == Block.bloodStone.blockID)
            {
                l++;
            }
            if (world.getBlockId(i, j - 1, k) == Block.bloodStone.blockID)
            {
                l++;
            }
            int i1 = 0;
            if (world.isAirBlock(i - 1, j, k))
            {
                i1++;
            }
            if (world.isAirBlock(i + 1, j, k))
            {
                i1++;
            }
            if (world.isAirBlock(i, j, k - 1))
            {
                i1++;
            }
            if (world.isAirBlock(i, j, k + 1))
            {
                i1++;
            }
            if (world.isAirBlock(i, j - 1, k))
            {
                i1++;
            }
            if (l == 4 && i1 == 1)
            {
                world.setBlockWithNotify(i, j, k, field_4250_a);
                world.scheduledUpdatesAreImmediate = true;
                Block.blocksList[field_4250_a].updateTick(world, i, j, k, random);
                world.scheduledUpdatesAreImmediate = false;
            }
            return true;
        }
    }
}