namespace CraftyServer.Core
{
    public class ItemSpade : ItemTool
    {
        private static readonly Block[] blocksEffectiveAgainst;

        static ItemSpade()
        {
            blocksEffectiveAgainst = (new[]
                                      {
                                          Block.grass, Block.dirt, Block.sand, Block.gravel, Block.snow, Block.blockSnow
                                          , Block.blockClay
                                      });
        }

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
    }
}