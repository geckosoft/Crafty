using java.lang;

namespace CraftyServer.Core
{
    public class RecipesArmor
    {
        public RecipesArmor()
        {
            recipeItems = (new object[][]
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
                    Item item = (Item) recipeItems[j + 1][i];
                    craftingmanager.addRecipe(new ItemStack(item), new object[]
                                                                   {
                                                                       recipePatterns[j], Character.valueOf('X'), obj
                                                                   });
                }
            }
        }

        private string[][] recipePatterns = new string[][]
                                            {
                                                new string[]
                                                {
                                                    "XXX", "X X"
                                                }, new string[]
                                                   {
                                                       "X X", "XXX", "XXX"
                                                   }, new string[]
                                                      {
                                                          "XXX", "X X", "X X"
                                                      }, new string[]
                                                         {
                                                             "X X", "X X"
                                                         }
                                            };

        private object[][] recipeItems;
    }
}