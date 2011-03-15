namespace CraftyServer.Core
{
    public class ItemSeeds : Item
    {
        private readonly int field_271_a;

        public ItemSeeds(int i, int j) : base(i)
        {
            field_271_a = j;
        }

        public override bool onItemUse(ItemStack itemstack, EntityPlayer entityplayer, World world, int i, int j, int k,
                                       int l)
        {
            if (l != 1)
            {
                return false;
            }
            int i1 = world.getBlockId(i, j, k);
            if (i1 == Block.tilledField.blockID && world.isAirBlock(i, j + 1, k))
            {
                world.setBlockWithNotify(i, j + 1, k, field_271_a);
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