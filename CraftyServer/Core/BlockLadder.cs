using java.util;


namespace CraftyServer.Core
{
    public class BlockLadder : Block
    {
        public BlockLadder(int i, int j) : base(i, j, Material.circuits)
        {
        }

        public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
        {
            int l = world.getBlockMetadata(i, j, k);
            float f = 0.125F;
            if (l == 2)
            {
                setBlockBounds(0.0F, 0.0F, 1.0F - f, 1.0F, 1.0F, 1.0F);
            }
            if (l == 3)
            {
                setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, f);
            }
            if (l == 4)
            {
                setBlockBounds(1.0F - f, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
            }
            if (l == 5)
            {
                setBlockBounds(0.0F, 0.0F, 0.0F, f, 1.0F, 1.0F);
            }
            return base.getCollisionBoundingBoxFromPool(world, i, j, k);
        }

        public override bool isOpaqueCube()
        {
            return false;
        }

        public override bool canPlaceBlockAt(World world, int i, int j, int k)
        {
            if (world.isBlockOpaqueCube(i - 1, j, k))
            {
                return true;
            }
            if (world.isBlockOpaqueCube(i + 1, j, k))
            {
                return true;
            }
            if (world.isBlockOpaqueCube(i, j, k - 1))
            {
                return true;
            }
            return world.isBlockOpaqueCube(i, j, k + 1);
        }

        public override void onBlockPlaced(World world, int i, int j, int k, int l)
        {
            int i1 = world.getBlockMetadata(i, j, k);
            if ((i1 == 0 || l == 2) && world.isBlockOpaqueCube(i, j, k + 1))
            {
                i1 = 2;
            }
            if ((i1 == 0 || l == 3) && world.isBlockOpaqueCube(i, j, k - 1))
            {
                i1 = 3;
            }
            if ((i1 == 0 || l == 4) && world.isBlockOpaqueCube(i + 1, j, k))
            {
                i1 = 4;
            }
            if ((i1 == 0 || l == 5) && world.isBlockOpaqueCube(i - 1, j, k))
            {
                i1 = 5;
            }
            world.setBlockMetadataWithNotify(i, j, k, i1);
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            int i1 = world.getBlockMetadata(i, j, k);
            bool flag = false;
            if (i1 == 2 && world.isBlockOpaqueCube(i, j, k + 1))
            {
                flag = true;
            }
            if (i1 == 3 && world.isBlockOpaqueCube(i, j, k - 1))
            {
                flag = true;
            }
            if (i1 == 4 && world.isBlockOpaqueCube(i + 1, j, k))
            {
                flag = true;
            }
            if (i1 == 5 && world.isBlockOpaqueCube(i - 1, j, k))
            {
                flag = true;
            }
            if (!flag)
            {
                dropBlockAsItem(world, i, j, k, i1);
                world.setBlockWithNotify(i, j, k, 0);
            }
            base.onNeighborBlockChange(world, i, j, k, l);
        }

        public override int quantityDropped(Random random)
        {
            return 1;
        }
    }
}