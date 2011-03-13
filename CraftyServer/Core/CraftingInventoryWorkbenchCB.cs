namespace CraftyServer.Core
{
    public class CraftingInventoryWorkbenchCB : CraftingInventoryCB
    {
        public CraftingInventoryWorkbenchCB(InventoryPlayer inventoryplayer, World world, int i, int j, int k)
        {
            craftMatrix = new InventoryCrafting(this, 3, 3);
            craftResult = new InventoryCraftResult();
            field_20150_c = world;
            field_20149_h = i;
            field_20148_i = j;
            field_20147_j = k;
            addSlot(new SlotCrafting(craftMatrix, craftResult, 0, 124, 35));
            for (int l = 0; l < 3; l++)
            {
                for (int k1 = 0; k1 < 3; k1++)
                {
                    addSlot(new Slot(craftMatrix, k1 + l*3, 30 + k1*18, 17 + l*18));
                }
            }

            for (int i1 = 0; i1 < 3; i1++)
            {
                for (int l1 = 0; l1 < 9; l1++)
                {
                    addSlot(new Slot(inventoryplayer, l1 + i1*9 + 9, 8 + l1*18, 84 + i1*18));
                }
            }

            for (int j1 = 0; j1 < 9; j1++)
            {
                addSlot(new Slot(inventoryplayer, j1, 8 + j1*18, 142));
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
            for (int i = 0; i < 9; i++)
            {
                ItemStack itemstack = craftMatrix.getStackInSlot(i);
                if (itemstack != null)
                {
                    entityplayer.dropPlayerItem(itemstack);
                }
            }
        }

        public override bool canInteractWith(EntityPlayer entityplayer)
        {
            if (field_20150_c.getBlockId(field_20149_h, field_20148_i, field_20147_j) != Block.workbench.blockID)
            {
                return false;
            }
            return
                entityplayer.getDistanceSq((double) field_20149_h + 0.5D, (double) field_20148_i + 0.5D,
                                           (double) field_20147_j + 0.5D) <= 64D;
        }

        public InventoryCrafting craftMatrix;
        public IInventory craftResult;
        private World field_20150_c;
        private int field_20149_h;
        private int field_20148_i;
        private int field_20147_j;
    }
}