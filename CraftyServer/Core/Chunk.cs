using System;
using java.lang;
using java.util;
using Random = java.util.Random;

namespace CraftyServer.Core
{
    public class Chunk
    {
        public static bool isLit;
        public NibbleArray blocklightMap;
        public byte[] blocks;
        public Map chunkTileEntityMap;
        public NibbleArray data;
        public List[] entities;
        public int field_686_i;
        public bool hasEntities;
        public byte[] heightMap;
        public bool isChunkLoaded;
        public bool isModified;
        public bool isTerrainPopulated;
        public long lastSaveTime;
        public bool neverSave;
        public NibbleArray skylightMap;
        public World worldObj;
        public int xPosition;
        public int zPosition;

        public Chunk(World world, int i, int j)
        {
            chunkTileEntityMap = new HashMap();
            entities = new List[8];
            isTerrainPopulated = false;
            isModified = false;
            hasEntities = false;
            lastSaveTime = 0L;
            worldObj = world;
            xPosition = i;
            zPosition = j;
            heightMap = new byte[256];
            for (int k = 0; k < entities.Length; k++)
            {
                entities[k] = new ArrayList();
            }
        }

        public Chunk(World world, byte[] abyte0, int i, int j)
            : this(world, i, j)
        {
            blocks = abyte0;
            data = new NibbleArray(abyte0.Length);
            skylightMap = new NibbleArray(abyte0.Length);
            blocklightMap = new NibbleArray(abyte0.Length);
        }

        public virtual bool isAtLocation(int i, int j)
        {
            return i == xPosition && j == zPosition;
        }

        public virtual int getHeightValue(int i, int j)
        {
            return heightMap[j << 4 | i] & 0xff;
        }

        public virtual void func_348_a()
        {
        }

        public virtual void func_353_b()
        {
            int i = 127;
            for (int j = 0; j < 16; j++)
            {
                for (int l = 0; l < 16; l++)
                {
                    int j1 = 127;
                    int k1;
                    for (k1 = j << 11 | l << 7; j1 > 0 && Block.lightOpacity[blocks[(k1 + j1) - 1]] == 0; j1--)
                    {
                    }
                    heightMap[l << 4 | j] = (byte) j1;
                    if (j1 < i)
                    {
                        i = j1;
                    }
                    if (worldObj.worldProvider.field_4306_c)
                    {
                        continue;
                    }
                    int l1 = 15;
                    int i2 = 127;
                    do
                    {
                        l1 -= Block.lightOpacity[blocks[k1 + i2]];
                        if (l1 > 0)
                        {
                            skylightMap.setNibble(j, i2, l, l1);
                        }
                    } while (--i2 > 0 && l1 > 0);
                }
            }

            field_686_i = i;
            for (int k = 0; k < 16; k++)
            {
                for (int i1 = 0; i1 < 16; i1++)
                {
                    func_333_c(k, i1);
                }
            }

            isModified = true;
        }

        public virtual void func_4053_c()
        {
        }

        private void func_333_c(int i, int j)
        {
            int k = getHeightValue(i, j);
            int l = xPosition*16 + i;
            int i1 = zPosition*16 + j;
            func_355_f(l - 1, i1, k);
            func_355_f(l + 1, i1, k);
            func_355_f(l, i1 - 1, k);
            func_355_f(l, i1 + 1, k);
        }

        private void func_355_f(int i, int j, int k)
        {
            int l = worldObj.getHeightValue(i, j);
            if (l > k)
            {
                worldObj.func_483_a(EnumSkyBlock.Sky, i, k, j, i, l, j);
                isModified = true;
            }
            else if (l < k)
            {
                worldObj.func_483_a(EnumSkyBlock.Sky, i, l, j, i, k, j);
                isModified = true;
            }
        }

        private void func_339_g(int i, int j, int k)
        {
            int l = heightMap[k << 4 | i] & 0xff;
            int i1 = l;
            if (j > l)
            {
                i1 = j;
            }
            for (int j1 = i << 11 | k << 7; i1 > 0 && Block.lightOpacity[blocks[(j1 + i1) - 1]] == 0; i1--)
            {
            }
            if (i1 == l)
            {
                return;
            }
            worldObj.markBlocksDirtyVertical(i, k, i1, l);
            heightMap[k << 4 | i] = (byte) i1;
            if (i1 < field_686_i)
            {
                field_686_i = i1;
            }
            else
            {
                int k1 = 127;
                for (int i2 = 0; i2 < 16; i2++)
                {
                    for (int k2 = 0; k2 < 16; k2++)
                    {
                        if ((heightMap[k2 << 4 | i2] & 0xff) < k1)
                        {
                            k1 = heightMap[k2 << 4 | i2] & 0xff;
                        }
                    }
                }

                field_686_i = k1;
            }
            int l1 = xPosition*16 + i;
            int j2 = zPosition*16 + k;
            if (i1 < l)
            {
                for (int l2 = i1; l2 < l; l2++)
                {
                    skylightMap.setNibble(i, l2, k, 15);
                }
            }
            else
            {
                worldObj.func_483_a(EnumSkyBlock.Sky, l1, l, j2, l1, i1, j2);
                for (int i3 = l; i3 < i1; i3++)
                {
                    skylightMap.setNibble(i, i3, k, 0);
                }
            }
            int j3 = 15;
            int k3 = i1;
            while (i1 > 0 && j3 > 0)
            {
                i1--;
                int l3 = Block.lightOpacity[getBlockID(i, i1, k)];
                if (l3 == 0)
                {
                    l3 = 1;
                }
                j3 -= l3;
                if (j3 < 0)
                {
                    j3 = 0;
                }
                skylightMap.setNibble(i, i1, k, j3);
            }
            for (; i1 > 0 && Block.lightOpacity[getBlockID(i, i1 - 1, k)] == 0; i1--)
            {
            }
            if (i1 != k3)
            {
                worldObj.func_483_a(EnumSkyBlock.Sky, l1 - 1, i1, j2 - 1, l1 + 1, k3, j2 + 1);
            }
            isModified = true;
        }

        public virtual int getBlockID(int i, int j, int k)
        {
            return blocks[i << 11 | k << 7 | j];
        }

        public virtual bool setBlockIDWithMetadata(int i, int j, int k, int l, int i1)
        {
            var byte0 = (byte) l;
            int j1 = heightMap[k << 4 | i] & 0xff;
            int k1 = blocks[i << 11 | k << 7 | j] & 0xff;
            if (k1 == l && data.getNibble(i, j, k) == i1)
            {
                return false;
            }
            int l1 = xPosition*16 + i;
            int i2 = zPosition*16 + k;
            blocks[i << 11 | k << 7 | j] = byte0;
            if (k1 != 0 && !worldObj.singleplayerWorld)
            {
                Block.blocksList[k1].onBlockRemoval(worldObj, l1, j, i2);
            }
            data.setNibble(i, j, k, i1);
            if (!worldObj.worldProvider.field_4306_c)
            {
                if (Block.lightOpacity[byte0] != 0)
                {
                    if (j >= j1)
                    {
                        func_339_g(i, j + 1, k);
                    }
                }
                else if (j == j1 - 1)
                {
                    func_339_g(i, j, k);
                }
                worldObj.func_483_a(EnumSkyBlock.Sky, l1, j, i2, l1, j, i2);
            }
            worldObj.func_483_a(EnumSkyBlock.Block, l1, j, i2, l1, j, i2);
            func_333_c(i, k);
            data.setNibble(i, j, k, i1);
            if (l != 0)
            {
                Block.blocksList[l].onBlockAdded(worldObj, l1, j, i2);
            }
            isModified = true;
            return true;
        }

        public virtual bool setBlockID(int i, int j, int k, int l)
        {
            var byte0 = (byte) l;
            int i1 = heightMap[k << 4 | i] & 0xff;
            int j1 = blocks[i << 11 | k << 7 | j] & 0xff;
            if (j1 == l)
            {
                return false;
            }
            int k1 = xPosition*16 + i;
            int l1 = zPosition*16 + k;
            blocks[i << 11 | k << 7 | j] = byte0;
            if (j1 != 0)
            {
                Block.blocksList[j1].onBlockRemoval(worldObj, k1, j, l1);
            }
            data.setNibble(i, j, k, 0);
            if (Block.lightOpacity[byte0] != 0)
            {
                if (j >= i1)
                {
                    func_339_g(i, j + 1, k);
                }
            }
            else if (j == i1 - 1)
            {
                func_339_g(i, j, k);
            }
            worldObj.func_483_a(EnumSkyBlock.Sky, k1, j, l1, k1, j, l1);
            worldObj.func_483_a(EnumSkyBlock.Block, k1, j, l1, k1, j, l1);
            func_333_c(i, k);
            if (l != 0 && !worldObj.singleplayerWorld)
            {
                Block.blocksList[l].onBlockAdded(worldObj, k1, j, l1);
            }
            isModified = true;
            return true;
        }

        public virtual int getBlockMetadata(int i, int j, int k)
        {
            return data.getNibble(i, j, k);
        }

        public virtual void setBlockMetadata(int i, int j, int k, int l)
        {
            isModified = true;
            data.setNibble(i, j, k, l);
        }

        public virtual int getSavedLightValue(EnumSkyBlock enumskyblock, int i, int j, int k)
        {
            if (enumskyblock == EnumSkyBlock.Sky)
            {
                return skylightMap.getNibble(i, j, k);
            }
            if (enumskyblock == EnumSkyBlock.Block)
            {
                return blocklightMap.getNibble(i, j, k);
            }
            else
            {
                return 0;
            }
        }

        public virtual void setLightValue(EnumSkyBlock enumskyblock, int i, int j, int k, int l)
        {
            isModified = true;
            if (enumskyblock == EnumSkyBlock.Sky)
            {
                skylightMap.setNibble(i, j, k, l);
            }
            else if (enumskyblock == EnumSkyBlock.Block)
            {
                blocklightMap.setNibble(i, j, k, l);
            }
            else
            {
                return;
            }
        }

        public virtual int getBlockLightValue(int i, int j, int k, int l)
        {
            int i1 = skylightMap.getNibble(i, j, k);
            if (i1 > 0)
            {
                isLit = true;
            }
            i1 -= l;
            int j1 = blocklightMap.getNibble(i, j, k);
            if (j1 > i1)
            {
                i1 = j1;
            }
            return i1;
        }

        public virtual void addEntity(Entity entity)
        {
            hasEntities = true;
            int i = MathHelper.floor_double(entity.posX/16D);
            int j = MathHelper.floor_double(entity.posZ/16D);
            if (i != xPosition || j != zPosition)
            {
                java.lang.System.@out.println((new StringBuilder()).append("Wrong location! ").append(entity).toString());
                Thread.dumpStack();
            }
            int k = MathHelper.floor_double(entity.posY/16D);
            if (k < 0)
            {
                k = 0;
            }
            if (k >= entities.Length)
            {
                k = entities.Length - 1;
            }
            entity.addedToChunk = true;
            entity.chunkCoordX = xPosition;
            entity.chunkCoordY = k;
            entity.chunkCoordZ = zPosition;
            entities[k].add(entity);
        }

        public virtual void removeEntity(Entity entity)
        {
            removeEntityAtIndex(entity, entity.chunkCoordY);
        }

        public virtual void removeEntityAtIndex(Entity entity, int i)
        {
            if (i < 0)
            {
                i = 0;
            }
            if (i >= entities.Length)
            {
                i = entities.Length - 1;
            }
            entities[i].remove(entity);
        }

        public virtual bool canBlockSeeTheSky(int i, int j, int k)
        {
            return j >= (heightMap[k << 4 | i] & 0xff);
        }

        public virtual TileEntity getChunkBlockTileEntity(int i, int j, int k)
        {
            var position = new ChunkPosition(i, j, k);
            var entity = (TileEntity) chunkTileEntityMap.get(position);
            if (entity != null)
            {
                return entity;
            }
            int index = getBlockID(i, j, k);
            if (!Block.isBlockContainer[index])
            {
                return null;
            }
            (Block.blocksList[index]).onBlockAdded(worldObj, (xPosition*0x10) + i, j,
                                                   (zPosition*0x10) + k);
            return (TileEntity) chunkTileEntityMap.get(position);
        }

        public virtual TileEntity getChunkBlockTileEntity_(int i, int j, int k)
        {
            var chunkposition = new ChunkPosition(i, j, k);
            var tileentity = (TileEntity) chunkTileEntityMap.get(chunkposition);
            if (tileentity == null)
            {
                int l = getBlockID(i, j, k);
                if (!Block.isBlockContainer[l])
                {
                    return null;
                }
                var blockcontainer = (BlockContainer) Block.blocksList[l];
                blockcontainer.onBlockAdded(worldObj, xPosition*16 + i, j, zPosition*16 + k);
                tileentity = (TileEntity) chunkTileEntityMap.get(chunkposition);
            }
            return tileentity;
        }

        public virtual void func_349_a(TileEntity tileentity)
        {
            int i = tileentity.xCoord - xPosition*16;
            int j = tileentity.yCoord;
            int k = tileentity.zCoord - zPosition*16;
            setChunkBlockTileEntity(i, j, k, tileentity);
        }

        public virtual void setChunkBlockTileEntity(int i, int j, int k, TileEntity tileentity)
        {
            var chunkposition = new ChunkPosition(i, j, k);
            tileentity.worldObj = worldObj;
            tileentity.xCoord = xPosition*16 + i;
            tileentity.yCoord = j;
            tileentity.zCoord = zPosition*16 + k;
            if (getBlockID(i, j, k) == 0 || !(Block.blocksList[getBlockID(i, j, k)] is BlockContainer))
            {
                java.lang.System.@out.println("Attempted to place a tile entity where there was no entity tile!");
                return;
            }
            if (isChunkLoaded)
            {
                if (chunkTileEntityMap.get(chunkposition) != null)
                {
                    worldObj.loadedTileEntityList.remove(chunkTileEntityMap.get(chunkposition));
                }
                worldObj.loadedTileEntityList.add(tileentity);
            }
            chunkTileEntityMap.put(chunkposition, tileentity);
        }

        public virtual void removeChunkBlockTileEntity(int i, int j, int k)
        {
            var chunkposition = new ChunkPosition(i, j, k);
            if (isChunkLoaded)
            {
                worldObj.loadedTileEntityList.remove(chunkTileEntityMap.remove(chunkposition));
            }
        }

        public virtual void onChunkLoad()
        {
            isChunkLoaded = true;
            worldObj.loadedTileEntityList.addAll(chunkTileEntityMap.values());
            for (int i = 0; i < entities.Length; i++)
            {
                worldObj.func_464_a(entities[i]);
            }
        }

        public virtual void onChunkUnload()
        {
            isChunkLoaded = false;
            worldObj.loadedTileEntityList.removeAll(chunkTileEntityMap.values());
            for (int i = 0; i < entities.Length; i++)
            {
                worldObj.func_461_b(entities[i]);
            }
        }

        public virtual void setChunkModified()
        {
            isModified = true;
        }

        public virtual void getEntitiesWithinAABBForEntity(Entity entity, AxisAlignedBB axisalignedbb, List list)
        {
            int i = MathHelper.floor_double((axisalignedbb.minY - 2D)/16D);
            int j = MathHelper.floor_double((axisalignedbb.maxY + 2D)/16D);
            if (i < 0)
            {
                i = 0;
            }
            if (j >= entities.Length)
            {
                j = entities.Length - 1;
            }
            for (int k = i; k <= j; k++)
            {
                List list1 = entities[k];
                for (int l = 0; l < list1.size(); l++)
                {
                    var entity1 = (Entity) list1.get(l);
                    if (entity1 != entity && entity1.boundingBox.intersectsWith(axisalignedbb))
                    {
                        list.add(entity1);
                    }
                }
            }
        }

        public virtual void getEntitiesOfTypeWithinAAAB(Class class1, AxisAlignedBB axisalignedbb, List list)
        {
            int i = MathHelper.floor_double((axisalignedbb.minY - 2D)/16D);
            int j = MathHelper.floor_double((axisalignedbb.maxY + 2D)/16D);
            if (i < 0)
            {
                i = 0;
            }
            if (j >= entities.Length)
            {
                j = entities.Length - 1;
            }
            for (int k = i; k <= j; k++)
            {
                List list1 = entities[k];
                for (int l = 0; l < list1.size(); l++)
                {
                    var entity = (Entity) list1.get(l);
                    if (class1.isAssignableFrom(entity.GetType()) && entity.boundingBox.intersectsWith(axisalignedbb))
                    {
                        list.add(entity);
                    }
                }
            }
        }

        public virtual bool needsSaving(bool flag)
        {
            if (neverSave)
            {
                return false;
            }
            if (flag)
            {
                if (hasEntities && worldObj.getWorldTime() != lastSaveTime)
                {
                    return true;
                }
            }
            else if (hasEntities && worldObj.getWorldTime() >= lastSaveTime + 600L)
            {
                return true;
            }
            return isModified;
        }

        public virtual int getChunkData(byte[] abyte0, int i, int j, int k, int l, int i1, int j1,
                                        int k1)
        {
            for (int l1 = i; l1 < l; l1++)
            {
                for (int l2 = k; l2 < j1; l2++)
                {
                    int l3 = l1 << 11 | l2 << 7 | j;
                    int l4 = i1 - j;
                    Array.Copy(blocks, l3, abyte0, k1, l4);
                    k1 += l4;
                }
            }

            for (int i2 = i; i2 < l; i2++)
            {
                for (int i3 = k; i3 < j1; i3++)
                {
                    int i4 = (i2 << 11 | i3 << 7 | j) >> 1;
                    int i5 = (i1 - j)/2;
                    Array.Copy(data.data, i4, abyte0, k1, i5);
                    k1 += i5;
                }
            }

            for (int j2 = i; j2 < l; j2++)
            {
                for (int j3 = k; j3 < j1; j3++)
                {
                    int j4 = (j2 << 11 | j3 << 7 | j) >> 1;
                    int j5 = (i1 - j)/2;
                    Array.Copy(blocklightMap.data, j4, abyte0, k1, j5);
                    k1 += j5;
                }
            }

            for (int k2 = i; k2 < l; k2++)
            {
                for (int k3 = k; k3 < j1; k3++)
                {
                    int k4 = (k2 << 11 | k3 << 7 | j) >> 1;
                    int k5 = (i1 - j)/2;
                    Array.Copy(skylightMap.data, k4, abyte0, k1, k5);
                    k1 += k5;
                }
            }

            return k1;
        }

        public virtual Random func_334_a(long l)
        {
            return
                new Random(worldObj.func_22079_j() + (xPosition*xPosition*0x4c1906) + (xPosition*0x5ac0db) +
                           (zPosition*zPosition)*0x4307a7L + (zPosition*0x5f24f) ^ l);
        }

        public virtual bool func_21101_g()
        {
            return false;
        }
    }
}