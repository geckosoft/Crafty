using java.lang;

namespace CraftyServer.Core
{
    public class EntitySkeleton : EntityMobs
    {
        private static ItemStack defaultHeldItem;

        static EntitySkeleton()
        {
            defaultHeldItem = new ItemStack(Item.bow, 1);
        }

        public EntitySkeleton(World world)
            : base(world)
        {
            texture = "/mob/skeleton.png";
        }

        protected override string getLivingSound()
        {
            return "mob.skeleton";
        }

        protected override string getHurtSound()
        {
            return "mob.skeletonhurt";
        }

        protected override string getDeathSound()
        {
            return "mob.skeletonhurt";
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

        public override void attackEntity(Entity entity, float f)
        {
            if (f < 10F)
            {
                double d = entity.posX - posX;
                double d1 = entity.posZ - posZ;
                if (attackTime == 0)
                {
                    var entityarrow = new EntityArrow(worldObj, this);
                    entityarrow.posY += 1.3999999761581421D;
                    double d2 = entity.posY - 0.20000000298023224D - entityarrow.posY;
                    float f1 = MathHelper.sqrt_double(d*d + d1*d1)*0.2F;
                    worldObj.playSoundAtEntity(this, "random.bow", 1.0F, 1.0F/(rand.nextFloat()*0.4F + 0.8F));
                    worldObj.entityJoinedWorld(entityarrow);
                    entityarrow.setArrowHeading(d, d2 + f1, d1, 0.6F, 12F);
                    attackTime = 30;
                }
                rotationYaw = (float) ((Math.atan2(d1, d)*180D)/3.1415927410125732D) - 90F;
                hasAttacked = true;
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
            return Item.arrow.shiftedIndex;
        }

        public override void func_21047_g_()
        {
            int i = rand.nextInt(3);
            for (int j = 0; j < i; j++)
            {
                dropItem(Item.arrow.shiftedIndex, 1);
            }

            i = rand.nextInt(3);
            for (int k = 0; k < i; k++)
            {
                dropItem(Item.bone.shiftedIndex, 1);
            }
        }
    }
}