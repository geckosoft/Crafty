namespace CraftyServer.Core
{
    public class ItemEgg : Item
    {
        public ItemEgg(int i)
            : base(i)
        {
            maxStackSize = 16;
        }

        public override ItemStack onItemRightClick(ItemStack itemstack, World world, EntityPlayer entityplayer)
        {
            itemstack.stackSize--;
            world.playSoundAtEntity(entityplayer, "random.bow", 0.5F, 0.4F/(itemRand.nextFloat()*0.4F + 0.8F));
            if (!world.singleplayerWorld)
            {
                world.entityJoinedWorld(new EntityEgg(world, entityplayer));
            }
            return itemstack;
        }
    }
}