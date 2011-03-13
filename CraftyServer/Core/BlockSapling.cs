using java.util;


namespace CraftyServer.Core
{
    public class BlockSapling : BlockFlower
    {
        public BlockSapling(int i, int j)
            : base(i, j)
        {
            float f = 0.4F;
            setBlockBounds(0.5F - f, 0.0F, 0.5F - f, 0.5F + f, f*2.0F, 0.5F + f);
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            base.updateTick(world, i, j, k, random);
            if (world.getBlockLightValue(i, j + 1, k) >= 9 && random.nextInt(5) == 0)
            {
                int l = world.getBlockMetadata(i, j, k);
                if (l < 15)
                {
                    world.setBlockMetadataWithNotify(i, j, k, l + 1);
                }
                else
                {
                    func_21027_b(world, i, j, k, random);
                }
            }
        }

        public void func_21027_b(World world, int i, int j, int k, Random random)
        {
            world.setBlock(i, j, k, 0);
            object obj = new WorldGenTrees();
            if (random.nextInt(10) == 0)
            {
                obj = new WorldGenBigTree();
            }
            if (!((WorldGenerator) (obj)).generate(world, random, i, j, k))
            {
                world.setBlock(i, j, k, blockID);
            }
        }
    }
}