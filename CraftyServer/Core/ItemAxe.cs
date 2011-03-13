namespace CraftyServer.Core
{
    public class ItemAxe : ItemTool
    {
        protected internal ItemAxe(int i, EnumToolMaterial enumtoolmaterial) :
            base(i, 3, enumtoolmaterial, blocksEffectiveAgainst)
        {
        }

        private static Block[] blocksEffectiveAgainst;

        static ItemAxe()
        {
            blocksEffectiveAgainst = (new Block[]
                                      {
                                          Block.planks, Block.bookShelf, Block.wood, Block.crate
                                      });
        }
    }
}