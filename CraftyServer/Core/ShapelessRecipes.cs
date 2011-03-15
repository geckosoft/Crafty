using java.util;

namespace CraftyServer.Core
{
    public class ShapelessRecipes
        : IRecipe
    {
        private readonly List field_21137_b;
        private readonly ItemStack field_21138_a;

        public ShapelessRecipes(ItemStack itemstack, List list)
        {
            field_21138_a = itemstack;
            field_21137_b = list;
        }

        #region IRecipe Members

        public bool func_21134_a(InventoryCrafting inventorycrafting)
        {
            var arraylist = new ArrayList(field_21137_b);
            int i = 0;
            do
            {
                if (i >= 3)
                {
                    break;
                }
                for (int j = 0; j < 3; j++)
                {
                    ItemStack itemstack = inventorycrafting.func_21084_a(j, i);
                    if (itemstack == null)
                    {
                        continue;
                    }
                    bool flag = false;
                    Iterator iterator = arraylist.iterator();
                    do
                    {
                        if (!iterator.hasNext())
                        {
                            break;
                        }
                        var itemstack1 = (ItemStack) iterator.next();
                        if (itemstack.itemID != itemstack1.itemID ||
                            itemstack1.getItemDamage() != -1 && itemstack.getItemDamage() != itemstack1.getItemDamage())
                        {
                            continue;
                        }
                        flag = true;
                        arraylist.remove(itemstack1);
                        break;
                    } while (true);
                    if (!flag)
                    {
                        return false;
                    }
                }

                i++;
            } while (true);
            return arraylist.isEmpty();
        }

        public ItemStack func_21136_b(InventoryCrafting inventorycrafting)
        {
            return field_21138_a.copy();
        }

        public int getRecipeSize()
        {
            return field_21137_b.size();
        }

        #endregion
    }
}