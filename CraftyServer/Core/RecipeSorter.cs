using java.util;

namespace CraftyServer.Core
{
    public class RecipeSorter
        : Comparator
    {
        private CraftingManager craftingManager; /* synthetic field */

        public RecipeSorter(CraftingManager craftingmanager)
        {
            craftingManager = craftingmanager;
//        
        }

        #region Comparator Members

        public int compare(object obj, object obj1)
        {
            return compareRecipes((IRecipe) obj, (IRecipe) obj1);
        }

        public bool equals(object obj)
        {
            return obj == this;
        }

        #endregion

        public int compareRecipes(IRecipe irecipe, IRecipe irecipe1)
        {
            if ((irecipe is ShapelessRecipes) && (irecipe1 is ShapedRecipes))
            {
                return 1;
            }
            if ((irecipe1 is ShapelessRecipes) && (irecipe is ShapedRecipes))
            {
                return -1;
            }
            if (irecipe1.getRecipeSize() < irecipe.getRecipeSize())
            {
                return -1;
            }
            return irecipe1.getRecipeSize() <= irecipe.getRecipeSize() ? 0 : 1;
        }
    }
}