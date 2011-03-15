namespace CraftyServer.Core
{
    public class TileEntityMobSpawner : TileEntity
    {
        public int delay;
        private string mobID;
        public double yaw;
        public double yaw2;

        public TileEntityMobSpawner()
        {
            delay = -1;
            yaw2 = 0.0D;
            mobID = "Pig";
            delay = 20;
        }

        public void setMobID(string s)
        {
            mobID = s;
        }

        public bool anyPlayerInRange()
        {
            return
                worldObj.getClosestPlayer(xCoord + 0.5D, yCoord + 0.5D, zCoord + 0.5D, 16D) !=
                null;
        }

        public override void updateEntity()
        {
            yaw2 = yaw;
            if (!anyPlayerInRange())
            {
                return;
            }
            double d = xCoord + worldObj.rand.nextFloat();
            double d2 = yCoord + worldObj.rand.nextFloat();
            double d4 = zCoord + worldObj.rand.nextFloat();
            worldObj.spawnParticle("smoke", d, d2, d4, 0.0D, 0.0D, 0.0D);
            worldObj.spawnParticle("flame", d, d2, d4, 0.0D, 0.0D, 0.0D);
            for (yaw += 1000F/(delay + 200F); yaw > 360D;)
            {
                yaw -= 360D;
                yaw2 -= 360D;
            }

            if (delay == -1)
            {
                updateDelay();
            }
            if (delay > 0)
            {
                delay--;
                return;
            }
            byte byte0 = 4;
            for (int i = 0; i < byte0; i++)
            {
                var entityliving = (EntityLiving) EntityList.createEntityInWorld(mobID, worldObj);
                if (entityliving == null)
                {
                    return;
                }
                int j =
                    worldObj.getEntitiesWithinAABB(entityliving.GetType(),
                                                   AxisAlignedBB.getBoundingBoxFromPool(xCoord, yCoord, zCoord,
                                                                                        xCoord + 1, yCoord + 1,
                                                                                        zCoord + 1).expand(8D, 4D, 8D)).
                        size();
                if (j >= 6)
                {
                    updateDelay();
                    return;
                }
                if (entityliving == null)
                {
                    continue;
                }
                double d6 = xCoord + (worldObj.rand.nextDouble() - worldObj.rand.nextDouble())*4D;
                double d7 = (yCoord + worldObj.rand.nextInt(3)) - 1;
                double d8 = zCoord + (worldObj.rand.nextDouble() - worldObj.rand.nextDouble())*4D;
                entityliving.setLocationAndAngles(d6, d7, d8, worldObj.rand.nextFloat()*360F, 0.0F);
                if (!entityliving.getCanSpawnHere())
                {
                    continue;
                }
                worldObj.entityJoinedWorld(entityliving);
                for (int k = 0; k < 20; k++)
                {
                    double d1 = xCoord + 0.5D + (worldObj.rand.nextFloat() - 0.5D)*2D;
                    double d3 = yCoord + 0.5D + (worldObj.rand.nextFloat() - 0.5D)*2D;
                    double d5 = zCoord + 0.5D + (worldObj.rand.nextFloat() - 0.5D)*2D;
                    worldObj.spawnParticle("smoke", d1, d3, d5, 0.0D, 0.0D, 0.0D);
                    worldObj.spawnParticle("flame", d1, d3, d5, 0.0D, 0.0D, 0.0D);
                }

                entityliving.spawnExplosionParticle();
                updateDelay();
            }

            base.updateEntity();
        }

        private void updateDelay()
        {
            delay = 200 + worldObj.rand.nextInt(600);
        }

        public override void readFromNBT(NBTTagCompound nbttagcompound)
        {
            base.readFromNBT(nbttagcompound);
            mobID = nbttagcompound.getString("EntityId");
            delay = nbttagcompound.getShort("Delay");
        }

        public override void writeToNBT(NBTTagCompound nbttagcompound)
        {
            base.writeToNBT(nbttagcompound);
            nbttagcompound.setString("EntityId", mobID);
            nbttagcompound.setShort("Delay", (short) delay);
        }
    }
}