namespace CraftyServer.Core
{
    public interface IRecipe
    {
        bool func_21134_a(InventoryCrafting inventorycrafting);
        ItemStack func_21136_b(InventoryCrafting inventorycrafting);
        int getRecipeSize();
    }
}