using java.util;

namespace CraftyServer.Core
{
    public interface ICrafting
    {
        void updateCraftingInventory(CraftingInventoryCB craftinginventorycb, List list);
        void updateCraftingInventorySlot(CraftingInventoryCB craftinginventorycb, int i, ItemStack itemstack);
        void updateCraftingInventoryInfo(CraftingInventoryCB craftinginventorycb, int i, int j);
    }
}