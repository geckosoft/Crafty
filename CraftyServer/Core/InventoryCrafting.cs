namespace CraftyServer.Core
{
    public class InventoryCrafting
        : IInventory
    {
        private readonly CraftingInventoryCB eventHandler;
        private readonly int field_21085_b;
        private readonly ItemStack[] stackList;

        public InventoryCrafting(CraftingInventoryCB craftinginventorycb, int i, int j)
        {
            int k = i*j;
            stackList = new ItemStack[k];
            eventHandler = craftinginventorycb;
            field_21085_b = i;
        }

        #region IInventory Members

        public int getSizeInventory()
        {
            return stackList.Length;
        }

        public ItemStack getStackInSlot(int i)
        {
            if (i >= getSizeInventory())
            {
                return null;
            }
            else
            {
                return stackList[i];
            }
        }

        public string getInvName()
        {
            return "Crafting";
        }

        public ItemStack decrStackSize(int i, int j)
        {
            if (stackList[i] != null)
            {
                if (stackList[i].stackSize <= j)
                {
                    ItemStack itemstack = stackList[i];
                    stackList[i] = null;
                    eventHandler.onCraftMatrixChanged(this);
                    return itemstack;
                }
                ItemStack itemstack1 = stackList[i].splitStack(j);
                if (stackList[i].stackSize == 0)
                {
                    stackList[i] = null;
                }
                eventHandler.onCraftMatrixChanged(this);
                return itemstack1;
            }
            else
            {
                return null;
            }
        }

        public void setInventorySlotContents(int i, ItemStack itemstack)
        {
            stackList[i] = itemstack;
            eventHandler.onCraftMatrixChanged(this);
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

        public ItemStack func_21084_a(int i, int j)
        {
            if (i < 0 || i >= field_21085_b)
            {
                return null;
            }
            else
            {
                int k = i + j*field_21085_b;
                return getStackInSlot(k);
            }
        }
    }
}