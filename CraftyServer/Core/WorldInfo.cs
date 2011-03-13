using java.util;

namespace CraftyServer.Core
{
    public class WorldInfo
    {
        public WorldInfo(NBTTagCompound nbttagcompound)
        {
            randomSeed = nbttagcompound.getLong("RandomSeed");
            spawnX = nbttagcompound.getInteger("SpawnX");
            spawnY = nbttagcompound.getInteger("SpawnY");
            spawnZ = nbttagcompound.getInteger("SpawnZ");
            worldTime = nbttagcompound.getLong("Time");
            lastPlayed = nbttagcompound.getLong("LastPlayed");
            sizeOnDisk = nbttagcompound.getLong("SizeOnDisk");
            levelName = nbttagcompound.getString("LevelName");
            version = nbttagcompound.getInteger("version");
            if (nbttagcompound.hasKey("Player"))
            {
                field_22195_h = nbttagcompound.getCompoundTag("Player");
                field_22194_i = field_22195_h.getInteger("Dimension");
            }
        }

        public WorldInfo(long l, string s)
        {
            randomSeed = l;
            levelName = s;
        }

        public WorldInfo(WorldInfo worldinfo)
        {
            randomSeed = worldinfo.randomSeed;
            spawnX = worldinfo.spawnX;
            spawnY = worldinfo.spawnY;
            spawnZ = worldinfo.spawnZ;
            worldTime = worldinfo.worldTime;
            lastPlayed = worldinfo.lastPlayed;
            sizeOnDisk = worldinfo.sizeOnDisk;
            field_22195_h = worldinfo.field_22195_h;
            field_22194_i = worldinfo.field_22194_i;
            levelName = worldinfo.levelName;
            version = worldinfo.version;
        }

        public NBTTagCompound func_22185_a()
        {
            NBTTagCompound nbttagcompound = new NBTTagCompound();
            func_22176_a(nbttagcompound, field_22195_h);
            return nbttagcompound;
        }

        public NBTTagCompound func_22183_a(List list)
        {
            NBTTagCompound nbttagcompound = new NBTTagCompound();
            EntityPlayer entityplayer = null;
            NBTTagCompound nbttagcompound1 = null;
            if (list.size() > 0)
            {
                entityplayer = (EntityPlayer) list.get(0);
            }
            if (entityplayer != null)
            {
                nbttagcompound1 = new NBTTagCompound();
                entityplayer.writeToNBT(nbttagcompound1);
            }
            func_22176_a(nbttagcompound, nbttagcompound1);
            return nbttagcompound;
        }

        private void func_22176_a(NBTTagCompound nbttagcompound, NBTTagCompound nbttagcompound1)
        {
            nbttagcompound.setLong("RandomSeed", randomSeed);
            nbttagcompound.setInteger("SpawnX", spawnX);
            nbttagcompound.setInteger("SpawnY", spawnY);
            nbttagcompound.setInteger("SpawnZ", spawnZ);
            nbttagcompound.setLong("Time", worldTime);
            nbttagcompound.setLong("SizeOnDisk", sizeOnDisk);
            nbttagcompound.setLong("LastPlayed", java.lang.System.currentTimeMillis());
            nbttagcompound.setString("LevelName", levelName);
            nbttagcompound.setInteger("version", version);
            if (nbttagcompound1 != null)
            {
                nbttagcompound.setCompoundTag("Player", nbttagcompound1);
            }
        }

        public long func_22187_b()
        {
            return randomSeed;
        }

        public int getSpawnX()
        {
            return spawnX;
        }

        public int getSpawnY()
        {
            return spawnY;
        }

        public int getSpawnZ()
        {
            return spawnZ;
        }

        public long getWorldTime()
        {
            return worldTime;
        }

        public long func_22182_g()
        {
            return sizeOnDisk;
        }

        public int func_22178_h()
        {
            return field_22194_i;
        }

        public void setWorldTime(long l)
        {
            worldTime = l;
        }

        public void func_22177_b(long l)
        {
            sizeOnDisk = l;
        }

        public void setSpawnPosition(int i, int j, int k)
        {
            spawnX = i;
            spawnY = j;
            spawnZ = k;
        }

        public void setLevelName(string s)
        {
            levelName = s;
        }

        public int func_22188_i()
        {
            return version;
        }

        public void func_22191_a(int i)
        {
            version = i;
        }

        private long randomSeed;
        private int spawnX;
        private int spawnY;
        private int spawnZ;
        private long worldTime;
        private long lastPlayed;
        private long sizeOnDisk;
        private NBTTagCompound field_22195_h;
        private int field_22194_i;
        private string levelName;
        private int version;
    }
}