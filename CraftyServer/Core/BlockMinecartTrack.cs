using java.util;

namespace CraftyServer.Core
{
    public class BlockMinecartTrack : Block
    {
        public BlockMinecartTrack(int i, int j)
            : base(i, j, Material.circuits)
        {
            setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.125F, 1.0F);
        }

        public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
        {
            return null;
        }

        public override bool isOpaqueCube()
        {
            return false;
        }

        public override MovingObjectPosition collisionRayTrace(World world, int i, int j, int k, Vec3D vec3d,
                                                               Vec3D vec3d1)
        {
            setBlockBoundsBasedOnState(world, i, j, k);
            return base.collisionRayTrace(world, i, j, k, vec3d, vec3d1);
        }

        public override void setBlockBoundsBasedOnState(IBlockAccess iblockaccess, int i, int j, int k)
        {
            int l = iblockaccess.getBlockMetadata(i, j, k);
            if (l >= 2 && l <= 5)
            {
                setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.625F, 1.0F);
            }
            else
            {
                setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.125F, 1.0F);
            }
        }

        public override int func_22009_a(int i, int j)
        {
            if (j >= 6)
            {
                return blockIndexInTexture - 16;
            }
            else
            {
                return blockIndexInTexture;
            }
        }

        public override int quantityDropped(Random random)
        {
            return 1;
        }

        public override bool canPlaceBlockAt(World world, int i, int j, int k)
        {
            return world.isBlockOpaqueCube(i, j - 1, k);
        }

        public override void onBlockAdded(World world, int i, int j, int k)
        {
            if (!world.singleplayerWorld)
            {
                world.setBlockMetadataWithNotify(i, j, k, 15);
                func_4038_g(world, i, j, k);
            }
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            if (world.singleplayerWorld)
            {
                return;
            }
            int i1 = world.getBlockMetadata(i, j, k);
            bool flag = false;
            if (!world.isBlockOpaqueCube(i, j - 1, k))
            {
                flag = true;
            }
            if (i1 == 2 && !world.isBlockOpaqueCube(i + 1, j, k))
            {
                flag = true;
            }
            if (i1 == 3 && !world.isBlockOpaqueCube(i - 1, j, k))
            {
                flag = true;
            }
            if (i1 == 4 && !world.isBlockOpaqueCube(i, j, k - 1))
            {
                flag = true;
            }
            if (i1 == 5 && !world.isBlockOpaqueCube(i, j, k + 1))
            {
                flag = true;
            }
            if (flag)
            {
                dropBlockAsItem(world, i, j, k, world.getBlockMetadata(i, j, k));
                world.setBlockWithNotify(i, j, k, 0);
            }
            else if (l > 0 && blocksList[l].canProvidePower() &&
                     MinecartTrackLogic.getNAdjacentTracks(new MinecartTrackLogic(this, world, i, j, k)) == 3)
            {
                func_4038_g(world, i, j, k);
            }
        }

        private void func_4038_g(World world, int i, int j, int k)
        {
            if (world.singleplayerWorld)
            {
                return;
            }
            else
            {
                (new MinecartTrackLogic(this, world, i, j, k)).func_596_a(world.isBlockIndirectlyGettingPowered(i, j, k));
                return;
            }
        }
    }
}