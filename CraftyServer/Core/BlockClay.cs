using java.util;

namespace CraftyServer.Core
{
    public class BlockClay : Block
    {
        public BlockClay(int i, int j)
            : base(i, j, Material.clay)
        {
        }

        public override int idDropped(int i, Random random)
        {
            return Item.clay.shiftedIndex;
        }

        public override int quantityDropped(Random random)
        {
            return 4;
        }
    }
}