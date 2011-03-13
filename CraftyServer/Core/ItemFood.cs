namespace CraftyServer.Core
{
    public class ItemFood : Item
    {
        public ItemFood(int i, int j) : base(i)
        {
            healAmount = j;
            maxStackSize = 1;
        }

        public override ItemStack onItemRightClick(ItemStack itemstack, World world, EntityPlayer entityplayer)
        {
            itemstack.stackSize--;
            entityplayer.heal(healAmount);
            return itemstack;
        }

        private int healAmount;
    }
}