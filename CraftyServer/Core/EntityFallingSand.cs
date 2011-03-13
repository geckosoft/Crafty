namespace CraftyServer.Core
{
    public class EntityFallingSand : Entity
    {
        public EntityFallingSand(World world)
            : base(world)
        {
            fallTime = 0;
        }

        public EntityFallingSand(World world, double d, double d1, double d2,
                                 int i) : base(world)
        {
            fallTime = 0;
            blockID = i;
            preventEntitySpawning = true;
            setSize(0.98F, 0.98F);
            yOffset = height/2.0F;
            setPosition(d, d1, d2);
            motionX = 0.0D;
            motionY = 0.0D;
            motionZ = 0.0D;
            entityWalks = false;
            prevPosX = d;
            prevPosY = d1;
            prevPosZ = d2;
        }

        protected override void entityInit()
        {
        }

        public override bool canBeCollidedWith()
        {
            return !isDead;
        }

        public override void onUpdate()
        {
            if (blockID == 0)
            {
                setEntityDead();
                return;
            }
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            fallTime++;
            motionY -= 0.039999999105930328D;
            moveEntity(motionX, motionY, motionZ);
            motionX *= 0.98000001907348633D;
            motionY *= 0.98000001907348633D;
            motionZ *= 0.98000001907348633D;
            int i = MathHelper.floor_double(posX);
            int j = MathHelper.floor_double(posY);
            int k = MathHelper.floor_double(posZ);
            if (worldObj.getBlockId(i, j, k) == blockID)
            {
                worldObj.setBlockWithNotify(i, j, k, 0);
            }
            if (onGround)
            {
                motionX *= 0.69999998807907104D;
                motionZ *= 0.69999998807907104D;
                motionY *= -0.5D;
                setEntityDead();
                if ((!worldObj.canBlockBePlacedAt(blockID, i, j, k, true) ||
                     !worldObj.setBlockWithNotify(i, j, k, blockID)) && !worldObj.singleplayerWorld)
                {
                    dropItem(blockID, 1);
                }
            }
            else if (fallTime > 100 && !worldObj.singleplayerWorld)
            {
                dropItem(blockID, 1);
                setEntityDead();
            }
        }

        public override void writeEntityToNBT(NBTTagCompound nbttagcompound)
        {
            nbttagcompound.setByte("Tile", (byte) blockID);
        }

        public override void readEntityFromNBT(NBTTagCompound nbttagcompound)
        {
            blockID = nbttagcompound.getByte("Tile") & 0xff;
        }

        public int blockID;
        public int fallTime;
    }
}