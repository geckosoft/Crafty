namespace CraftyServer.Core
{
    public class BlockSlowSand : Block
    {
        public BlockSlowSand(int i, int j)
            : base(i, j, Material.sand)
        {
        }

        public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
        {
            float f = 0.125F;
            return AxisAlignedBB.getBoundingBoxFromPool(i, j, k, i + 1, (j + 1) - f, k + 1);
        }

        public override void onEntityCollidedWithBlock(World world, int i, int j, int k, Entity entity)
        {
            entity.motionX *= 0.40000000000000002D;
            entity.motionZ *= 0.40000000000000002D;
        }
    }
}