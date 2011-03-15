using java.lang;

namespace CraftyServer.Core
{
    public class RecipesTools
    {
        private readonly object[][] recipeItems;

        private readonly string[][] recipePatterns = new[]
                                                     {
                                                         new[]
                                                         {
                                                             "XXX", " # ", " # "
                                                         }, new[]
                                                            {
                                                                "X", "#", "#"
                                                            }, new[]
                                                               {
                                                                   "XX", "X#", " #"
                                                               }, new[]
                                                                  {
                                                                      "XX", " #", " #"
                                                                  }
                                                     };

        public RecipesTools()
        {
            recipeItems = (new[]
                           {
                               new object[]
                               {
                                   Block.planks, Block.cobblestone, Item.ingotIron, Item.diamond, Item.ingotGold
                               }, new object[]
                                  {
                                      Item.pickaxeWood, Item.pickaxeStone, Item.pickaxeSteel, Item.pickaxeDiamond,
                                      Item.pickaxeGold
                                  }, new object[]
                                     {
                                         Item.shovelWood, Item.shovelStone, Item.shovelSteel, Item.shovelDiamond,
                                         Item.shovelGold
                                     }, new object[]
                                        {
                                            Item.axeWood, Item.axeStone, Item.axeSteel, Item.axeDiamond, Item.axeGold
                                        }, new object[]
                                           {
                                               Item.hoeWood, Item.hoeStone, Item.hoeSteel, Item.hoeDiamond, Item.hoeGold
                                           }
                           });
        }

        public void addRecipes(CraftingManager craftingmanager)
        {
            for (int i = 0; i < recipeItems[0].Length; i++)
            {
                object obj = recipeItems[0][i];
                for (int j = 0; j < recipeItems.Length - 1; j++)
                {
                    var item = (Item) recipeItems[j + 1][i];
                    craftingmanager.addRecipe(new ItemStack(item), new[]
                                                                   {
                                                                       recipePatterns[j], Character.valueOf('#'),
                                                                       Item.stick, Character.valueOf('X'), obj
                                                                   });
                }
            }
        }
    }
}