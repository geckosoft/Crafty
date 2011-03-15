using java.lang;
using java.util;

namespace CraftyServer.Core
{
    public class BlockDoor : Block
    {
        public BlockDoor(int i, Material material)
            : base(i, material)
        {
            blockIndexInTexture = 97;
            if (material == Material.iron)
            {
                blockIndexInTexture++;
            }
            float f = 0.5F;
            float f1 = 1.0F;
            setBlockBounds(0.5F - f, 0.0F, 0.5F - f, 0.5F + f, f1, 0.5F + f);
        }

        public override int func_22009_a(int i, int j)
        {
            if (i == 0 || i == 1)
            {
                return blockIndexInTexture;
            }
            int k = func_271_d(j);
            if ((k == 0 || k == 2) ^ (i <= 3))
            {
                return blockIndexInTexture;
            }
            int l = k/2 + (i & 1 ^ k);
            l += (j & 4)/4;
            int i1 = blockIndexInTexture - (j & 8)*2;
            if ((l & 1) != 0)
            {
                i1 = -i1;
            }
            return i1;
        }

        public override bool isOpaqueCube()
        {
            return false;
        }

        public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
        {
            setBlockBoundsBasedOnState(world, i, j, k);
            return base.getCollisionBoundingBoxFromPool(world, i, j, k);
        }

        public override void setBlockBoundsBasedOnState(IBlockAccess iblockaccess, int i, int j, int k)
        {
            func_273_b(func_271_d(iblockaccess.getBlockMetadata(i, j, k)));
        }

        public void func_273_b(int i)
        {
            float f = 0.1875F;
            setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 2.0F, 1.0F);
            if (i == 0)
            {
                setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, f);
            }
            if (i == 1)
            {
                setBlockBounds(1.0F - f, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
            }
            if (i == 2)
            {
                setBlockBounds(0.0F, 0.0F, 1.0F - f, 1.0F, 1.0F, 1.0F);
            }
            if (i == 3)
            {
                setBlockBounds(0.0F, 0.0F, 0.0F, f, 1.0F, 1.0F);
            }
        }

        public override void onBlockClicked(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            blockActivated(world, i, j, k, entityplayer);
        }

        public override bool blockActivated(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            if (blockMaterial == Material.iron)
            {
                return true;
            }
            int l = world.getBlockMetadata(i, j, k);
            if ((l & 8) != 0)
            {
                if (world.getBlockId(i, j - 1, k) == blockID)
                {
                    blockActivated(world, i, j - 1, k, entityplayer);
                }
                return true;
            }
            if (world.getBlockId(i, j + 1, k) == blockID)
            {
                world.setBlockMetadataWithNotify(i, j + 1, k, (l ^ 4) + 8);
            }
            world.setBlockMetadataWithNotify(i, j, k, l ^ 4);
            world.markBlocksDirty(i, j - 1, k, i, j, k);
            if (Math.random() < 0.5D)
            {
                world.playSoundEffect(i + 0.5D, j + 0.5D, k + 0.5D, "random.door_open", 1.0F,
                                      world.rand.nextFloat()*0.1F + 0.9F);
            }
            else
            {
                world.playSoundEffect(i + 0.5D, j + 0.5D, k + 0.5D, "random.door_close", 1.0F,
                                      world.rand.nextFloat()*0.1F + 0.9F);
            }
            return true;
        }

        public void func_272_a(World world, int i, int j, int k, bool flag)
        {
            int l = world.getBlockMetadata(i, j, k);
            if ((l & 8) != 0)
            {
                if (world.getBlockId(i, j - 1, k) == blockID)
                {
                    func_272_a(world, i, j - 1, k, flag);
                }
                return;
            }
            bool flag1 = (world.getBlockMetadata(i, j, k) & 4) > 0;
            if (flag1 == flag)
            {
                return;
            }
            if (world.getBlockId(i, j + 1, k) == blockID)
            {
                world.setBlockMetadataWithNotify(i, j + 1, k, (l ^ 4) + 8);
            }
            world.setBlockMetadataWithNotify(i, j, k, l ^ 4);
            world.markBlocksDirty(i, j - 1, k, i, j, k);
            if (Math.random() < 0.5D)
            {
                world.playSoundEffect(i + 0.5D, j + 0.5D, k + 0.5D, "random.door_open", 1.0F,
                                      world.rand.nextFloat()*0.1F + 0.9F);
            }
            else
            {
                world.playSoundEffect(i + 0.5D, j + 0.5D, k + 0.5D, "random.door_close", 1.0F,
                                      world.rand.nextFloat()*0.1F + 0.9F);
            }
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            int i1 = world.getBlockMetadata(i, j, k);
            if ((i1 & 8) != 0)
            {
                if (world.getBlockId(i, j - 1, k) != blockID)
                {
                    world.setBlockWithNotify(i, j, k, 0);
                }
                if (l > 0 && blocksList[l].canProvidePower())
                {
                    onNeighborBlockChange(world, i, j - 1, k, l);
                }
            }
            else
            {
                bool flag = false;
                if (world.getBlockId(i, j + 1, k) != blockID)
                {
                    world.setBlockWithNotify(i, j, k, 0);
                    flag = true;
                }
                if (!world.isBlockOpaqueCube(i, j - 1, k))
                {
                    world.setBlockWithNotify(i, j, k, 0);
                    flag = true;
                    if (world.getBlockId(i, j + 1, k) == blockID)
                    {
                        world.setBlockWithNotify(i, j + 1, k, 0);
                    }
                }
                if (flag)
                {
                    if (!world.singleplayerWorld)
                    {
                        dropBlockAsItem(world, i, j, k, i1);
                    }
                }
                else if (l > 0 && blocksList[l].canProvidePower())
                {
                    bool flag1 = world.isBlockIndirectlyGettingPowered(i, j, k) ||
                                 world.isBlockIndirectlyGettingPowered(i, j + 1, k);
                    func_272_a(world, i, j, k, flag1);
                }
            }
        }

        public override int idDropped(int i, Random random)
        {
            if ((i & 8) != 0)
            {
                return 0;
            }
            if (blockMaterial == Material.iron)
            {
                return Item.doorSteel.shiftedIndex;
            }
            else
            {
                return Item.doorWood.shiftedIndex;
            }
        }

        public override MovingObjectPosition collisionRayTrace(World world, int i, int j, int k, Vec3D vec3d,
                                                               Vec3D vec3d1)
        {
            setBlockBoundsBasedOnState(world, i, j, k);
            return base.collisionRayTrace(world, i, j, k, vec3d, vec3d1);
        }

        public int func_271_d(int i)
        {
            if ((i & 4) == 0)
            {
                return i - 1 & 3;
            }
            else
            {
                return i & 3;
            }
        }

        public override bool canPlaceBlockAt(World world, int i, int j, int k)
        {
            if (j >= 127)
            {
                return false;
            }
            else
            {
                return world.isBlockOpaqueCube(i, j - 1, k) && base.canPlaceBlockAt(world, i, j, k) &&
                       base.canPlaceBlockAt(world, i, j + 1, k);
            }
        }
    }
}