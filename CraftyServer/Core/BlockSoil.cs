using java.util;

namespace CraftyServer.Core
{
    public class BlockSoil : Block
    {
        public BlockSoil(int i)
            : base(i, Material.ground)
        {
            blockIndexInTexture = 87;
            setTickOnLoad(true);
            setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.9375F, 1.0F);
            setLightOpacity(255);
        }

        public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
        {
            return AxisAlignedBB.getBoundingBoxFromPool(i + 0, j + 0, k + 0, i + 1, j + 1, k + 1);
        }

        public override bool isOpaqueCube()
        {
            return false;
        }

        public override int func_22009_a(int i, int j)
        {
            if (i == 1 && j > 0)
            {
                return blockIndexInTexture - 1;
            }
            if (i == 1)
            {
                return blockIndexInTexture;
            }
            else
            {
                return 2;
            }
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            if (random.nextInt(5) == 0)
            {
                if (isWaterNearby(world, i, j, k))
                {
                    world.setBlockMetadataWithNotify(i, j, k, 7);
                }
                else
                {
                    int l = world.getBlockMetadata(i, j, k);
                    if (l > 0)
                    {
                        world.setBlockMetadataWithNotify(i, j, k, l - 1);
                    }
                    else if (!isCropsNearby(world, i, j, k))
                    {
                        world.setBlockWithNotify(i, j, k, dirt.blockID);
                    }
                }
            }
        }

        public override void onEntityWalking(World world, int i, int j, int k, Entity entity)
        {
            if (world.rand.nextInt(4) == 0)
            {
                world.setBlockWithNotify(i, j, k, dirt.blockID);
            }
        }

        private bool isCropsNearby(World world, int i, int j, int k)
        {
            int l = 0;
            for (int i1 = i - l; i1 <= i + l; i1++)
            {
                for (int j1 = k - l; j1 <= k + l; j1++)
                {
                    if (world.getBlockId(i1, j + 1, j1) == crops.blockID)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool isWaterNearby(World world, int i, int j, int k)
        {
            for (int l = i - 4; l <= i + 4; l++)
            {
                for (int i1 = j; i1 <= j + 1; i1++)
                {
                    for (int j1 = k - 4; j1 <= k + 4; j1++)
                    {
                        if (world.getBlockMaterial(l, i1, j1) == Material.water)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            base.onNeighborBlockChange(world, i, j, k, l);
            Material material = world.getBlockMaterial(i, j + 1, k);
            if (material.isSolid())
            {
                world.setBlockWithNotify(i, j, k, dirt.blockID);
            }
        }

        public override int idDropped(int i, Random random)
        {
            return dirt.idDropped(0, random);
        }
    }
}