using java.io;
using java.lang;
using java.util;
using java.util.logging;

namespace CraftyServer.Core
{
    public class PlayerNBTManager
        : IPlayerFileData, ISaveHandler
    {
        private static readonly Logger logger = Logger.getLogger("Minecraft");
        private readonly File field_22099_b;
        private readonly long field_22100_d = java.lang.System.currentTimeMillis();
        private readonly File worldFile;

        public PlayerNBTManager(File file, string s, bool flag)
        {
            field_22099_b = new File(file, s);
            field_22099_b.mkdirs();
            worldFile = new File(field_22099_b, "players");
            if (flag)
            {
                worldFile.mkdirs();
            }
            func_22098_f();
        }

        #region IPlayerFileData Members

        public virtual void writePlayerData(EntityPlayer entityplayer)
        {
            try
            {
                var nbttagcompound = new NBTTagCompound();
                entityplayer.writeToNBT(nbttagcompound);
                var file = new File(worldFile, "_tmp_.dat");
                var file1 = new File(worldFile,
                                     (new StringBuilder()).append(entityplayer.username).append(".dat").toString());
                CompressedStreamTools.writeGzippedCompoundToOutputStream(nbttagcompound, new FileOutputStream(file));
                if (file1.exists())
                {
                    file1.delete();
                }
                file.renameTo(file1);
            }
            catch (Exception)
            {
                logger.warning(
                    (new StringBuilder()).append("Failed to save player data for ").append(entityplayer.username).
                        toString());
            }
        }

        public virtual void readPlayerData(EntityPlayer entityplayer)
        {
            try
            {
                var file = new File(worldFile,
                                    (new StringBuilder()).append(entityplayer.username).append(".dat").toString());
                if (file.exists())
                {
                    NBTTagCompound nbttagcompound = CompressedStreamTools.func_770_a(new FileInputStream(file));
                    if (nbttagcompound != null)
                    {
                        entityplayer.readFromNBT(nbttagcompound);
                    }
                }
            }
            catch (Exception)
            {
                logger.warning(
                    (new StringBuilder()).append("Failed to load player data for ").append(entityplayer.username).
                        toString());
            }
        }

        #endregion

        #region ISaveHandler Members

        public void func_22091_b()
        {
            try
            {
                var file = new File(field_22099_b, "session.lock");
                var datainputstream = new DataInputStream(new FileInputStream(file));
                try
                {
                    if (datainputstream.readLong() != field_22100_d)
                    {
                        throw new MinecraftException("The save is being accessed from another location, aborting");
                    }
                }
                finally
                {
                    datainputstream.close();
                }
            }
            catch (IOException)
            {
                throw new MinecraftException("Failed to check session lock, aborting");
            }
        }

        public virtual IChunkLoader func_22092_a(WorldProvider worldprovider)
        {
            if (worldprovider is WorldProviderHell)
            {
                var file = new File(field_22099_b, "DIM-1");
                file.mkdirs();
                return new ChunkLoader(file, true);
            }
            else
            {
                return new ChunkLoader(field_22099_b, true);
            }
        }

        public virtual WorldInfo func_22096_c()
        {
            var file = new File(field_22099_b, "level.dat");
            if (file.exists())
            {
                try
                {
                    NBTTagCompound nbttagcompound = CompressedStreamTools.func_770_a(new FileInputStream(file));
                    NBTTagCompound nbttagcompound1 = nbttagcompound.getCompoundTag("Data");
                    return new WorldInfo(nbttagcompound1);
                }
                catch (Exception exception)
                {
                    exception.printStackTrace();
                }
            }
            return null;
        }

        public virtual void func_22095_a(WorldInfo worldinfo, List list)
        {
            NBTTagCompound nbttagcompound = worldinfo.func_22183_a(list);
            var nbttagcompound1 = new NBTTagCompound();
            nbttagcompound1.setTag("Data", nbttagcompound);
            try
            {
                var file = new File(field_22099_b, "level.dat_new");
                var file1 = new File(field_22099_b, "level.dat_old");
                var file2 = new File(field_22099_b, "level.dat");
                CompressedStreamTools.writeGzippedCompoundToOutputStream(nbttagcompound1, new FileOutputStream(file));
                if (file1.exists())
                {
                    file1.delete();
                }
                file2.renameTo(file1);
                if (file2.exists())
                {
                    file2.delete();
                }
                file.renameTo(file2);
                if (file.exists())
                {
                    file.delete();
                }
            }
            catch (Exception exception)
            {
                exception.printStackTrace();
            }
        }

        public virtual void func_22094_a(WorldInfo worldinfo)
        {
            NBTTagCompound nbttagcompound = worldinfo.func_22185_a();
            var nbttagcompound1 = new NBTTagCompound();
            nbttagcompound1.setTag("Data", nbttagcompound);
            try
            {
                var file = new File(field_22099_b, "level.dat_new");
                var file1 = new File(field_22099_b, "level.dat_old");
                var file2 = new File(field_22099_b, "level.dat");
                CompressedStreamTools.writeGzippedCompoundToOutputStream(nbttagcompound1, new FileOutputStream(file));
                if (file1.exists())
                {
                    file1.delete();
                }
                file2.renameTo(file1);
                if (file2.exists())
                {
                    file2.delete();
                }
                file.renameTo(file2);
                if (file.exists())
                {
                    file.delete();
                }
            }
            catch (Exception exception)
            {
                exception.printStackTrace();
            }
        }

        public virtual IPlayerFileData func_22090_d()
        {
            return this;
        }

        public virtual void func_22093_e()
        {
        }

        #endregion

        private void func_22098_f()
        {
            try
            {
                var file = new File(field_22099_b, "session.lock");
                var dataoutputstream = new DataOutputStream(new FileOutputStream(file));
                try
                {
                    dataoutputstream.writeLong(field_22100_d);
                }
                finally
                {
                    dataoutputstream.close();
                }
            }
            catch (IOException ioexception)
            {
                ioexception.printStackTrace();
                throw new RuntimeException("Failed to check session lock, aborting");
            }
        }

        protected File func_22097_a()
        {
            return field_22099_b;
        }
    }
}