namespace CraftyServer.Core
{
    public class BlockPumpkin : Block
    {
        private readonly bool blockType;

        public BlockPumpkin(int i, int j, bool flag)
            : base(i, Material.pumpkin)
        {
            blockIndexInTexture = j;
            setTickOnLoad(true);
            blockType = flag;
        }

        public override int func_22009_a(int i, int j)
        {
            if (i == 1)
            {
                return blockIndexInTexture;
            }
            if (i == 0)
            {
                return blockIndexInTexture;
            }
            int k = blockIndexInTexture + 1 + 16;
            if (blockType)
            {
                k++;
            }
            if (j == 0 && i == 2)
            {
                return k;
            }
            if (j == 1 && i == 5)
            {
                return k;
            }
            if (j == 2 && i == 3)
            {
                return k;
            }
            if (j == 3 && i == 4)
            {
                return k;
            }
            else
            {
                return blockIndexInTexture + 16;
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
                return blockIndexInTexture;
            }
            if (i == 3)
            {
                return blockIndexInTexture + 1 + 16;
            }
            else
            {
                return blockIndexInTexture + 16;
            }
        }

        public override void onBlockAdded(World world, int i, int j, int k)
        {
            base.onBlockAdded(world, i, j, k);
        }

        public override bool canPlaceBlockAt(World world, int i, int j, int k)
        {
            int l = world.getBlockId(i, j, k);
            return (l == 0 || blocksList[l].blockMaterial.getIsLiquid()) && world.isBlockOpaqueCube(i, j - 1, k);
        }

        public override void onBlockPlacedBy(World world, int i, int j, int k, EntityLiving entityliving)
        {
            int l = MathHelper.floor_double(((entityliving.rotationYaw*4F)/360F) + 0.5D) & 3;
            world.setBlockMetadataWithNotify(i, j, k, l);
        }
    }
}