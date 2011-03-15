namespace CraftyServer.Core
{
    public class BlockBreakable : Block
    {
        private readonly bool field_6084_a;

        public BlockBreakable(int i, int j, Material material, bool flag) : base(i, j, material)
        {
            field_6084_a = flag;
        }

        public override bool isOpaqueCube()
        {
            return false;
        }

        public override bool shouldSideBeRendered(IBlockAccess iblockaccess, int i, int j, int k, int l)
        {
            int i1 = iblockaccess.getBlockId(i, j, k);
            if (!field_6084_a && i1 == blockID)
            {
                return false;
            }
            else
            {
                return base.shouldSideBeRendered(iblockaccess, i, j, k, l);
            }
        }
    }
}