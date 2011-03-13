using java.util;


namespace CraftyServer.Core
{
    public class BlockRedstoneRepeater : Block
    {
        public BlockRedstoneRepeater(int i, bool flag)
            : base(i, 102, Material.circuits)
        {
            field_22015_c = flag;
            setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.125F, 1.0F);
        }

        public override bool canPlaceBlockAt(World world, int i, int j, int k)
        {
            if (!world.isBlockOpaqueCube(i, j - 1, k))
            {
                return false;
            }
            else
            {
                return base.canPlaceBlockAt(world, i, j, k);
            }
        }

        public override bool canBlockStay(World world, int i, int j, int k)
        {
            if (!world.isBlockOpaqueCube(i, j - 1, k))
            {
                return false;
            }
            else
            {
                return base.canBlockStay(world, i, j, k);
            }
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            int l = world.getBlockMetadata(i, j, k);
            bool flag = func_22012_g(world, i, j, k, l);
            if (field_22015_c && !flag)
            {
                world.setBlockAndMetadataWithNotify(i, j, k, Block.field_22011_bh.blockID, l);
            }
            else if (!field_22015_c)
            {
                world.setBlockAndMetadataWithNotify(i, j, k, Block.field_22010_bi.blockID, l);
                if (!flag)
                {
                    int i1 = (l & 0xc) >> 2;
                    world.func_22074_c(i, j, k, Block.field_22010_bi.blockID, field_22013_b[i1]*2);
                }
            }
        }

        public override int func_22009_a(int i, int j)
        {
            if (i == 0)
            {
                return !field_22015_c ? 115 : 99;
            }
            if (i == 1)
            {
                return !field_22015_c ? 131 : 147;
            }
            else
            {
                return 5;
            }
        }

        public override bool shouldSideBeRendered(IBlockAccess iblockaccess, int i, int j, int k, int l)
        {
            return l != 0 && l != 1;
        }

        public override int getBlockTextureFromSide(int i)
        {
            return func_22009_a(i, 0);
        }

        public override bool isIndirectlyPoweringTo(World world, int i, int j, int k, int l)
        {
            return isPoweringTo(world, i, j, k, l);
        }

        public override bool isPoweringTo(IBlockAccess iblockaccess, int i, int j, int k, int l)
        {
            if (!field_22015_c)
            {
                return false;
            }
            int i1 = iblockaccess.getBlockMetadata(i, j, k) & 3;
            if (i1 == 0 && l == 3)
            {
                return true;
            }
            if (i1 == 1 && l == 4)
            {
                return true;
            }
            if (i1 == 2 && l == 2)
            {
                return true;
            }
            return i1 == 3 && l == 5;
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            if (!canBlockStay(world, i, j, k))
            {
                dropBlockAsItem(world, i, j, k, world.getBlockMetadata(i, j, k));
                world.setBlockWithNotify(i, j, k, 0);
                return;
            }
            int i1 = world.getBlockMetadata(i, j, k);
            bool flag = func_22012_g(world, i, j, k, i1);
            int j1 = (i1 & 0xc) >> 2;
            if (field_22015_c && !flag)
            {
                world.func_22074_c(i, j, k, blockID, field_22013_b[j1]*2);
            }
            else if (!field_22015_c && flag)
            {
                world.func_22074_c(i, j, k, blockID, field_22013_b[j1]*2);
            }
        }

        private bool func_22012_g(World world, int i, int j, int k, int l)
        {
            int i1 = l & 3;
            switch (i1)
            {
                case 0: // '\0'
                    return world.isBlockIndirectlyProvidingPowerTo(i, j, k + 1, 3);

                case 2: // '\002'
                    return world.isBlockIndirectlyProvidingPowerTo(i, j, k - 1, 2);

                case 3: // '\003'
                    return world.isBlockIndirectlyProvidingPowerTo(i + 1, j, k, 5);

                case 1: // '\001'
                    return world.isBlockIndirectlyProvidingPowerTo(i - 1, j, k, 4);
            }
            return false;
        }

        public override bool blockActivated(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            int l = world.getBlockMetadata(i, j, k);
            int i1 = (l & 0xc) >> 2;
            i1 = i1 + 1 << 2 & 0xc;
            world.setBlockMetadataWithNotify(i, j, k, i1 | l & 3);
            return true;
        }

        public override bool canProvidePower()
        {
            return false;
        }

        public override void onBlockPlacedBy(World world, int i, int j, int k, EntityLiving entityliving)
        {
            int l = ((MathHelper.floor_double((double) ((entityliving.rotationYaw*4F)/360F) + 0.5D) & 3) + 2)%4;
            world.setBlockMetadataWithNotify(i, j, k, l);
            bool flag = func_22012_g(world, i, j, k, l);
            if (flag)
            {
                world.func_22074_c(i, j, k, blockID, 1);
            }
        }

        public override void onBlockAdded(World world, int i, int j, int k)
        {
            world.notifyBlocksOfNeighborChange(i + 1, j, k, blockID);
            world.notifyBlocksOfNeighborChange(i - 1, j, k, blockID);
            world.notifyBlocksOfNeighborChange(i, j, k + 1, blockID);
            world.notifyBlocksOfNeighborChange(i, j, k - 1, blockID);
            world.notifyBlocksOfNeighborChange(i, j - 1, k, blockID);
            world.notifyBlocksOfNeighborChange(i, j + 1, k, blockID);
        }

        public override bool isOpaqueCube()
        {
            return false;
        }

        public override int idDropped(int i, Random random)
        {
            return Item.redstoneRepeater.shiftedIndex;
        }

        public static double[] field_22014_a = {
                                                   -0.0625D, 0.0625D, 0.1875D, 0.3125D
                                               };

        private static int[] field_22013_b = {
                                                 1, 2, 3, 4
                                             };

        private bool field_22015_c;
    }
}