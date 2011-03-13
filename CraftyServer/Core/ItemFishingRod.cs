namespace CraftyServer.Core
{
    public class ItemFishingRod : Item
    {
        public ItemFishingRod(int i) : base(i)
        {
            maxDamage = 64;
        }

        public override ItemStack onItemRightClick(ItemStack itemstack, World world, EntityPlayer entityplayer)
        {
            if (entityplayer.fishEntity != null)
            {
                int i = entityplayer.fishEntity.func_6143_c();
                itemstack.damageItem(i);
                entityplayer.swingItem();
            }
            else
            {
                world.playSoundAtEntity(entityplayer, "random.bow", 0.5F, 0.4F/(itemRand.nextFloat()*0.4F + 0.8F));
                if (!world.singleplayerWorld)
                {
                    world.entityJoinedWorld(new EntityFish(world, entityplayer));
                }
                entityplayer.swingItem();
            }
            return itemstack;
        }
    }
}