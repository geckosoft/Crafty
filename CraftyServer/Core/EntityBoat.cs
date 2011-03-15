using java.lang;
using java.util;

namespace CraftyServer.Core
{
    public class EntityBoat : Entity
    {
        public int damageTaken;
        private double field_9171_al;
        private double field_9172_f;
        private double field_9173_ak;
        private double field_9174_e;
        private double field_9175_aj;
        private int field_9176_d;
        public int field_9177_b;
        public int forwardDirection;

        public EntityBoat(World world)
            : base(world)
        {
            damageTaken = 0;
            field_9177_b = 0;
            forwardDirection = 1;
            preventEntitySpawning = true;
            setSize(1.5F, 0.6F);
            yOffset = height/2.0F;
            entityWalks = false;
        }

        public EntityBoat(World world, double d, double d1, double d2)
            : this(world)
        {
            setPosition(d, d1 + yOffset, d2);
            motionX = 0.0D;
            motionY = 0.0D;
            motionZ = 0.0D;
            prevPosX = d;
            prevPosY = d1;
            prevPosZ = d2;
        }

        protected override void entityInit()
        {
        }

        public override AxisAlignedBB func_89_d(Entity entity)
        {
            return entity.boundingBox;
        }

        public override AxisAlignedBB getBoundingBox()
        {
            return boundingBox;
        }

        public override bool canBePushed()
        {
            return true;
        }

        public override double getMountedYOffset()
        {
            return height*0.0D - 0.30000001192092896D;
        }

        public override bool attackEntityFrom(Entity entity, int i)
        {
            if (worldObj.singleplayerWorld || isDead)
            {
                return true;
            }
            forwardDirection = -forwardDirection;
            field_9177_b = 10;
            damageTaken += i*10;
            setBeenAttacked();
            if (damageTaken > 40)
            {
                for (int j = 0; j < 3; j++)
                {
                    dropItemWithOffset(Block.planks.blockID, 1, 0.0F);
                }

                for (int k = 0; k < 2; k++)
                {
                    dropItemWithOffset(Item.stick.shiftedIndex, 1, 0.0F);
                }

                setEntityDead();
            }
            return true;
        }

        public override bool canBeCollidedWith()
        {
            return !isDead;
        }

        public override void onUpdate()
        {
            base.onUpdate();
            if (field_9177_b > 0)
            {
                field_9177_b--;
            }
            if (damageTaken > 0)
            {
                damageTaken--;
            }
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            int i = 5;
            double d = 0.0D;
            for (int j = 0; j < i; j++)
            {
                double d4 = (boundingBox.minY + ((boundingBox.maxY - boundingBox.minY)*(j + 0))/i) -
                            0.125D;
                double d8 = (boundingBox.minY + ((boundingBox.maxY - boundingBox.minY)*(j + 1))/i) -
                            0.125D;
                AxisAlignedBB axisalignedbb = AxisAlignedBB.getBoundingBoxFromPool(boundingBox.minX, d4,
                                                                                   boundingBox.minZ, boundingBox.maxX,
                                                                                   d8, boundingBox.maxZ);
                if (worldObj.isAABBInMaterial(axisalignedbb, Material.water))
                {
                    d += 1.0D/i;
                }
            }

            if (worldObj.singleplayerWorld)
            {
                if (field_9176_d > 0)
                {
                    double d1 = posX + (field_9174_e - posX)/field_9176_d;
                    double d5 = posY + (field_9172_f - posY)/field_9176_d;
                    double d9 = posZ + (field_9175_aj - posZ)/field_9176_d;
                    double d12;
                    for (d12 = field_9173_ak - rotationYaw; d12 < -180D; d12 += 360D)
                    {
                    }
                    for (; d12 >= 180D; d12 -= 360D)
                    {
                    }
                    rotationYaw += (float) (d12/field_9176_d);
                    rotationPitch += (float) ((field_9171_al - rotationPitch)/field_9176_d);
                    field_9176_d--;
                    setPosition(d1, d5, d9);
                    setRotation(rotationYaw, rotationPitch);
                }
                else
                {
                    double d2 = posX + motionX;
                    double d6 = posY + motionY;
                    double d10 = posZ + motionZ;
                    setPosition(d2, d6, d10);
                    if (onGround)
                    {
                        motionX *= 0.5D;
                        motionY *= 0.5D;
                        motionZ *= 0.5D;
                    }
                    motionX *= 0.99000000953674316D;
                    motionY *= 0.94999998807907104D;
                    motionZ *= 0.99000000953674316D;
                }
                return;
            }
            double d3 = d*2D - 1.0D;
            motionY += 0.039999999105930328D*d3;
            if (riddenByEntity != null)
            {
                motionX += riddenByEntity.motionX*0.20000000000000001D;
                motionZ += riddenByEntity.motionZ*0.20000000000000001D;
            }
            double d7 = 0.40000000000000002D;
            if (motionX < -d7)
            {
                motionX = -d7;
            }
            if (motionX > d7)
            {
                motionX = d7;
            }
            if (motionZ < -d7)
            {
                motionZ = -d7;
            }
            if (motionZ > d7)
            {
                motionZ = d7;
            }
            if (onGround)
            {
                motionX *= 0.5D;
                motionY *= 0.5D;
                motionZ *= 0.5D;
            }
            moveEntity(motionX, motionY, motionZ);
            double d11 = Math.sqrt(motionX*motionX + motionZ*motionZ);
            if (d11 > 0.14999999999999999D)
            {
                double d13 = Math.cos((rotationYaw*3.1415926535897931D)/180D);
                double d15 = Math.sin((rotationYaw*3.1415926535897931D)/180D);
                for (int i1 = 0; i1 < 1.0D + d11*60D; i1++)
                {
                    double d18 = rand.nextFloat()*2.0F - 1.0F;
                    double d20 = (rand.nextInt(2)*2 - 1)*0.69999999999999996D;
                    if (rand.nextBoolean())
                    {
                        double d21 = (posX - d13*d18*0.80000000000000004D) + d15*d20;
                        double d23 = posZ - d15*d18*0.80000000000000004D - d13*d20;
                        worldObj.spawnParticle("splash", d21, posY - 0.125D, d23, motionX, motionY, motionZ);
                    }
                    else
                    {
                        double d22 = posX + d13 + d15*d18*0.69999999999999996D;
                        double d24 = (posZ + d15) - d13*d18*0.69999999999999996D;
                        worldObj.spawnParticle("splash", d22, posY - 0.125D, d24, motionX, motionY, motionZ);
                    }
                }
            }
            if (isCollidedHorizontally && d11 > 0.14999999999999999D)
            {
                if (!worldObj.singleplayerWorld)
                {
                    setEntityDead();
                    for (int k = 0; k < 3; k++)
                    {
                        dropItemWithOffset(Block.planks.blockID, 1, 0.0F);
                    }

                    for (int l = 0; l < 2; l++)
                    {
                        dropItemWithOffset(Item.stick.shiftedIndex, 1, 0.0F);
                    }
                }
            }
            else
            {
                motionX *= 0.99000000953674316D;
                motionY *= 0.94999998807907104D;
                motionZ *= 0.99000000953674316D;
            }
            rotationPitch = 0.0F;
            double d14 = rotationYaw;
            double d16 = prevPosX - posX;
            double d17 = prevPosZ - posZ;
            if (d16*d16 + d17*d17 > 0.001D)
            {
                d14 = (float) ((Math.atan2(d17, d16)*180D)/3.1415926535897931D);
            }
            double d19;
            for (d19 = d14 - rotationYaw; d19 >= 180D; d19 -= 360D)
            {
            }
            for (; d19 < -180D; d19 += 360D)
            {
            }
            if (d19 > 20D)
            {
                d19 = 20D;
            }
            if (d19 < -20D)
            {
                d19 = -20D;
            }
            rotationYaw += (float) (d19);
            setRotation(rotationYaw, rotationPitch);
            List list = worldObj.getEntitiesWithinAABBExcludingEntity(this,
                                                                      boundingBox.expand(0.20000000298023224D, 0.0D,
                                                                                         0.20000000298023224D));
            if (list != null && list.size() > 0)
            {
                for (int j1 = 0; j1 < list.size(); j1++)
                {
                    var entity = (Entity) list.get(j1);
                    if (entity != riddenByEntity && entity.canBePushed() && (entity is EntityBoat))
                    {
                        entity.applyEntityCollision(this);
                    }
                }
            }
            if (riddenByEntity != null && riddenByEntity.isDead)
            {
                riddenByEntity = null;
            }
        }

        public override void updateRiderPosition()
        {
            if (riddenByEntity == null)
            {
                return;
            }
            else
            {
                double d = Math.cos((rotationYaw*3.1415926535897931D)/180D)*0.40000000000000002D;
                double d1 = Math.sin((rotationYaw*3.1415926535897931D)/180D)*0.40000000000000002D;
                riddenByEntity.setPosition(posX + d, posY + getMountedYOffset() + riddenByEntity.getYOffset(), posZ + d1);
                return;
            }
        }

        public override void writeEntityToNBT(NBTTagCompound nbttagcompound)
        {
        }

        public override void readEntityFromNBT(NBTTagCompound nbttagcompound)
        {
        }

        public override bool interact(EntityPlayer entityplayer)
        {
            if (riddenByEntity != null && (riddenByEntity is EntityPlayer) && riddenByEntity != entityplayer)
            {
                return true;
            }
            if (!worldObj.singleplayerWorld)
            {
                entityplayer.mountEntity(this);
            }
            return true;
        }
    }
}