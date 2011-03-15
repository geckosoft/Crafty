namespace CraftyServer.Core
{
    public class Slot
    {
        private readonly IInventory inventory;
        private readonly int slotIndex;
        public int id;
        public int xDisplayPosition;
        public int yDisplayPosition;

        public Slot(IInventory iinventory, int i, int j, int k)
        {
            inventory = iinventory;
            slotIndex = i;
            xDisplayPosition = j;
            yDisplayPosition = k;
        }

        public virtual void onPickupFromSlot()
        {
            onSlotChanged();
        }

        public virtual bool isItemValid(ItemStack itemstack)
        {
            return true;
        }

        public virtual ItemStack getStack()
        {
            return inventory.getStackInSlot(slotIndex);
        }

        public virtual void putStack(ItemStack itemstack)
        {
            inventory.setInventorySlotContents(slotIndex, itemstack);
            onSlotChanged();
        }

        public virtual void onSlotChanged()
        {
            inventory.onInventoryChanged();
        }

        public virtual int getSlotStackLimit()
        {
            return inventory.getInventoryStackLimit();
        }

        public virtual ItemStack decrStackSize(int i)
        {
            return inventory.decrStackSize(slotIndex, i);
        }

        public virtual bool isHere(IInventory iinventory, int i)
        {
            return iinventory == inventory && i == slotIndex;
        }
    }
}