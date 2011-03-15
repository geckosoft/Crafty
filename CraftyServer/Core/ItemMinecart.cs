namespace CraftyServer.Core
{
    public class ItemMinecart : Item
    {
        public int minecartType;

        public ItemMinecart(int i, int j)
            : base(i)
        {
            maxStackSize = 1;
            minecartType = j;
        }

        public override bool onItemUse(ItemStack itemstack, EntityPlayer entityplayer, World world, int i, int j, int k,
                                       int l)
        {
            int i1 = world.getBlockId(i, j, k);
            if (i1 == Block.minecartTrack.blockID)
            {
                if (!world.singleplayerWorld)
                {
                    world.entityJoinedWorld(new EntityMinecart(world, i + 0.5F, j + 0.5F,
                                                               k + 0.5F, minecartType));
                }
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