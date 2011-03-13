namespace CraftyServer.Core
{
    public class CraftingInventoryDispenserCB : CraftingInventoryCB
    {
        public CraftingInventoryDispenserCB(IInventory iinventory, TileEntityDispenser tileentitydispenser)
        {
            field_21133_a = tileentitydispenser;
            for (int i = 0; i < 3; i++)
            {
                for (int l = 0; l < 3; l++)
                {
                    addSlot(new Slot(tileentitydispenser, l + i*3, 61 + l*18, 17 + i*18));
                }
            }

            for (int j = 0; j < 3; j++)
            {
                for (int i1 = 0; i1 < 9; i1++)
                {
                    addSlot(new Slot(iinventory, i1 + j*9 + 9, 8 + i1*18, 84 + j*18));
                }
            }

            for (int k = 0; k < 9; k++)
            {
                addSlot(new Slot(iinventory, k, 8 + k*18, 142));
            }
        }

        public override bool canInteractWith(EntityPlayer entityplayer)
        {
            return field_21133_a.canInteractWith(entityplayer);
        }

        private TileEntityDispenser field_21133_a;
    }
}