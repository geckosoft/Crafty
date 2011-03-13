namespace CraftyServer.Core
{
    public class ItemPickaxe : ItemTool
    {
        protected internal ItemPickaxe(int i, EnumToolMaterial enumtoolmaterial)
            : base(i, 2, enumtoolmaterial, blocksEffectiveAgainst)
        {
        }

        public override bool canHarvestBlock(Block block)
        {
            if (block == Block.obsidian)
            {
                return toolMaterial.getHarvestLevel() == 3;
            }
            if (block == Block.blockDiamond || block == Block.oreDiamond)
            {
                return toolMaterial.getHarvestLevel() >= 2;
            }
            if (block == Block.blockGold || block == Block.oreGold)
            {
                return toolMaterial.getHarvestLevel() >= 2;
            }
            if (block == Block.blockSteel || block == Block.oreIron)
            {
                return toolMaterial.getHarvestLevel() >= 1;
            }
            if (block == Block.blockLapis || block == Block.oreLapis)
            {
                return toolMaterial.getHarvestLevel() >= 1;
            }
            if (block == Block.oreRedstone || block == Block.oreRedstoneGlowing)
            {
                return toolMaterial.getHarvestLevel() >= 2;
            }
            if (block.blockMaterial == Material.rock)
            {
                return true;
            }
            return block.blockMaterial == Material.iron;
        }

        private static Block[] blocksEffectiveAgainst;

        static ItemPickaxe()
        {
            blocksEffectiveAgainst = (new Block[]
                                      {
                                          Block.cobblestone, Block.stairDouble, Block.stairSingle, Block.stone,
                                          Block.sandStone, Block.cobblestoneMossy, Block.oreIron, Block.blockSteel,
                                          Block.oreCoal, Block.blockGold,
                                          Block.oreGold, Block.oreDiamond, Block.blockDiamond, Block.ice,
                                          Block.bloodStone, Block.oreLapis, Block.blockLapis
                                      });
        }
    }
}