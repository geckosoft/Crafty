using java.util;


namespace CraftyServer.Core
{
    public class BlockStep : Block
    {
        public BlockStep(int i, bool flag)
            : base(i, 6, Material.rock)
        {
            blockType = flag;
            if (!flag)
            {
                setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.5F, 1.0F);
            }
            setLightOpacity(255);
        }

        public override int func_22009_a(int i, int j)
        {
            if (j == 0)
            {
                return i > 1 ? 5 : 6;
            }
            if (j == 1)
            {
                if (i == 0)
                {
                    return 208;
                }
                return i != 1 ? 192 : 176;
            }
            if (j == 2)
            {
                return 4;
            }
            return j != 3 ? 6 : 16;
        }

        public override int getBlockTextureFromSide(int i)
        {
            return func_22009_a(i, 0);
        }

        public override bool isOpaqueCube()
        {
            return blockType;
        }

        public override void onBlockAdded(World world, int i, int j, int k)
        {
            if (this != Block.stairSingle)
            {
                base.onBlockAdded(world, i, j, k);
            }
            int l = world.getBlockId(i, j - 1, k);
            int i1 = world.getBlockMetadata(i, j, k);
            int j1 = world.getBlockMetadata(i, j - 1, k);
            if (i1 != j1)
            {
                return;
            }
            if (l == stairSingle.blockID)
            {
                world.setBlockWithNotify(i, j, k, 0);
                world.setBlockAndMetadataWithNotify(i, j - 1, k, Block.stairDouble.blockID, i1);
            }
        }

        public override int idDropped(int i, Random random)
        {
            return Block.stairSingle.blockID;
        }

        public override int quantityDropped(Random random)
        {
            return !blockType ? 1 : 2;
        }

        protected override int damageDropped(int i)
        {
            return i;
        }

        public override bool shouldSideBeRendered(IBlockAccess iblockaccess, int i, int j, int k, int l)
        {
            if (this != Block.stairSingle)
            {
                base.shouldSideBeRendered(iblockaccess, i, j, k, l);
            }
            if (l == 1)
            {
                return true;
            }
            if (!base.shouldSideBeRendered(iblockaccess, i, j, k, l))
            {
                return false;
            }
            if (l == 0)
            {
                return true;
            }
            else
            {
                return iblockaccess.getBlockId(i, j, k) != blockID;
            }
        }

        public static string[] field_22027_a = {
                                                   "stone", "sand", "wood", "cobble"
                                               };

        private bool blockType;
    }
}