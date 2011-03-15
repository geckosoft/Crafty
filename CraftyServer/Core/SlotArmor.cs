namespace CraftyServer.Core
{
    public class SlotArmor : Slot
    {
        private readonly int field_20102_a; /* synthetic field */
        private CraftingInventoryPlayerCB field_20101_b; /* synthetic field */

        public SlotArmor(CraftingInventoryPlayerCB craftinginventoryplayercb, IInventory iinventory, int i, int j, int k,
                         int l)
            : base(iinventory, i, j, k)
        {
            field_20101_b = craftinginventoryplayercb;
            field_20102_a = l;
        }

        public override int getSlotStackLimit()
        {
            return 1;
        }

        public override bool isItemValid(ItemStack itemstack)
        {
            if (itemstack.getItem() is ItemArmor)
            {
                return ((ItemArmor) itemstack.getItem()).armorType == field_20102_a;
            }
            if (itemstack.getItem().shiftedIndex == Block.pumpkin.blockID)
            {
                return field_20102_a == 0;
            }
            else
            {
                return false;
            }
        }
    }
}