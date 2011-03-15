using java.lang;

namespace CraftyServer.Core
{
    public class EntityGhast : EntityFlying
                               , IMobs
    {
        private int aggroCooldown;
        public int attackCounter;
        public int courseChangeCooldown;
        public int prevAttackCounter;
        private Entity targetedEntity;
        public double waypointX;
        public double waypointY;
        public double waypointZ;

        public EntityGhast(World world)
            : base(world)
        {
            courseChangeCooldown = 0;
            targetedEntity = null;
            aggroCooldown = 0;
            prevAttackCounter = 0;
            attackCounter = 0;
            texture = "/mob/ghast.png";
            setSize(4F, 4F);
            isImmuneToFire = true;
        }

        public override void updatePlayerActionState()
        {
            if (worldObj.difficultySetting == 0)
            {
                setEntityDead();
            }
            prevAttackCounter = attackCounter;
            double d = waypointX - posX;
            double d1 = waypointY - posY;
            double d2 = waypointZ - posZ;
            double d3 = MathHelper.sqrt_double(d*d + d1*d1 + d2*d2);
            if (d3 < 1.0D || d3 > 60D)
            {
                waypointX = posX + ((rand.nextFloat()*2.0F - 1.0F)*16F);
                waypointY = posY + ((rand.nextFloat()*2.0F - 1.0F)*16F);
                waypointZ = posZ + ((rand.nextFloat()*2.0F - 1.0F)*16F);
            }
            if (courseChangeCooldown-- <= 0)
            {
                courseChangeCooldown += rand.nextInt(5) + 2;
                if (isCourseTraversable(waypointX, waypointY, waypointZ, d3))
                {
                    motionX += (d/d3)*0.10000000000000001D;
                    motionY += (d1/d3)*0.10000000000000001D;
                    motionZ += (d2/d3)*0.10000000000000001D;
                }
                else
                {
                    waypointX = posX;
                    waypointY = posY;
                    waypointZ = posZ;
                }
            }
            if (targetedEntity != null && targetedEntity.isDead)
            {
                targetedEntity = null;
            }
            if (targetedEntity == null || aggroCooldown-- <= 0)
            {
                targetedEntity = worldObj.getClosestPlayerToEntity(this, 100D);
                if (targetedEntity != null)
                {
                    aggroCooldown = 20;
                }
            }
            double d4 = 64D;
            if (targetedEntity != null && targetedEntity.getDistanceSqToEntity(this) < d4*d4)
            {
                double d5 = targetedEntity.posX - posX;
                double d6 = (targetedEntity.boundingBox.minY + (targetedEntity.height/2.0F)) -
                            (posY + (height/2.0F));
                double d7 = targetedEntity.posZ - posZ;
                renderYawOffset = rotationYaw = (-(float) Math.atan2(d5, d7)*180F)/3.141593F;
                if (canEntityBeSeen(targetedEntity))
                {
                    if (attackCounter == 10)
                    {
                        worldObj.playSoundAtEntity(this, "mob.ghast.charge", getSoundVolume(),
                                                   (rand.nextFloat() - rand.nextFloat())*0.2F + 1.0F);
                    }
                    attackCounter++;
                    if (attackCounter == 20)
                    {
                        worldObj.playSoundAtEntity(this, "mob.ghast.fireball", getSoundVolume(),
                                                   (rand.nextFloat() - rand.nextFloat())*0.2F + 1.0F);
                        var entityfireball = new EntityFireball(worldObj, this, d5, d6, d7);
                        double d8 = 4D;
                        Vec3D vec3d = getLook(1.0F);
                        entityfireball.posX = posX + vec3d.xCoord*d8;
                        entityfireball.posY = posY + (height/2.0F) + 0.5D;
                        entityfireball.posZ = posZ + vec3d.zCoord*d8;
                        worldObj.entityJoinedWorld(entityfireball);
                        attackCounter = -40;
                    }
                }
                else if (attackCounter > 0)
                {
                    attackCounter--;
                }
            }
            else
            {
                renderYawOffset = rotationYaw = (-(float) Math.atan2(motionX, motionZ)*180F)/3.141593F;
                if (attackCounter > 0)
                {
                    attackCounter--;
                }
            }
            texture = attackCounter <= 10 ? "/mob/ghast.png" : "/mob/ghast_fire.png";
        }

        private bool isCourseTraversable(double d, double d1, double d2, double d3)
        {
            double d4 = (waypointX - posX)/d3;
            double d5 = (waypointY - posY)/d3;
            double d6 = (waypointZ - posZ)/d3;
            AxisAlignedBB axisalignedbb = boundingBox.copy();
            for (int i = 1; i < d3; i++)
            {
                axisalignedbb.offset(d4, d5, d6);
                if (worldObj.getCollidingBoundingBoxes(this, axisalignedbb).size() > 0)
                {
                    return false;
                }
            }

            return true;
        }

        protected override string getLivingSound()
        {
            return "mob.ghast.moan";
        }

        protected override string getHurtSound()
        {
            return "mob.ghast.scream";
        }

        protected override string getDeathSound()
        {
            return "mob.ghast.death";
        }

        protected override int getDropItemId()
        {
            return Item.gunpowder.shiftedIndex;
        }

        protected override float getSoundVolume()
        {
            return 10F;
        }

        public override bool getCanSpawnHere()
        {
            return rand.nextInt(20) == 0 && base.getCanSpawnHere() && worldObj.difficultySetting > 0;
        }

        public override int getMaxSpawnedInChunk()
        {
            return 1;
        }
    }
}