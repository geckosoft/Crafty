using java.lang;

namespace CraftyServer.Core
{
    public class RecipesIngots
    {
        private readonly object[][] recipeItems;

        public RecipesIngots()
        {
            recipeItems = (new[]
                           {
                               new object[]
                               {
                                   Block.blockGold, new ItemStack(Item.ingotGold, 9)
                               }, new object[]
                                  {
                                      Block.blockSteel, new ItemStack(Item.ingotIron, 9)
                                  }, new object[]
                                     {
                                         Block.blockDiamond, new ItemStack(Item.diamond, 9)
                                     }, new object[]
                                        {
                                            Block.blockLapis, new ItemStack(Item.dyePowder, 9, 4)
                                        }
                           });
        }

        public void addRecipes(CraftingManager craftingmanager)
        {
            for (int i = 0; i < recipeItems.Length; i++)
            {
                var block = (Block) recipeItems[i][0];
                var itemstack = (ItemStack) recipeItems[i][1];
                craftingmanager.addRecipe(new ItemStack(block), new object[]
                                                                {
                                                                    "###", "###", "###", Character.valueOf('#'),
                                                                    itemstack
                                                                });
                craftingmanager.addRecipe(itemstack, new object[]
                                                     {
                                                         "#", Character.valueOf('#'), block
                                                     });
            }
        }
    }
}