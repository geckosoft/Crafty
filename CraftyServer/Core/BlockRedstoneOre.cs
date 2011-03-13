using java.util;


namespace CraftyServer.Core
{
    public class BlockRedstoneOre : Block
    {
        public BlockRedstoneOre(int i, int j, bool flag)
            : base(i, j, Material.rock)
        {
            if (flag)
            {
                setTickOnLoad(true);
            }
            field_665_a = flag;
        }

        public override int tickRate()
        {
            return 30;
        }

        public override void onBlockClicked(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            func_321_g(world, i, j, k);
            base.onBlockClicked(world, i, j, k, entityplayer);
        }

        public override void onEntityWalking(World world, int i, int j, int k, Entity entity)
        {
            func_321_g(world, i, j, k);
            base.onEntityWalking(world, i, j, k, entity);
        }

        public override bool blockActivated(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            func_321_g(world, i, j, k);
            return base.blockActivated(world, i, j, k, entityplayer);
        }

        private void func_321_g(World world, int i, int j, int k)
        {
            func_320_h(world, i, j, k);
            if (blockID == Block.oreRedstone.blockID)
            {
                world.setBlockWithNotify(i, j, k, Block.oreRedstoneGlowing.blockID);
            }
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            if (blockID == Block.oreRedstoneGlowing.blockID)
            {
                world.setBlockWithNotify(i, j, k, Block.oreRedstone.blockID);
            }
        }

        public override int idDropped(int i, Random random)
        {
            return Item.redstone.shiftedIndex;
        }

        public override int quantityDropped(Random random)
        {
            return 4 + random.nextInt(2);
        }

        private void func_320_h(World world, int i, int j, int k)
        {
            Random random = world.rand;
            double d = 0.0625D;
            for (int l = 0; l < 6; l++)
            {
                double d1 = (float) i + random.nextFloat();
                double d2 = (float) j + random.nextFloat();
                double d3 = (float) k + random.nextFloat();
                if (l == 0 && !world.isBlockOpaqueCube(i, j + 1, k))
                {
                    d2 = (double) (j + 1) + d;
                }
                if (l == 1 && !world.isBlockOpaqueCube(i, j - 1, k))
                {
                    d2 = (double) (j + 0) - d;
                }
                if (l == 2 && !world.isBlockOpaqueCube(i, j, k + 1))
                {
                    d3 = (double) (k + 1) + d;
                }
                if (l == 3 && !world.isBlockOpaqueCube(i, j, k - 1))
                {
                    d3 = (double) (k + 0) - d;
                }
                if (l == 4 && !world.isBlockOpaqueCube(i + 1, j, k))
                {
                    d1 = (double) (i + 1) + d;
                }
                if (l == 5 && !world.isBlockOpaqueCube(i - 1, j, k))
                {
                    d1 = (double) (i + 0) - d;
                }
                if (d1 < (double) i || d1 > (double) (i + 1) || d2 < 0.0D || d2 > (double) (j + 1) || d3 < (double) k ||
                    d3 > (double) (k + 1))
                {
                    world.spawnParticle("reddust", d1, d2, d3, 0.0D, 0.0D, 0.0D);
                }
            }
        }

        private bool field_665_a;
    }
}