using java.util;

namespace CraftyServer.Core
{
    public class EntityPigZombie : EntityZombie
    {
        private static ItemStack defaultHeldItem;
        private int angerLevel;
        private int randomSoundDelay;

        static EntityPigZombie()
        {
            defaultHeldItem = new ItemStack(Item.swordGold, 1);
        }

        public EntityPigZombie(World world)
            : base(world)
        {
            angerLevel = 0;
            randomSoundDelay = 0;
            texture = "/mob/pigzombie.png";
            moveSpeed = 0.5F;
            attackStrength = 5;
            isImmuneToFire = true;
        }

        public override void onUpdate()
        {
            moveSpeed = playerToAttack == null ? 0.5F : 0.95F;
            if (randomSoundDelay > 0 && --randomSoundDelay == 0)
            {
                worldObj.playSoundAtEntity(this, "mob.zombiepig.zpigangry", getSoundVolume()*2.0F,
                                           ((rand.nextFloat() - rand.nextFloat())*0.2F + 1.0F)*1.8F);
            }
            base.onUpdate();
        }

        public override bool getCanSpawnHere()
        {
            return worldObj.difficultySetting > 0 && worldObj.checkIfAABBIsClear(boundingBox) &&
                   worldObj.getCollidingBoundingBoxes(this, boundingBox).size() == 0 &&
                   !worldObj.getIsAnyLiquid(boundingBox);
        }

        public override void writeEntityToNBT(NBTTagCompound nbttagcompound)
        {
            base.writeEntityToNBT(nbttagcompound);
            nbttagcompound.setShort("Anger", (short) angerLevel);
        }

        public override void readEntityFromNBT(NBTTagCompound nbttagcompound)
        {
            base.readEntityFromNBT(nbttagcompound);
            angerLevel = nbttagcompound.getShort("Anger");
        }

        protected override Entity findPlayerToAttack()
        {
            if (angerLevel == 0)
            {
                return null;
            }
            else
            {
                return base.findPlayerToAttack();
            }
        }

        public override void onLivingUpdate()
        {
            base.onLivingUpdate();
        }

        public override bool attackEntityFrom(Entity entity, int i)
        {
            if (entity is EntityPlayer)
            {
                List list = worldObj.getEntitiesWithinAABBExcludingEntity(this, boundingBox.expand(32D, 32D, 32D));
                for (int j = 0; j < list.size(); j++)
                {
                    var entity1 = (Entity) list.get(j);
                    if (entity1 is EntityPigZombie)
                    {
                        var entitypigzombie = (EntityPigZombie) entity1;
                        entitypigzombie.becomeAngryAt(entity);
                    }
                }

                becomeAngryAt(entity);
            }
            return base.attackEntityFrom(entity, i);
        }

        private void becomeAngryAt(Entity entity)
        {
            playerToAttack = entity;
            angerLevel = 400 + rand.nextInt(400);
            randomSoundDelay = rand.nextInt(40);
        }

        protected override string getLivingSound()
        {
            return "mob.zombiepig.zpig";
        }

        protected override string getHurtSound()
        {
            return "mob.zombiepig.zpighurt";
        }

        protected override string getDeathSound()
        {
            return "mob.zombiepig.zpigdeath";
        }

        protected override int getDropItemId()
        {
            return Item.porkCooked.shiftedIndex;
        }
    }
}