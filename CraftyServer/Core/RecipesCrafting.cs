using java.lang;

namespace CraftyServer.Core
{
    public class RecipesCrafting
    {
        public RecipesCrafting()
        {
        }

        public void addRecipes(CraftingManager craftingmanager)
        {
            craftingmanager.addRecipe(new ItemStack(Block.crate), new object[]
                                                                  {
                                                                      "###", "# #", "###", Character.valueOf('#'),
                                                                      Block.planks
                                                                  });
            craftingmanager.addRecipe(new ItemStack(Block.stoneOvenIdle), new object[]
                                                                          {
                                                                              "###", "# #", "###",
                                                                              Character.valueOf('#'), Block.cobblestone
                                                                          });
            craftingmanager.addRecipe(new ItemStack(Block.workbench), new object[]
                                                                      {
                                                                          "##", "##", Character.valueOf('#'),
                                                                          Block.planks
                                                                      });
            craftingmanager.addRecipe(new ItemStack(Block.sandStone), new object[]
                                                                      {
                                                                          "##", "##", Character.valueOf('#'), Block.sand
                                                                      });
        }
    }
}