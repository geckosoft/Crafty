using java.util;


namespace CraftyServer.Core
{
    public class BlockGlass : BlockBreakable
    {
        public BlockGlass(int i, int j, Material material, bool flag) : base(i, j, material, flag)
        {
        }

        public override int quantityDropped(Random random)
        {
            return 0;
        }
    }
}