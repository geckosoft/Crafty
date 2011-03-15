using java.util;

namespace CraftyServer.Core
{
    public class BlockSnowBlock : Block
    {
        public BlockSnowBlock(int i, int j)
            : base(i, j, Material.builtSnow)
        {
            setTickOnLoad(true);
        }

        public override int idDropped(int i, Random random)
        {
            return Item.snowball.shiftedIndex;
        }

        public override int quantityDropped(Random random)
        {
            return 4;
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            if (world.getSavedLightValue(EnumSkyBlock.Block, i, j, k) > 11)
            {
                dropBlockAsItem(world, i, j, k, world.getBlockMetadata(i, j, k));
                world.setBlockWithNotify(i, j, k, 0);
            }
        }
    }
}