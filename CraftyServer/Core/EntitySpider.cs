namespace CraftyServer.Core
{
    public class EntitySpider : EntityMobs
    {
        public EntitySpider(World world)
            : base(world)
        {
            texture = "/mob/spider.png";
            setSize(1.4F, 0.9F);
            moveSpeed = 0.8F;
        }

        public override double getMountedYOffset()
        {
            return height*0.75D - 0.5D;
        }

        protected override Entity findPlayerToAttack()
        {
            float f = getEntityBrightness(1.0F);
            if (f < 0.5F)
            {
                double d = 16D;
                return worldObj.getClosestPlayerToEntity(this, d);
            }
            else
            {
                return null;
            }
        }

        protected override string getLivingSound()
        {
            return "mob.spider";
        }

        protected override string getHurtSound()
        {
            return "mob.spider";
        }

        protected override string getDeathSound()
        {
            return "mob.spiderdeath";
        }

        public override void attackEntity(Entity entity, float f)
        {
            float f1 = getEntityBrightness(1.0F);
            if (f1 > 0.5F && rand.nextInt(100) == 0)
            {
                playerToAttack = null;
                return;
            }
            if (f > 2.0F && f < 6F && rand.nextInt(10) == 0)
            {
                if (onGround)
                {
                    double d = entity.posX - posX;
                    double d1 = entity.posZ - posZ;
                    float f2 = MathHelper.sqrt_double(d*d + d1*d1);
                    motionX = (d/f2)*0.5D*0.80000001192092896D + motionX*0.20000000298023224D;
                    motionZ = (d1/f2)*0.5D*0.80000001192092896D + motionZ*0.20000000298023224D;
                    motionY = 0.40000000596046448D;
                }
            }
            else
            {
                base.attackEntity(entity, f);
            }
        }

        public override void writeEntityToNBT(NBTTagCompound nbttagcompound)
        {
            base.writeEntityToNBT(nbttagcompound);
        }

        public override void readEntityFromNBT(NBTTagCompound nbttagcompound)
        {
            base.readEntityFromNBT(nbttagcompound);
        }

        protected override int getDropItemId()
        {
            return Item.silk.shiftedIndex;
        }

        public override bool isOnLadder()
        {
            return isCollidedHorizontally;
        }
    }
}