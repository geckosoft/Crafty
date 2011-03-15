using java.lang;
using java.util;

namespace CraftyServer.Core
{
    public class BlockSign : BlockContainer
    {
        private readonly bool isFreestanding;
        private readonly Class signEntityClass;

        public BlockSign(int i, Class class1, bool flag)
            : base(i, Material.wood)
        {
            isFreestanding = flag;
            blockIndexInTexture = 4;
            signEntityClass = class1;
            float f = 0.25F;
            float f1 = 1.0F;
            setBlockBounds(0.5F - f, 0.0F, 0.5F - f, 0.5F + f, f1, 0.5F + f);
        }

        public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
        {
            return null;
        }

        public override void setBlockBoundsBasedOnState(IBlockAccess iblockaccess, int i, int j, int k)
        {
            if (isFreestanding)
            {
                return;
            }
            int l = iblockaccess.getBlockMetadata(i, j, k);
            float f = 0.28125F;
            float f1 = 0.78125F;
            float f2 = 0.0F;
            float f3 = 1.0F;
            float f4 = 0.125F;
            setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
            if (l == 2)
            {
                setBlockBounds(f2, f, 1.0F - f4, f3, f1, 1.0F);
            }
            if (l == 3)
            {
                setBlockBounds(f2, f, 0.0F, f3, f1, f4);
            }
            if (l == 4)
            {
                setBlockBounds(1.0F - f4, f, f2, 1.0F, f1, f3);
            }
            if (l == 5)
            {
                setBlockBounds(0.0F, f, f2, f4, f1, f3);
            }
        }

        public override bool isOpaqueCube()
        {
            return false;
        }

        protected override TileEntity getBlockEntity()
        {
            try
            {
                return (TileEntity) signEntityClass.newInstance();
            }
            catch (Exception exception)
            {
                throw new RuntimeException(exception);
            }
        }

        public override int idDropped(int i, Random random)
        {
            return Item.sign.shiftedIndex;
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            bool flag = false;
            if (isFreestanding)
            {
                if (!world.getBlockMaterial(i, j - 1, k).isSolid())
                {
                    flag = true;
                }
            }
            else
            {
                int i1 = world.getBlockMetadata(i, j, k);
                flag = true;
                if (i1 == 2 && world.getBlockMaterial(i, j, k + 1).isSolid())
                {
                    flag = false;
                }
                if (i1 == 3 && world.getBlockMaterial(i, j, k - 1).isSolid())
                {
                    flag = false;
                }
                if (i1 == 4 && world.getBlockMaterial(i + 1, j, k).isSolid())
                {
                    flag = false;
                }
                if (i1 == 5 && world.getBlockMaterial(i - 1, j, k).isSolid())
                {
                    flag = false;
                }
            }
            if (flag)
            {
                dropBlockAsItem(world, i, j, k, world.getBlockMetadata(i, j, k));
                world.setBlockWithNotify(i, j, k, 0);
            }
            base.onNeighborBlockChange(world, i, j, k, l);
        }
    }
}