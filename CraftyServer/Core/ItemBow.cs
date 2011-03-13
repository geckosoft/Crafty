namespace CraftyServer.Core
{
    public class ItemBow : Item
    {
        public ItemBow(int i) : base(i)
        {
            maxStackSize = 1;
        }

        public override ItemStack onItemRightClick(ItemStack itemstack, World world, EntityPlayer entityplayer)
        {
            if (entityplayer.inventory.consumeInventoryItem(Item.arrow.shiftedIndex))
            {
                world.playSoundAtEntity(entityplayer, "random.bow", 1.0F, 1.0F/(itemRand.nextFloat()*0.4F + 0.8F));
                if (!world.singleplayerWorld)
                {
                    world.entityJoinedWorld(new EntityArrow(world, entityplayer));
                }
            }
            return itemstack;
        }
    }
}