using java.lang;
using java.util;

namespace CraftyServer.Core
{
    public abstract class Entity : Object
    {
        private static int nextEntityID;
        public bool addedToChunk;
        public int air;
        public bool beenAttacked;
        public AxisAlignedBB boundingBox = AxisAlignedBB.getBoundingBox(0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D);
        public int chunkCoordX;
        public int chunkCoordY;
        public int chunkCoordZ;
        protected DataWatcher dataWatcher;
        public float distanceWalkedModified;
        public float entityCollisionReduction;
        public int entityId;
        private double entityRiderPitchDelta;
        private double entityRiderYawDelta;
        protected bool entityWalks;
        protected float fallDistance;
        private bool field_4131_c;
        public bool field_9065_V;
        public bool field_9077_F;
        public int field_9083_ac;
        public int fire;
        public int fireResistance;
        public float height;
        protected bool inWater;
        public bool isCollided;
        public bool isCollidedHorizontally;
        public bool isCollidedVertically;
        public bool isDead;
        protected bool isImmuneToFire;
        public double lastTickPosX;
        public double lastTickPosY;
        public double lastTickPosZ;
        protected int maxAir;
        public double motionX;
        public double motionY;
        public double motionZ;
        private int nextStepDistance;
        public bool noClip;
        public bool onGround;
        public double posX;
        public double posY;
        public double posZ;
        public float prevDistanceWalkedModified;
        public double prevPosX;
        public double prevPosY;
        public double prevPosZ;
        public float prevRotationPitch;
        public float prevRotationYaw;
        public bool preventEntitySpawning;
        protected Random rand;
        public double renderDistanceWeight;
        public Entity riddenByEntity;
        public Entity ridingEntity;
        public float rotationPitch;
        public float rotationYaw;
        public float stepHeight;
        public int ticksExisted;
        public float width;
        public World worldObj;
        public float yOffset;
        public float ySize;

        public Entity(World world)
        {
            entityId = nextEntityID++;
            renderDistanceWeight = 1.0D;
            preventEntitySpawning = false;
            onGround = false;
            isCollided = false;
            beenAttacked = false;
            field_9077_F = true;
            isDead = false;
            yOffset = 0.0F;
            width = 0.6F;
            height = 1.8F;
            prevDistanceWalkedModified = 0.0F;
            distanceWalkedModified = 0.0F;
            entityWalks = true;
            fallDistance = 0.0F;
            nextStepDistance = 1;
            ySize = 0.0F;
            stepHeight = 0.0F;
            noClip = false;
            entityCollisionReduction = 0.0F;
            field_9065_V = false;
            rand = new Random();
            ticksExisted = 0;
            fireResistance = 1;
            fire = 0;
            maxAir = 300;
            inWater = false;
            field_9083_ac = 0;
            air = 300;
            field_4131_c = true;
            isImmuneToFire = false;
            dataWatcher = new DataWatcher();
            addedToChunk = false;
            worldObj = world;
            setPosition(0.0D, 0.0D, 0.0D);
            dataWatcher.addObject(0, Byte.valueOf(0));
            entityInit();
        }

        protected abstract void entityInit();

        public DataWatcher getDataWatcher()
        {
            return dataWatcher;
        }

        public override bool equals(object obj)
        {
            if (obj is Entity)
            {
                return ((Entity) obj).entityId == entityId;
            }
            else
            {
                return false;
            }
        }

        public override int hashCode()
        {
            return entityId;
        }

        public virtual void setEntityDead()
        {
            isDead = true;
        }

        public virtual void setSize(float f, float f1)
        {
            width = f;
            height = f1;
        }

        public void setRotation(float f, float f1)
        {
            rotationYaw = f;
            rotationPitch = f1;
        }

        public void setPosition(double d, double d1, double d2)
        {
            posX = d;
            posY = d1;
            posZ = d2;
            float f = width/2.0F;
            float f1 = height;
            boundingBox.setBounds(d - f, (d1 - yOffset) + ySize, d2 - f,
                                  d + f, (d1 - yOffset) + ySize + f1,
                                  d2 + f);
        }

        public virtual void onUpdate()
        {
            onEntityUpdate();
        }

        public virtual void onEntityUpdate()
        {
            if (ridingEntity != null && ridingEntity.isDead)
            {
                ridingEntity = null;
            }
            ticksExisted++;
            prevDistanceWalkedModified = distanceWalkedModified;
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            prevRotationPitch = rotationPitch;
            prevRotationYaw = rotationYaw;
            if (handleWaterMovement())
            {
                if (!inWater && !field_4131_c)
                {
                    float f =
                        MathHelper.sqrt_double(motionX*motionX*0.20000000298023224D + motionY*motionY +
                                               motionZ*motionZ*0.20000000298023224D)*0.2F;
                    if (f > 1.0F)
                    {
                        f = 1.0F;
                    }
                    worldObj.playSoundAtEntity(this, "random.splash", f,
                                               1.0F + (rand.nextFloat() - rand.nextFloat())*0.4F);
                    float f1 = MathHelper.floor_double(boundingBox.minY);
                    for (int i = 0; i < 1.0F + width*20F; i++)
                    {
                        float f2 = (rand.nextFloat()*2.0F - 1.0F)*width;
                        float f4 = (rand.nextFloat()*2.0F - 1.0F)*width;
                        worldObj.spawnParticle("bubble", posX + f2, f1 + 1.0F, posZ + f4, motionX,
                                               motionY - (rand.nextFloat()*0.2F), motionZ);
                    }

                    for (int j = 0; j < 1.0F + width*20F; j++)
                    {
                        float f3 = (rand.nextFloat()*2.0F - 1.0F)*width;
                        float f5 = (rand.nextFloat()*2.0F - 1.0F)*width;
                        worldObj.spawnParticle("splash", posX + f3, f1 + 1.0F, posZ + f5, motionX,
                                               motionY, motionZ);
                    }
                }
                fallDistance = 0.0F;
                inWater = true;
                fire = 0;
            }
            else
            {
                inWater = false;
            }
            if (worldObj.singleplayerWorld)
            {
                fire = 0;
            }
            else if (fire > 0)
            {
                if (isImmuneToFire)
                {
                    fire -= 4;
                    if (fire < 0)
                    {
                        fire = 0;
                    }
                }
                else
                {
                    if (fire%20 == 0)
                    {
                        attackEntityFrom(null, 1);
                    }
                    fire--;
                }
            }
            if (handleLavaMovement())
            {
                setOnFireFromLava();
            }
            if (posY < -64D)
            {
                kill();
            }
            if (!worldObj.singleplayerWorld)
            {
                func_21041_a(0, fire > 0);
                func_21041_a(2, ridingEntity != null);
            }
            field_4131_c = false;
        }

        public void setOnFireFromLava()
        {
            if (!isImmuneToFire)
            {
                attackEntityFrom(null, 4);
                fire = 600;
            }
        }

        public virtual void kill()
        {
            setEntityDead();
        }

        public bool isOffsetPositionInLiquid(double d, double d1, double d2)
        {
            AxisAlignedBB axisalignedbb = boundingBox.getOffsetBoundingBox(d, d1, d2);
            List list = worldObj.getCollidingBoundingBoxes(this, axisalignedbb);
            if (list.size() > 0)
            {
                return false;
            }
            return !worldObj.getIsAnyLiquid(axisalignedbb);
        }

        public void moveEntity(double d, double d1, double d2)
        {
            if (noClip)
            {
                boundingBox.offset(d, d1, d2);
                posX = (boundingBox.minX + boundingBox.maxX)/2D;
                posY = (boundingBox.minY + yOffset) - ySize;
                posZ = (boundingBox.minZ + boundingBox.maxZ)/2D;
                return;
            }
            double d3 = posX;
            double d4 = posZ;
            double d5 = d;
            double d6 = d1;
            double d7 = d2;
            AxisAlignedBB axisalignedbb = boundingBox.copy();
            bool flag = onGround && isSneaking();
            if (flag)
            {
                double d8 = 0.050000000000000003D;
                for (;
                    d != 0.0D &&
                    worldObj.getCollidingBoundingBoxes(this, boundingBox.getOffsetBoundingBox(d, -1D, 0.0D)).size() == 0;
                    d5 = d)
                {
                    if (d < d8 && d >= -d8)
                    {
                        d = 0.0D;
                        continue;
                    }
                    if (d > 0.0D)
                    {
                        d -= d8;
                    }
                    else
                    {
                        d += d8;
                    }
                }

                for (;
                    d2 != 0.0D &&
                    worldObj.getCollidingBoundingBoxes(this, boundingBox.getOffsetBoundingBox(0.0D, -1D, d2)).size() ==
                    0;
                    d7 = d2)
                {
                    if (d2 < d8 && d2 >= -d8)
                    {
                        d2 = 0.0D;
                        continue;
                    }
                    if (d2 > 0.0D)
                    {
                        d2 -= d8;
                    }
                    else
                    {
                        d2 += d8;
                    }
                }
            }
            List list = worldObj.getCollidingBoundingBoxes(this, boundingBox.addCoord(d, d1, d2));
            for (int i = 0; i < list.size(); i++)
            {
                d1 = ((AxisAlignedBB) list.get(i)).calculateYOffset(boundingBox, d1);
            }

            boundingBox.offset(0.0D, d1, 0.0D);
            if (!field_9077_F && d6 != d1)
            {
                d = d1 = d2 = 0.0D;
            }
            bool flag1 = onGround || d6 != d1 && d6 < 0.0D;
            for (int j = 0; j < list.size(); j++)
            {
                d = ((AxisAlignedBB) list.get(j)).calculateXOffset(boundingBox, d);
            }

            boundingBox.offset(d, 0.0D, 0.0D);
            if (!field_9077_F && d5 != d)
            {
                d = d1 = d2 = 0.0D;
            }
            for (int k = 0; k < list.size(); k++)
            {
                d2 = ((AxisAlignedBB) list.get(k)).calculateZOffset(boundingBox, d2);
            }

            boundingBox.offset(0.0D, 0.0D, d2);
            if (!field_9077_F && d7 != d2)
            {
                d = d1 = d2 = 0.0D;
            }
            if (stepHeight > 0.0F && flag1 && ySize < 0.05F && (d5 != d || d7 != d2))
            {
                double d9 = d;
                double d11 = d1;
                double d13 = d2;
                d = d5;
                d1 = stepHeight;
                d2 = d7;
                AxisAlignedBB axisalignedbb1 = boundingBox.copy();
                boundingBox.setBB(axisalignedbb);
                List list1 = worldObj.getCollidingBoundingBoxes(this, boundingBox.addCoord(d, d1, d2));
                for (int j2 = 0; j2 < list1.size(); j2++)
                {
                    d1 = ((AxisAlignedBB) list1.get(j2)).calculateYOffset(boundingBox, d1);
                }

                boundingBox.offset(0.0D, d1, 0.0D);
                if (!field_9077_F && d6 != d1)
                {
                    d = d1 = d2 = 0.0D;
                }
                for (int k2 = 0; k2 < list1.size(); k2++)
                {
                    d = ((AxisAlignedBB) list1.get(k2)).calculateXOffset(boundingBox, d);
                }

                boundingBox.offset(d, 0.0D, 0.0D);
                if (!field_9077_F && d5 != d)
                {
                    d = d1 = d2 = 0.0D;
                }
                for (int l2 = 0; l2 < list1.size(); l2++)
                {
                    d2 = ((AxisAlignedBB) list1.get(l2)).calculateZOffset(boundingBox, d2);
                }

                boundingBox.offset(0.0D, 0.0D, d2);
                if (!field_9077_F && d7 != d2)
                {
                    d = d1 = d2 = 0.0D;
                }
                if (d9*d9 + d13*d13 >= d*d + d2*d2)
                {
                    d = d9;
                    d1 = d11;
                    d2 = d13;
                    boundingBox.setBB(axisalignedbb1);
                }
                else
                {
                    ySize += (float) 0.5D;
                }
            }
            posX = (boundingBox.minX + boundingBox.maxX)/2D;
            posY = (boundingBox.minY + yOffset) - ySize;
            posZ = (boundingBox.minZ + boundingBox.maxZ)/2D;
            isCollidedHorizontally = d5 != d || d7 != d2;
            isCollidedVertically = d6 != d1;
            onGround = d6 != d1 && d6 < 0.0D;
            isCollided = isCollidedHorizontally || isCollidedVertically;
            updateFallState(d1, onGround);
            if (d5 != d)
            {
                motionX = 0.0D;
            }
            if (d6 != d1)
            {
                motionY = 0.0D;
            }
            if (d7 != d2)
            {
                motionZ = 0.0D;
            }
            double d10 = posX - d3;
            double d12 = posZ - d4;
            if (entityWalks && !flag)
            {
                distanceWalkedModified +=
                    (float) (MathHelper.sqrt_double(d10*d10 + d12*d12)*0.59999999999999998D);
                int l = MathHelper.floor_double(posX);
                int j1 = MathHelper.floor_double(posY - 0.20000000298023224D - yOffset);
                int l1 = MathHelper.floor_double(posZ);
                int i3 = worldObj.getBlockId(l, j1, l1);
                if (distanceWalkedModified > nextStepDistance && i3 > 0)
                {
                    nextStepDistance++;
                    StepSound stepsound = Block.blocksList[i3].stepSound;
                    if (worldObj.getBlockId(l, j1 + 1, l1) == Block.snow.blockID)
                    {
                        stepsound = Block.snow.stepSound;
                        worldObj.playSoundAtEntity(this, stepsound.func_737_c(), stepsound.func_738_a()*0.15F,
                                                   stepsound.func_739_b());
                    }
                    else if (!Block.blocksList[i3].blockMaterial.getIsLiquid())
                    {
                        worldObj.playSoundAtEntity(this, stepsound.func_737_c(), stepsound.func_738_a()*0.15F,
                                                   stepsound.func_739_b());
                    }
                    Block.blocksList[i3].onEntityWalking(worldObj, l, j1, l1, this);
                }
            }
            int i1 = MathHelper.floor_double(boundingBox.minX);
            int k1 = MathHelper.floor_double(boundingBox.minY);
            int i2 = MathHelper.floor_double(boundingBox.minZ);
            int j3 = MathHelper.floor_double(boundingBox.maxX);
            int k3 = MathHelper.floor_double(boundingBox.maxY);
            int l3 = MathHelper.floor_double(boundingBox.maxZ);
            if (worldObj.checkChunksExist(i1, k1, i2, j3, k3, l3))
            {
                for (int i4 = i1; i4 <= j3; i4++)
                {
                    for (int j4 = k1; j4 <= k3; j4++)
                    {
                        for (int k4 = i2; k4 <= l3; k4++)
                        {
                            int l4 = worldObj.getBlockId(i4, j4, k4);
                            if (l4 > 0)
                            {
                                Block.blocksList[l4].onEntityCollidedWithBlock(worldObj, i4, j4, k4, this);
                            }
                        }
                    }
                }
            }
            ySize *= 0.4F;
            bool flag2 = handleWaterMovement();
            if (worldObj.isBoundingBoxBurning(boundingBox))
            {
                dealFireDamage(1);
                if (!flag2)
                {
                    fire++;
                    if (fire == 0)
                    {
                        fire = 300;
                    }
                }
            }
            else if (fire <= 0)
            {
                fire = -fireResistance;
            }
            if (flag2 && fire > 0)
            {
                worldObj.playSoundAtEntity(this, "random.fizz", 0.7F, 1.6F + (rand.nextFloat() - rand.nextFloat())*0.4F);
                fire = -fireResistance;
            }
        }

        public virtual void updateFallState(double d, bool flag)
        {
            if (flag)
            {
                if (fallDistance > 0.0F)
                {
                    fall(fallDistance);
                    fallDistance = 0.0F;
                }
            }
            else if (d < 0.0D)
            {
                fallDistance -= (float) d;
            }
        }

        public virtual AxisAlignedBB getBoundingBox()
        {
            return null;
        }

        public virtual void dealFireDamage(int i)
        {
            if (!isImmuneToFire)
            {
                attackEntityFrom(null, i);
            }
        }

        public virtual void fall(float f)
        {
        }

        public virtual bool handleWaterMovement()
        {
            return worldObj.handleMaterialAcceleration(boundingBox.expand(0.0D, -0.40000000596046448D, 0.0D),
                                                       Material.water, this);
        }

        public virtual bool isInsideOfMaterial(Material material)
        {
            double d = posY + getEyeHeight();
            int i = MathHelper.floor_double(posX);
            int j = MathHelper.floor_float(MathHelper.floor_double(d));
            int k = MathHelper.floor_double(posZ);
            int l = worldObj.getBlockId(i, j, k);
            if (l != 0 && Block.blocksList[l].blockMaterial == material)
            {
                float f = BlockFluids.setFluidHeight(worldObj.getBlockMetadata(i, j, k)) - 0.1111111F;
                float f1 = (j + 1) - f;
                return d < f1;
            }
            else
            {
                return false;
            }
        }

        public virtual float getEyeHeight()
        {
            return 0.0F;
        }

        public bool handleLavaMovement()
        {
            return
                worldObj.isMaterialInBB(
                    boundingBox.expand(-0.10000000149011612D, -0.40000000596046448D, -0.10000000149011612D),
                    Material.lava);
        }

        public void moveFlying(float f, float f1, float f2)
        {
            float f3 = MathHelper.sqrt_float(f*f + f1*f1);
            if (f3 < 0.01F)
            {
                return;
            }
            if (f3 < 1.0F)
            {
                f3 = 1.0F;
            }
            f3 = f2/f3;
            f *= f3;
            f1 *= f3;
            float f4 = MathHelper.sin((rotationYaw*3.141593F)/180F);
            float f5 = MathHelper.cos((rotationYaw*3.141593F)/180F);
            motionX += f*f5 - f1*f4;
            motionZ += f1*f5 + f*f4;
        }

        public float getEntityBrightness(float f)
        {
            int i = MathHelper.floor_double(posX);
            double d = (boundingBox.maxY - boundingBox.minY)*0.66000000000000003D;
            int j = MathHelper.floor_double((posY - yOffset) + d);
            int k = MathHelper.floor_double(posZ);
            if (worldObj.checkChunksExist(MathHelper.floor_double(boundingBox.minX),
                                          MathHelper.floor_double(boundingBox.minY),
                                          MathHelper.floor_double(boundingBox.minZ),
                                          MathHelper.floor_double(boundingBox.maxX),
                                          MathHelper.floor_double(boundingBox.maxY),
                                          MathHelper.floor_double(boundingBox.maxZ)))
            {
                return worldObj.getLightBrightness(i, j, k);
            }
            else
            {
                return 0.0F;
            }
        }

        public void setPositionAndRotation(double d, double d1, double d2, float f,
                                           float f1)
        {
            prevPosX = posX = d;
            prevPosY = posY = d1;
            prevPosZ = posZ = d2;
            prevRotationYaw = rotationYaw = f;
            prevRotationPitch = rotationPitch = f1;
            ySize = 0.0F;
            double d3 = prevRotationYaw - f;
            if (d3 < -180D)
            {
                prevRotationYaw += 360F;
            }
            if (d3 >= 180D)
            {
                prevRotationYaw -= 360F;
            }
            setPosition(posX, posY, posZ);
            setRotation(f, f1);
        }

        public void setLocationAndAngles(double d, double d1, double d2, float f,
                                         float f1)
        {
            lastTickPosX = prevPosX = posX = d;
            lastTickPosY = prevPosY = posY = d1 + yOffset;
            lastTickPosZ = prevPosZ = posZ = d2;
            rotationYaw = f;
            rotationPitch = f1;
            setPosition(posX, posY, posZ);
        }

        public float getDistanceToEntity(Entity entity)
        {
            var f = (float) (posX - entity.posX);
            var f1 = (float) (posY - entity.posY);
            var f2 = (float) (posZ - entity.posZ);
            return MathHelper.sqrt_float(f*f + f1*f1 + f2*f2);
        }

        public double getDistanceSq(double d, double d1, double d2)
        {
            double d3 = posX - d;
            double d4 = posY - d1;
            double d5 = posZ - d2;
            return d3*d3 + d4*d4 + d5*d5;
        }

        public double getDistance(double d, double d1, double d2)
        {
            double d3 = posX - d;
            double d4 = posY - d1;
            double d5 = posZ - d2;
            return MathHelper.sqrt_double(d3*d3 + d4*d4 + d5*d5);
        }

        public double getDistanceSqToEntity(Entity entity)
        {
            double d = posX - entity.posX;
            double d1 = posY - entity.posY;
            double d2 = posZ - entity.posZ;
            return d*d + d1*d1 + d2*d2;
        }

        public virtual void onCollideWithPlayer(EntityPlayer entityplayer)
        {
        }

        public virtual void applyEntityCollision(Entity entity)
        {
            if (entity.riddenByEntity == this || entity.ridingEntity == this)
            {
                return;
            }
            double d = entity.posX - posX;
            double d1 = entity.posZ - posZ;
            double d2 = MathHelper.abs_max(d, d1);
            if (d2 >= 0.0099999997764825821D)
            {
                d2 = MathHelper.sqrt_double(d2);
                d /= d2;
                d1 /= d2;
                double d3 = 1.0D/d2;
                if (d3 > 1.0D)
                {
                    d3 = 1.0D;
                }
                d *= d3;
                d1 *= d3;
                d *= 0.05000000074505806D;
                d1 *= 0.05000000074505806D;
                d *= 1.0F - entityCollisionReduction;
                d1 *= 1.0F - entityCollisionReduction;
                addVelocity(-d, 0.0D, -d1);
                entity.addVelocity(d, 0.0D, d1);
            }
        }

        public void addVelocity(double d, double d1, double d2)
        {
            motionX += d;
            motionY += d1;
            motionZ += d2;
        }

        public virtual void setBeenAttacked()
        {
            beenAttacked = true;
        }

        public virtual bool attackEntityFrom(Entity entity, int i)
        {
            setBeenAttacked();
            return false;
        }

        public virtual bool canBeCollidedWith()
        {
            return false;
        }

        public virtual bool canBePushed()
        {
            return false;
        }

        public virtual void addToPlayerScore(Entity entity, int i)
        {
        }

        public virtual bool addEntityID(NBTTagCompound nbttagcompound)
        {
            string s = getEntityString();
            if (isDead || s == null)
            {
                return false;
            }
            else
            {
                nbttagcompound.setString("id", s);
                writeToNBT(nbttagcompound);
                return true;
            }
        }

        public virtual void writeToNBT(NBTTagCompound nbttagcompound)
        {
            nbttagcompound.setTag("Pos", newDoubleNBTList(new[]
                                                          {
                                                              posX, posY, posZ
                                                          }));
            nbttagcompound.setTag("Motion", newDoubleNBTList(new[]
                                                             {
                                                                 motionX, motionY, motionZ
                                                             }));
            nbttagcompound.setTag("Rotation", newFloatNBTList(new[]
                                                              {
                                                                  rotationYaw, rotationPitch
                                                              }));
            nbttagcompound.setFloat("FallDistance", fallDistance);
            nbttagcompound.setShort("Fire", (short) fire);
            nbttagcompound.setShort("Air", (short) air);
            nbttagcompound.setBoolean("OnGround", onGround);
            writeEntityToNBT(nbttagcompound);
        }

        public virtual void readFromNBT(NBTTagCompound nbttagcompound)
        {
            NBTTagList nbttaglist = nbttagcompound.getTagList("Pos");
            NBTTagList nbttaglist1 = nbttagcompound.getTagList("Motion");
            NBTTagList nbttaglist2 = nbttagcompound.getTagList("Rotation");
            setPosition(0.0D, 0.0D, 0.0D);
            motionX = ((NBTTagDouble) nbttaglist1.tagAt(0)).doubleValue;
            motionY = ((NBTTagDouble) nbttaglist1.tagAt(1)).doubleValue;
            motionZ = ((NBTTagDouble) nbttaglist1.tagAt(2)).doubleValue;
            if (Math.abs(motionX) > 10D)
            {
                motionX = 0.0D;
            }
            if (Math.abs(motionY) > 10D)
            {
                motionY = 0.0D;
            }
            if (Math.abs(motionZ) > 10D)
            {
                motionZ = 0.0D;
            }
            prevPosX = lastTickPosX = posX = ((NBTTagDouble) nbttaglist.tagAt(0)).doubleValue;
            prevPosY = lastTickPosY = posY = ((NBTTagDouble) nbttaglist.tagAt(1)).doubleValue;
            prevPosZ = lastTickPosZ = posZ = ((NBTTagDouble) nbttaglist.tagAt(2)).doubleValue;
            prevRotationYaw = rotationYaw = ((NBTTagFloat) nbttaglist2.tagAt(0)).floatValue%6.283185F;
            prevRotationPitch = rotationPitch = ((NBTTagFloat) nbttaglist2.tagAt(1)).floatValue%6.283185F;
            fallDistance = nbttagcompound.getFloat("FallDistance");
            fire = nbttagcompound.getShort("Fire");
            air = nbttagcompound.getShort("Air");
            onGround = nbttagcompound.getBoolean("OnGround");
            setPosition(posX, posY, posZ);
            readEntityFromNBT(nbttagcompound);
        }

        protected string getEntityString()
        {
            return EntityList.getEntityString(this);
        }

        public abstract void readEntityFromNBT(NBTTagCompound nbttagcompound);

        public abstract void writeEntityToNBT(NBTTagCompound nbttagcompound);

        protected NBTTagList newDoubleNBTList(double[] ad)
        {
            var nbttaglist = new NBTTagList();
            double[] ad1 = ad;
            int i = ad1.Length;
            for (int j = 0; j < i; j++)
            {
                double d = ad1[j];
                nbttaglist.setTag(new NBTTagDouble(d));
            }

            return nbttaglist;
        }

        protected NBTTagList newFloatNBTList(float[] af)
        {
            var nbttaglist = new NBTTagList();
            float[] af1 = af;
            int i = af1.Length;
            for (int j = 0; j < i; j++)
            {
                float f = af1[j];
                nbttaglist.setTag(new NBTTagFloat(f));
            }

            return nbttaglist;
        }

        public virtual EntityItem dropItem(int i, int j)
        {
            return dropItemWithOffset(i, j, 0.0F);
        }

        public virtual EntityItem dropItemWithOffset(int i, int j, float f)
        {
            return entityDropItem(new ItemStack(i, j, 0), f);
        }

        public virtual EntityItem entityDropItem(ItemStack itemstack, float f)
        {
            var entityitem = new EntityItem(worldObj, posX, posY + f, posZ, itemstack);
            entityitem.delayBeforeCanPickup = 10;
            worldObj.entityJoinedWorld(entityitem);
            return entityitem;
        }

        public virtual bool isEntityAlive()
        {
            return !isDead;
        }

        public virtual bool func_91_u()
        {
            int i = MathHelper.floor_double(posX);
            int j = MathHelper.floor_double(posY + getEyeHeight());
            int k = MathHelper.floor_double(posZ);
            return worldObj.isBlockOpaqueCube(i, j, k);
        }

        public virtual bool interact(EntityPlayer entityplayer)
        {
            return false;
        }

        public virtual AxisAlignedBB func_89_d(Entity entity)
        {
            return null;
        }

        public virtual void updateRidden()
        {
            if (ridingEntity.isDead)
            {
                ridingEntity = null;
                return;
            }
            motionX = 0.0D;
            motionY = 0.0D;
            motionZ = 0.0D;
            onUpdate();
            ridingEntity.updateRiderPosition();
            entityRiderYawDelta += ridingEntity.rotationYaw - ridingEntity.prevRotationYaw;
            entityRiderPitchDelta += ridingEntity.rotationPitch - ridingEntity.prevRotationPitch;
            for (; entityRiderYawDelta >= 180D; entityRiderYawDelta -= 360D)
            {
            }
            for (; entityRiderYawDelta < -180D; entityRiderYawDelta += 360D)
            {
            }
            for (; entityRiderPitchDelta >= 180D; entityRiderPitchDelta -= 360D)
            {
            }
            for (; entityRiderPitchDelta < -180D; entityRiderPitchDelta += 360D)
            {
            }
            double d = entityRiderYawDelta*0.5D;
            double d1 = entityRiderPitchDelta*0.5D;
            float f = 10F;
            if (d > f)
            {
                d = f;
            }
            if (d < (-f))
            {
                d = -f;
            }
            if (d1 > f)
            {
                d1 = f;
            }
            if (d1 < (-f))
            {
                d1 = -f;
            }
            entityRiderYawDelta -= d;
            entityRiderPitchDelta -= d1;
            rotationYaw += (float) d;
            rotationPitch += (float) d1;
        }

        public virtual void updateRiderPosition()
        {
            riddenByEntity.setPosition(posX, posY + getMountedYOffset() + riddenByEntity.getYOffset(), posZ);
        }

        public virtual double getYOffset()
        {
            return yOffset;
        }

        public virtual double getMountedYOffset()
        {
            return height*0.75D;
        }

        public virtual void mountEntity(Entity entity)
        {
            entityRiderPitchDelta = 0.0D;
            entityRiderYawDelta = 0.0D;
            if (entity == null)
            {
                if (ridingEntity != null)
                {
                    setLocationAndAngles(ridingEntity.posX, ridingEntity.boundingBox.minY + ridingEntity.height,
                                         ridingEntity.posZ, rotationYaw, rotationPitch);
                    ridingEntity.riddenByEntity = null;
                }
                ridingEntity = null;
                return;
            }
            if (ridingEntity == entity)
            {
                ridingEntity.riddenByEntity = null;
                ridingEntity = null;
                setLocationAndAngles(entity.posX, entity.boundingBox.minY + entity.height, entity.posZ,
                                     rotationYaw, rotationPitch);
                return;
            }
            if (ridingEntity != null)
            {
                ridingEntity.riddenByEntity = null;
            }
            if (entity.riddenByEntity != null)
            {
                entity.riddenByEntity.ridingEntity = null;
            }
            ridingEntity = entity;
            entity.riddenByEntity = this;
        }

        public virtual Vec3D getLookVec()
        {
            return null;
        }

        public virtual void setInPortal()
        {
        }

        public virtual ItemStack[] getInventory()
        {
            return null;
        }

        public virtual bool isSneaking()
        {
            return func_21042_c(1);
        }

        public void func_21043_b(bool flag)
        {
            func_21041_a(1, flag);
        }

        protected bool func_21042_c(int i)
        {
            return (dataWatcher.getWatchableObjectByte(0) & 1 << i) != 0;
        }

        public void func_21041_a(int i, bool flag)
        {
            byte byte0 = dataWatcher.getWatchableObjectByte(0);
            if (flag)
            {
                dataWatcher.updateObject(0, Byte.valueOf((byte) (byte0 | 1 << i)));
            }
            else
            {
                dataWatcher.updateObject(0, Byte.valueOf((byte) (byte0 & ~(1 << i))));
            }
        }
    }
}