namespace CraftyServer.Core
{
    public abstract class BlockContainer : Block
    {
        public BlockContainer(int i, Material material)
            : base(i, material)
        {
            isBlockContainer[i] = true;
        }

        public BlockContainer(int i, int j, Material material)
            : base(i, j, material)
        {
            isBlockContainer[i] = true;
        }

        public override void onBlockAdded(World world, int i, int j, int k)
        {
            base.onBlockAdded(world, i, j, k);
            world.setBlockTileEntity(i, j, k, getBlockEntity());
        }

        public override void onBlockRemoval(World world, int i, int j, int k)
        {
            base.onBlockRemoval(world, i, j, k);
            world.removeBlockTileEntity(i, j, k);
        }

        protected abstract TileEntity getBlockEntity();
    }
}