namespace CraftyServer.Core
{
    public class InventoryCraftResult
        : IInventory
    {
        private readonly ItemStack[] stackResult;

        public InventoryCraftResult()
        {
            stackResult = new ItemStack[1];
        }

        #region IInventory Members

        public int getSizeInventory()
        {
            return 1;
        }

        public ItemStack getStackInSlot(int i)
        {
            return stackResult[i];
        }

        public string getInvName()
        {
            return "Result";
        }

        public ItemStack decrStackSize(int i, int j)
        {
            if (stackResult[i] != null)
            {
                ItemStack itemstack = stackResult[i];
                stackResult[i] = null;
                return itemstack;
            }
            else
            {
                return null;
            }
        }

        public void setInventorySlotContents(int i, ItemStack itemstack)
        {
            stackResult[i] = itemstack;
        }

        public int getInventoryStackLimit()
        {
            return 64;
        }

        public void onInventoryChanged()
        {
        }

        public virtual bool canInteractWith(EntityPlayer entityplayer)
        {
            return true;
        }

        #endregion
    }
}