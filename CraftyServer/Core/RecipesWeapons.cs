using java.lang;

namespace CraftyServer.Core
{
    public class RecipesWeapons
    {
        private readonly object[][] recipeItems;

        private readonly string[][] recipePatterns = new[]
                                                     {
                                                         new[] {"X", "X", "#"}
                                                     };

        public RecipesWeapons()
        {
            recipeItems = (new[]
                           {
                               new object[]
                               {
                                   Block.planks, Block.cobblestone, Item.ingotIron, Item.diamond, Item.ingotGold
                               }, new object[]
                                  {
                                      Item.swordWood, Item.swordStone, Item.swordSteel, Item.swordDiamond,
                                      Item.swordGold
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

            craftingmanager.addRecipe(new ItemStack(Item.bow, 1), new object[]
                                                                  {
                                                                      " #X", "# X", " #X", Character.valueOf('X'),
                                                                      Item.silk, Character.valueOf('#'), Item.stick
                                                                  });
            craftingmanager.addRecipe(new ItemStack(Item.arrow, 4), new object[]
                                                                    {
                                                                        "X", "#", "Y", Character.valueOf('Y'),
                                                                        Item.feather, Character.valueOf('X'), Item.flint
                                                                        , Character.valueOf('#'), Item.stick
                                                                    });
        }
    }
}