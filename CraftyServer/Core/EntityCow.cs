namespace CraftyServer.Core
{
    public class EntityCow : EntityAnimals
    {
        public EntityCow(World world)
            : base(world)
        {
            texture = "/mob/cow.png";
            setSize(0.9F, 1.3F);
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
            return "mob.cow";
        }

        protected override string getHurtSound()
        {
            return "mob.cowhurt";
        }

        protected override string getDeathSound()
        {
            return "mob.cowhurt";
        }

        protected override float getSoundVolume()
        {
            return 0.4F;
        }

        protected override int getDropItemId()
        {
            return Item.leather.shiftedIndex;
        }

        public override bool interact(EntityPlayer entityplayer)
        {
            ItemStack itemstack = entityplayer.inventory.getCurrentItem();
            if (itemstack != null && itemstack.itemID == Item.bucketEmpty.shiftedIndex)
            {
                entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem,
                                                                new ItemStack(Item.bucketMilk));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}