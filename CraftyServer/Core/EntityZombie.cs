namespace CraftyServer.Core
{
    public class EntityZombie : EntityMobs
    {
        public EntityZombie(World world) : base(world)
        {
            texture = "/mob/zombie.png";
            moveSpeed = 0.5F;
            attackStrength = 5;
        }

        public override void onLivingUpdate()
        {
            if (worldObj.isDaytime())
            {
                float f = getEntityBrightness(1.0F);
                if (f > 0.5F &&
                    worldObj.canBlockSeeTheSky(MathHelper.floor_double(posX), MathHelper.floor_double(posY),
                                               MathHelper.floor_double(posZ)) && rand.nextFloat()*30F < (f - 0.4F)*2.0F)
                {
                    fire = 300;
                }
            }
            base.onLivingUpdate();
        }

        protected override string getLivingSound()
        {
            return "mob.zombie";
        }

        protected override string getHurtSound()
        {
            return "mob.zombiehurt";
        }

        protected override string getDeathSound()
        {
            return "mob.zombiedeath";
        }

        protected override int getDropItemId()
        {
            return Item.feather.shiftedIndex;
        }
    }
}