namespace CraftyServer.Core
{
    public class EntityChicken : EntityAnimals
    {
        public float field_390_ai;
        public float field_391_b;
        public bool field_392_a;
        public float field_393_af;
        public float field_394_ae;
        public float field_395_ad;
        public int timeUntilNextEgg;

        public EntityChicken(World world)
            : base(world)
        {
            field_392_a = false;
            field_391_b = 0.0F;
            field_395_ad = 0.0F;
            field_390_ai = 1.0F;
            texture = "/mob/chicken.png";
            setSize(0.3F, 0.4F);
            health = 4;
            timeUntilNextEgg = rand.nextInt(6000) + 6000;
        }

        public override void onLivingUpdate()
        {
            base.onLivingUpdate();
            field_393_af = field_391_b;
            field_394_ae = field_395_ad;
            field_395_ad += (float) ((onGround ? -1 : 4)*0.29999999999999999D);
            if (field_395_ad < 0.0F)
            {
                field_395_ad = 0.0F;
            }
            if (field_395_ad > 1.0F)
            {
                field_395_ad = 1.0F;
            }
            if (!onGround && field_390_ai < 1.0F)
            {
                field_390_ai = 1.0F;
            }
            field_390_ai *= (float) 0.90000000000000002D;
            if (!onGround && motionY < 0.0D)
            {
                motionY *= 0.59999999999999998D;
            }
            field_391_b += field_390_ai*2.0F;
            if (!worldObj.singleplayerWorld && --timeUntilNextEgg <= 0)
            {
                worldObj.playSoundAtEntity(this, "mob.chickenplop", 1.0F,
                                           (rand.nextFloat() - rand.nextFloat())*0.2F + 1.0F);
                dropItem(Item.egg.shiftedIndex, 1);
                timeUntilNextEgg = rand.nextInt(6000) + 6000;
            }
        }

        public override void fall(float f)
        {
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
            return "mob.chicken";
        }

        protected override string getHurtSound()
        {
            return "mob.chickenhurt";
        }

        protected override string getDeathSound()
        {
            return "mob.chickenhurt";
        }

        protected override int getDropItemId()
        {
            return Item.feather.shiftedIndex;
        }
    }
}