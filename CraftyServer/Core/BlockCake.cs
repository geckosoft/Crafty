using java.util;

namespace CraftyServer.Core
{
    public class BlockCake : Block
    {
        public BlockCake(int i, int j)
            : base(i, j, Material.field_21100_y)
        {
            setTickOnLoad(true);
        }

        public override void setBlockBoundsBasedOnState(IBlockAccess iblockaccess, int i, int j, int k)
        {
            int l = iblockaccess.getBlockMetadata(i, j, k);
            float f = 0.0625F;
            float f1 = (1 + l*2)/16F;
            float f2 = 0.5F;
            setBlockBounds(f1, 0.0F, f, 1.0F - f, f2, 1.0F - f);
        }

        public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
        {
            int l = world.getBlockMetadata(i, j, k);
            float f = 0.0625F;
            float f1 = (1 + l*2)/16F;
            float f2 = 0.5F;
            return AxisAlignedBB.getBoundingBoxFromPool(i + f1, j, k + f, (i + 1) - f,
                                                        (j + f2) - f, (k + 1) - f);
        }

        public override int func_22009_a(int i, int j)
        {
            if (i == 1)
            {
                return blockIndexInTexture;
            }
            if (i == 0)
            {
                return blockIndexInTexture + 3;
            }
            if (j > 0 && i == 4)
            {
                return blockIndexInTexture + 2;
            }
            else
            {
                return blockIndexInTexture + 1;
            }
        }

        public override int getBlockTextureFromSide(int i)
        {
            if (i == 1)
            {
                return blockIndexInTexture;
            }
            if (i == 0)
            {
                return blockIndexInTexture + 3;
            }
            else
            {
                return blockIndexInTexture + 1;
            }
        }

        public override bool isOpaqueCube()
        {
            return false;
        }

        public override bool blockActivated(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            func_21030_c(world, i, j, k, entityplayer);
            return true;
        }

        public override void onBlockClicked(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            func_21030_c(world, i, j, k, entityplayer);
        }

        private void func_21030_c(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            if (entityplayer.health < 20)
            {
                entityplayer.heal(3);
                int l = world.getBlockMetadata(i, j, k) + 1;
                if (l >= 6)
                {
                    world.setBlockWithNotify(i, j, k, 0);
                }
                else
                {
                    world.setBlockMetadataWithNotify(i, j, k, l);
                    world.markBlockAsNeedsUpdate(i, j, k);
                }
            }
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
            return world.getBlockMaterial(i, j - 1, k).isSolid();
        }

        public override int quantityDropped(Random random)
        {
            return 0;
        }

        public override int idDropped(int i, Random random)
        {
            return 0;
        }
    }
}