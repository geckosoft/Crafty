using java.io;
using java.lang;
using java.util;

namespace CraftyServer.Core
{
    public class ChunkLoader
        : IChunkLoader
    {
        private readonly bool createIfNecessary;
        private readonly File saveDir;

        public ChunkLoader(File file, bool flag)
        {
            saveDir = file;
            createIfNecessary = flag;
        }

        #region IChunkLoader Members

        public Chunk loadChunk(World world, int i, int j)
        {
            File file = chunkFileForXZ(i, j);
            if (file != null && file.exists())
            {
                try
                {
                    var fileinputstream = new FileInputStream(file);
                    NBTTagCompound nbttagcompound = CompressedStreamTools.func_770_a(fileinputstream);
                    if (!nbttagcompound.hasKey("Level"))
                    {
                        java.lang.System.@out.println(
                            (new StringBuilder()).append("Chunk file at ").append(i).append(",").append(j).append(
                                " is missing level data, skipping").toString());
                        return null;
                    }
                    if (!nbttagcompound.getCompoundTag("Level").hasKey("Blocks"))
                    {
                        java.lang.System.@out.println(
                            (new StringBuilder()).append("Chunk file at ").append(i).append(",").append(j).append(
                                " is missing block data, skipping").toString());
                        return null;
                    }
                    Chunk chunk = loadChunkIntoWorldFromCompound(world, nbttagcompound.getCompoundTag("Level"));
                    if (!chunk.isAtLocation(i, j))
                    {
                        java.lang.System.@out.println(
                            (new StringBuilder()).append("Chunk file at ").append(i).append(",").append(j).append(
                                " is in the wrong location; relocating. (Expected ").append(i).append(", ").append(j).
                                append(", got ").append(chunk.xPosition).append(", ").append(chunk.zPosition).append(")")
                                .toString());
                        nbttagcompound.setInteger("xPos", i);
                        nbttagcompound.setInteger("zPos", j);
                        chunk = loadChunkIntoWorldFromCompound(world, nbttagcompound.getCompoundTag("Level"));
                    }
                    return chunk;
                }
                catch (Exception exception)
                {
                    exception.printStackTrace();
                }
            }
            return null;
        }

        public void saveChunk(World world, Chunk chunk)
        {
            world.checkSessionLock();
            File file = chunkFileForXZ(chunk.xPosition, chunk.zPosition);
            if (file.exists())
            {
                WorldInfo worldinfo = world.getWorldInfo();
                worldinfo.func_22177_b(worldinfo.func_22182_g() - file.length());
            }
            try
            {
                var file1 = new File(saveDir, "tmp_chunk.dat");
                var fileoutputstream = new FileOutputStream(file1);
                var nbttagcompound = new NBTTagCompound();
                var nbttagcompound1 = new NBTTagCompound();
                nbttagcompound.setTag("Level", nbttagcompound1);
                storeChunkInCompound(chunk, world, nbttagcompound1);
                CompressedStreamTools.writeGzippedCompoundToOutputStream(nbttagcompound, fileoutputstream);
                fileoutputstream.close();
                if (file.exists())
                {
                    file.delete();
                }
                file1.renameTo(file);
                WorldInfo worldinfo1 = world.getWorldInfo();
                worldinfo1.func_22177_b(worldinfo1.func_22182_g() + file.length());
            }
            catch (Exception exception)
            {
                exception.printStackTrace();
            }
        }

        public void func_661_a()
        {
        }

        public void saveExtraData()
        {
        }

        public void saveExtraChunkData(World world, Chunk chunk)
        {
        }

        #endregion

        private File chunkFileForXZ(int i, int j)
        {
            string s =
                (new StringBuilder()).append("c.").append(Integer.toString(i, 36)).append(".").append(
                    Integer.toString(j, 36)).append(".dat").toString();
            string s1 = Integer.toString(i & 0x3f, 36);
            string s2 = Integer.toString(j & 0x3f, 36);
            var file = new File(saveDir, s1);
            if (!file.exists())
            {
                if (createIfNecessary)
                {
                    file.mkdir();
                }
                else
                {
                    return null;
                }
            }
            file = new File(file, s2);
            if (!file.exists())
            {
                if (createIfNecessary)
                {
                    file.mkdir();
                }
                else
                {
                    return null;
                }
            }
            file = new File(file, s);
            if (!file.exists() && !createIfNecessary)
            {
                return null;
            }
            else
            {
                return file;
            }
        }

        public static void storeChunkInCompound(Chunk chunk, World world, NBTTagCompound nbttagcompound)
        {
            world.checkSessionLock();
            nbttagcompound.setInteger("xPos", chunk.xPosition);
            nbttagcompound.setInteger("zPos", chunk.zPosition);
            nbttagcompound.setLong("LastUpdate", world.getWorldTime());
            nbttagcompound.setByteArray("Blocks", chunk.blocks);
            nbttagcompound.setByteArray("Data", chunk.data.data);
            nbttagcompound.setByteArray("SkyLight", chunk.skylightMap.data);
            nbttagcompound.setByteArray("BlockLight", chunk.blocklightMap.data);
            nbttagcompound.setByteArray("HeightMap", chunk.heightMap);
            nbttagcompound.setBoolean("TerrainPopulated", chunk.isTerrainPopulated);
            chunk.hasEntities = false;
            var nbttaglist = new NBTTagList();

            for (int i = 0; i < chunk.entities.Length; i++)
            {
                Iterator iterator = chunk.entities[i].iterator();
                do
                {
                    if (!iterator.hasNext())
                    {
                        goto label0;
                    }
                    var entity = (Entity) iterator.next();
                    chunk.hasEntities = true;
                    var nbttagcompound1 = new NBTTagCompound();
                    if (entity.addEntityID(nbttagcompound1))
                    {
                        nbttaglist.setTag(nbttagcompound1);
                    }
                } while (true);
            }
            label0:

            nbttagcompound.setTag("Entities", nbttaglist);
            var nbttaglist1 = new NBTTagList();
            NBTTagCompound nbttagcompound2;
            for (Iterator iterator1 = chunk.chunkTileEntityMap.values().iterator();
                 iterator1.hasNext();
                 nbttaglist1.setTag(nbttagcompound2))
            {
                var tileentity = (TileEntity) iterator1.next();
                nbttagcompound2 = new NBTTagCompound();
                tileentity.writeToNBT(nbttagcompound2);
            }

            nbttagcompound.setTag("TileEntities", nbttaglist1);
        }

        public static Chunk loadChunkIntoWorldFromCompound(World world, NBTTagCompound nbttagcompound)
        {
            int i = nbttagcompound.getInteger("xPos");
            int j = nbttagcompound.getInteger("zPos");
            var chunk = new Chunk(world, i, j);
            chunk.blocks = nbttagcompound.getByteArray("Blocks");
            chunk.data = new NibbleArray(nbttagcompound.getByteArray("Data"));
            chunk.skylightMap = new NibbleArray(nbttagcompound.getByteArray("SkyLight"));
            chunk.blocklightMap = new NibbleArray(nbttagcompound.getByteArray("BlockLight"));
            chunk.heightMap = nbttagcompound.getByteArray("HeightMap");
            chunk.isTerrainPopulated = nbttagcompound.getBoolean("TerrainPopulated");
            if (!chunk.data.isValid())
            {
                chunk.data = new NibbleArray(chunk.blocks.Length);
            }
            if (chunk.heightMap == null || !chunk.skylightMap.isValid())
            {
                chunk.heightMap = new byte[256];
                chunk.skylightMap = new NibbleArray(chunk.blocks.Length);
                chunk.func_353_b();
            }
            if (!chunk.blocklightMap.isValid())
            {
                chunk.blocklightMap = new NibbleArray(chunk.blocks.Length);
                chunk.func_348_a();
            }
            NBTTagList nbttaglist = nbttagcompound.getTagList("Entities");
            if (nbttaglist != null)
            {
                for (int k = 0; k < nbttaglist.tagCount(); k++)
                {
                    var nbttagcompound1 = (NBTTagCompound) nbttaglist.tagAt(k);
                    Entity entity = EntityList.createEntityFromNBT(nbttagcompound1, world);
                    chunk.hasEntities = true;
                    if (entity != null)
                    {
                        chunk.addEntity(entity);
                    }
                }
            }
            NBTTagList nbttaglist1 = nbttagcompound.getTagList("TileEntities");
            if (nbttaglist1 != null)
            {
                for (int l = 0; l < nbttaglist1.tagCount(); l++)
                {
                    var nbttagcompound2 = (NBTTagCompound) nbttaglist1.tagAt(l);
                    TileEntity tileentity = TileEntity.createAndLoadEntity(nbttagcompound2);
                    if (tileentity != null)
                    {
                        chunk.func_349_a(tileentity);
                    }
                }
            }
            return chunk;
        }
    }
}