using java.lang;
using java.util;

namespace CraftyServer.Core
{
    public abstract class EntityLiving : Entity
    {
        protected int age;
        public int attackTime;
        public float attackedAtYaw;
        private Entity currentTarget;
        public int deathTime;
        protected float defaultPitch;
        protected string entityType;
        public float field_386_ba;
        private int field_4099_a;
        public float field_9096_ay;
        public float field_9098_aw;
        public int field_9099_av;
        protected bool field_9100_aZ;
        public float field_9101_aY;
        public float field_9102_aX;
        public bool field_9112_aN;
        protected float field_9113_aM;
        protected float field_9115_aK;
        protected float field_9117_aI;
        protected bool field_9118_aH;
        protected bool field_9120_aF;
        protected float field_9121_aE;
        protected float field_9122_aD;
        protected float field_9123_aC;
        protected float field_9124_aB;
        protected int field_9133_bm;
        private float field_9134_bl;
        protected double field_9135_bk;
        protected double field_9136_bj;
        protected double field_9137_bi;
        protected double field_9138_bh;
        protected double field_9139_bg;
        protected int field_9140_bf;
        public float field_9141_bd;
        public float field_9142_bc;
        public float field_9143_bb;
        public int field_9144_ba;
        public int health;
        public int hurtTime;
        protected bool isJumping;
        public int maxHurtTime;
        protected float moveForward;
        protected float moveSpeed;
        protected float moveStrafing;
        private int numTicksToChaseTarget;
        public int prevHealth;
        public float prevRenderYawOffset;
        public float prevSwingProgress;
        protected float randomYawVelocity;
        public float renderYawOffset;
        protected int scoreValue;
        public float swingProgress;
        protected string texture;

        public EntityLiving(World world) : base(world)
        {
            field_9099_av = 20;
            renderYawOffset = 0.0F;
            prevRenderYawOffset = 0.0F;
            field_9120_aF = true;
            texture = "/mob/char.png";
            field_9118_aH = true;
            field_9117_aI = 0.0F;
            entityType = null;
            field_9115_aK = 1.0F;
            scoreValue = 0;
            field_9113_aM = 0.0F;
            field_9112_aN = false;
            attackedAtYaw = 0.0F;
            deathTime = 0;
            attackTime = 0;
            field_9100_aZ = false;
            field_9144_ba = -1;
            field_9143_bb = (float) (Math.random()*0.89999997615814209D + 0.10000000149011612D);
            field_9134_bl = 0.0F;
            field_9133_bm = 0;
            age = 0;
            isJumping = false;
            defaultPitch = 0.0F;
            moveSpeed = 0.7F;
            numTicksToChaseTarget = 0;
            health = 10;
            preventEntitySpawning = true;
            field_9096_ay = (float) (Math.random() + 1.0D)*0.01F;
            setPosition(posX, posY, posZ);
            field_9098_aw = (float) Math.random()*12398F;
            rotationYaw = (float) (Math.random()*3.1415927410125732D*2D);
            stepHeight = 0.5F;
        }

        protected override void entityInit()
        {
        }

        public bool canEntityBeSeen(Entity entity)
        {
            return
                worldObj.rayTraceBlocks(Vec3D.createVector(posX, posY + getEyeHeight(), posZ),
                                        Vec3D.createVector(entity.posX, entity.posY + entity.getEyeHeight(),
                                                           entity.posZ)) == null;
        }

        public override bool canBeCollidedWith()
        {
            return !isDead;
        }

        public override bool canBePushed()
        {
            return !isDead;
        }

        public override float getEyeHeight()
        {
            return height*0.85F;
        }

        public virtual int func_146_b()
        {
            return 80;
        }

        public void func_22056_G()
        {
            string s = getLivingSound();
            if (s != null)
            {
                worldObj.playSoundAtEntity(this, s, getSoundVolume(), (rand.nextFloat() - rand.nextFloat())*0.2F + 1.0F);
            }
        }

        public override void onEntityUpdate()
        {
            prevSwingProgress = swingProgress;
            base.onEntityUpdate();
            if (rand.nextInt(1000) < field_4099_a++)
            {
                field_4099_a = -func_146_b();
                func_22056_G();
            }
            if (isEntityAlive() && func_91_u())
            {
                attackEntityFrom(null, 1);
            }
            if (isImmuneToFire || worldObj.singleplayerWorld)
            {
                fire = 0;
            }
            if (isEntityAlive() && isInsideOfMaterial(Material.water) && !canBreatheUnderwater())
            {
                air--;
                if (air == -20)
                {
                    air = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        float f = rand.nextFloat() - rand.nextFloat();
                        float f1 = rand.nextFloat() - rand.nextFloat();
                        float f2 = rand.nextFloat() - rand.nextFloat();
                        worldObj.spawnParticle("bubble", posX + f, posY + f1, posZ + f2,
                                               motionX, motionY, motionZ);
                    }

                    attackEntityFrom(null, 2);
                }
                fire = 0;
            }
            else
            {
                air = maxAir;
            }
            field_9102_aX = field_9101_aY;
            if (attackTime > 0)
            {
                attackTime--;
            }
            if (hurtTime > 0)
            {
                hurtTime--;
            }
            if (field_9083_ac > 0)
            {
                field_9083_ac--;
            }
            if (health <= 0)
            {
                deathTime++;
                if (deathTime > 20)
                {
                    func_6101_K();
                    setEntityDead();
                    for (int j = 0; j < 20; j++)
                    {
                        double d = rand.nextGaussian()*0.02D;
                        double d1 = rand.nextGaussian()*0.02D;
                        double d2 = rand.nextGaussian()*0.02D;
                        worldObj.spawnParticle("explode",
                                               (posX + (rand.nextFloat()*width*2.0F)) - width,
                                               posY + (rand.nextFloat()*height),
                                               (posZ + (rand.nextFloat()*width*2.0F)) - width, d, d1,
                                               d2);
                    }
                }
            }
            field_9121_aE = field_9122_aD;
            prevRenderYawOffset = renderYawOffset;
            prevRotationYaw = rotationYaw;
            prevRotationPitch = rotationPitch;
        }

        public void spawnExplosionParticle()
        {
            for (int i = 0; i < 20; i++)
            {
                double d = rand.nextGaussian()*0.02D;
                double d1 = rand.nextGaussian()*0.02D;
                double d2 = rand.nextGaussian()*0.02D;
                double d3 = 10D;
                worldObj.spawnParticle("explode",
                                       (posX + (rand.nextFloat()*width*2.0F)) - width - d*d3,
                                       (posY + (rand.nextFloat()*height)) - d1*d3,
                                       (posZ + (rand.nextFloat()*width*2.0F)) - width - d2*d3, d, d1,
                                       d2);
            }
        }

        public override void updateRidden()
        {
            base.updateRidden();
            field_9124_aB = field_9123_aC;
            field_9123_aC = 0.0F;
        }

        public override void onUpdate()
        {
            base.onUpdate();
            onLivingUpdate();
            double d = posX - prevPosX;
            double d1 = posZ - prevPosZ;
            float f = MathHelper.sqrt_double(d*d + d1*d1);
            float f1 = renderYawOffset;
            float f2 = 0.0F;
            field_9124_aB = field_9123_aC;
            float f3 = 0.0F;
            if (f > 0.05F)
            {
                f3 = 1.0F;
                f2 = f*3F;
                f1 = ((float) Math.atan2(d1, d)*180F)/3.141593F - 90F;
            }
            if (swingProgress > 0.0F)
            {
                f1 = rotationYaw;
            }
            if (!onGround)
            {
                f3 = 0.0F;
            }
            field_9123_aC = field_9123_aC + (f3 - field_9123_aC)*0.3F;
            float f4;
            for (f4 = f1 - renderYawOffset; f4 < -180F; f4 += 360F)
            {
            }
            for (; f4 >= 180F; f4 -= 360F)
            {
            }
            renderYawOffset += f4*0.3F;
            float f5;
            for (f5 = rotationYaw - renderYawOffset; f5 < -180F; f5 += 360F)
            {
            }
            for (; f5 >= 180F; f5 -= 360F)
            {
            }
            bool flag = f5 < -90F || f5 >= 90F;
            if (f5 < -75F)
            {
                f5 = -75F;
            }
            if (f5 >= 75F)
            {
                f5 = 75F;
            }
            renderYawOffset = rotationYaw - f5;
            if (f5*f5 > 2500F)
            {
                renderYawOffset += f5*0.2F;
            }
            if (flag)
            {
                f2 *= -1F;
            }
            for (; rotationYaw - prevRotationYaw < -180F; prevRotationYaw -= 360F)
            {
            }
            for (; rotationYaw - prevRotationYaw >= 180F; prevRotationYaw += 360F)
            {
            }
            for (; renderYawOffset - prevRenderYawOffset < -180F; prevRenderYawOffset -= 360F)
            {
            }
            for (; renderYawOffset - prevRenderYawOffset >= 180F; prevRenderYawOffset += 360F)
            {
            }
            for (; rotationPitch - prevRotationPitch < -180F; prevRotationPitch -= 360F)
            {
            }
            for (; rotationPitch - prevRotationPitch >= 180F; prevRotationPitch += 360F)
            {
            }
            field_9122_aD += f2;
        }

        public override void setSize(float f, float f1)
        {
            base.setSize(f, f1);
        }

        public virtual void heal(int i)
        {
            if (health <= 0)
            {
                return;
            }
            health += i;
            if (health > 20)
            {
                health = 20;
            }
            field_9083_ac = field_9099_av/2;
        }

        public override bool attackEntityFrom(Entity entity, int i)
        {
            if (worldObj.singleplayerWorld)
            {
                return false;
            }
            age = 0;
            if (health <= 0)
            {
                return false;
            }
            field_9141_bd = 1.5F;
            bool flag = true;
            if (field_9083_ac > field_9099_av/2.0F)
            {
                if (i <= field_9133_bm)
                {
                    return false;
                }
                damageEntity(i - field_9133_bm);
                field_9133_bm = i;
                flag = false;
            }
            else
            {
                field_9133_bm = i;
                prevHealth = health;
                field_9083_ac = field_9099_av;
                damageEntity(i);
                hurtTime = maxHurtTime = 10;
            }
            attackedAtYaw = 0.0F;
            if (flag)
            {
                worldObj.func_9206_a(this, 2);
                setBeenAttacked();
                if (entity != null)
                {
                    double d = entity.posX - posX;
                    double d1;
                    for (d1 = entity.posZ - posZ; d*d + d1*d1 < 0.0001D; d1 = (Math.random() - Math.random())*0.01D)
                    {
                        d = (Math.random() - Math.random())*0.01D;
                    }

                    attackedAtYaw = (float) ((Math.atan2(d1, d)*180D)/3.1415927410125732D) - rotationYaw;
                    knockBack(entity, i, d, d1);
                }
                else
                {
                    attackedAtYaw = (int) (Math.random()*2D)*180;
                }
            }
            if (health <= 0)
            {
                if (flag)
                {
                    worldObj.playSoundAtEntity(this, getDeathSound(), getSoundVolume(),
                                               (rand.nextFloat() - rand.nextFloat())*0.2F + 1.0F);
                }
                onDeath(entity);
            }
            else if (flag)
            {
                worldObj.playSoundAtEntity(this, getHurtSound(), getSoundVolume(),
                                           (rand.nextFloat() - rand.nextFloat())*0.2F + 1.0F);
            }
            return true;
        }

        public virtual void damageEntity(int i)
        {
            health -= i;
        }

        protected virtual float getSoundVolume()
        {
            return 1.0F;
        }

        protected virtual string getLivingSound()
        {
            return null;
        }

        protected virtual string getHurtSound()
        {
            return "random.hurt";
        }

        protected virtual string getDeathSound()
        {
            return "random.hurt";
        }

        public void knockBack(Entity entity, int i, double d, double d1)
        {
            float f = MathHelper.sqrt_double(d*d + d1*d1);
            float f1 = 0.4F;
            motionX /= 2D;
            motionY /= 2D;
            motionZ /= 2D;
            motionX -= (d/f)*f1;
            motionY += 0.40000000596046448D;
            motionZ -= (d1/f)*f1;
            if (motionY > 0.40000000596046448D)
            {
                motionY = 0.40000000596046448D;
            }
        }

        public virtual void onDeath(Entity entity)
        {
            if (scoreValue > 0 && entity != null)
            {
                entity.addToPlayerScore(this, scoreValue);
            }
            field_9100_aZ = true;
            if (!worldObj.singleplayerWorld)
            {
                func_21047_g_();
            }
            worldObj.func_9206_a(this, 3);
        }

        public virtual void func_21047_g_()
        {
            int i = getDropItemId();
            if (i > 0)
            {
                int j = rand.nextInt(3);
                for (int k = 0; k < j; k++)
                {
                    dropItem(i, 1);
                }
            }
        }

        protected virtual int getDropItemId()
        {
            return 0;
        }

        public override void fall(float f)
        {
            var i = (int) Math.ceil(f - 3F);
            if (i > 0)
            {
                attackEntityFrom(null, i);
                int j = worldObj.getBlockId(MathHelper.floor_double(posX),
                                            MathHelper.floor_double(posY - 0.20000000298023224D - yOffset),
                                            MathHelper.floor_double(posZ));
                if (j > 0)
                {
                    StepSound stepsound = Block.blocksList[j].stepSound;
                    worldObj.playSoundAtEntity(this, stepsound.func_737_c(), stepsound.func_738_a()*0.5F,
                                               stepsound.func_739_b()*0.75F);
                }
            }
        }

        public virtual void moveEntityWithHeading(float f, float f1)
        {
            if (handleWaterMovement())
            {
                double d = posY;
                moveFlying(f, f1, 0.02F);
                moveEntity(motionX, motionY, motionZ);
                motionX *= 0.80000001192092896D;
                motionY *= 0.80000001192092896D;
                motionZ *= 0.80000001192092896D;
                motionY -= 0.02D;
                if (isCollidedHorizontally &&
                    isOffsetPositionInLiquid(motionX, ((motionY + 0.60000002384185791D) - posY) + d, motionZ))
                {
                    motionY = 0.30000001192092896D;
                }
            }
            else if (handleLavaMovement())
            {
                double d1 = posY;
                moveFlying(f, f1, 0.02F);
                moveEntity(motionX, motionY, motionZ);
                motionX *= 0.5D;
                motionY *= 0.5D;
                motionZ *= 0.5D;
                motionY -= 0.02D;
                if (isCollidedHorizontally &&
                    isOffsetPositionInLiquid(motionX, ((motionY + 0.60000002384185791D) - posY) + d1, motionZ))
                {
                    motionY = 0.30000001192092896D;
                }
            }
            else
            {
                float f2 = 0.91F;
                if (onGround)
                {
                    f2 = 0.5460001F;
                    int i = worldObj.getBlockId(MathHelper.floor_double(posX),
                                                MathHelper.floor_double(boundingBox.minY) - 1,
                                                MathHelper.floor_double(posZ));
                    if (i > 0)
                    {
                        f2 = Block.blocksList[i].slipperiness*0.91F;
                    }
                }
                float f3 = 0.1627714F/(f2*f2*f2);
                moveFlying(f, f1, onGround ? 0.1F*f3 : 0.02F);
                f2 = 0.91F;
                if (onGround)
                {
                    f2 = 0.5460001F;
                    int j = worldObj.getBlockId(MathHelper.floor_double(posX),
                                                MathHelper.floor_double(boundingBox.minY) - 1,
                                                MathHelper.floor_double(posZ));
                    if (j > 0)
                    {
                        f2 = Block.blocksList[j].slipperiness*0.91F;
                    }
                }
                if (isOnLadder())
                {
                    fallDistance = 0.0F;
                    if (motionY < -0.14999999999999999D)
                    {
                        motionY = -0.14999999999999999D;
                    }
                }
                moveEntity(motionX, motionY, motionZ);
                if (isCollidedHorizontally && isOnLadder())
                {
                    motionY = 0.20000000000000001D;
                }
                motionY -= 0.080000000000000002D;
                motionY *= 0.98000001907348633D;
                motionX *= f2;
                motionZ *= f2;
            }
            field_9142_bc = field_9141_bd;
            double d2 = posX - prevPosX;
            double d3 = posZ - prevPosZ;
            float f4 = MathHelper.sqrt_double(d2*d2 + d3*d3)*4F;
            if (f4 > 1.0F)
            {
                f4 = 1.0F;
            }
            field_9141_bd += (f4 - field_9141_bd)*0.4F;
            field_386_ba += field_9141_bd;
        }

        public virtual bool isOnLadder()
        {
            int i = MathHelper.floor_double(posX);
            int j = MathHelper.floor_double(boundingBox.minY);
            int k = MathHelper.floor_double(posZ);
            return worldObj.getBlockId(i, j, k) == Block.ladder.blockID ||
                   worldObj.getBlockId(i, j + 1, k) == Block.ladder.blockID;
        }

        public override void writeEntityToNBT(NBTTagCompound nbttagcompound)
        {
            nbttagcompound.setShort("Health", (short) health);
            nbttagcompound.setShort("HurtTime", (short) hurtTime);
            nbttagcompound.setShort("DeathTime", (short) deathTime);
            nbttagcompound.setShort("AttackTime", (short) attackTime);
        }

        public override void readEntityFromNBT(NBTTagCompound nbttagcompound)
        {
            health = nbttagcompound.getShort("Health");
            if (!nbttagcompound.hasKey("Health"))
            {
                health = 10;
            }
            hurtTime = nbttagcompound.getShort("HurtTime");
            deathTime = nbttagcompound.getShort("DeathTime");
            attackTime = nbttagcompound.getShort("AttackTime");
        }

        public override bool isEntityAlive()
        {
            return !isDead && health > 0;
        }

        public virtual bool canBreatheUnderwater()
        {
            return false;
        }

        public virtual void onLivingUpdate()
        {
            if (field_9140_bf > 0)
            {
                double d = posX + (field_9139_bg - posX)/field_9140_bf;
                double d1 = posY + (field_9138_bh - posY)/field_9140_bf;
                double d2 = posZ + (field_9137_bi - posZ)/field_9140_bf;
                double d3;
                for (d3 = field_9136_bj - rotationYaw; d3 < -180D; d3 += 360D)
                {
                }
                for (; d3 >= 180D; d3 -= 360D)
                {
                }
                rotationYaw += (float) (d3/field_9140_bf);
                rotationPitch += (float) ((field_9135_bk - rotationPitch)/field_9140_bf);
                field_9140_bf--;
                setPosition(d, d1, d2);
                setRotation(rotationYaw, rotationPitch);
            }
            if (canMove())
            {
                isJumping = false;
                moveStrafing = 0.0F;
                moveForward = 0.0F;
                randomYawVelocity = 0.0F;
            }
            else if (!field_9112_aN)
            {
                updatePlayerActionState();
            }
            bool flag = handleWaterMovement();
            bool flag1 = handleLavaMovement();
            if (isJumping)
            {
                if (flag)
                {
                    motionY += 0.039999999105930328D;
                }
                else if (flag1)
                {
                    motionY += 0.039999999105930328D;
                }
                else if (onGround)
                {
                    jump();
                }
            }
            moveStrafing *= 0.98F;
            moveForward *= 0.98F;
            randomYawVelocity *= 0.9F;
            moveEntityWithHeading(moveStrafing, moveForward);
            List list = worldObj.getEntitiesWithinAABBExcludingEntity(this,
                                                                      boundingBox.expand(0.20000000298023224D, 0.0D,
                                                                                         0.20000000298023224D));
            if (list != null && list.size() > 0)
            {
                for (int i = 0; i < list.size(); i++)
                {
                    var entity = (Entity) list.get(i);
                    if (entity.canBePushed())
                    {
                        entity.applyEntityCollision(this);
                    }
                }
            }
        }

        protected virtual bool canMove()
        {
            return health <= 0;
        }

        public void jump()
        {
            motionY = 0.41999998688697815D;
        }

        public virtual void updatePlayerActionState()
        {
            age++;
            EntityPlayer entityplayer = worldObj.getClosestPlayerToEntity(this, -1D);
            if (entityplayer != null)
            {
                double d = ((entityplayer)).posX - posX;
                double d1 = ((entityplayer)).posY - posY;
                double d2 = ((entityplayer)).posZ - posZ;
                double d3 = d*d + d1*d1 + d2*d2;
                if (d3 > 16384D)
                {
                    setEntityDead();
                }
                if (age > 600 && rand.nextInt(800) == 0)
                {
                    if (d3 < 1024D)
                    {
                        age = 0;
                    }
                    else
                    {
                        setEntityDead();
                    }
                }
            }
            moveStrafing = 0.0F;
            moveForward = 0.0F;
            float f = 8F;
            if (rand.nextFloat() < 0.02F)
            {
                EntityPlayer entityplayer1 = worldObj.getClosestPlayerToEntity(this, f);
                if (entityplayer1 != null)
                {
                    currentTarget = entityplayer1;
                    numTicksToChaseTarget = 10 + rand.nextInt(20);
                }
                else
                {
                    randomYawVelocity = (rand.nextFloat() - 0.5F)*20F;
                }
            }
            if (currentTarget != null)
            {
                faceEntity(currentTarget, 10F);
                if (numTicksToChaseTarget-- <= 0 || currentTarget.isDead ||
                    currentTarget.getDistanceSqToEntity(this) > (f*f))
                {
                    currentTarget = null;
                }
            }
            else
            {
                if (rand.nextFloat() < 0.05F)
                {
                    randomYawVelocity = (rand.nextFloat() - 0.5F)*20F;
                }
                rotationYaw += randomYawVelocity;
                rotationPitch = defaultPitch;
            }
            bool flag = handleWaterMovement();
            bool flag1 = handleLavaMovement();
            if (flag || flag1)
            {
                isJumping = rand.nextFloat() < 0.8F;
            }
        }

        public void faceEntity(Entity entity, float f)
        {
            double d = entity.posX - posX;
            double d2 = entity.posZ - posZ;
            double d1;
            if (entity is EntityLiving)
            {
                var entityliving = (EntityLiving) entity;
                d1 = (entityliving.posY + entityliving.getEyeHeight()) - (posY + getEyeHeight());
            }
            else
            {
                d1 = (entity.boundingBox.minY + entity.boundingBox.maxY)/2D - (posY + getEyeHeight());
            }
            double d3 = MathHelper.sqrt_double(d*d + d2*d2);
            float f1 = (float) ((Math.atan2(d2, d)*180D)/3.1415927410125732D) - 90F;
            var f2 = (float) ((Math.atan2(d1, d3)*180D)/3.1415927410125732D);
            rotationPitch = -updateRotation(rotationPitch, f2, f);
            rotationYaw = updateRotation(rotationYaw, f1, f);
        }

        private float updateRotation(float f, float f1, float f2)
        {
            float f3;
            for (f3 = f1 - f; f3 < -180F; f3 += 360F)
            {
            }
            for (; f3 >= 180F; f3 -= 360F)
            {
            }
            if (f3 > f2)
            {
                f3 = f2;
            }
            if (f3 < -f2)
            {
                f3 = -f2;
            }
            return f + f3;
        }

        public void func_6101_K()
        {
        }

        public virtual bool getCanSpawnHere()
        {
            return worldObj.checkIfAABBIsClear(boundingBox) &&
                   worldObj.getCollidingBoundingBoxes(this, boundingBox).size() == 0 &&
                   !worldObj.getIsAnyLiquid(boundingBox);
        }

        public override void kill()
        {
            attackEntityFrom(null, 4);
        }

        public override Vec3D getLookVec()
        {
            return getLook(1.0F);
        }

        public Vec3D getLook(float f)
        {
            if (f == 1.0F)
            {
                float f1 = MathHelper.cos(-rotationYaw*0.01745329F - 3.141593F);
                float f3 = MathHelper.sin(-rotationYaw*0.01745329F - 3.141593F);
                float f5 = -MathHelper.cos(-rotationPitch*0.01745329F);
                float f7 = MathHelper.sin(-rotationPitch*0.01745329F);
                return Vec3D.createVector(f3*f5, f7, f1*f5);
            }
            else
            {
                float f2 = prevRotationPitch + (rotationPitch - prevRotationPitch)*f;
                float f4 = prevRotationYaw + (rotationYaw - prevRotationYaw)*f;
                float f6 = MathHelper.cos(-f4*0.01745329F - 3.141593F);
                float f8 = MathHelper.sin(-f4*0.01745329F - 3.141593F);
                float f9 = -MathHelper.cos(-f2*0.01745329F);
                float f10 = MathHelper.sin(-f2*0.01745329F);
                return Vec3D.createVector(f8*f9, f10, f6*f9);
            }
        }

        public virtual int getMaxSpawnedInChunk()
        {
            return 4;
        }

        public virtual bool isPlayerSleeping()
        {
            return false;
        }
    }
}