namespace CraftyServer.Core
{
    public class EntityWaterMob : EntityCreature
                                  , IAnimals
    {
        public EntityWaterMob(World world) : base(world)
        {
        }

        public override bool canBreatheUnderwater()
        {
            return true;
        }

        public override void writeEntityToNBT(NBTTagCompound nbttagcompound)
        {
            base.writeEntityToNBT(nbttagcompound);
        }

        public override void readEntityFromNBT(NBTTagCompound nbttagcompound)
        {
            base.readEntityFromNBT(nbttagcompound);
        }

        public override bool getCanSpawnHere()
        {
            return worldObj.checkIfAABBIsClear(boundingBox);
        }

        public override int func_146_b()
        {
            return 120;
        }
    }
}