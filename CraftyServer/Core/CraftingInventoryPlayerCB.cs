namespace CraftyServer.Core
{
    public class CraftingInventoryPlayerCB : CraftingInventoryCB
    {
        public CraftingInventoryPlayerCB(InventoryPlayer inventoryplayer)
            : this(inventoryplayer, true)
        {
        }

        public CraftingInventoryPlayerCB(InventoryPlayer inventoryplayer, bool flag)
        {
            craftMatrix = new InventoryCrafting(this, 2, 2);
            craftResult = new InventoryCraftResult();
            isMP = false;
            isMP = flag;
            addSlot(new SlotCrafting(craftMatrix, craftResult, 0, 144, 36));
            for (int i = 0; i < 2; i++)
            {
                for (int i1 = 0; i1 < 2; i1++)
                {
                    addSlot(new Slot(craftMatrix, i1 + i*2, 88 + i1*18, 26 + i*18));
                }
            }

            for (int j = 0; j < 4; j++)
            {
                int j1 = j;
                addSlot(new SlotArmor(this, inventoryplayer, inventoryplayer.getSizeInventory() - 1 - j, 8, 8 + j*18, j1));
            }

            for (int k = 0; k < 3; k++)
            {
                for (int k1 = 0; k1 < 9; k1++)
                {
                    addSlot(new Slot(inventoryplayer, k1 + (k + 1)*9, 8 + k1*18, 84 + k*18));
                }
            }

            for (int l = 0; l < 9; l++)
            {
                addSlot(new Slot(inventoryplayer, l, 8 + l*18, 142));
            }

            onCraftMatrixChanged(craftMatrix);
        }

        public override void onCraftMatrixChanged(IInventory iinventory)
        {
            craftResult.setInventorySlotContents(0, CraftingManager.getInstance().findMatchingRecipe(craftMatrix));
        }

        public override void onCraftGuiClosed(EntityPlayer entityplayer)
        {
            base.onCraftGuiClosed(entityplayer);
            for (int i = 0; i < 4; i++)
            {
                ItemStack itemstack = craftMatrix.getStackInSlot(i);
                if (itemstack != null)
                {
                    entityplayer.dropPlayerItem(itemstack);
                    craftMatrix.setInventorySlotContents(i, null);
                }
            }
        }

        public override bool canInteractWith(EntityPlayer entityplayer)
        {
            return true;
        }

        public InventoryCrafting craftMatrix;
        public IInventory craftResult;
        public bool isMP;
    }
}