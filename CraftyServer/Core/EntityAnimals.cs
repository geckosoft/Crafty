namespace CraftyServer.Core
{
    public abstract class EntityAnimals : EntityCreature
                                          , IAnimals
    {
        public EntityAnimals(World world) : base(world)
        {
        }

        protected override float getBlockPathWeight(int i, int j, int k)
        {
            if (worldObj.getBlockId(i, j - 1, k) == Block.grass.blockID)
            {
                return 10F;
            }
            else
            {
                return worldObj.getLightBrightness(i, j, k) - 0.5F;
            }
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
            int i = MathHelper.floor_double(posX);
            int j = MathHelper.floor_double(boundingBox.minY);
            int k = MathHelper.floor_double(posZ);
            return worldObj.getBlockId(i, j - 1, k) == Block.grass.blockID && worldObj.getBlockLightValue(i, j, k) > 8 &&
                   base.getCanSpawnHere();
        }

        public override int func_146_b()
        {
            return 120;
        }
    }
}