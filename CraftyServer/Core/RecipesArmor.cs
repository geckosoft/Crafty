using java.lang;

namespace CraftyServer.Core
{
    public class RecipesArmor
    {
        private readonly object[][] recipeItems;

        private readonly string[][] recipePatterns = new[]
                                                     {
                                                         new[]
                                                         {
                                                             "XXX", "X X"
                                                         }, new[]
                                                            {
                                                                "X X", "XXX", "XXX"
                                                            }, new[]
                                                               {
                                                                   "XXX", "X X", "X X"
                                                               }, new[]
                                                                  {
                                                                      "X X", "X X"
                                                                  }
                                                     };

        public RecipesArmor()
        {
            recipeItems = (new[]
                           {
                               new object[]
                               {
                                   Item.leather, Block.fire, Item.ingotIron, Item.diamond, Item.ingotGold
                               }, new object[]
                                  {
                                      Item.helmetLeather, Item.helmetChain, Item.helmetSteel, Item.helmetDiamond,
                                      Item.helmetGold
                                  }, new object[]
                                     {
                                         Item.plateLeather, Item.plateChain, Item.plateSteel, Item.plateDiamond,
                                         Item.plateGold
                                     }, new object[]
                                        {
                                            Item.legsLeather, Item.legsChain, Item.legsSteel, Item.legsDiamond,
                                            Item.legsGold
                                        }, new object[]
                                           {
                                               Item.bootsLeather, Item.bootsChain, Item.bootsSteel, Item.bootsDiamond,
                                               Item.bootsGold
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
                                                                       recipePatterns[j], Character.valueOf('X'), obj
                                                                   });
                }
            }
        }
    }
}