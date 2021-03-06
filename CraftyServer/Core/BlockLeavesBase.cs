namespace CraftyServer.Core
{
    public class BlockLeavesBase : Block
    {
        protected bool graphicsLevel;

        public BlockLeavesBase(int i, int j, Material material, bool flag)
            : base(i, j, material)
        {
            graphicsLevel = flag;
        }

        public override bool isOpaqueCube()
        {
            return false;
        }

        public override bool shouldSideBeRendered(IBlockAccess iblockaccess, int i, int j, int k, int l)
        {
            int i1 = iblockaccess.getBlockId(i, j, k);
            if (!graphicsLevel && i1 == blockID)
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