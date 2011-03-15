namespace CraftyServer.Core
{
    public class ItemBed : Item
    {
        public ItemBed(int i) : base(i)
        {
        }

        public override bool onItemUse(ItemStack itemstack, EntityPlayer entityplayer, World world, int i, int j, int k,
                                       int l)
        {
            if (l != 1)
            {
                return false;
            }
            j++;
            var blockbed = (BlockBed) Block.bed;
            int i1 = MathHelper.floor_double(((entityplayer.rotationYaw*4F)/360F) + 0.5D) & 3;
            sbyte byte0 = 0;
            sbyte byte1 = 0;
            if (i1 == 0)
            {
                byte1 = 1;
            }
            if (i1 == 1)
            {
                byte0 = -1;
            }
            if (i1 == 2)
            {
                byte1 = -1;
            }
            if (i1 == 3)
            {
                byte0 = 1;
            }
            if (world.isAirBlock(i, j, k) && world.isAirBlock(i + byte0, j, k + byte1) &&
                world.isBlockOpaqueCube(i, j - 1, k) && world.isBlockOpaqueCube(i + byte0, j - 1, k + byte1))
            {
                world.setBlockAndMetadataWithNotify(i, j, k, blockbed.blockID, i1);
                world.setBlockAndMetadataWithNotify(i + byte0, j, k + byte1, blockbed.blockID, i1 + 8);
                itemstack.stackSize--;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}