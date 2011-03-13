namespace CraftyServer.Core
{
    public class ItemSpade : ItemTool
    {
        public ItemSpade(int i, EnumToolMaterial enumtoolmaterial)
            : base(i, 1, enumtoolmaterial, blocksEffectiveAgainst)
        {
        }

        public override bool canHarvestBlock(Block block)
        {
            if (block == Block.snow)
            {
                return true;
            }
            return block == Block.blockSnow;
        }

        private static Block[] blocksEffectiveAgainst;

        static ItemSpade()
        {
            blocksEffectiveAgainst = (new Block[]
                                      {
                                          Block.grass, Block.dirt, Block.sand, Block.gravel, Block.snow, Block.blockSnow
                                          , Block.blockClay
                                      });
        }
    }
}