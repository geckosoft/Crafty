using java.util;


namespace CraftyServer.Core
{
    public class BlockButton : Block
    {
        public BlockButton(int i, int j)
            : base(i, j, Material.circuits)
        {
            setTickOnLoad(true);
        }

        public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
        {
            return null;
        }

        public override int tickRate()
        {
            return 20;
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
            int j1 = i1 & 8;
            i1 &= 7;
            if (l == 2 && world.isBlockOpaqueCube(i, j, k + 1))
            {
                i1 = 4;
            }
            else if (l == 3 && world.isBlockOpaqueCube(i, j, k - 1))
            {
                i1 = 3;
            }
            else if (l == 4 && world.isBlockOpaqueCube(i + 1, j, k))
            {
                i1 = 2;
            }
            else if (l == 5 && world.isBlockOpaqueCube(i - 1, j, k))
            {
                i1 = 1;
            }
            else
            {
                i1 = func_22026_g(world, i, j, k);
            }
            world.setBlockMetadataWithNotify(i, j, k, i1 + j1);
        }

        private int func_22026_g(World world, int i, int j, int k)
        {
            if (world.isBlockOpaqueCube(i - 1, j, k))
            {
                return 1;
            }
            if (world.isBlockOpaqueCube(i + 1, j, k))
            {
                return 2;
            }
            if (world.isBlockOpaqueCube(i, j, k - 1))
            {
                return 3;
            }
            return !world.isBlockOpaqueCube(i, j, k + 1) ? 1 : 4;
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            if (func_322_g(world, i, j, k))
            {
                int i1 = world.getBlockMetadata(i, j, k) & 7;
                bool flag = false;
                if (!world.isBlockOpaqueCube(i - 1, j, k) && i1 == 1)
                {
                    flag = true;
                }
                if (!world.isBlockOpaqueCube(i + 1, j, k) && i1 == 2)
                {
                    flag = true;
                }
                if (!world.isBlockOpaqueCube(i, j, k - 1) && i1 == 3)
                {
                    flag = true;
                }
                if (!world.isBlockOpaqueCube(i, j, k + 1) && i1 == 4)
                {
                    flag = true;
                }
                if (flag)
                {
                    dropBlockAsItem(world, i, j, k, world.getBlockMetadata(i, j, k));
                    world.setBlockWithNotify(i, j, k, 0);
                }
            }
        }

        private bool func_322_g(World world, int i, int j, int k)
        {
            if (!canPlaceBlockAt(world, i, j, k))
            {
                dropBlockAsItem(world, i, j, k, world.getBlockMetadata(i, j, k));
                world.setBlockWithNotify(i, j, k, 0);
                return false;
            }
            else
            {
                return true;
            }
        }

        public override void setBlockBoundsBasedOnState(IBlockAccess iblockaccess, int i, int j, int k)
        {
            int l = iblockaccess.getBlockMetadata(i, j, k);
            int i1 = l & 7;
            bool flag = (l & 8) > 0;
            float f = 0.375F;
            float f1 = 0.625F;
            float f2 = 0.1875F;
            float f3 = 0.125F;
            if (flag)
            {
                f3 = 0.0625F;
            }
            if (i1 == 1)
            {
                setBlockBounds(0.0F, f, 0.5F - f2, f3, f1, 0.5F + f2);
            }
            else if (i1 == 2)
            {
                setBlockBounds(1.0F - f3, f, 0.5F - f2, 1.0F, f1, 0.5F + f2);
            }
            else if (i1 == 3)
            {
                setBlockBounds(0.5F - f2, f, 0.0F, 0.5F + f2, f1, f3);
            }
            else if (i1 == 4)
            {
                setBlockBounds(0.5F - f2, f, 1.0F - f3, 0.5F + f2, f1, 1.0F);
            }
        }

        public override void onBlockClicked(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            blockActivated(world, i, j, k, entityplayer);
        }

        public override bool blockActivated(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            int l = world.getBlockMetadata(i, j, k);
            int i1 = l & 7;
            int j1 = 8 - (l & 8);
            if (j1 == 0)
            {
                return true;
            }
            world.setBlockMetadataWithNotify(i, j, k, i1 + j1);
            world.markBlocksDirty(i, j, k, i, j, k);
            world.playSoundEffect((double) i + 0.5D, (double) j + 0.5D, (double) k + 0.5D, "random.click", 0.3F, 0.6F);
            world.notifyBlocksOfNeighborChange(i, j, k, blockID);
            if (i1 == 1)
            {
                world.notifyBlocksOfNeighborChange(i - 1, j, k, blockID);
            }
            else if (i1 == 2)
            {
                world.notifyBlocksOfNeighborChange(i + 1, j, k, blockID);
            }
            else if (i1 == 3)
            {
                world.notifyBlocksOfNeighborChange(i, j, k - 1, blockID);
            }
            else if (i1 == 4)
            {
                world.notifyBlocksOfNeighborChange(i, j, k + 1, blockID);
            }
            else
            {
                world.notifyBlocksOfNeighborChange(i, j - 1, k, blockID);
            }
            world.func_22074_c(i, j, k, blockID, tickRate());
            return true;
        }

        public override void onBlockRemoval(World world, int i, int j, int k)
        {
            int l = world.getBlockMetadata(i, j, k);
            if ((l & 8) > 0)
            {
                world.notifyBlocksOfNeighborChange(i, j, k, blockID);
                int i1 = l & 7;
                if (i1 == 1)
                {
                    world.notifyBlocksOfNeighborChange(i - 1, j, k, blockID);
                }
                else if (i1 == 2)
                {
                    world.notifyBlocksOfNeighborChange(i + 1, j, k, blockID);
                }
                else if (i1 == 3)
                {
                    world.notifyBlocksOfNeighborChange(i, j, k - 1, blockID);
                }
                else if (i1 == 4)
                {
                    world.notifyBlocksOfNeighborChange(i, j, k + 1, blockID);
                }
                else
                {
                    world.notifyBlocksOfNeighborChange(i, j - 1, k, blockID);
                }
            }
            base.onBlockRemoval(world, i, j, k);
        }

        public override bool isPoweringTo(IBlockAccess iblockaccess, int i, int j, int k, int l)
        {
            return (iblockaccess.getBlockMetadata(i, j, k) & 8) > 0;
        }

        public override bool isIndirectlyPoweringTo(World world, int i, int j, int k, int l)
        {
            int i1 = world.getBlockMetadata(i, j, k);
            if ((i1 & 8) == 0)
            {
                return false;
            }
            int j1 = i1 & 7;
            if (j1 == 5 && l == 1)
            {
                return true;
            }
            if (j1 == 4 && l == 2)
            {
                return true;
            }
            if (j1 == 3 && l == 3)
            {
                return true;
            }
            if (j1 == 2 && l == 4)
            {
                return true;
            }
            return j1 == 1 && l == 5;
        }

        public override bool canProvidePower()
        {
            return true;
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            if (world.singleplayerWorld)
            {
                return;
            }
            int l = world.getBlockMetadata(i, j, k);
            if ((l & 8) == 0)
            {
                return;
            }
            world.setBlockMetadataWithNotify(i, j, k, l & 7);
            world.notifyBlocksOfNeighborChange(i, j, k, blockID);
            int i1 = l & 7;
            if (i1 == 1)
            {
                world.notifyBlocksOfNeighborChange(i - 1, j, k, blockID);
            }
            else if (i1 == 2)
            {
                world.notifyBlocksOfNeighborChange(i + 1, j, k, blockID);
            }
            else if (i1 == 3)
            {
                world.notifyBlocksOfNeighborChange(i, j, k - 1, blockID);
            }
            else if (i1 == 4)
            {
                world.notifyBlocksOfNeighborChange(i, j, k + 1, blockID);
            }
            else
            {
                world.notifyBlocksOfNeighborChange(i, j - 1, k, blockID);
            }
            world.playSoundEffect((double) i + 0.5D, (double) j + 0.5D, (double) k + 0.5D, "random.click", 0.3F, 0.5F);
            world.markBlocksDirty(i, j, k, i, j, k);
        }
    }
}