using java.util;
using java.lang;

namespace CraftyServer.Core
{
    public class CraftingManager
    {
        public static CraftingManager getInstance()
        {
            return instance;
        }

        private CraftingManager()
        {
            recipes = new ArrayList();
            (new RecipesTools()).addRecipes(this);
            (new RecipesWeapons()).addRecipes(this);
            (new RecipesIngots()).addRecipes(this);
            (new RecipesFood()).addRecipes(this);
            (new RecipesCrafting()).addRecipes(this);
            (new RecipesArmor()).addRecipes(this);
            (new RecipesDyes()).addRecipes(this);
            addRecipe(new ItemStack(Item.paper, 3), new object[]
                                                    {
                                                        "###", Character.valueOf('#'), Item.reed
                                                    });
            addRecipe(new ItemStack(Item.book, 1), new object[]
                                                   {
                                                       "#", "#", "#", Character.valueOf('#'), Item.paper
                                                   });
            addRecipe(new ItemStack(Block.fence, 2), new object[]
                                                     {
                                                         "###", "###", Character.valueOf('#'), Item.stick
                                                     });
            addRecipe(new ItemStack(Block.jukebox, 1), new object[]
                                                       {
                                                           "###", "#X#", "###", Character.valueOf('#'), Block.planks,
                                                           Character.valueOf('X'), Item.diamond
                                                       });
            addRecipe(new ItemStack(Block.musicBlock, 1), new object[]
                                                          {
                                                              "###", "#X#", "###", Character.valueOf('#'), Block.planks,
                                                              Character.valueOf('X'), Item.redstone
                                                          });
            addRecipe(new ItemStack(Block.bookShelf, 1), new object[]
                                                         {
                                                             "###", "XXX", "###", Character.valueOf('#'), Block.planks,
                                                             Character.valueOf('X'), Item.book
                                                         });
            addRecipe(new ItemStack(Block.blockSnow, 1), new object[]
                                                         {
                                                             "##", "##", Character.valueOf('#'), Item.snowball
                                                         });
            addRecipe(new ItemStack(Block.blockClay, 1), new object[]
                                                         {
                                                             "##", "##", Character.valueOf('#'), Item.clay
                                                         });
            addRecipe(new ItemStack(Block.brick, 1), new object[]
                                                     {
                                                         "##", "##", Character.valueOf('#'), Item.brick
                                                     });
            addRecipe(new ItemStack(Block.lightStone, 1), new object[]
                                                          {
                                                              "###", "###", "###", Character.valueOf('#'),
                                                              Item.lightStoneDust
                                                          });
            addRecipe(new ItemStack(Block.cloth, 1), new object[]
                                                     {
                                                         "###", "###", "###", Character.valueOf('#'), Item.silk
                                                     });
            addRecipe(new ItemStack(Block.tnt, 1), new object[]
                                                   {
                                                       "X#X", "#X#", "X#X", Character.valueOf('X'), Item.gunpowder,
                                                       Character.valueOf('#'), Block.sand
                                                   });
            addRecipe(new ItemStack(Block.stairSingle, 3, 3), new object[]
                                                              {
                                                                  "###", Character.valueOf('#'), Block.cobblestone
                                                              });
            addRecipe(new ItemStack(Block.stairSingle, 3, 0), new object[]
                                                              {
                                                                  "###", Character.valueOf('#'), Block.stone
                                                              });
            addRecipe(new ItemStack(Block.stairSingle, 3, 1), new object[]
                                                              {
                                                                  "###", Character.valueOf('#'), Block.sandStone
                                                              });
            addRecipe(new ItemStack(Block.stairSingle, 3, 2), new object[]
                                                              {
                                                                  "###", Character.valueOf('#'), Block.planks
                                                              });
            addRecipe(new ItemStack(Block.ladder, 1), new object[]
                                                      {
                                                          "# #", "###", "# #", Character.valueOf('#'), Item.stick
                                                      });
            addRecipe(new ItemStack(Item.doorWood, 1), new object[]
                                                       {
                                                           "##", "##", "##", Character.valueOf('#'), Block.planks
                                                       });
            addRecipe(new ItemStack(Item.doorSteel, 1), new object[]
                                                        {
                                                            "##", "##", "##", Character.valueOf('#'), Item.ingotIron
                                                        });
            addRecipe(new ItemStack(Item.sign, 1), new object[]
                                                   {
                                                       "###", "###", " X ", Character.valueOf('#'), Block.planks,
                                                       Character.valueOf('X'), Item.stick
                                                   });
            addRecipe(new ItemStack(Item.cake, 1), new object[]
                                                   {
                                                       "AAA", "BEB", "CCC", Character.valueOf('A'), Item.bucketMilk,
                                                       Character.valueOf('B'), Item.sugar, Character.valueOf('C'),
                                                       Item.wheat, Character.valueOf('E'),
                                                       Item.egg
                                                   });
            addRecipe(new ItemStack(Item.sugar, 1), new object[]
                                                    {
                                                        "#", Character.valueOf('#'), Item.reed
                                                    });
            addRecipe(new ItemStack(Block.planks, 4), new object[]
                                                      {
                                                          "#", Character.valueOf('#'), Block.wood
                                                      });
            addRecipe(new ItemStack(Item.stick, 4), new object[]
                                                    {
                                                        "#", "#", Character.valueOf('#'), Block.planks
                                                    });
            addRecipe(new ItemStack(Block.torchWood, 4), new object[]
                                                         {
                                                             "X", "#", Character.valueOf('X'), Item.coal,
                                                             Character.valueOf('#'), Item.stick
                                                         });
            addRecipe(new ItemStack(Block.torchWood, 4), new object[]
                                                         {
                                                             "X", "#", Character.valueOf('X'),
                                                             new ItemStack(Item.coal, 1, 1), Character.valueOf('#'),
                                                             Item.stick
                                                         });
            addRecipe(new ItemStack(Item.bowlEmpty, 4), new object[]
                                                        {
                                                            "# #", " # ", Character.valueOf('#'), Block.planks
                                                        });
            addRecipe(new ItemStack(Block.minecartTrack, 16), new object[]
                                                              {
                                                                  "X X", "X#X", "X X", Character.valueOf('X'),
                                                                  Item.ingotIron, Character.valueOf('#'), Item.stick
                                                              });
            addRecipe(new ItemStack(Item.minecartEmpty, 1), new object[]
                                                            {
                                                                "# #", "###", Character.valueOf('#'), Item.ingotIron
                                                            });
            addRecipe(new ItemStack(Block.pumpkinLantern, 1), new object[]
                                                              {
                                                                  "A", "B", Character.valueOf('A'), Block.pumpkin,
                                                                  Character.valueOf('B'), Block.torchWood
                                                              });
            addRecipe(new ItemStack(Item.minecartCrate, 1), new object[]
                                                            {
                                                                "A", "B", Character.valueOf('A'), Block.crate,
                                                                Character.valueOf('B'), Item.minecartEmpty
                                                            });
            addRecipe(new ItemStack(Item.minecartPowered, 1), new object[]
                                                              {
                                                                  "A", "B", Character.valueOf('A'), Block.stoneOvenIdle,
                                                                  Character.valueOf('B'), Item.minecartEmpty
                                                              });
            addRecipe(new ItemStack(Item.boat, 1), new object[]
                                                   {
                                                       "# #", "###", Character.valueOf('#'), Block.planks
                                                   });
            addRecipe(new ItemStack(Item.bucketEmpty, 1), new object[]
                                                          {
                                                              "# #", " # ", Character.valueOf('#'), Item.ingotIron
                                                          });
            addRecipe(new ItemStack(Item.flintAndSteel, 1), new object[]
                                                            {
                                                                "A ", " B", Character.valueOf('A'), Item.ingotIron,
                                                                Character.valueOf('B'), Item.flint
                                                            });
            addRecipe(new ItemStack(Item.bread, 1), new object[]
                                                    {
                                                        "###", Character.valueOf('#'), Item.wheat
                                                    });
            addRecipe(new ItemStack(Block.stairCompactPlanks, 4), new object[]
                                                                  {
                                                                      "#  ", "## ", "###", Character.valueOf('#'),
                                                                      Block.planks
                                                                  });
            addRecipe(new ItemStack(Item.fishingRod, 1), new object[]
                                                         {
                                                             "  #", " #X", "# X", Character.valueOf('#'), Item.stick,
                                                             Character.valueOf('X'), Item.silk
                                                         });
            addRecipe(new ItemStack(Block.stairCompactCobblestone, 4), new object[]
                                                                       {
                                                                           "#  ", "## ", "###", Character.valueOf('#'),
                                                                           Block.cobblestone
                                                                       });
            addRecipe(new ItemStack(Item.painting, 1), new object[]
                                                       {
                                                           "###", "#X#", "###", Character.valueOf('#'), Item.stick,
                                                           Character.valueOf('X'), Block.cloth
                                                       });
            addRecipe(new ItemStack(Item.appleGold, 1), new object[]
                                                        {
                                                            "###", "#X#", "###", Character.valueOf('#'), Block.blockGold
                                                            , Character.valueOf('X'), Item.appleRed
                                                        });
            addRecipe(new ItemStack(Block.lever, 1), new object[]
                                                     {
                                                         "X", "#", Character.valueOf('#'), Block.cobblestone,
                                                         Character.valueOf('X'), Item.stick
                                                     });
            addRecipe(new ItemStack(Block.torchRedstoneActive, 1), new object[]
                                                                   {
                                                                       "X", "#", Character.valueOf('#'), Item.stick,
                                                                       Character.valueOf('X'), Item.redstone
                                                                   });
            addRecipe(new ItemStack(Item.redstoneRepeater, 1), new object[]
                                                               {
                                                                   "#X#", "III", Character.valueOf('#'),
                                                                   Block.torchRedstoneActive, Character.valueOf('X'),
                                                                   Item.redstone, Character.valueOf('I'), Block.stone
                                                               });
            addRecipe(new ItemStack(Item.pocketSundial, 1), new object[]
                                                            {
                                                                " # ", "#X#", " # ", Character.valueOf('#'),
                                                                Item.ingotGold, Character.valueOf('X'), Item.redstone
                                                            });
            addRecipe(new ItemStack(Item.compass, 1), new object[]
                                                      {
                                                          " # ", "#X#", " # ", Character.valueOf('#'), Item.ingotIron,
                                                          Character.valueOf('X'), Item.redstone
                                                      });
            addRecipe(new ItemStack(Block.button, 1), new object[]
                                                      {
                                                          "#", "#", Character.valueOf('#'), Block.stone
                                                      });
            addRecipe(new ItemStack(Block.pressurePlateStone, 1), new object[]
                                                                  {
                                                                      "##", Character.valueOf('#'), Block.stone
                                                                  });
            addRecipe(new ItemStack(Block.pressurePlatePlanks, 1), new object[]
                                                                   {
                                                                       "##", Character.valueOf('#'), Block.planks
                                                                   });
            addRecipe(new ItemStack(Block.dispenser, 1), new object[]
                                                         {
                                                             "###", "#X#", "#R#", Character.valueOf('#'),
                                                             Block.cobblestone, Character.valueOf('X'), Item.bow,
                                                             Character.valueOf('R'), Item.redstone
                                                         });
            addRecipe(new ItemStack(Item.field_22008_aY, 1), new object[]
                                                             {
                                                                 "###", "XXX", Character.valueOf('#'), Block.cloth,
                                                                 Character.valueOf('X'), Block.planks
                                                             });
            Collections.sort(recipes, new RecipeSorter(this));
            java.lang.System.@out.println((new StringBuilder()).append(recipes.size()).append(" recipes").toString());
        }

        public void addRecipe(ItemStack itemstack, object[] aobj)
        {
            string s = "";
            int i = 0;
            int j = 0;
            int k = 0;
            if (aobj[i] is string[])
            {
                string[] ask = (string[]) aobj[i++];
                for (int l = 0; l < ask.Length; l++)
                {
                    string s2 = ask[l];
                    k++;
                    j = s2.Length;
                    s = (new StringBuilder()).append(s).append(s2).toString();
                }
            }
            else
            {
                while (aobj[i] is string)
                {
                    string s1 = (string) aobj[i++];
                    k++;
                    j = s1.Length;
                    s = (new StringBuilder()).append(s).append(s1).toString();
                }
            }
            HashMap hashmap = new HashMap();
            for (; i < aobj.Length; i += 2)
            {
                Character character = (Character) aobj[i];
                ItemStack itemstack1 = null;
                if (aobj[i + 1] is Item)
                {
                    itemstack1 = new ItemStack((Item) aobj[i + 1]);
                }
                else if (aobj[i + 1] is Block)
                {
                    itemstack1 = new ItemStack((Block) aobj[i + 1], 1, -1);
                }
                else if (aobj[i + 1] is ItemStack)
                {
                    itemstack1 = (ItemStack) aobj[i + 1];
                }
                hashmap.put(character, itemstack1);
            }

            ItemStack[] aitemstack = new ItemStack[j*k];
            for (int i1 = 0; i1 < j*k; i1++)
            {
                char c = s[i1];
                if (hashmap.containsKey(Character.valueOf(c)))
                {
                    aitemstack[i1] = ((ItemStack) hashmap.get(Character.valueOf(c))).copy();
                }
                else
                {
                    aitemstack[i1] = null;
                }
            }

            recipes.add(new ShapedRecipes(j, k, aitemstack, itemstack));
        }

        public void addShapelessRecipe(ItemStack itemstack, object[] aobj)
        {
            ArrayList arraylist = new ArrayList();
            object[] aobj1 = aobj;
            int i = aobj1.Length;
            for (int j = 0; j < i; j++)
            {
                object obj = aobj1[j];
                if (obj is ItemStack)
                {
                    arraylist.add(((ItemStack) obj).copy());
                    continue;
                }
                if (obj is Item)
                {
                    arraylist.add(new ItemStack((Item) obj));
                    continue;
                }
                if (obj is Block)
                {
                    arraylist.add(new ItemStack((Block) obj));
                }
                else
                {
                    throw new RuntimeException("Invalid shapeless recipy!");
                }
            }

            recipes.add(new ShapelessRecipes(itemstack, arraylist));
        }

        public ItemStack findMatchingRecipe(InventoryCrafting inventorycrafting)
        {
            for (int i = 0; i < recipes.size(); i++)
            {
                IRecipe irecipe = (IRecipe) recipes.get(i);
                if (irecipe.func_21134_a(inventorycrafting))
                {
                    return irecipe.func_21136_b(inventorycrafting);
                }
            }

            return null;
        }

        private static CraftingManager instance = new CraftingManager();
        private List recipes;
    }
}