namespace CraftyServer.Core
{
    public class ItemAxe : ItemTool
    {
        private static readonly Block[] blocksEffectiveAgainst;

        static ItemAxe()
        {
            blocksEffectiveAgainst = (new[]
                                      {
                                          Block.planks, Block.bookShelf, Block.wood, Block.crate
                                      });
        }

        protected internal ItemAxe(int i, EnumToolMaterial enumtoolmaterial) :
            base(i, 3, enumtoolmaterial, blocksEffectiveAgainst)
        {
        }
    }
}