using java.util;


namespace CraftyServer.Core
{
    public class BlockMobSpawner : BlockContainer
    {
        public BlockMobSpawner(int i, int j)
            : base(i, j, Material.rock)
        {
        }

        protected override TileEntity getBlockEntity()
        {
            return new TileEntityMobSpawner();
        }

        public override int idDropped(int i, Random random)
        {
            return 0;
        }

        public override int quantityDropped(Random random)
        {
            return 0;
        }

        public override bool isOpaqueCube()
        {
            return false;
        }
    }
}