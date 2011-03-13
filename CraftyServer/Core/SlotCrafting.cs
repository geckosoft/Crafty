namespace CraftyServer.Core
{
    public class SlotCrafting : Slot
    {
        public SlotCrafting(IInventory iinventory, IInventory iinventory1, int i, int j, int k)
            : base(iinventory1, i, j, k)
        {
            craftMatrix = iinventory;
        }

        public override bool isItemValid(ItemStack itemstack)
        {
            return false;
        }

        public override void onPickupFromSlot()
        {
            for (int i = 0; i < craftMatrix.getSizeInventory(); i++)
            {
                ItemStack itemstack = craftMatrix.getStackInSlot(i);
                if (itemstack == null)
                {
                    continue;
                }
                craftMatrix.decrStackSize(i, 1);
                if (itemstack.getItem().hasContainerItem())
                {
                    craftMatrix.setInventorySlotContents(i, new ItemStack(itemstack.getItem().getContainerItem()));
                }
            }
        }

        private IInventory craftMatrix;
    }
}