using java.util;

namespace CraftyServer.Core
{
    public class BlockSand : Block
    {
        public static bool fallInstantly;

        public BlockSand(int i, int j)
            : base(i, j, Material.sand)
        {
        }

        public override void onBlockAdded(World world, int i, int j, int k)
        {
            world.func_22074_c(i, j, k, blockID, tickRate());
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            world.func_22074_c(i, j, k, blockID, tickRate());
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            tryToFall(world, i, j, k);
        }

        private void tryToFall(World world, int i, int j, int k)
        {
            int l = i;
            int i1 = j;
            int j1 = k;
            if (canFallBelow(world, l, i1 - 1, j1) && i1 >= 0)
            {
                byte byte0 = 32;
                if (fallInstantly ||
                    !world.checkChunksExist(i - byte0, j - byte0, k - byte0, i + byte0, j + byte0, k + byte0))
                {
                    world.setBlockWithNotify(i, j, k, 0);
                    for (; canFallBelow(world, i, j - 1, k) && j > 0; j--)
                    {
                    }
                    if (j > 0)
                    {
                        world.setBlockWithNotify(i, j, k, blockID);
                    }
                }
                else
                {
                    var entityfallingsand = new EntityFallingSand(world, i + 0.5F,
                                                                  j + 0.5F, k + 0.5F,
                                                                  blockID);
                    world.entityJoinedWorld(entityfallingsand);
                }
            }
        }

        public override int tickRate()
        {
            return 3;
        }

        public static bool canFallBelow(World world, int i, int j, int k)
        {
            int l = world.getBlockId(i, j, k);
            if (l == 0)
            {
                return true;
            }
            if (l == fire.blockID)
            {
                return true;
            }
            Material material = blocksList[l].blockMaterial;
            if (material == Material.water)
            {
                return true;
            }
            return material == Material.lava;
        }
    }
}