namespace CraftyServer.Core
{
    public class ItemFood : Item
    {
        private readonly int healAmount;

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
    }
}