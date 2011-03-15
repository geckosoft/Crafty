namespace CraftyServer.Core
{
    public class ItemSoup : ItemFood
    {
        public ItemSoup(int i, int j)
            : base(i, j)
        {
        }

        public override ItemStack onItemRightClick(ItemStack itemstack, World world, EntityPlayer entityplayer)
        {
            base.onItemRightClick(itemstack, world, entityplayer);
            return new ItemStack(bowlEmpty);
        }
    }
}