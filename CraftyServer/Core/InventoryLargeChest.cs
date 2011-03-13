namespace CraftyServer.Core
{
    public class InventoryLargeChest
        : IInventory
    {
        public InventoryLargeChest(string s, IInventory iinventory, IInventory iinventory1)
        {
            name = s;
            upperChest = iinventory;
            lowerChest = iinventory1;
        }

        public int getSizeInventory()
        {
            return upperChest.getSizeInventory() + lowerChest.getSizeInventory();
        }

        public string getInvName()
        {
            return name;
        }

        public ItemStack getStackInSlot(int i)
        {
            if (i >= upperChest.getSizeInventory())
            {
                return lowerChest.getStackInSlot(i - upperChest.getSizeInventory());
            }
            else
            {
                return upperChest.getStackInSlot(i);
            }
        }

        public ItemStack decrStackSize(int i, int j)
        {
            if (i >= upperChest.getSizeInventory())
            {
                return lowerChest.decrStackSize(i - upperChest.getSizeInventory(), j);
            }
            else
            {
                return upperChest.decrStackSize(i, j);
            }
        }

        public void setInventorySlotContents(int i, ItemStack itemstack)
        {
            if (i >= upperChest.getSizeInventory())
            {
                lowerChest.setInventorySlotContents(i - upperChest.getSizeInventory(), itemstack);
            }
            else
            {
                upperChest.setInventorySlotContents(i, itemstack);
            }
        }

        public int getInventoryStackLimit()
        {
            return upperChest.getInventoryStackLimit();
        }

        public void onInventoryChanged()
        {
            upperChest.onInventoryChanged();
            lowerChest.onInventoryChanged();
        }

        public virtual bool canInteractWith(EntityPlayer entityplayer)
        {
            return upperChest.canInteractWith(entityplayer) && lowerChest.canInteractWith(entityplayer);
        }

        private string name;
        private IInventory upperChest;
        private IInventory lowerChest;
    }
}