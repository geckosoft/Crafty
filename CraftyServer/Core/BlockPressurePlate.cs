using java.util;


namespace CraftyServer.Core
{
    public class BlockPressurePlate : Block
    {
        public BlockPressurePlate(int i, int j, EnumMobType enummobtype)
            : base(i, j, Material.rock)
        {
            triggerMobType = enummobtype;
            setTickOnLoad(true);
            float f = 0.0625F;
            setBlockBounds(f, 0.0F, f, 1.0F - f, 0.03125F, 1.0F - f);
        }

        public override int tickRate()
        {
            return 20;
        }

        public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
        {
            return null;
        }

        public override bool isOpaqueCube()
        {
            return false;
        }

        public override bool canPlaceBlockAt(World world, int i, int j, int k)
        {
            return world.isBlockOpaqueCube(i, j - 1, k);
        }

        public override void onBlockAdded(World world, int i, int j, int k)
        {
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            bool flag = false;
            if (!world.isBlockOpaqueCube(i, j - 1, k))
            {
                flag = true;
            }
            if (flag)
            {
                dropBlockAsItem(world, i, j, k, world.getBlockMetadata(i, j, k));
                world.setBlockWithNotify(i, j, k, 0);
            }
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            if (world.singleplayerWorld)
            {
                return;
            }
            if (world.getBlockMetadata(i, j, k) == 0)
            {
                return;
            }
            else
            {
                setStateIfMobInteractsWithPlate(world, i, j, k);
                return;
            }
        }

        public override void onEntityCollidedWithBlock(World world, int i, int j, int k, Entity entity)
        {
            if (world.singleplayerWorld)
            {
                return;
            }
            if (world.getBlockMetadata(i, j, k) == 1)
            {
                return;
            }
            else
            {
                setStateIfMobInteractsWithPlate(world, i, j, k);
                return;
            }
        }

        private void setStateIfMobInteractsWithPlate(World world, int i, int j, int k)
        {
            bool flag = world.getBlockMetadata(i, j, k) == 1;
            bool flag1 = false;
            float f = 0.125F;
            List list = null;
            if (triggerMobType == EnumMobType.everything)
            {
                list = world.getEntitiesWithinAABBExcludingEntity(null,
                                                                  AxisAlignedBB.getBoundingBoxFromPool((float) i + f, j,
                                                                                                       (float) k + f,
                                                                                                       (float) (i + 1) -
                                                                                                       f,
                                                                                                       (double) j +
                                                                                                       0.25D,
                                                                                                       (float) (k + 1) -
                                                                                                       f));
            }
            if (triggerMobType == EnumMobType.mobs)
            {
                list = world.getEntitiesWithinAABB(typeof (EntityLiving),
                                                   AxisAlignedBB.getBoundingBoxFromPool((float) i + f, j, (float) k + f,
                                                                                        (float) (i + 1) - f,
                                                                                        (double) j + 0.25D,
                                                                                        (float) (k + 1) - f));
            }
            if (triggerMobType == EnumMobType.players)
            {
                list = world.getEntitiesWithinAABB(typeof (EntityPlayer),
                                                   AxisAlignedBB.getBoundingBoxFromPool((float) i + f, j, (float) k + f,
                                                                                        (float) (i + 1) - f,
                                                                                        (double) j + 0.25D,
                                                                                        (float) (k + 1) - f));
            }
            if (list.size() > 0)
            {
                flag1 = true;
            }
            if (flag1 && !flag)
            {
                world.setBlockMetadataWithNotify(i, j, k, 1);
                world.notifyBlocksOfNeighborChange(i, j, k, blockID);
                world.notifyBlocksOfNeighborChange(i, j - 1, k, blockID);
                world.markBlocksDirty(i, j, k, i, j, k);
                world.playSoundEffect((double) i + 0.5D, (double) j + 0.10000000000000001D, (double) k + 0.5D,
                                      "random.click", 0.3F, 0.6F);
            }
            if (!flag1 && flag)
            {
                world.setBlockMetadataWithNotify(i, j, k, 0);
                world.notifyBlocksOfNeighborChange(i, j, k, blockID);
                world.notifyBlocksOfNeighborChange(i, j - 1, k, blockID);
                world.markBlocksDirty(i, j, k, i, j, k);
                world.playSoundEffect((double) i + 0.5D, (double) j + 0.10000000000000001D, (double) k + 0.5D,
                                      "random.click", 0.3F, 0.5F);
            }
            if (flag1)
            {
                world.func_22074_c(i, j, k, blockID, tickRate());
            }
        }

        public override void onBlockRemoval(World world, int i, int j, int k)
        {
            int l = world.getBlockMetadata(i, j, k);
            if (l > 0)
            {
                world.notifyBlocksOfNeighborChange(i, j, k, blockID);
                world.notifyBlocksOfNeighborChange(i, j - 1, k, blockID);
            }
            base.onBlockRemoval(world, i, j, k);
        }

        public override void setBlockBoundsBasedOnState(IBlockAccess iblockaccess, int i, int j, int k)
        {
            bool flag = iblockaccess.getBlockMetadata(i, j, k) == 1;
            float f = 0.0625F;
            if (flag)
            {
                setBlockBounds(f, 0.0F, f, 1.0F - f, 0.03125F, 1.0F - f);
            }
            else
            {
                setBlockBounds(f, 0.0F, f, 1.0F - f, 0.0625F, 1.0F - f);
            }
        }

        public override bool isPoweringTo(IBlockAccess iblockaccess, int i, int j, int k, int l)
        {
            return iblockaccess.getBlockMetadata(i, j, k) > 0;
        }

        public override bool isIndirectlyPoweringTo(World world, int i, int j, int k, int l)
        {
            if (world.getBlockMetadata(i, j, k) == 0)
            {
                return false;
            }
            else
            {
                return l == 1;
            }
        }

        public override bool canProvidePower()
        {
            return true;
        }

        private EnumMobType triggerMobType;
    }
}