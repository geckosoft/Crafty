using java.util;


namespace CraftyServer.Core
{
    public class BlockObsidian : BlockStone
    {
        public BlockObsidian(int i, int j) : base(i, j)
        {
        }

        public override int quantityDropped(Random random)
        {
            return 1;
        }

        public override int idDropped(int i, Random random)
        {
            return Block.obsidian.blockID;
        }
    }
}