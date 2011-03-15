using java.lang;
using java.util;

namespace CraftyServer.Core
{
    public class FurnaceRecipes
    {
        private static readonly FurnaceRecipes smeltingBase = new FurnaceRecipes();
        private readonly Map smeltingList;

        private FurnaceRecipes()
        {
            smeltingList = new HashMap();
            addSmelting(Block.oreIron.blockID, new ItemStack(Item.ingotIron));
            addSmelting(Block.oreGold.blockID, new ItemStack(Item.ingotGold));
            addSmelting(Block.oreDiamond.blockID, new ItemStack(Item.diamond));
            addSmelting(Block.sand.blockID, new ItemStack(Block.glass));
            addSmelting(Item.porkRaw.shiftedIndex, new ItemStack(Item.porkCooked));
            addSmelting(Item.fishRaw.shiftedIndex, new ItemStack(Item.fishCooked));
            addSmelting(Block.cobblestone.blockID, new ItemStack(Block.stone));
            addSmelting(Item.clay.shiftedIndex, new ItemStack(Item.brick));
            addSmelting(Block.cactus.blockID, new ItemStack(Item.dyePowder, 1, 2));
            addSmelting(Block.wood.blockID, new ItemStack(Item.coal, 1, 1));
        }

        public static FurnaceRecipes smelting()
        {
            return smeltingBase;
        }

        public void addSmelting(int i, ItemStack itemstack)
        {
            smeltingList.put(Integer.valueOf(i), itemstack);
        }

        public ItemStack getSmeltingResult(int i)
        {
            return (ItemStack) smeltingList.get(Integer.valueOf(i));
        }
    }
}