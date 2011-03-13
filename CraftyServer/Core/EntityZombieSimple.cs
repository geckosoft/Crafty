namespace CraftyServer.Core
{
    public class EntityZombieSimple : EntityMobs
    {
        public EntityZombieSimple(World world)
            : base(world)
        {
            texture = "/mob/zombie.png";
            moveSpeed = 0.5F;
            attackStrength = 50;
            health *= 10;
            yOffset *= 6F;
            setSize(width*6F, height*6F);
        }

        protected override float getBlockPathWeight(int i, int j, int k)
        {
            return worldObj.getLightBrightness(i, j, k) - 0.5F;
        }
    }
}