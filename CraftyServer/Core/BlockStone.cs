using java.util;


namespace CraftyServer.Core
{
    public class BlockStone : Block
    {
        public BlockStone(int i, int j)
            : base(i, j, Material.rock)
        {
        }

        public override int idDropped(int i, Random random)
        {
            return Block.cobblestone.blockID;
        }
    }
}