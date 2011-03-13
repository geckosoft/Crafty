namespace CraftyServer.Core
{
    public class EntityMobs : EntityCreature
                              , IMobs
    {
        public EntityMobs(World world) : base(world)
        {
            attackStrength = 2;
            health = 20;
        }

        public override void onLivingUpdate()
        {
            float f = getEntityBrightness(1.0F);
            if (f > 0.5F)
            {
                age += 2;
            }
            base.onLivingUpdate();
        }

        public override void onUpdate()
        {
            base.onUpdate();
            if (worldObj.difficultySetting == 0)
            {
                setEntityDead();
            }
        }

        protected override Entity findPlayerToAttack()
        {
            EntityPlayer entityplayer = worldObj.getClosestPlayerToEntity(this, 16D);
            if (entityplayer != null && canEntityBeSeen(entityplayer))
            {
                return entityplayer;
            }
            else
            {
                return null;
            }
        }

        public override bool attackEntityFrom(Entity entity, int i)
        {
            if (base.attackEntityFrom(entity, i))
            {
                if (riddenByEntity == entity || ridingEntity == entity)
                {
                    return true;
                }
                if (entity != this)
                {
                    playerToAttack = entity;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void attackEntity(Entity entity, float f)
        {
            if ((double) f < 1.5D && entity.boundingBox.maxY > boundingBox.minY &&
                entity.boundingBox.minY < boundingBox.maxY)
            {
                attackTime = 20;
                entity.attackEntityFrom(this, attackStrength);
            }
        }

        protected override float getBlockPathWeight(int i, int j, int k)
        {
            return 0.5F - worldObj.getLightBrightness(i, j, k);
        }

        public override void writeEntityToNBT(NBTTagCompound nbttagcompound)
        {
            base.writeEntityToNBT(nbttagcompound);
        }

        public override void readEntityFromNBT(NBTTagCompound nbttagcompound)
        {
            base.readEntityFromNBT(nbttagcompound);
        }

        public override bool getCanSpawnHere()
        {
            int i = MathHelper.floor_double(posX);
            int j = MathHelper.floor_double(boundingBox.minY);
            int k = MathHelper.floor_double(posZ);
            if (worldObj.getSavedLightValue(EnumSkyBlock.Sky, i, j, k) > rand.nextInt(32))
            {
                return false;
            }
            else
            {
                int l = worldObj.getBlockLightValue(i, j, k);
                return l <= rand.nextInt(8) && base.getCanSpawnHere();
            }
        }

        protected int attackStrength;
    }
}