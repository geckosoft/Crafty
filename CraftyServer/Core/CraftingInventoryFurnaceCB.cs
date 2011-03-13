namespace CraftyServer.Core
{
    public class CraftingInventoryFurnaceCb : CraftingInventoryCB
    {
        public CraftingInventoryFurnaceCb(IInventory iinventory, TileEntityFurnace tileentityfurnace)
        {
            lastCookTime = 0;
            lastBurnTime = 0;
            lastItemBurnTime = 0;
            furnace = tileentityfurnace;
            addSlot(new Slot(tileentityfurnace, 0, 56, 17));
            addSlot(new Slot(tileentityfurnace, 1, 56, 53));
            addSlot(new Slot(tileentityfurnace, 2, 116, 35));
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    addSlot(new Slot(iinventory, k + i*9 + 9, 8 + k*18, 84 + i*18));
                }
            }

            for (int j = 0; j < 9; j++)
            {
                addSlot(new Slot(iinventory, j, 8 + j*18, 142));
            }
        }

        public override void onCraftGuiOpened(ICrafting icrafting)
        {
            base.onCraftGuiOpened(icrafting);
            icrafting.updateCraftingInventoryInfo(this, 0, furnace.furnaceCookTime);
            icrafting.updateCraftingInventoryInfo(this, 1, furnace.furnaceBurnTime);
            icrafting.updateCraftingInventoryInfo(this, 2, furnace.currentItemBurnTime);
        }

        public override void updateCraftingMatrix()
        {
            base.updateCraftingMatrix();
            for (int i = 0; i < crafters.size(); i++)
            {
                ICrafting icrafting = (ICrafting) crafters.get(i);
                if (lastCookTime != furnace.furnaceCookTime)
                {
                    icrafting.updateCraftingInventoryInfo(this, 0, furnace.furnaceCookTime);
                }
                if (lastBurnTime != furnace.furnaceBurnTime)
                {
                    icrafting.updateCraftingInventoryInfo(this, 1, furnace.furnaceBurnTime);
                }
                if (lastItemBurnTime != furnace.currentItemBurnTime)
                {
                    icrafting.updateCraftingInventoryInfo(this, 2, furnace.currentItemBurnTime);
                }
            }

            lastCookTime = furnace.furnaceCookTime;
            lastBurnTime = furnace.furnaceBurnTime;
            lastItemBurnTime = furnace.currentItemBurnTime;
        }

        public override bool canInteractWith(EntityPlayer entityplayer)
        {
            return furnace.canInteractWith(entityplayer);
        }

        private TileEntityFurnace furnace;
        private int lastCookTime;
        private int lastBurnTime;
        private int lastItemBurnTime;
    }
}