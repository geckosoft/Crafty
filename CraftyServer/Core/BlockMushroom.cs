namespace CraftyServer.Core
{
    public class BlockMushroom : BlockFlower
    {
        public BlockMushroom(int i, int j) : base(i, j)
        {
            float f = 0.2F;
            setBlockBounds(0.5F - f, 0.0F, 0.5F - f, 0.5F + f, f*2.0F, 0.5F + f);
        }

        protected override bool canThisPlantGrowOnThisBlockID(int i)
        {
            return Block.opaqueCubeLookup[i];
        }

        public override bool canBlockStay(World world, int i, int j, int k)
        {
            return world.getBlockLightValue(i, j, k) <= 13 &&
                   canThisPlantGrowOnThisBlockID(world.getBlockId(i, j - 1, k));
        }
    }
}