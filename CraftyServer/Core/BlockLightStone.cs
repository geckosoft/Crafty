using java.util;


namespace CraftyServer.Core
{
    public class BlockLightStone : Block
    {
        public BlockLightStone(int i, int j, Material material)
            : base(i, j, material)
        {
        }

        public override int idDropped(int i, Random random)
        {
            return Item.lightStoneDust.shiftedIndex;
        }
    }
}