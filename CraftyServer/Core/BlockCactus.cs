using java.util;


namespace CraftyServer.Core
{
    public class BlockCactus : Block
    {
        public BlockCactus(int i, int j)
            : base(i, j, Material.cactus)
        {
            setTickOnLoad(true);
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            if (world.isAirBlock(i, j + 1, k))
            {
                int l;
                for (l = 1; world.getBlockId(i, j - l, k) == blockID; l++)
                {
                }
                if (l < 3)
                {
                    int i1 = world.getBlockMetadata(i, j, k);
                    if (i1 == 15)
                    {
                        world.setBlockWithNotify(i, j + 1, k, blockID);
                        world.setBlockMetadataWithNotify(i, j, k, 0);
                    }
                    else
                    {
                        world.setBlockMetadataWithNotify(i, j, k, i1 + 1);
                    }
                }
            }
        }

        public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
        {
            float f = 0.0625F;
            return AxisAlignedBB.getBoundingBoxFromPool((float) i + f, j, (float) k + f, (float) (i + 1) - f,
                                                        (float) (j + 1) - f, (float) (k + 1) - f);
        }

        public override int getBlockTextureFromSide(int i)
        {
            if (i == 1)
            {
                return blockIndexInTexture - 1;
            }
            if (i == 0)
            {
                return blockIndexInTexture + 1;
            }
            else
            {
                return blockIndexInTexture;
            }
        }

        public override bool isOpaqueCube()
        {
            return false;
        }

        public override bool canPlaceBlockAt(World world, int i, int j, int k)
        {
            if (!base.canPlaceBlockAt(world, i, j, k))
            {
                return false;
            }
            else
            {
                return canBlockStay(world, i, j, k);
            }
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            if (!canBlockStay(world, i, j, k))
            {
                dropBlockAsItem(world, i, j, k, world.getBlockMetadata(i, j, k));
                world.setBlockWithNotify(i, j, k, 0);
            }
        }

        public override bool canBlockStay(World world, int i, int j, int k)
        {
            if (world.getBlockMaterial(i - 1, j, k).isSolid())
            {
                return false;
            }
            if (world.getBlockMaterial(i + 1, j, k).isSolid())
            {
                return false;
            }
            if (world.getBlockMaterial(i, j, k - 1).isSolid())
            {
                return false;
            }
            if (world.getBlockMaterial(i, j, k + 1).isSolid())
            {
                return false;
            }
            else
            {
                int l = world.getBlockId(i, j - 1, k);
                return l == Block.cactus.blockID || l == Block.sand.blockID;
            }
        }

        public override void onEntityCollidedWithBlock(World world, int i, int j, int k, Entity entity)
        {
            entity.attackEntityFrom(null, 1);
        }
    }
}