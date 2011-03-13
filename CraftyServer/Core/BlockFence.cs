namespace CraftyServer.Core
{
    public class BlockFence : Block
    {
        public BlockFence(int i, int j)
            : base(i, j, Material.wood)
        {
        }

        public override bool canPlaceBlockAt(World world, int i, int j, int k)
        {
            if (world.getBlockId(i, j - 1, k) == blockID)
            {
                return false;
            }
            if (!world.getBlockMaterial(i, j - 1, k).isSolid())
            {
                return false;
            }
            else
            {
                return base.canPlaceBlockAt(world, i, j, k);
            }
        }

        public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
        {
            return AxisAlignedBB.getBoundingBoxFromPool(i, j, k, i + 1, (float) j + 1.5F, k + 1);
        }

        public override bool isOpaqueCube()
        {
            return false;
        }
    }
}