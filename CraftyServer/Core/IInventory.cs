namespace CraftyServer.Core
{
    public interface IInventory
    {
        int getSizeInventory();
        ItemStack getStackInSlot(int i);
        ItemStack decrStackSize(int i, int j);
        void setInventorySlotContents(int i, ItemStack itemstack);
        string getInvName();
        int getInventoryStackLimit();
        void onInventoryChanged();
        bool canInteractWith(EntityPlayer entityplayer);
    }
}