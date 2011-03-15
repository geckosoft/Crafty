using java.util;

namespace CraftyServer.Core
{
    public class BlockIce : BlockBreakable
    {
        public BlockIce(int i, int j)
            : base(i, j, Material.ice, false)
        {
            slipperiness = 0.98F;
            setTickOnLoad(true);
        }

        public override bool shouldSideBeRendered(IBlockAccess iblockaccess, int i, int j, int k, int l)
        {
            return base.shouldSideBeRendered(iblockaccess, i, j, k, 1 - l);
        }

        public override void onBlockRemoval(World world, int i, int j, int k)
        {
            Material material = world.getBlockMaterial(i, j - 1, k);
            if (material.getIsSolid() || material.getIsLiquid())
            {
                world.setBlockWithNotify(i, j, k, waterStill.blockID);
            }
        }

        public override int quantityDropped(Random random)
        {
            return 0;
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            if (world.getSavedLightValue(EnumSkyBlock.Block, i, j, k) > 11 - lightOpacity[blockID])
            {
                dropBlockAsItem(world, i, j, k, world.getBlockMetadata(i, j, k));
                world.setBlockWithNotify(i, j, k, waterMoving.blockID);
            }
        }
    }
}