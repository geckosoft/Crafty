using java.lang;

namespace CraftyServer.Core
{
    public class EntitySquid : EntityWaterMob
    {
        private float field_21050_at;
        private float field_21051_as;
        private float field_21052_ar;
        private float field_21053_aq;
        private float field_21054_ap;
        private float field_21055_ao;
        public float field_21056_an;
        public float field_21057_am;
        public float field_21058_al;
        public float field_21059_f;
        public float field_21060_ak;
        public float field_21061_c;
        public float field_21062_b;
        public float field_21063_a;

        public EntitySquid(World world) : base(world)
        {
            field_21063_a = 0.0F;
            field_21062_b = 0.0F;
            field_21061_c = 0.0F;
            field_21059_f = 0.0F;
            field_21060_ak = 0.0F;
            field_21058_al = 0.0F;
            field_21057_am = 0.0F;
            field_21056_an = 0.0F;
            field_21055_ao = 0.0F;
            field_21054_ap = 0.0F;
            field_21053_aq = 0.0F;
            field_21052_ar = 0.0F;
            field_21051_as = 0.0F;
            field_21050_at = 0.0F;
            texture = "/mob/squid.png";
            setSize(0.95F, 0.95F);
            field_21054_ap = (1.0F/(rand.nextFloat() + 1.0F))*0.2F;
        }

        public override void writeEntityToNBT(NBTTagCompound nbttagcompound)
        {
            base.writeEntityToNBT(nbttagcompound);
        }

        public override void readEntityFromNBT(NBTTagCompound nbttagcompound)
        {
            base.readEntityFromNBT(nbttagcompound);
        }

        protected override string getLivingSound()
        {
            return null;
        }

        protected override string getHurtSound()
        {
            return null;
        }

        protected override string getDeathSound()
        {
            return null;
        }

        protected override float getSoundVolume()
        {
            return 0.4F;
        }

        protected override int getDropItemId()
        {
            return 0;
        }

        public override void func_21047_g_()
        {
            int i = rand.nextInt(3) + 1;
            for (int j = 0; j < i; j++)
            {
                entityDropItem(new ItemStack(Item.dyePowder, 1, 0), 0.0F);
            }
        }

        public override bool interact(EntityPlayer entityplayer)
        {
            return false;
        }

        public override bool handleWaterMovement()
        {
            return worldObj.handleMaterialAcceleration(boundingBox.expand(0.0D, -0.60000002384185791D, 0.0D),
                                                       Material.water, this);
        }

        public override void onLivingUpdate()
        {
            base.onLivingUpdate();
            field_21062_b = field_21063_a;
            field_21059_f = field_21061_c;
            field_21058_al = field_21060_ak;
            field_21056_an = field_21057_am;
            field_21060_ak += field_21054_ap;
            if (field_21060_ak > 6.283185F)
            {
                field_21060_ak -= 6.283185F;
                if (rand.nextInt(10) == 0)
                {
                    field_21054_ap = (1.0F/(rand.nextFloat() + 1.0F))*0.2F;
                }
            }
            if (handleWaterMovement())
            {
                if (field_21060_ak < 3.141593F)
                {
                    float f = field_21060_ak/3.141593F;
                    field_21057_am = MathHelper.sin(f*f*3.141593F)*3.141593F*0.25F;
                    if (f > 0.75D)
                    {
                        field_21055_ao = 1.0F;
                        field_21053_aq = 1.0F;
                    }
                    else
                    {
                        field_21053_aq = field_21053_aq*0.8F;
                    }
                }
                else
                {
                    field_21057_am = 0.0F;
                    field_21055_ao = field_21055_ao*0.9F;
                    field_21053_aq = field_21053_aq*0.99F;
                }
                if (!field_9112_aN)
                {
                    motionX = field_21052_ar*field_21055_ao;
                    motionY = field_21051_as*field_21055_ao;
                    motionZ = field_21050_at*field_21055_ao;
                }
                float f1 = MathHelper.sqrt_double(motionX*motionX + motionZ*motionZ);
                renderYawOffset += ((-(float) Math.atan2(motionX, motionZ)*180F)/3.141593F - renderYawOffset)*0.1F;
                rotationYaw = renderYawOffset;
                field_21061_c = field_21061_c + 3.141593F*field_21053_aq*1.5F;
                field_21063_a += ((-(float) Math.atan2(f1, motionY)*180F)/3.141593F - field_21063_a)*0.1F;
            }
            else
            {
                field_21057_am = MathHelper.abs(MathHelper.sin(field_21060_ak))*3.141593F*0.25F;
                if (!field_9112_aN)
                {
                    motionX = 0.0D;
                    motionY -= 0.080000000000000002D;
                    motionY *= 0.98000001907348633D;
                    motionZ = 0.0D;
                }
                field_21063_a += (float) ((-90F - field_21063_a)*0.02D);
            }
        }

        public override void moveEntityWithHeading(float f, float f1)
        {
            moveEntity(motionX, motionY, motionZ);
        }

        public override void updatePlayerActionState()
        {
            if (rand.nextInt(50) == 0 || !inWater ||
                field_21052_ar == 0.0F && field_21051_as == 0.0F && field_21050_at == 0.0F)
            {
                float f = rand.nextFloat()*3.141593F*2.0F;
                field_21052_ar = MathHelper.cos(f)*0.2F;
                field_21051_as = -0.1F + rand.nextFloat()*0.2F;
                field_21050_at = MathHelper.sin(f)*0.2F;
            }
        }
    }
}