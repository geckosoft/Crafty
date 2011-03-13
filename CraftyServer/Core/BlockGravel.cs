using java.util;


namespace CraftyServer.Core
{
    public class BlockGravel : BlockSand
    {
        public BlockGravel(int i, int j)
            : base(i, j)
        {
        }

        public override int idDropped(int i, Random random)
        {
            if (random.nextInt(10) == 0)
            {
                return Item.flint.shiftedIndex;
            }
            else
            {
                return blockID;
            }
        }
    }
}