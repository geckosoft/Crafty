using java.util;


namespace CraftyServer.Core
{
    public class BlockFlower : Block
    {
        public BlockFlower(int i, int j)
            : base(i, Material.plants)
        {
            blockIndexInTexture = j;
            setTickOnLoad(true);
            float f = 0.2F;
            setBlockBounds(0.5F - f, 0.0F, 0.5F - f, 0.5F + f, f*3F, 0.5F + f);
        }

        public override bool canPlaceBlockAt(World world, int i, int j, int k)
        {
            return canThisPlantGrowOnThisBlockID(world.getBlockId(i, j - 1, k));
        }

        protected virtual bool canThisPlantGrowOnThisBlockID(int i)
        {
            return i == Block.grass.blockID || i == Block.dirt.blockID || i == Block.tilledField.blockID;
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            base.onNeighborBlockChange(world, i, j, k, l);
            func_276_g(world, i, j, k);
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            func_276_g(world, i, j, k);
        }

        protected void func_276_g(World world, int i, int j, int k)
        {
            if (!canBlockStay(world, i, j, k))
            {
                dropBlockAsItem(world, i, j, k, world.getBlockMetadata(i, j, k));
                world.setBlockWithNotify(i, j, k, 0);
            }
        }

        public override bool canBlockStay(World world, int i, int j, int k)
        {
            return (world.getBlockLightValue(i, j, k) >= 8 || world.canBlockSeeTheSky(i, j, k)) &&
                   canThisPlantGrowOnThisBlockID(world.getBlockId(i, j - 1, k));
        }

        public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
        {
            return null;
        }

        public override bool isOpaqueCube()
        {
            return false;
        }
    }
}