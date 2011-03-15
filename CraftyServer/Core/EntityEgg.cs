using java.lang;
using java.util;

namespace CraftyServer.Core
{
    public class EntityEgg : Entity
    {
        private readonly EntityLiving field_20083_aj;
        private int field_20079_al;
        private int field_20081_ak;
        private bool inGround;
        private int inTile;
        public int shake;
        private int xTile;
        private int yTile;
        private int zTile;

        public EntityEgg(World world) : base(world)
        {
            xTile = -1;
            yTile = -1;
            zTile = -1;
            inTile = 0;
            inGround = false;
            shake = 0;
            field_20079_al = 0;
            setSize(0.25F, 0.25F);
        }

        public EntityEgg(World world, EntityLiving entityliving)
            : base(world)
        {
            xTile = -1;
            yTile = -1;
            zTile = -1;
            inTile = 0;
            inGround = false;
            shake = 0;
            field_20079_al = 0;
            field_20083_aj = entityliving;
            setSize(0.25F, 0.25F);
            setLocationAndAngles(entityliving.posX, entityliving.posY + entityliving.getEyeHeight(),
                                 entityliving.posZ, entityliving.rotationYaw, entityliving.rotationPitch);
            posX -= MathHelper.cos((rotationYaw/180F)*3.141593F)*0.16F;
            posY -= 0.10000000149011612D;
            posZ -= MathHelper.sin((rotationYaw/180F)*3.141593F)*0.16F;
            setPosition(posX, posY, posZ);
            yOffset = 0.0F;
            float f = 0.4F;
            motionX = -MathHelper.sin((rotationYaw/180F)*3.141593F)*MathHelper.cos((rotationPitch/180F)*3.141593F)*f;
            motionZ = MathHelper.cos((rotationYaw/180F)*3.141593F)*MathHelper.cos((rotationPitch/180F)*3.141593F)*f;
            motionY = -MathHelper.sin((rotationPitch/180F)*3.141593F)*f;
            func_20078_a(motionX, motionY, motionZ, 1.5F, 1.0F);
        }

        public EntityEgg(World world, double d, double d1, double d2)
            : base(world)
        {
            xTile = -1;
            yTile = -1;
            zTile = -1;
            inTile = 0;
            inGround = false;
            shake = 0;
            field_20079_al = 0;
            field_20081_ak = 0;
            setSize(0.25F, 0.25F);
            setPosition(d, d1, d2);
            yOffset = 0.0F;
        }

        protected override void entityInit()
        {
        }

        public void func_20078_a(double d, double d1, double d2, float f,
                                 float f1)
        {
            float f2 = MathHelper.sqrt_double(d*d + d1*d1 + d2*d2);
            d /= f2;
            d1 /= f2;
            d2 /= f2;
            d += rand.nextGaussian()*0.0074999998323619366D*f1;
            d1 += rand.nextGaussian()*0.0074999998323619366D*f1;
            d2 += rand.nextGaussian()*0.0074999998323619366D*f1;
            d *= f;
            d1 *= f;
            d2 *= f;
            motionX = d;
            motionY = d1;
            motionZ = d2;
            float f3 = MathHelper.sqrt_double(d*d + d2*d2);
            prevRotationYaw = rotationYaw = (float) ((Math.atan2(d, d2)*180D)/3.1415927410125732D);
            prevRotationPitch = rotationPitch = (float) ((Math.atan2(d1, f3)*180D)/3.1415927410125732D);
            field_20081_ak = 0;
        }

        public override void onUpdate()
        {
            lastTickPosX = posX;
            lastTickPosY = posY;
            lastTickPosZ = posZ;
            base.onUpdate();
            if (shake > 0)
            {
                shake--;
            }
            if (inGround)
            {
                int i = worldObj.getBlockId(xTile, yTile, zTile);
                if (i != inTile)
                {
                    inGround = false;
                    motionX *= rand.nextFloat()*0.2F;
                    motionY *= rand.nextFloat()*0.2F;
                    motionZ *= rand.nextFloat()*0.2F;
                    field_20081_ak = 0;
                    field_20079_al = 0;
                }
                else
                {
                    field_20081_ak++;
                    if (field_20081_ak == 1200)
                    {
                        setEntityDead();
                    }
                    return;
                }
            }
            else
            {
                field_20079_al++;
            }
            Vec3D vec3d = Vec3D.createVector(posX, posY, posZ);
            Vec3D vec3d1 = Vec3D.createVector(posX + motionX, posY + motionY, posZ + motionZ);
            MovingObjectPosition movingobjectposition = worldObj.rayTraceBlocks(vec3d, vec3d1);
            vec3d = Vec3D.createVector(posX, posY, posZ);
            vec3d1 = Vec3D.createVector(posX + motionX, posY + motionY, posZ + motionZ);
            if (movingobjectposition != null)
            {
                vec3d1 = Vec3D.createVector(movingobjectposition.hitVec.xCoord, movingobjectposition.hitVec.yCoord,
                                            movingobjectposition.hitVec.zCoord);
            }
            if (!worldObj.singleplayerWorld)
            {
                Entity entity = null;
                List list = worldObj.getEntitiesWithinAABBExcludingEntity(this,
                                                                          boundingBox.addCoord(motionX, motionY, motionZ)
                                                                              .expand(1.0D, 1.0D, 1.0D));
                double d = 0.0D;
                for (int i1 = 0; i1 < list.size(); i1++)
                {
                    var entity1 = (Entity) list.get(i1);
                    if (!entity1.canBeCollidedWith() || entity1 == field_20083_aj && field_20079_al < 5)
                    {
                        continue;
                    }
                    float f4 = 0.3F;
                    AxisAlignedBB axisalignedbb = entity1.boundingBox.expand(f4, f4, f4);
                    MovingObjectPosition movingobjectposition1 = axisalignedbb.func_706_a(vec3d, vec3d1);
                    if (movingobjectposition1 == null)
                    {
                        continue;
                    }
                    double d1 = vec3d.distanceTo(movingobjectposition1.hitVec);
                    if (d1 < d || d == 0.0D)
                    {
                        entity = entity1;
                        d = d1;
                    }
                }

                if (entity != null)
                {
                    movingobjectposition = new MovingObjectPosition(entity);
                }
            }
            if (movingobjectposition != null)
            {
                if (movingobjectposition.entityHit != null)
                {
                    if (!movingobjectposition.entityHit.attackEntityFrom(field_20083_aj, 0)) ;
                }
                if (!worldObj.singleplayerWorld && rand.nextInt(8) == 0)
                {
                    byte byte0 = 1;
                    if (rand.nextInt(32) == 0)
                    {
                        byte0 = 4;
                    }
                    for (int k = 0; k < byte0; k++)
                    {
                        var entitychicken = new EntityChicken(worldObj);
                        entitychicken.setLocationAndAngles(posX, posY, posZ, rotationYaw, 0.0F);
                        worldObj.entityJoinedWorld(entitychicken);
                    }
                }
                for (int j = 0; j < 8; j++)
                {
                    worldObj.spawnParticle("snowballpoof", posX, posY, posZ, 0.0D, 0.0D, 0.0D);
                }

                setEntityDead();
            }
            posX += motionX;
            posY += motionY;
            posZ += motionZ;
            float f = MathHelper.sqrt_double(motionX*motionX + motionZ*motionZ);
            rotationYaw = (float) ((Math.atan2(motionX, motionZ)*180D)/3.1415927410125732D);
            for (rotationPitch = (float) ((Math.atan2(motionY, f)*180D)/3.1415927410125732D);
                 rotationPitch - prevRotationPitch < -180F;
                 prevRotationPitch -= 360F)
            {
            }
            for (; rotationPitch - prevRotationPitch >= 180F; prevRotationPitch += 360F)
            {
            }
            for (; rotationYaw - prevRotationYaw < -180F; prevRotationYaw -= 360F)
            {
            }
            for (; rotationYaw - prevRotationYaw >= 180F; prevRotationYaw += 360F)
            {
            }
            rotationPitch = prevRotationPitch + (rotationPitch - prevRotationPitch)*0.2F;
            rotationYaw = prevRotationYaw + (rotationYaw - prevRotationYaw)*0.2F;
            float f1 = 0.99F;
            float f2 = 0.03F;
            if (handleWaterMovement())
            {
                for (int l = 0; l < 4; l++)
                {
                    float f3 = 0.25F;
                    worldObj.spawnParticle("bubble", posX - motionX*f3, posY - motionY*f3,
                                           posZ - motionZ*f3, motionX, motionY, motionZ);
                }

                f1 = 0.8F;
            }
            motionX *= f1;
            motionY *= f1;
            motionZ *= f1;
            motionY -= f2;
            setPosition(posX, posY, posZ);
        }

        public override void writeEntityToNBT(NBTTagCompound nbttagcompound)
        {
            nbttagcompound.setShort("xTile", (short) xTile);
            nbttagcompound.setShort("yTile", (short) yTile);
            nbttagcompound.setShort("zTile", (short) zTile);
            nbttagcompound.setByte("inTile", (byte) inTile);
            nbttagcompound.setByte("shake", (byte) shake);
            nbttagcompound.setByte("inGround", (byte) (inGround ? 1 : 0));
        }

        public override void readEntityFromNBT(NBTTagCompound nbttagcompound)
        {
            xTile = nbttagcompound.getShort("xTile");
            yTile = nbttagcompound.getShort("yTile");
            zTile = nbttagcompound.getShort("zTile");
            inTile = nbttagcompound.getByte("inTile") & 0xff;
            shake = nbttagcompound.getByte("shake") & 0xff;
            inGround = nbttagcompound.getByte("inGround") == 1;
        }

        public override void onCollideWithPlayer(EntityPlayer entityplayer)
        {
            if (inGround && field_20083_aj == entityplayer && shake <= 0 &&
                entityplayer.inventory.addItemStackToInventory(new ItemStack(Item.arrow, 1)))
            {
                worldObj.playSoundAtEntity(this, "random.pop", 0.2F,
                                           ((rand.nextFloat() - rand.nextFloat())*0.7F + 1.0F)*2.0F);
                entityplayer.onItemPickup(this, 1);
                setEntityDead();
            }
        }
    }
}