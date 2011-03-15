using java.util;

namespace CraftyServer.Core
{
    public class BlockRedstoneTorch : BlockTorch
    {
        private static readonly List torchUpdates = new ArrayList();
        private readonly bool torchActive;

        public BlockRedstoneTorch(int i, int j, bool flag)
            : base(i, j)
        {
            torchActive = false;
            torchActive = flag;
            setTickOnLoad(true);
        }

        public override int func_22009_a(int i, int j)
        {
            if (i == 1)
            {
                return redstoneWire.func_22009_a(i, j);
            }
            else
            {
                return base.func_22009_a(i, j);
            }
        }

        private bool checkForBurnout(World world, int i, int j, int k, bool flag)
        {
            if (flag)
            {
                torchUpdates.add(new RedstoneUpdateInfo(i, j, k, world.getWorldTime()));
            }
            int l = 0;
            for (int i1 = 0; i1 < torchUpdates.size(); i1++)
            {
                var redstoneupdateinfo = (RedstoneUpdateInfo) torchUpdates.get(i1);
                if (redstoneupdateinfo.x == i && redstoneupdateinfo.y == j && redstoneupdateinfo.z == k && ++l >= 8)
                {
                    return true;
                }
            }

            return false;
        }

        public override int tickRate()
        {
            return 2;
        }

        public override void onBlockAdded(World world, int i, int j, int k)
        {
            if (world.getBlockMetadata(i, j, k) == 0)
            {
                base.onBlockAdded(world, i, j, k);
            }
            if (torchActive)
            {
                world.notifyBlocksOfNeighborChange(i, j - 1, k, blockID);
                world.notifyBlocksOfNeighborChange(i, j + 1, k, blockID);
                world.notifyBlocksOfNeighborChange(i - 1, j, k, blockID);
                world.notifyBlocksOfNeighborChange(i + 1, j, k, blockID);
                world.notifyBlocksOfNeighborChange(i, j, k - 1, blockID);
                world.notifyBlocksOfNeighborChange(i, j, k + 1, blockID);
            }
        }

        public override void onBlockRemoval(World world, int i, int j, int k)
        {
            if (torchActive)
            {
                world.notifyBlocksOfNeighborChange(i, j - 1, k, blockID);
                world.notifyBlocksOfNeighborChange(i, j + 1, k, blockID);
                world.notifyBlocksOfNeighborChange(i - 1, j, k, blockID);
                world.notifyBlocksOfNeighborChange(i + 1, j, k, blockID);
                world.notifyBlocksOfNeighborChange(i, j, k - 1, blockID);
                world.notifyBlocksOfNeighborChange(i, j, k + 1, blockID);
            }
        }

        public override bool isPoweringTo(IBlockAccess iblockaccess, int i, int j, int k, int l)
        {
            if (!torchActive)
            {
                return false;
            }
            int i1 = iblockaccess.getBlockMetadata(i, j, k);
            if (i1 == 5 && l == 1)
            {
                return false;
            }
            if (i1 == 3 && l == 3)
            {
                return false;
            }
            if (i1 == 4 && l == 2)
            {
                return false;
            }
            if (i1 == 1 && l == 5)
            {
                return false;
            }
            return i1 != 2 || l != 4;
        }

        private bool func_22016_g(World world, int i, int j, int k)
        {
            int l = world.getBlockMetadata(i, j, k);
            if (l == 5 && world.isBlockIndirectlyProvidingPowerTo(i, j - 1, k, 0))
            {
                return true;
            }
            if (l == 3 && world.isBlockIndirectlyProvidingPowerTo(i, j, k - 1, 2))
            {
                return true;
            }
            if (l == 4 && world.isBlockIndirectlyProvidingPowerTo(i, j, k + 1, 3))
            {
                return true;
            }
            if (l == 1 && world.isBlockIndirectlyProvidingPowerTo(i - 1, j, k, 4))
            {
                return true;
            }
            return l == 2 && world.isBlockIndirectlyProvidingPowerTo(i + 1, j, k, 5);
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            bool flag = func_22016_g(world, i, j, k);
            for (;
                torchUpdates.size() > 0 &&
                world.getWorldTime() - ((RedstoneUpdateInfo) torchUpdates.get(0)).updateTime > 100L;
                torchUpdates.remove(0))
            {
            }
            if (torchActive)
            {
                if (flag)
                {
                    world.setBlockAndMetadataWithNotify(i, j, k, torchRedstoneIdle.blockID,
                                                        world.getBlockMetadata(i, j, k));
                    if (checkForBurnout(world, i, j, k, true))
                    {
                        world.playSoundEffect(i + 0.5F, j + 0.5F, k + 0.5F, "random.fizz", 0.5F,
                                              2.6F + (world.rand.nextFloat() - world.rand.nextFloat())*0.8F);
                        for (int l = 0; l < 5; l++)
                        {
                            double d = i + random.nextDouble()*0.59999999999999998D + 0.20000000000000001D;
                            double d1 = j + random.nextDouble()*0.59999999999999998D + 0.20000000000000001D;
                            double d2 = k + random.nextDouble()*0.59999999999999998D + 0.20000000000000001D;
                            world.spawnParticle("smoke", d, d1, d2, 0.0D, 0.0D, 0.0D);
                        }
                    }
                }
            }
            else if (!flag && !checkForBurnout(world, i, j, k, false))
            {
                world.setBlockAndMetadataWithNotify(i, j, k, torchRedstoneActive.blockID,
                                                    world.getBlockMetadata(i, j, k));
            }
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            base.onNeighborBlockChange(world, i, j, k, l);
            world.func_22074_c(i, j, k, blockID, tickRate());
        }

        public override bool isIndirectlyPoweringTo(World world, int i, int j, int k, int l)
        {
            if (l == 0)
            {
                return isPoweringTo(world, i, j, k, l);
            }
            else
            {
                return false;
            }
        }

        public override int idDropped(int i, Random random)
        {
            return torchRedstoneActive.blockID;
        }

        public override bool canProvidePower()
        {
            return true;
        }
    }
}