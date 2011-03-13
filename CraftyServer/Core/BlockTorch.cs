using java.util;


namespace CraftyServer.Core
{
    public class BlockTorch : Block
    {
        public BlockTorch(int i, int j)
            : base(i, j, Material.circuits)
        {
            setTickOnLoad(true);
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
            if (world.isBlockOpaqueCube(i, j, k + 1))
            {
                return true;
            }
            return world.isBlockOpaqueCube(i, j - 1, k);
        }

        public override void onBlockPlaced(World world, int i, int j, int k, int l)
        {
            int i1 = world.getBlockMetadata(i, j, k);
            if (l == 1 && world.isBlockOpaqueCube(i, j - 1, k))
            {
                i1 = 5;
            }
            if (l == 2 && world.isBlockOpaqueCube(i, j, k + 1))
            {
                i1 = 4;
            }
            if (l == 3 && world.isBlockOpaqueCube(i, j, k - 1))
            {
                i1 = 3;
            }
            if (l == 4 && world.isBlockOpaqueCube(i + 1, j, k))
            {
                i1 = 2;
            }
            if (l == 5 && world.isBlockOpaqueCube(i - 1, j, k))
            {
                i1 = 1;
            }
            world.setBlockMetadataWithNotify(i, j, k, i1);
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            base.updateTick(world, i, j, k, random);
            if (world.getBlockMetadata(i, j, k) == 0)
            {
                onBlockAdded(world, i, j, k);
            }
        }

        public override void onBlockAdded(World world, int i, int j, int k)
        {
            if (world.isBlockOpaqueCube(i - 1, j, k))
            {
                world.setBlockMetadataWithNotify(i, j, k, 1);
            }
            else if (world.isBlockOpaqueCube(i + 1, j, k))
            {
                world.setBlockMetadataWithNotify(i, j, k, 2);
            }
            else if (world.isBlockOpaqueCube(i, j, k - 1))
            {
                world.setBlockMetadataWithNotify(i, j, k, 3);
            }
            else if (world.isBlockOpaqueCube(i, j, k + 1))
            {
                world.setBlockMetadataWithNotify(i, j, k, 4);
            }
            else if (world.isBlockOpaqueCube(i, j - 1, k))
            {
                world.setBlockMetadataWithNotify(i, j, k, 5);
            }
            dropTorchIfCantStay(world, i, j, k);
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            if (dropTorchIfCantStay(world, i, j, k))
            {
                int i1 = world.getBlockMetadata(i, j, k);
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
                if (!world.isBlockOpaqueCube(i, j - 1, k) && i1 == 5)
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

        private bool dropTorchIfCantStay(World world, int i, int j, int k)
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

        public override MovingObjectPosition collisionRayTrace(World world, int i, int j, int k, Vec3D vec3d,
                                                               Vec3D vec3d1)
        {
            int l = world.getBlockMetadata(i, j, k) & 7;
            float f = 0.15F;
            if (l == 1)
            {
                setBlockBounds(0.0F, 0.2F, 0.5F - f, f*2.0F, 0.8F, 0.5F + f);
            }
            else if (l == 2)
            {
                setBlockBounds(1.0F - f*2.0F, 0.2F, 0.5F - f, 1.0F, 0.8F, 0.5F + f);
            }
            else if (l == 3)
            {
                setBlockBounds(0.5F - f, 0.2F, 0.0F, 0.5F + f, 0.8F, f*2.0F);
            }
            else if (l == 4)
            {
                setBlockBounds(0.5F - f, 0.2F, 1.0F - f*2.0F, 0.5F + f, 0.8F, 1.0F);
            }
            else
            {
                float f1 = 0.1F;
                setBlockBounds(0.5F - f1, 0.0F, 0.5F - f1, 0.5F + f1, 0.6F, 0.5F + f1);
            }
            return base.collisionRayTrace(world, i, j, k, vec3d, vec3d1);
        }
    }
}