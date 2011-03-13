using java.util;


namespace CraftyServer.Core
{
    public class BlockFire : Block
    {
        public BlockFire(int i, int j)
            : base(i, j, Material.fire)
        {
            chanceToEncourageFire = new int[256];
            abilityToCatchFire = new int[256];
            setBurnRate(Block.planks.blockID, 5, 20);
            setBurnRate(Block.wood.blockID, 5, 5);
            setBurnRate(Block.leaves.blockID, 30, 60);
            setBurnRate(Block.bookShelf.blockID, 30, 20);
            setBurnRate(Block.tnt.blockID, 15, 100);
            setBurnRate(Block.cloth.blockID, 30, 60);
            setTickOnLoad(true);
        }

        private void setBurnRate(int i, int j, int k)
        {
            chanceToEncourageFire[i] = j;
            abilityToCatchFire[i] = k;
        }

        public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
        {
            return null;
        }

        public override bool isOpaqueCube()
        {
            return false;
        }

        public override int quantityDropped(Random random)
        {
            return 0;
        }

        public override int tickRate()
        {
            return 10;
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            bool flag = world.getBlockId(i, j - 1, k) == Block.bloodStone.blockID;
            int l = world.getBlockMetadata(i, j, k);
            if (l < 15)
            {
                world.setBlockMetadataWithNotify(i, j, k, l + 1);
                world.func_22074_c(i, j, k, blockID, tickRate());
            }
            if (!flag && !func_268_g(world, i, j, k))
            {
                if (!world.isBlockOpaqueCube(i, j - 1, k) || l > 3)
                {
                    world.setBlockWithNotify(i, j, k, 0);
                }
                return;
            }
            if (!flag && !canBlockCatchFire(world, i, j - 1, k) && l == 15 && random.nextInt(4) == 0)
            {
                world.setBlockWithNotify(i, j, k, 0);
                return;
            }
            if (l%2 == 0 && l > 2)
            {
                tryToCatchBlockOnFire(world, i + 1, j, k, 300, random);
                tryToCatchBlockOnFire(world, i - 1, j, k, 300, random);
                tryToCatchBlockOnFire(world, i, j - 1, k, 250, random);
                tryToCatchBlockOnFire(world, i, j + 1, k, 250, random);
                tryToCatchBlockOnFire(world, i, j, k - 1, 300, random);
                tryToCatchBlockOnFire(world, i, j, k + 1, 300, random);
                for (int i1 = i - 1; i1 <= i + 1; i1++)
                {
                    for (int j1 = k - 1; j1 <= k + 1; j1++)
                    {
                        for (int k1 = j - 1; k1 <= j + 4; k1++)
                        {
                            if (i1 == i && k1 == j && j1 == k)
                            {
                                continue;
                            }
                            int l1 = 100;
                            if (k1 > j + 1)
                            {
                                l1 += (k1 - (j + 1))*100;
                            }
                            int i2 = getChanceOfNeighborsEncouragingFire(world, i1, k1, j1);
                            if (i2 > 0 && random.nextInt(l1) <= i2)
                            {
                                world.setBlockWithNotify(i1, k1, j1, blockID);
                            }
                        }
                    }
                }
            }
            if (l == 15)
            {
                tryToCatchBlockOnFire(world, i + 1, j, k, 1, random);
                tryToCatchBlockOnFire(world, i - 1, j, k, 1, random);
                tryToCatchBlockOnFire(world, i, j - 1, k, 1, random);
                tryToCatchBlockOnFire(world, i, j + 1, k, 1, random);
                tryToCatchBlockOnFire(world, i, j, k - 1, 1, random);
                tryToCatchBlockOnFire(world, i, j, k + 1, 1, random);
            }
        }

        private void tryToCatchBlockOnFire(World world, int i, int j, int k, int l, Random random)
        {
            int i1 = abilityToCatchFire[world.getBlockId(i, j, k)];
            if (random.nextInt(l) < i1)
            {
                bool flag = world.getBlockId(i, j, k) == Block.tnt.blockID;
                if (random.nextInt(2) == 0)
                {
                    world.setBlockWithNotify(i, j, k, blockID);
                }
                else
                {
                    world.setBlockWithNotify(i, j, k, 0);
                }
                if (flag)
                {
                    Block.tnt.onBlockDestroyedByPlayer(world, i, j, k, 0);
                }
            }
        }

        private bool func_268_g(World world, int i, int j, int k)
        {
            if (canBlockCatchFire(world, i + 1, j, k))
            {
                return true;
            }
            if (canBlockCatchFire(world, i - 1, j, k))
            {
                return true;
            }
            if (canBlockCatchFire(world, i, j - 1, k))
            {
                return true;
            }
            if (canBlockCatchFire(world, i, j + 1, k))
            {
                return true;
            }
            if (canBlockCatchFire(world, i, j, k - 1))
            {
                return true;
            }
            return canBlockCatchFire(world, i, j, k + 1);
        }

        private int getChanceOfNeighborsEncouragingFire(World world, int i, int j, int k)
        {
            int l = 0;
            if (!world.isAirBlock(i, j, k))
            {
                return 0;
            }
            else
            {
                l = getChanceToEncourageFire(world, i + 1, j, k, l);
                l = getChanceToEncourageFire(world, i - 1, j, k, l);
                l = getChanceToEncourageFire(world, i, j - 1, k, l);
                l = getChanceToEncourageFire(world, i, j + 1, k, l);
                l = getChanceToEncourageFire(world, i, j, k - 1, l);
                l = getChanceToEncourageFire(world, i, j, k + 1, l);
                return l;
            }
        }

        public override bool isCollidable()
        {
            return false;
        }

        public bool canBlockCatchFire(IBlockAccess iblockaccess, int i, int j, int k)
        {
            return chanceToEncourageFire[iblockaccess.getBlockId(i, j, k)] > 0;
        }

        public int getChanceToEncourageFire(World world, int i, int j, int k, int l)
        {
            int i1 = chanceToEncourageFire[world.getBlockId(i, j, k)];
            if (i1 > l)
            {
                return i1;
            }
            else
            {
                return l;
            }
        }

        public override bool canPlaceBlockAt(World world, int i, int j, int k)
        {
            return world.isBlockOpaqueCube(i, j - 1, k) || func_268_g(world, i, j, k);
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            if (!world.isBlockOpaqueCube(i, j - 1, k) && !func_268_g(world, i, j, k))
            {
                world.setBlockWithNotify(i, j, k, 0);
                return;
            }
            else
            {
                return;
            }
        }

        public override void onBlockAdded(World world, int i, int j, int k)
        {
            if (world.getBlockId(i, j - 1, k) == Block.obsidian.blockID &&
                Block.portal.tryToCreatePortal(world, i, j, k))
            {
                return;
            }
            if (!world.isBlockOpaqueCube(i, j - 1, k) && !func_268_g(world, i, j, k))
            {
                world.setBlockWithNotify(i, j, k, 0);
                return;
            }
            else
            {
                world.func_22074_c(i, j, k, blockID, tickRate());
                return;
            }
        }

        private int[] chanceToEncourageFire;
        private int[] abilityToCatchFire;
    }
}