using java.lang;

namespace CraftyServer.Core
{
    public class EntityPig : EntityAnimals
    {
        public EntityPig(World world)
            : base(world)
        {
            texture = "/mob/pig.png";
            setSize(0.9F, 0.9F);
        }

        protected override void entityInit()
        {
            dataWatcher.addObject(16, Byte.valueOf(0));
        }

        public override void writeEntityToNBT(NBTTagCompound nbttagcompound)
        {
            base.writeEntityToNBT(nbttagcompound);
            nbttagcompound.setBoolean("Saddle", func_21065_K());
        }

        public override void readEntityFromNBT(NBTTagCompound nbttagcompound)
        {
            base.readEntityFromNBT(nbttagcompound);
            func_21064_a(nbttagcompound.getBoolean("Saddle"));
        }

        protected override string getLivingSound()
        {
            return "mob.pig";
        }

        protected override string getHurtSound()
        {
            return "mob.pig";
        }

        protected override string getDeathSound()
        {
            return "mob.pigdeath";
        }

        public override bool interact(EntityPlayer entityplayer)
        {
            if (func_21065_K() && !worldObj.singleplayerWorld &&
                (riddenByEntity == null || riddenByEntity == entityplayer))
            {
                entityplayer.mountEntity(this);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override int getDropItemId()
        {
            return Item.porkRaw.shiftedIndex;
        }

        public bool func_21065_K()
        {
            return (dataWatcher.getWatchableObjectByte(16) & 1) != 0;
        }

        public void func_21064_a(bool flag)
        {
            if (flag)
            {
                dataWatcher.updateObject(16, Byte.valueOf(1));
            }
            else
            {
                dataWatcher.updateObject(16, Byte.valueOf(0));
            }
        }
    }
}