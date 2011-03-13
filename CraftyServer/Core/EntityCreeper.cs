namespace CraftyServer.Core
{
    public class EntityCreeper : EntityMobs
    {
        public EntityCreeper(World world)
            : base(world)
        {
            texture = "/mob/creeper.png";
        }

        protected override void entityInit()
        {
            base.entityInit();

            dataWatcher.addObject(16, (sbyte) (-1));
        }

        public override void writeEntityToNBT(NBTTagCompound nbttagcompound)
        {
            base.writeEntityToNBT(nbttagcompound);
        }

        public override void readEntityFromNBT(NBTTagCompound nbttagcompound)
        {
            base.readEntityFromNBT(nbttagcompound);
        }

        public override void onUpdate()
        {
            lastActiveTime = timeSinceIgnited;
            if (worldObj.singleplayerWorld)
            {
                int i = func_21048_K();
                if (i > 0 && timeSinceIgnited == 0)
                {
                    worldObj.playSoundAtEntity(this, "random.fuse", 1.0F, 0.5F);
                }
                timeSinceIgnited += i;
                if (timeSinceIgnited < 0)
                {
                    timeSinceIgnited = 0;
                }
                if (timeSinceIgnited >= 30)
                {
                    timeSinceIgnited = 30;
                }
            }
            base.onUpdate();
        }

        protected override string getHurtSound()
        {
            return "mob.creeper";
        }

        protected override string getDeathSound()
        {
            return "mob.creeperdeath";
        }

        public override void onDeath(Entity entity)
        {
            base.onDeath(entity);
            if (entity is EntitySkeleton)
            {
                dropItem(Item.record13.shiftedIndex + rand.nextInt(2), 1);
            }
        }

        public override void attackEntity(Entity entity, float f)
        {
            int i = func_21048_K();
            if (i <= 0 && f < 3F || i > 0 && f < 7F)
            {
                if (timeSinceIgnited == 0)
                {
                    worldObj.playSoundAtEntity(this, "random.fuse", 1.0F, 0.5F);
                }
                func_21049_a(1);
                timeSinceIgnited++;
                if (timeSinceIgnited >= 30)
                {
                    worldObj.createExplosion(this, posX, posY, posZ, 3F);
                    setEntityDead();
                }
                hasAttacked = true;
            }
            else
            {
                func_21049_a(-1);
                timeSinceIgnited--;
                if (timeSinceIgnited < 0)
                {
                    timeSinceIgnited = 0;
                }
            }
        }

        protected override int getDropItemId()
        {
            return Item.gunpowder.shiftedIndex;
        }

        private int func_21048_K()
        {
            return dataWatcher.getWatchableObjectSByte(16);
        }

        private void func_21049_a(int i)
        {
            dataWatcher.updateObject(16, (sbyte) i);
        }

        private int timeSinceIgnited;
        private int lastActiveTime;
    }
}