using java.lang;
using java.util;
using Object = System.Object;

namespace CraftyServer.Core
{
    public class World
        : IBlockAccess
    {
        public static int field_4268_y;
        private readonly Set activeChunkSet;
        private readonly List field_778_L;
        private readonly List field_821_y;
        private readonly ArrayList field_9207_I;
        private readonly Set scheduledTickSet;
        private readonly TreeSet scheduledTickTreeSet;
        private readonly List unloadedEntityList;
        protected int DIST_HASH_MAGIC;
        private bool allPlayersSleeping;
        private int ambientTickCountdown;
        protected int autosavePeriod;
        protected IChunkProvider chunkProvider;
        public int difficultySetting;
        protected int distHashCounter;
        public bool editingBlocks;
        private int field_4265_J;
        private long field_6159_E;
        public bool field_9209_x;
        public bool field_9212_p;
        public List loadedEntityList;
        public List loadedTileEntityList;
        private long lockTimestamp;
        public List playerEntities;
        public Random rand;
        public bool scheduledUpdatesAreImmediate;
        public bool singleplayerWorld;
        public int skylightSubtracted;
        private bool spawnHostileMobs;
        private bool spawnPeacefulMobs;
        protected List worldAccesses;
        protected ISaveHandler worldFile;
        protected WorldInfo worldInfo;
        public WorldProvider worldProvider;

        public World(ISaveHandler isavehandler, string s, long l, WorldProvider worldprovider)
        {
            scheduledUpdatesAreImmediate = false;
            field_821_y = new ArrayList();
            loadedEntityList = new ArrayList();
            unloadedEntityList = new ArrayList();
            scheduledTickTreeSet = new TreeSet();
            scheduledTickSet = new HashSet();
            loadedTileEntityList = new ArrayList();
            playerEntities = new ArrayList();
            field_6159_E = 0xffffffL;
            skylightSubtracted = 0;
            distHashCounter = (new Random()).nextInt();
            DIST_HASH_MAGIC = 0x3c6ef35f;
            editingBlocks = false;
            lockTimestamp = java.lang.System.currentTimeMillis();
            autosavePeriod = 40;
            rand = new Random();
            field_9212_p = false;
            worldAccesses = new ArrayList();
            field_9207_I = new ArrayList();
            field_4265_J = 0;
            spawnHostileMobs = true;
            spawnPeacefulMobs = true;
            activeChunkSet = new HashSet();
            ambientTickCountdown = rand.nextInt(12000);
            field_778_L = new ArrayList();
            singleplayerWorld = false;
            worldFile = isavehandler;
            worldInfo = isavehandler.func_22096_c();
            field_9212_p = worldInfo == null;
            if (worldprovider != null)
            {
                worldProvider = worldprovider;
            }
            else if (worldInfo != null && worldInfo.func_22178_h() == -1)
            {
                worldProvider = new WorldProviderHell();
            }
            else
            {
                worldProvider = new WorldProvider();
            }
            bool flag = false;
            if (worldInfo == null)
            {
                worldInfo = new WorldInfo(l, s);
                flag = true;
            }
            else
            {
                worldInfo.setLevelName(s);
            }
            worldProvider.registerWorld(this);
            chunkProvider = func_22086_b();
            if (flag)
            {
                field_9209_x = true;
                int i = 0;
                byte byte0 = 64;
                int j;
                for (j = 0; !worldProvider.canCoordinateBeSpawn(i, j); j += rand.nextInt(64) - rand.nextInt(64))
                {
                    i += rand.nextInt(64) - rand.nextInt(64);
                }

                worldInfo.setSpawnPosition(i, byte0, j);
                field_9209_x = false;
            }
            calculateInitialSkylight();
        }

        #region IBlockAccess Members

        public int getBlockId(int i, int j, int k)
        {
            // converted using reflector ^^
            if (((i < -32000000) || (k < -32000000)) || ((i >= 0x1e84800) || (k > 0x1e84800)))
            {
                return 0;
            }
            if (j < 0)
            {
                return 0;
            }
            if (j >= 128)
            {
                return 0;
            }
            else
            {
                return getChunkFromChunkCoords(i >> 4, k >> 4).getBlockID(i & 0xf, j, k & 0xf);
            }
        }

        public Material getBlockMaterial(int i, int j, int k)
        {
            int l = getBlockId(i, j, k);
            if (l == 0)
            {
                return Material.air;
            }
            else
            {
                return Block.blocksList[l].blockMaterial;
            }
        }

        public int getBlockMetadata(int i, int j, int k)
        {
            if (((i < -32000000) || (k < -32000000)) || ((i >= 0x1e84800) || (k > 0x1e84800)))
            {
                return 0;
            }
            if (j < 0)
            {
                return 0;
            }
            if (j >= 128)
            {
                return 0;
            }
            else
            {
                Chunk chunk = getChunkFromChunkCoords(i >> 4, k >> 4);
                i &= 0xf;
                k &= 0xf;
                return chunk.getBlockMetadata(i, j, k);
            }
        }

        public virtual bool isBlockOpaqueCube(int i, int j, int k)
        {
            Block block = Block.blocksList[getBlockId(i, j, k)];
            if (block == null)
            {
                return false;
            }
            else
            {
                return block.isOpaqueCube();
            }
        }

        #endregion

        public WorldChunkManager getWorldChunkManager()
        {
            return worldProvider.worldChunkMgr;
        }

        protected virtual IChunkProvider func_22086_b()
        {
            IChunkLoader ichunkloader = worldFile.func_22092_a(worldProvider);
            return new ChunkProviderLoadOrGenerate(this, ichunkloader, worldProvider.getChunkProvider());
        }

        public int getFirstUncoveredBlock(int i, int j)
        {
            int k;
            for (k = 63; !isAirBlock(i, k + 1, j); k++)
            {
            }
            return getBlockId(i, k, j);
        }

        public void saveWorld(bool flag, IProgressUpdate iprogressupdate)
        {
            if (!chunkProvider.func_364_b())
            {
                return;
            }
            if (iprogressupdate != null)
            {
                iprogressupdate.func_438_a("Saving level");
            }
            saveLevel();
            if (iprogressupdate != null)
            {
                iprogressupdate.displayLoadingString("Saving chunks");
            }
            chunkProvider.saveChunks(flag, iprogressupdate);
        }

        private void saveLevel()
        {
            checkSessionLock();
            worldFile.func_22095_a(worldInfo, playerEntities);
        }

        public bool isAirBlock(int i, int j, int k)
        {
            return getBlockId(i, j, k) == 0;
        }

        public bool blockExists(int i, int j, int k)
        {
            if (j < 0 || j >= 128)
            {
                return false;
            }
            else
            {
                return chunkExists(i >> 4, k >> 4);
            }
        }

        public bool doChunksNearChunkExist(int i, int j, int k, int l)
        {
            return checkChunksExist(i - l, j - l, k - l, i + l, j + l, k + l);
        }

        public bool checkChunksExist(int i, int j, int k, int l, int i1, int j1)
        {
            if (i1 < 0 || j >= 128)
            {
                return false;
            }
            i >>= 4;
            j >>= 4;
            k >>= 4;
            l >>= 4;
            i1 >>= 4;
            j1 >>= 4;
            for (int k1 = i; k1 <= l; k1++)
            {
                for (int l1 = k; l1 <= j1; l1++)
                {
                    if (!chunkExists(k1, l1))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool chunkExists(int i, int j)
        {
            return chunkProvider.chunkExists(i, j);
        }

        public Chunk getChunkFromBlockCoords(int i, int j)
        {
            return getChunkFromChunkCoords(i >> 4, j >> 4);
        }

        public Chunk getChunkFromChunkCoords(int i, int j)
        {
            return chunkProvider.provideChunk(i, j);
        }

        public bool setBlockAndMetadata(int i, int j, int k, int l, int i1)
        {
            if (((i < -32000000) || (k < -32000000)) || ((i >= 0x1e84800) || (k > 0x1e84800)))
            {
                return false;
            }
            if (j < 0)
            {
                return false;
            }
            if (j >= 128)
            {
                return false;
            }
            else
            {
                Chunk chunk = getChunkFromChunkCoords(i >> 4, k >> 4);
                return chunk.setBlockIDWithMetadata(i & 0xf, j, k & 0xf, l, i1);
            }
        }

        public bool setBlock(int i, int j, int k, int l)
        {
            if (((i < -32000000) || (k < -32000000)) || ((i >= 0x1e84800) || (k > 0x1e84800)))
            {
                return false;
            }
            if (j < 0)
            {
                return false;
            }
            if (j >= 128)
            {
                return false;
            }
            else
            {
                Chunk chunk = getChunkFromChunkCoords(i >> 4, k >> 4);
                return chunk.setBlockID(i & 0xf, j, k & 0xf, l);
            }
        }

        public void setBlockMetadataWithNotify(int i, int j, int k, int l)
        {
            if (setBlockMetadata(i, j, k, l))
            {
                notifyBlockChange(i, j, k, getBlockId(i, j, k));
            }
        }

        public bool setBlockMetadata(int i, int j, int k, int l)
        {
            if (((i < -32000000) || (k < -32000000)) || ((i >= 0x1e84800) || (k > 0x1e84800)))
            {
                return false;
            }
            if (j < 0)
            {
                return false;
            }
            if (j >= 128)
            {
                return false;
            }
            else
            {
                Chunk chunk = getChunkFromChunkCoords(i >> 4, k >> 4);
                i &= 0xf;
                k &= 0xf;
                chunk.setBlockMetadata(i, j, k, l);
                return true;
            }
        }

        public bool setBlockWithNotify(int i, int j, int k, int l)
        {
            if (setBlock(i, j, k, l))
            {
                notifyBlockChange(i, j, k, l);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool setBlockAndMetadataWithNotify(int i, int j, int k, int l, int i1)
        {
            if (setBlockAndMetadata(i, j, k, l, i1))
            {
                notifyBlockChange(i, j, k, l);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void markBlockNeedsUpdate(int i, int j, int k)
        {
            for (int l = 0; l < worldAccesses.size(); l++)
            {
                ((IWorldAccess) worldAccesses.get(l)).func_683_a(i, j, k);
            }
        }

        public void notifyBlockChange(int i, int j, int k, int l)
        {
            markBlockNeedsUpdate(i, j, k);
            notifyBlocksOfNeighborChange(i, j, k, l);
        }

        public void markBlocksDirtyVertical(int i, int j, int k, int l)
        {
            if (k > l)
            {
                int i1 = l;
                l = k;
                k = i1;
            }
            markBlocksDirty(i, k, j, i, l, j);
        }

        public void markBlockAsNeedsUpdate(int i, int j, int k)
        {
            for (int l = 0; l < worldAccesses.size(); l++)
            {
                ((IWorldAccess) worldAccesses.get(l)).markBlockRangeNeedsUpdate(i, j, k, i, j, k);
            }
        }

        public void markBlocksDirty(int i, int j, int k, int l, int i1, int j1)
        {
            for (int k1 = 0; k1 < worldAccesses.size(); k1++)
            {
                ((IWorldAccess) worldAccesses.get(k1)).markBlockRangeNeedsUpdate(i, j, k, l, i1, j1);
            }
        }

        public void notifyBlocksOfNeighborChange(int i, int j, int k, int l)
        {
            notifyBlockOfNeighborChange(i - 1, j, k, l);
            notifyBlockOfNeighborChange(i + 1, j, k, l);
            notifyBlockOfNeighborChange(i, j - 1, k, l);
            notifyBlockOfNeighborChange(i, j + 1, k, l);
            notifyBlockOfNeighborChange(i, j, k - 1, l);
            notifyBlockOfNeighborChange(i, j, k + 1, l);
        }

        private void notifyBlockOfNeighborChange(int i, int j, int k, int l)
        {
            if (editingBlocks || singleplayerWorld)
            {
                return;
            }
            Block block = Block.blocksList[getBlockId(i, j, k)];
            if (block != null)
            {
                block.onNeighborBlockChange(this, i, j, k, l);
            }
        }

        public bool canBlockSeeTheSky(int i, int j, int k)
        {
            return getChunkFromChunkCoords(i >> 4, k >> 4).canBlockSeeTheSky(i & 0xf, j, k & 0xf);
        }

        public int getBlockLightValue(int i, int j, int k)
        {
            return getBlockLightValue_do(i, j, k, true);
        }

        public int getBlockLightValue_do(int i, int j, int k, bool flag)
        {
            if (((i < -32000000) || (k < -32000000)) || ((i >= 0x1e84800) || (k > 0x1e84800)))
            {
                return 15;
            }
            if (flag)
            {
                int l = getBlockId(i, j, k);
                if (l == Block.stairSingle.blockID || l == Block.tilledField.blockID)
                {
                    int j1 = getBlockLightValue_do(i, j + 1, k, false);
                    int k1 = getBlockLightValue_do(i + 1, j, k, false);
                    int l1 = getBlockLightValue_do(i - 1, j, k, false);
                    int i2 = getBlockLightValue_do(i, j, k + 1, false);
                    int j2 = getBlockLightValue_do(i, j, k - 1, false);
                    if (k1 > j1)
                    {
                        j1 = k1;
                    }
                    if (l1 > j1)
                    {
                        j1 = l1;
                    }
                    if (i2 > j1)
                    {
                        j1 = i2;
                    }
                    if (j2 > j1)
                    {
                        j1 = j2;
                    }
                    return j1;
                }
            }
            if (j < 0)
            {
                return 0;
            }
            if (j >= 128)
            {
                int i1 = 15 - skylightSubtracted;
                if (i1 < 0)
                {
                    i1 = 0;
                }
                return i1;
            }
            else
            {
                Chunk chunk = getChunkFromChunkCoords(i >> 4, k >> 4);
                i &= 0xf;
                k &= 0xf;
                return chunk.getBlockLightValue(i, j, k, skylightSubtracted);
            }
        }

        public bool canExistingBlockSeeTheSky(int i, int j, int k)
        {
            if (((i < -32000000) || (k < -32000000)) || ((i >= 0x1e84800) || (k > 0x1e84800)))
            {
                return false;
            }
            if (j < 0)
            {
                return false;
            }
            if (j >= 128)
            {
                return true;
            }
            if (!chunkExists(i >> 4, k >> 4))
            {
                return false;
            }
            else
            {
                Chunk chunk = getChunkFromChunkCoords(i >> 4, k >> 4);
                i &= 0xf;
                k &= 0xf;
                return chunk.canBlockSeeTheSky(i, j, k);
            }
        }

        public int getHeightValue(int i, int j)
        {
            if (((i < -32000000) || (j < -32000000)) || ((i >= 0x1e84800) || (j > 0x1e84800)))
            {
                return 0;
            }
            if (!chunkExists(i >> 4, j >> 4))
            {
                return 0;
            }
            else
            {
                Chunk chunk = getChunkFromChunkCoords(i >> 4, j >> 4);
                return chunk.getHeightValue(i & 0xf, j & 0xf);
            }
        }

        public void neighborLightPropagationChanged(EnumSkyBlock enumskyblock, int i, int j, int k, int l)
        {
            if (worldProvider.field_4306_c && enumskyblock == EnumSkyBlock.Sky)
            {
                return;
            }
            if (!blockExists(i, j, k))
            {
                return;
            }
            if (enumskyblock == EnumSkyBlock.Sky)
            {
                if (canExistingBlockSeeTheSky(i, j, k))
                {
                    l = 15;
                }
            }
            else if (enumskyblock == EnumSkyBlock.Block)
            {
                int i1 = getBlockId(i, j, k);
                if (Block.lightValue[i1] > l)
                {
                    l = Block.lightValue[i1];
                }
            }
            if (getSavedLightValue(enumskyblock, i, j, k) != l)
            {
                func_483_a(enumskyblock, i, j, k, i, j, k);
            }
        }

        public virtual int getSavedLightValue(EnumSkyBlock enumskyblock, int i, int j, int k)
        {
            if ((((j < 0) || (j >= 0x80)) || ((i < -32000000) || (k < -32000000))) ||
                ((i >= 0x1e84800) || (k > 0x1e84800)))
            {
                return enumskyblock.field_984_c;
            }
            int num = i >> 4;
            int num2 = k >> 4;
            if (!chunkExists(num, num2))
            {
                return 0;
            }
            return getChunkFromChunkCoords(num, num2).getSavedLightValue(enumskyblock, i & 15, j, k & 15);
        }

        public virtual void setLightValue(EnumSkyBlock enumskyblock, int i, int j, int k, int l)
        {
            if (((((i >= -32000000) && (k >= -32000000)) && ((i < 0x1e84800) && (k <= 0x1e84800))) && (j >= 0)) &&
                ((j < 0x80) && chunkExists(i >> 4, k >> 4)))
            {
                return;
            }
            if (j < 0)
            {
                return;
            }
            if (j >= 128)
            {
                return;
            }
            if (!chunkExists(i >> 4, k >> 4))
            {
                return;
            }
            Chunk chunk = getChunkFromChunkCoords(i >> 4, k >> 4);
            chunk.setLightValue(enumskyblock, i & 0xf, j, k & 0xf, l);
            for (int i1 = 0; i1 < worldAccesses.size(); i1++)
            {
                ((IWorldAccess) worldAccesses.get(i1)).func_683_a(i, j, k);
            }
        }

        public virtual float getLightBrightness(int i, int j, int k)
        {
            return worldProvider.lightBrightnessTable[getBlockLightValue(i, j, k)];
        }

        public virtual bool isDaytime()
        {
            return skylightSubtracted < 4;
        }

        public virtual MovingObjectPosition rayTraceBlocks(Vec3D vec3d, Vec3D vec3d1)
        {
            return rayTraceBlocks_do(vec3d, vec3d1, false);
        }

        public virtual MovingObjectPosition rayTraceBlocks_do(Vec3D vec3d, Vec3D vec3d1, bool flag)
        {
            if (Double.isNaN(vec3d.xCoord) || Double.isNaN(vec3d.yCoord) || Double.isNaN(vec3d.zCoord))
            {
                return null;
            }
            if (Double.isNaN(vec3d1.xCoord) || Double.isNaN(vec3d1.yCoord) || Double.isNaN(vec3d1.zCoord))
            {
                return null;
            }
            int i = MathHelper.floor_double(vec3d1.xCoord);
            int j = MathHelper.floor_double(vec3d1.yCoord);
            int k = MathHelper.floor_double(vec3d1.zCoord);
            int l = MathHelper.floor_double(vec3d.xCoord);
            int i1 = MathHelper.floor_double(vec3d.yCoord);
            int j1 = MathHelper.floor_double(vec3d.zCoord);
            for (int k1 = 200; k1-- >= 0;)
            {
                if (Double.isNaN(vec3d.xCoord) || Double.isNaN(vec3d.yCoord) || Double.isNaN(vec3d.zCoord))
                {
                    return null;
                }
                if (l == i && i1 == j && j1 == k)
                {
                    return null;
                }
                double d = 999D;
                double d1 = 999D;
                double d2 = 999D;
                if (i > l)
                {
                    d = l + 1.0D;
                }
                if (i < l)
                {
                    d = l + 0.0D;
                }
                if (j > i1)
                {
                    d1 = i1 + 1.0D;
                }
                if (j < i1)
                {
                    d1 = i1 + 0.0D;
                }
                if (k > j1)
                {
                    d2 = j1 + 1.0D;
                }
                if (k < j1)
                {
                    d2 = j1 + 0.0D;
                }
                double d3 = 999D;
                double d4 = 999D;
                double d5 = 999D;
                double d6 = vec3d1.xCoord - vec3d.xCoord;
                double d7 = vec3d1.yCoord - vec3d.yCoord;
                double d8 = vec3d1.zCoord - vec3d.zCoord;
                if (d != 999D)
                {
                    d3 = (d - vec3d.xCoord)/d6;
                }
                if (d1 != 999D)
                {
                    d4 = (d1 - vec3d.yCoord)/d7;
                }
                if (d2 != 999D)
                {
                    d5 = (d2 - vec3d.zCoord)/d8;
                }
                byte byte0 = 0;
                if (d3 < d4 && d3 < d5)
                {
                    if (i > l)
                    {
                        byte0 = 4;
                    }
                    else
                    {
                        byte0 = 5;
                    }
                    vec3d.xCoord = d;
                    vec3d.yCoord += d7*d3;
                    vec3d.zCoord += d8*d3;
                }
                else if (d4 < d5)
                {
                    if (j > i1)
                    {
                        byte0 = 0;
                    }
                    else
                    {
                        byte0 = 1;
                    }
                    vec3d.xCoord += d6*d4;
                    vec3d.yCoord = d1;
                    vec3d.zCoord += d8*d4;
                }
                else
                {
                    if (k > j1)
                    {
                        byte0 = 2;
                    }
                    else
                    {
                        byte0 = 3;
                    }
                    vec3d.xCoord += d6*d5;
                    vec3d.yCoord += d7*d5;
                    vec3d.zCoord = d2;
                }
                Vec3D vec3d2 = Vec3D.createVector(vec3d.xCoord, vec3d.yCoord, vec3d.zCoord);
                l = (int) (vec3d2.xCoord = MathHelper.floor_double(vec3d.xCoord));
                if (byte0 == 5)
                {
                    l--;
                    vec3d2.xCoord++;
                }
                i1 = (int) (vec3d2.yCoord = MathHelper.floor_double(vec3d.yCoord));
                if (byte0 == 1)
                {
                    i1--;
                    vec3d2.yCoord++;
                }
                j1 = (int) (vec3d2.zCoord = MathHelper.floor_double(vec3d.zCoord));
                if (byte0 == 3)
                {
                    j1--;
                    vec3d2.zCoord++;
                }
                int l1 = getBlockId(l, i1, j1);
                int i2 = getBlockMetadata(l, i1, j1);
                Block block = Block.blocksList[l1];
                if (l1 > 0 && block.canCollideCheck(i2, flag))
                {
                    MovingObjectPosition movingobjectposition = block.collisionRayTrace(this, l, i1, j1, vec3d, vec3d1);
                    if (movingobjectposition != null)
                    {
                        return movingobjectposition;
                    }
                }
            }

            return null;
        }

        public virtual void playSoundAtEntity(Entity entity, string s, float f, float f1)
        {
            for (int i = 0; i < worldAccesses.size(); i++)
            {
                ((IWorldAccess) worldAccesses.get(i)).playSound(s, entity.posX, entity.posY - entity.yOffset,
                                                                entity.posZ, f, f1);
            }
        }

        public virtual void playSoundEffect(double d, double d1, double d2, string s,
                                            float f, float f1)
        {
            for (int i = 0; i < worldAccesses.size(); i++)
            {
                ((IWorldAccess) worldAccesses.get(i)).playSound(s, d, d1, d2, f, f1);
            }
        }

        public virtual void playRecord(string s, int i, int j, int k)
        {
            for (int l = 0; l < worldAccesses.size(); l++)
            {
                ((IWorldAccess) worldAccesses.get(l)).playRecord(s, i, j, k);
            }
        }

        public virtual void spawnParticle(string s, double d, double d1, double d2,
                                          double d3, double d4, double d5)
        {
            for (int i = 0; i < worldAccesses.size(); i++)
            {
                ((IWorldAccess) worldAccesses.get(i)).spawnParticle(s, d, d1, d2, d3, d4, d5);
            }
        }

        public virtual bool entityJoinedWorld(Entity entity)
        {
            int i = MathHelper.floor_double(entity.posX/16D);
            int j = MathHelper.floor_double(entity.posZ/16D);
            bool flag = false;
            if (entity is EntityPlayer)
            {
                flag = true;
            }
            if (flag || chunkExists(i, j))
            {
                if (entity is EntityPlayer)
                {
                    var entityplayer = (EntityPlayer) entity;
                    playerEntities.add(entityplayer);
                    updateAllPlayersSleepingFlag();
                }
                getChunkFromChunkCoords(i, j).addEntity(entity);
                loadedEntityList.add(entity);
                obtainEntitySkin(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void obtainEntitySkin(Entity entity)
        {
            for (int i = 0; i < worldAccesses.size(); i++)
            {
                ((IWorldAccess) worldAccesses.get(i)).obtainEntitySkin(entity);
            }
        }

        public virtual void releaseEntitySkin(Entity entity)
        {
            for (int i = 0; i < worldAccesses.size(); i++)
            {
                ((IWorldAccess) worldAccesses.get(i)).releaseEntitySkin(entity);
            }
        }

        public virtual void func_22085_d(Entity entity)
        {
            if (entity.riddenByEntity != null)
            {
                entity.riddenByEntity.mountEntity(null);
            }
            if (entity.ridingEntity != null)
            {
                entity.mountEntity(null);
            }
            entity.setEntityDead();
            if (entity is EntityPlayer)
            {
                playerEntities.remove(entity);
                updateAllPlayersSleepingFlag();
            }
        }

        public virtual void func_22073_e(Entity entity)
        {
            entity.setEntityDead();
            if (entity is EntityPlayer)
            {
                playerEntities.remove(entity);
                updateAllPlayersSleepingFlag();
            }
            int i = entity.chunkCoordX;
            int j = entity.chunkCoordZ;
            if (entity.addedToChunk && chunkExists(i, j))
            {
                getChunkFromChunkCoords(i, j).removeEntity(entity);
            }
            loadedEntityList.remove(entity);
            releaseEntitySkin(entity);
        }

        public virtual void addWorldAccess(IWorldAccess iworldaccess)
        {
            worldAccesses.add(iworldaccess);
        }

        public virtual List getCollidingBoundingBoxes(Entity entity, AxisAlignedBB axisalignedbb)
        {
            field_9207_I.clear();
            int i = MathHelper.floor_double(axisalignedbb.minX);
            int j = MathHelper.floor_double(axisalignedbb.maxX + 1.0D);
            int k = MathHelper.floor_double(axisalignedbb.minY);
            int l = MathHelper.floor_double(axisalignedbb.maxY + 1.0D);
            int i1 = MathHelper.floor_double(axisalignedbb.minZ);
            int j1 = MathHelper.floor_double(axisalignedbb.maxZ + 1.0D);
            for (int k1 = i; k1 < j; k1++)
            {
                for (int l1 = i1; l1 < j1; l1++)
                {
                    if (!blockExists(k1, 64, l1))
                    {
                        continue;
                    }
                    for (int i2 = k - 1; i2 < l; i2++)
                    {
                        Block block = Block.blocksList[getBlockId(k1, i2, l1)];
                        if (block != null)
                        {
                            block.getCollidingBoundingBoxes(this, k1, i2, l1, axisalignedbb, field_9207_I);
                        }
                    }
                }
            }

            double d = 0.25D;
            List list = getEntitiesWithinAABBExcludingEntity(entity, axisalignedbb.expand(d, d, d));
            for (int j2 = 0; j2 < list.size(); j2++)
            {
                AxisAlignedBB axisalignedbb1 = ((Entity) list.get(j2)).getBoundingBox();
                if (axisalignedbb1 != null && axisalignedbb1.intersectsWith(axisalignedbb))
                {
                    field_9207_I.add(axisalignedbb1);
                }
                axisalignedbb1 = entity.func_89_d((Entity) list.get(j2));
                if (axisalignedbb1 != null && axisalignedbb1.intersectsWith(axisalignedbb))
                {
                    field_9207_I.add(axisalignedbb1);
                }
            }

            return field_9207_I;
        }

        public virtual int calculateSkylightSubtracted(float f)
        {
            float f1 = getCelestialAngle(f);
            float f2 = 1.0F - (MathHelper.cos(f1*3.141593F*2.0F)*2.0F + 0.5F);
            if (f2 < 0.0F)
            {
                f2 = 0.0F;
            }
            if (f2 > 1.0F)
            {
                f2 = 1.0F;
            }
            return (int) (f2*11F);
        }

        public virtual float getCelestialAngle(float f)
        {
            return worldProvider.calculateCelestialAngle(worldInfo.getWorldTime(), f);
        }

        public virtual int findTopSolidBlock(int i, int j)
        {
            Chunk chunk = getChunkFromBlockCoords(i, j);
            int k;
            for (k = 127; getBlockMaterial(i, k, j).getIsSolid() && k > 0; k--)
            {
            }
            i &= 0xf;
            j &= 0xf;
            while (k > 0)
            {
                int l = chunk.getBlockID(i, k, j);
                if (l == 0 ||
                    !Block.blocksList[l].blockMaterial.getIsSolid() && !Block.blocksList[l].blockMaterial.getIsLiquid())
                {
                    k--;
                }
                else
                {
                    return k + 1;
                }
            }
            return -1;
        }

        public virtual void func_22074_c(int i, int j, int k, int l, int i1)
        {
            var nextticklistentry = new NextTickListEntry(i, j, k, l);
            byte byte0 = 8;
            if (scheduledUpdatesAreImmediate)
            {
                if (checkChunksExist(nextticklistentry.xCoord - byte0, nextticklistentry.yCoord - byte0,
                                     nextticklistentry.zCoord - byte0, nextticklistentry.xCoord + byte0,
                                     nextticklistentry.yCoord + byte0, nextticklistentry.zCoord + byte0))
                {
                    int j1 = getBlockId(nextticklistentry.xCoord, nextticklistentry.yCoord, nextticklistentry.zCoord);
                    if (j1 == nextticklistentry.blockID && j1 > 0)
                    {
                        Block.blocksList[j1].updateTick(this, nextticklistentry.xCoord, nextticklistentry.yCoord,
                                                        nextticklistentry.zCoord, rand);
                    }
                }
                return;
            }
            if (checkChunksExist(i - byte0, j - byte0, k - byte0, i + byte0, j + byte0, k + byte0))
            {
                if (l > 0)
                {
                    nextticklistentry.setScheduledTime(i1 + worldInfo.getWorldTime());
                }
                if (!scheduledTickSet.contains(nextticklistentry))
                {
                    scheduledTickSet.add(nextticklistentry);
                    scheduledTickTreeSet.add(nextticklistentry);
                }
            }
        }

        public virtual void updateEntities()
        {
            loadedEntityList.removeAll(unloadedEntityList);
            for (int i = 0; i < unloadedEntityList.size(); i++)
            {
                var entity = (Entity) unloadedEntityList.get(i);
                int i1 = entity.chunkCoordX;
                int k1 = entity.chunkCoordZ;
                if (entity.addedToChunk && chunkExists(i1, k1))
                {
                    getChunkFromChunkCoords(i1, k1).removeEntity(entity);
                }
            }

            for (int j = 0; j < unloadedEntityList.size(); j++)
            {
                releaseEntitySkin((Entity) unloadedEntityList.get(j));
            }

            unloadedEntityList.clear();
            for (int k = 0; k < loadedEntityList.size(); k++)
            {
                var entity1 = (Entity) loadedEntityList.get(k);
                if (entity1.ridingEntity != null)
                {
                    if (!entity1.ridingEntity.isDead && entity1.ridingEntity.riddenByEntity == entity1)
                    {
                        continue;
                    }
                    entity1.ridingEntity.riddenByEntity = null;
                    entity1.ridingEntity = null;
                }
                if (!entity1.isDead)
                {
                    updateEntity(entity1);
                }
                if (!entity1.isDead)
                {
                    continue;
                }
                int j1 = entity1.chunkCoordX;
                int l1 = entity1.chunkCoordZ;
                if (entity1.addedToChunk && chunkExists(j1, l1))
                {
                    getChunkFromChunkCoords(j1, l1).removeEntity(entity1);
                }
                loadedEntityList.remove(k--);
                releaseEntitySkin(entity1);
            }

            for (int l = 0; l < loadedTileEntityList.size(); l++)
            {
                var tileentity = (TileEntity) loadedTileEntityList.get(l);
                tileentity.updateEntity();
            }
        }

        public virtual void updateEntity(Entity entity)
        {
            updateEntityWithOptionalForce(entity, true);
        }

        public virtual void updateEntityWithOptionalForce(Entity entity, bool flag)
        {
            int i = MathHelper.floor_double(entity.posX);
            int j = MathHelper.floor_double(entity.posZ);
            byte byte0 = 32;
            if (flag && !checkChunksExist(i - byte0, 0, j - byte0, i + byte0, 128, j + byte0))
            {
                return;
            }
            entity.lastTickPosX = entity.posX;
            entity.lastTickPosY = entity.posY;
            entity.lastTickPosZ = entity.posZ;
            entity.prevRotationYaw = entity.rotationYaw;
            entity.prevRotationPitch = entity.rotationPitch;
            if (flag && entity.addedToChunk)
            {
                if (entity.ridingEntity != null)
                {
                    entity.updateRidden();
                }
                else
                {
                    entity.onUpdate();
                }
            }
            if (Double.isNaN(entity.posX) || Double.isInfinite(entity.posX))
            {
                entity.posX = entity.lastTickPosX;
            }
            if (Double.isNaN(entity.posY) || Double.isInfinite(entity.posY))
            {
                entity.posY = entity.lastTickPosY;
            }
            if (Double.isNaN(entity.posZ) || Double.isInfinite(entity.posZ))
            {
                entity.posZ = entity.lastTickPosZ;
            }
            if (Double.isNaN(entity.rotationPitch) || Double.isInfinite(entity.rotationPitch))
            {
                entity.rotationPitch = entity.prevRotationPitch;
            }
            if (Double.isNaN(entity.rotationYaw) || Double.isInfinite(entity.rotationYaw))
            {
                entity.rotationYaw = entity.prevRotationYaw;
            }
            int k = MathHelper.floor_double(entity.posX/16D);
            int l = MathHelper.floor_double(entity.posY/16D);
            int i1 = MathHelper.floor_double(entity.posZ/16D);
            if (!entity.addedToChunk || entity.chunkCoordX != k || entity.chunkCoordY != l || entity.chunkCoordZ != i1)
            {
                if (entity.addedToChunk && chunkExists(entity.chunkCoordX, entity.chunkCoordZ))
                {
                    getChunkFromChunkCoords(entity.chunkCoordX, entity.chunkCoordZ).removeEntityAtIndex(entity,
                                                                                                        entity.
                                                                                                            chunkCoordY);
                }
                if (chunkExists(k, i1))
                {
                    entity.addedToChunk = true;
                    getChunkFromChunkCoords(k, i1).addEntity(entity);
                }
                else
                {
                    entity.addedToChunk = false;
                }
            }
            if (flag && entity.addedToChunk && entity.riddenByEntity != null)
            {
                if (entity.riddenByEntity.isDead || entity.riddenByEntity.ridingEntity != entity)
                {
                    entity.riddenByEntity.ridingEntity = null;
                    entity.riddenByEntity = null;
                }
                else
                {
                    updateEntity(entity.riddenByEntity);
                }
            }
        }

        public bool checkIfAABBIsClear(AxisAlignedBB axisalignedbb)
        {
            List list = getEntitiesWithinAABBExcludingEntity(null, axisalignedbb);
            for (int i = 0; i < list.size(); i++)
            {
                var entity = (Entity) list.get(i);
                if (!entity.isDead && entity.preventEntitySpawning)
                {
                    return false;
                }
            }

            return true;
        }

        public virtual bool getIsAnyLiquid(AxisAlignedBB axisalignedbb)
        {
            int i = MathHelper.floor_double(axisalignedbb.minX);
            int j = MathHelper.floor_double(axisalignedbb.maxX + 1.0D);
            int k = MathHelper.floor_double(axisalignedbb.minY);
            int l = MathHelper.floor_double(axisalignedbb.maxY + 1.0D);
            int i1 = MathHelper.floor_double(axisalignedbb.minZ);
            int j1 = MathHelper.floor_double(axisalignedbb.maxZ + 1.0D);
            if (axisalignedbb.minX < 0.0D)
            {
                i--;
            }
            if (axisalignedbb.minY < 0.0D)
            {
                k--;
            }
            if (axisalignedbb.minZ < 0.0D)
            {
                i1--;
            }
            for (int k1 = i; k1 < j; k1++)
            {
                for (int l1 = k; l1 < l; l1++)
                {
                    for (int i2 = i1; i2 < j1; i2++)
                    {
                        Block block = Block.blocksList[getBlockId(k1, l1, i2)];
                        if (block != null && block.blockMaterial.getIsLiquid())
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public virtual bool isBoundingBoxBurning(AxisAlignedBB axisalignedbb)
        {
            int i = MathHelper.floor_double(axisalignedbb.minX);
            int j = MathHelper.floor_double(axisalignedbb.maxX + 1.0D);
            int k = MathHelper.floor_double(axisalignedbb.minY);
            int l = MathHelper.floor_double(axisalignedbb.maxY + 1.0D);
            int i1 = MathHelper.floor_double(axisalignedbb.minZ);
            int j1 = MathHelper.floor_double(axisalignedbb.maxZ + 1.0D);
            if (checkChunksExist(i, k, i1, j, l, j1))
            {
                for (int k1 = i; k1 < j; k1++)
                {
                    for (int l1 = k; l1 < l; l1++)
                    {
                        for (int i2 = i1; i2 < j1; i2++)
                        {
                            int j2 = getBlockId(k1, l1, i2);
                            if (j2 == Block.fire.blockID || j2 == Block.lavaStill.blockID ||
                                j2 == Block.lavaMoving.blockID)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public virtual bool handleMaterialAcceleration(AxisAlignedBB axisalignedbb, Material material, Entity entity)
        {
            int i = MathHelper.floor_double(axisalignedbb.minX);
            int j = MathHelper.floor_double(axisalignedbb.maxX + 1.0D);
            int k = MathHelper.floor_double(axisalignedbb.minY);
            int l = MathHelper.floor_double(axisalignedbb.maxY + 1.0D);
            int i1 = MathHelper.floor_double(axisalignedbb.minZ);
            int j1 = MathHelper.floor_double(axisalignedbb.maxZ + 1.0D);
            if (!checkChunksExist(i, k, i1, j, l, j1))
            {
                return false;
            }
            bool flag = false;
            Vec3D vec3d = Vec3D.createVector(0.0D, 0.0D, 0.0D);
            for (int k1 = i; k1 < j; k1++)
            {
                for (int l1 = k; l1 < l; l1++)
                {
                    for (int i2 = i1; i2 < j1; i2++)
                    {
                        Block block = Block.blocksList[getBlockId(k1, l1, i2)];
                        if (block == null || block.blockMaterial != material)
                        {
                            continue;
                        }
                        double d1 = (l1 + 1) - BlockFluids.setFluidHeight(getBlockMetadata(k1, l1, i2));
                        if (l >= d1)
                        {
                            flag = true;
                            block.velocityToAddToEntity(this, k1, l1, i2, entity, vec3d);
                        }
                    }
                }
            }

            if (vec3d.lengthVector() > 0.0D)
            {
                vec3d = vec3d.normalize();
                double d = 0.0040000000000000001D;
                entity.motionX += vec3d.xCoord*d;
                entity.motionY += vec3d.yCoord*d;
                entity.motionZ += vec3d.zCoord*d;
            }
            return flag;
        }

        public virtual bool isMaterialInBB(AxisAlignedBB axisalignedbb, Material material)
        {
            int i = MathHelper.floor_double(axisalignedbb.minX);
            int j = MathHelper.floor_double(axisalignedbb.maxX + 1.0D);
            int k = MathHelper.floor_double(axisalignedbb.minY);
            int l = MathHelper.floor_double(axisalignedbb.maxY + 1.0D);
            int i1 = MathHelper.floor_double(axisalignedbb.minZ);
            int j1 = MathHelper.floor_double(axisalignedbb.maxZ + 1.0D);
            for (int k1 = i; k1 < j; k1++)
            {
                for (int l1 = k; l1 < l; l1++)
                {
                    for (int i2 = i1; i2 < j1; i2++)
                    {
                        Block block = Block.blocksList[getBlockId(k1, l1, i2)];
                        if (block != null && block.blockMaterial == material)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public virtual bool isAABBInMaterial(AxisAlignedBB axisalignedbb, Material material)
        {
            int i = MathHelper.floor_double(axisalignedbb.minX);
            int j = MathHelper.floor_double(axisalignedbb.maxX + 1.0D);
            int k = MathHelper.floor_double(axisalignedbb.minY);
            int l = MathHelper.floor_double(axisalignedbb.maxY + 1.0D);
            int i1 = MathHelper.floor_double(axisalignedbb.minZ);
            int j1 = MathHelper.floor_double(axisalignedbb.maxZ + 1.0D);
            for (int k1 = i; k1 < j; k1++)
            {
                for (int l1 = k; l1 < l; l1++)
                {
                    for (int i2 = i1; i2 < j1; i2++)
                    {
                        Block block = Block.blocksList[getBlockId(k1, l1, i2)];
                        if (block == null || block.blockMaterial != material)
                        {
                            continue;
                        }
                        int j2 = getBlockMetadata(k1, l1, i2);
                        double d = l1 + 1;
                        if (j2 < 8)
                        {
                            d = (l1 + 1) - j2/8D;
                        }
                        if (d >= axisalignedbb.minY)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public virtual Explosion createExplosion(Entity entity, double d, double d1, double d2,
                                                 float f)
        {
            return newExplosion(entity, d, d1, d2, f, false);
        }

        public virtual Explosion newExplosion(Entity entity, double d, double d1, double d2,
                                              float f, bool flag)
        {
            var explosion = new Explosion(this, entity, d, d1, d2, f);
            explosion.isFlaming = flag;
            explosion.doExplosion();
            explosion.doEffects();
            return explosion;
        }

        public virtual float func_494_a(Vec3D vec3d, AxisAlignedBB axisalignedbb)
        {
            double d = 1.0D/((axisalignedbb.maxX - axisalignedbb.minX)*2D + 1.0D);
            double d1 = 1.0D/((axisalignedbb.maxY - axisalignedbb.minY)*2D + 1.0D);
            double d2 = 1.0D/((axisalignedbb.maxZ - axisalignedbb.minZ)*2D + 1.0D);
            int i = 0;
            int j = 0;
            for (float f = 0.0F; f <= 1.0F; f = (float) (f + d))
            {
                for (float f1 = 0.0F; f1 <= 1.0F; f1 = (float) (f1 + d1))
                {
                    for (float f2 = 0.0F; f2 <= 1.0F; f2 = (float) (f2 + d2))
                    {
                        double d3 = axisalignedbb.minX + (axisalignedbb.maxX - axisalignedbb.minX)*f;
                        double d4 = axisalignedbb.minY + (axisalignedbb.maxY - axisalignedbb.minY)*f1;
                        double d5 = axisalignedbb.minZ + (axisalignedbb.maxZ - axisalignedbb.minZ)*f2;
                        if (rayTraceBlocks(Vec3D.createVector(d3, d4, d5), vec3d) == null)
                        {
                            i++;
                        }
                        j++;
                    }
                }
            }

            return i/(float) j;
        }

        public virtual TileEntity getBlockTileEntity(int i, int j, int k)
        {
            Chunk chunk = getChunkFromChunkCoords(i >> 4, k >> 4);
            if (chunk != null)
            {
                return chunk.getChunkBlockTileEntity(i & 0xf, j, k & 0xf);
            }
            else
            {
                return null;
            }
        }

        public virtual void setBlockTileEntity(int i, int j, int k, TileEntity tileentity)
        {
            Chunk chunk = getChunkFromChunkCoords(i >> 4, k >> 4);
            if (chunk != null)
            {
                chunk.setChunkBlockTileEntity(i & 0xf, j, k & 0xf, tileentity);
            }
        }

        public virtual void removeBlockTileEntity(int i, int j, int k)
        {
            Chunk chunk = getChunkFromChunkCoords(i >> 4, k >> 4);
            if (chunk != null)
            {
                chunk.removeChunkBlockTileEntity(i & 0xf, j, k & 0xf);
            }
        }

        /*
    public virtual bool func_6156_d()
    {
        if(field_4265_J >= 50)
        {
            return false;
        }
        field_4265_J++;
        try
        {
            int i = 500;
            for(; field_821_y.size() > 0; ((MetadataChunkBlock)field_821_y.remove(field_821_y.size() - 1)).func_4107_a(this))
            {
                if(--i <= 0)
                {
                    bool flag = true;
                    return flag;
                }
            }

            bool flag1 = false;
            return flag1;
        }
        finally
        {
            field_4265_J--;
        }
    }*/

        public virtual bool func_6156_d()
        {
            int num;
            if (field_4265_J >= 50)
            {
                return false;
            }
            field_4265_J++;
            try
            {
                num = 500;
            }
            catch (Exception ex)
            {
                field_4265_J--;
                throw ex;
            }
            while (true)
            {
                int num2;
                int num3;
                try
                {
                    if (field_821_y.size() <= 0)
                    {
                        num2 = 0;
                        num3 = num2;
                        field_4265_J--;
                        return false; // ?
                    }
                }
                catch (Exception ex)
                {
                    field_4265_J--;
                    throw ex;
                }
                try
                {
                    num += -1;
                    if (num <= 0)
                    {
                        num2 = 1;
                        num3 = num2;
                        field_4265_J--;
                        return true; //?
                    }
                }
                catch (Exception ex)
                {
                    field_4265_J--;
                    throw ex;
                }
                try
                {
                    ((MetadataChunkBlock) field_821_y.remove((field_821_y.size() - 1))).func_4107_a(this);
                }
                catch (Exception ex)
                {
                    field_4265_J--;
                    throw ex;
                }

                return false;
            }
        }

        public virtual bool func_6156_d___()
        {
            if (field_4265_J >= 50)
            {
                return false;
            }
            field_4265_J++;
            try
            {
                int i = 500;
                Object tempObject;
                //System.Console.WriteLine("Count: {0}", field_821_y.size());
                for (; field_821_y.size() > 0; ((MetadataChunkBlock) tempObject).func_4107_a(this))
                {
                    if (--i <= 0)
                    {
                        bool flag = true;

                        return flag;
                    }
                    tempObject = field_821_y.get(field_821_y.size() - 1);
                    field_821_y.remove(field_821_y.size() - 1);
                }

                bool flag1 = false;
                return flag1;
            }
            finally
            {
                field_4265_J--;
            }
        }


        public virtual void func_483_a(EnumSkyBlock enumskyblock, int i, int j, int k, int l, int i1, int j1)
        {
            func_484_a(enumskyblock, i, j, k, l, i1, j1, true);
        }

        public virtual void func_484_a(EnumSkyBlock enumskyblock, int i, int j, int k, int l, int i1, int j1,
                                       bool flag)
        {
            if (worldProvider.field_4306_c && enumskyblock == EnumSkyBlock.Sky)
            {
                return;
            }
            field_4268_y++;
            if (field_4268_y == 50)
            {
                field_4268_y--;
                return;
            }
            int k1 = (l + i)/2;
            int l1 = (j1 + k)/2;
            if (!blockExists(k1, 64, l1))
            {
                field_4268_y--;
                return;
            }
            if (getChunkFromBlockCoords(k1, l1).func_21101_g())
            {
                return;
            }
            int i2 = field_821_y.size();
            if (flag)
            {
                int j2 = 5;
                if (j2 > i2)
                {
                    j2 = i2;
                }
                for (int l2 = 0; l2 < j2; l2++)
                {
                    var metadatachunkblock =
                        (MetadataChunkBlock) field_821_y.get(field_821_y.size() - l2 - 1);
                    if (metadatachunkblock.field_957_a == enumskyblock &&
                        metadatachunkblock.func_692_a(i, j, k, l, i1, j1))
                    {
                        field_4268_y--;
                        return;
                    }
                }
            }
            field_821_y.add(new MetadataChunkBlock(enumskyblock, i, j, k, l, i1, j1));
            int k2 = 0xf4240;
            if (field_821_y.size() > 0xf4240)
            {
                java.lang.System.@out.println(
                    (new StringBuilder()).append("More than ").append(k2).append(" updates, aborting lighting updates").
                        toString());
                field_821_y.clear();
            }
            field_4268_y--;
        }

        public virtual void calculateInitialSkylight()
        {
            int i = calculateSkylightSubtracted(1.0F);
            if (i != skylightSubtracted)
            {
                skylightSubtracted = i;
            }
        }

        public virtual void setAllowedSpawnTypes(bool flag, bool flag1)
        {
            spawnHostileMobs = flag;
            spawnPeacefulMobs = flag1;
        }

        public virtual void tick()
        {
            if (isAllPlayersFullyAsleep())
            {
                bool flag = false;
                if (spawnHostileMobs && difficultySetting >= 1)
                {
                    flag = SpawnerAnimals.performSleepSpawning(this, playerEntities);
                }
                if (!flag)
                {
                    long l = worldInfo.getWorldTime() + 24000L;
                    worldInfo.setWorldTime(l - l%24000L);
                    wakeUpAllPlayers();
                }
            }
            SpawnerAnimals.performSpawning(this, spawnHostileMobs, spawnPeacefulMobs);
            chunkProvider.func_361_a();
            int i = calculateSkylightSubtracted(1.0F);
            if (i != skylightSubtracted)
            {
                skylightSubtracted = i;
                for (int j = 0; j < worldAccesses.size(); j++)
                {
                    ((IWorldAccess) worldAccesses.get(j)).updateAllRenderers();
                }
            }
            long l1 = worldInfo.getWorldTime() + 1L;
            if (l1%autosavePeriod == 0L)
            {
                saveWorld(false, null);
            }
            worldInfo.setWorldTime(l1);
            TickUpdates(false);
            doRandomUpdateTicks();
        }

        public virtual void doRandomUpdateTicks()
        {
            activeChunkSet.clear();
            for (int i = 0; i < playerEntities.size(); i++)
            {
                var entityplayer = (EntityPlayer) playerEntities.get(i);
                int j = MathHelper.floor_double(entityplayer.posX/16D);
                int l = MathHelper.floor_double(entityplayer.posZ/16D);
                byte byte0 = 9;
                for (int j1 = -byte0; j1 <= byte0; j1++)
                {
                    for (int i2 = -byte0; i2 <= byte0; i2++)
                    {
                        activeChunkSet.add(new ChunkCoordIntPair(j1 + j, i2 + l));
                    }
                }
            }

            if (ambientTickCountdown > 0)
            {
                ambientTickCountdown--;
            }
            for (Iterator iterator = activeChunkSet.iterator(); iterator.hasNext();)
            {
                var chunkcoordintpair = (ChunkCoordIntPair) iterator.next();
                int k = chunkcoordintpair.chunkXPos*16;
                int i1 = chunkcoordintpair.chunkZPos*16;
                Chunk chunk = getChunkFromChunkCoords(chunkcoordintpair.chunkXPos, chunkcoordintpair.chunkZPos);
                if (ambientTickCountdown == 0)
                {
                    distHashCounter = distHashCounter*3 + DIST_HASH_MAGIC;
                    int k1 = distHashCounter >> 2;
                    int j2 = k1 & 0xf;
                    int l2 = k1 >> 8 & 0xf;
                    int j3 = k1 >> 16 & 0x7f;
                    int l3 = chunk.getBlockID(j2, j3, l2);
                    j2 += k;
                    l2 += i1;
                    if (l3 == 0 && getBlockLightValue(j2, j3, l2) <= rand.nextInt(8) &&
                        getSavedLightValue(EnumSkyBlock.Sky, j2, j3, l2) <= 0)
                    {
                        EntityPlayer entityplayer1 = getClosestPlayer(j2 + 0.5D, j3 + 0.5D,
                                                                      l2 + 0.5D, 8D);
                        if (entityplayer1 != null &&
                            entityplayer1.getDistanceSq(j2 + 0.5D, j3 + 0.5D, l2 + 0.5D) > 4D)
                        {
                            playSoundEffect(j2 + 0.5D, j3 + 0.5D, l2 + 0.5D,
                                            "ambient.cave.cave", 0.7F, 0.8F + rand.nextFloat()*0.2F);
                            ambientTickCountdown = rand.nextInt(12000) + 6000;
                        }
                    }
                }
                int l1 = 0;
                while (l1 < 80)
                {
                    distHashCounter = distHashCounter*3 + DIST_HASH_MAGIC;
                    int k2 = distHashCounter >> 2;
                    int i3 = k2 & 0xf;
                    int k3 = k2 >> 8 & 0xf;
                    int i4 = k2 >> 16 & 0x7f;
                    byte byte1 = chunk.blocks[i3 << 11 | k3 << 7 | i4];
                    if (Block.tickOnLoad[byte1])
                    {
                        Block.blocksList[byte1].updateTick(this, i3 + k, i4, k3 + i1, rand);
                    }
                    l1++;
                }
            }
        }

        public virtual bool TickUpdates(bool flag)
        {
            int i = scheduledTickTreeSet.size();
            if (i != scheduledTickSet.size())
            {
                throw new IllegalStateException("TickNextTick list out of synch");
            }
            if (i > 1000)
            {
                i = 1000;
            }
            for (int j = 0; j < i; j++)
            {
                var nextticklistentry = (NextTickListEntry) scheduledTickTreeSet.first();
                if (!flag && nextticklistentry.scheduledTime > worldInfo.getWorldTime())
                {
                    break;
                }
                scheduledTickTreeSet.remove(nextticklistentry);
                scheduledTickSet.remove(nextticklistentry);
                byte byte0 = 8;
                if (
                    !checkChunksExist(nextticklistentry.xCoord - byte0, nextticklistentry.yCoord - byte0,
                                      nextticklistentry.zCoord - byte0, nextticklistentry.xCoord + byte0,
                                      nextticklistentry.yCoord + byte0, nextticklistentry.zCoord + byte0))
                {
                    continue;
                }
                int k = getBlockId(nextticklistentry.xCoord, nextticklistentry.yCoord, nextticklistentry.zCoord);
                if (k == nextticklistentry.blockID && k > 0)
                {
                    Block.blocksList[k].updateTick(this, nextticklistentry.xCoord, nextticklistentry.yCoord,
                                                   nextticklistentry.zCoord, rand);
                }
            }

            return scheduledTickTreeSet.size() != 0;
        }

        public virtual List getEntitiesWithinAABBExcludingEntity(Entity entity, AxisAlignedBB axisalignedbb)
        {
            field_778_L.clear();
            int i = MathHelper.floor_double((axisalignedbb.minX - 2D)/16D);
            int j = MathHelper.floor_double((axisalignedbb.maxX + 2D)/16D);
            int k = MathHelper.floor_double((axisalignedbb.minZ - 2D)/16D);
            int l = MathHelper.floor_double((axisalignedbb.maxZ + 2D)/16D);
            for (int i1 = i; i1 <= j; i1++)
            {
                for (int j1 = k; j1 <= l; j1++)
                {
                    if (chunkExists(i1, j1))
                    {
                        getChunkFromChunkCoords(i1, j1).getEntitiesWithinAABBForEntity(entity, axisalignedbb,
                                                                                       field_778_L);
                    }
                }
            }

            return field_778_L;
        }

        public virtual List getEntitiesWithinAABB(Class class1, AxisAlignedBB axisalignedbb)
        {
            int i = MathHelper.floor_double((axisalignedbb.minX - 2D)/16D);
            int j = MathHelper.floor_double((axisalignedbb.maxX + 2D)/16D);
            int k = MathHelper.floor_double((axisalignedbb.minZ - 2D)/16D);
            int l = MathHelper.floor_double((axisalignedbb.maxZ + 2D)/16D);
            var arraylist = new ArrayList();
            for (int i1 = i; i1 <= j; i1++)
            {
                for (int j1 = k; j1 <= l; j1++)
                {
                    if (chunkExists(i1, j1))
                    {
                        getChunkFromChunkCoords(i1, j1).getEntitiesOfTypeWithinAAAB(class1, axisalignedbb, arraylist);
                    }
                }
            }

            return arraylist;
        }

        public virtual void func_515_b(int i, int j, int k, TileEntity tileentity)
        {
            if (blockExists(i, j, k))
            {
                getChunkFromBlockCoords(i, k).setChunkModified();
            }
            for (int l = 0; l < worldAccesses.size(); l++)
            {
                ((IWorldAccess) worldAccesses.get(l)).doNothingWithTileEntity(i, j, k, tileentity);
            }
        }

        public virtual int countEntities(Class class1)
        {
            int i = 0;
            for (int j = 0; j < loadedEntityList.size(); j++)
            {
                var entity = (Entity) loadedEntityList.get(j);
                if (class1.isAssignableFrom(entity.GetType()))
                {
                    i++;
                }
            }

            return i;
        }

        public virtual void func_464_a(List list)
        {
            loadedEntityList.addAll(list);
            for (int i = 0; i < list.size(); i++)
            {
                obtainEntitySkin((Entity) list.get(i));
            }
        }

        public virtual void func_461_b(List list)
        {
            unloadedEntityList.addAll(list);
        }

        public virtual bool canBlockBePlacedAt(int i, int j, int k, int l, bool flag)
        {
            int i1 = getBlockId(j, k, l);
            Block block = Block.blocksList[i1];
            Block block1 = Block.blocksList[i];
            AxisAlignedBB axisalignedbb = block1.getCollisionBoundingBoxFromPool(this, j, k, l);
            if (flag)
            {
                axisalignedbb = null;
            }
            if (axisalignedbb != null && !checkIfAABBIsClear(axisalignedbb))
            {
                return false;
            }
            if (block == Block.waterStill || block == Block.waterMoving || block == Block.lavaStill ||
                block == Block.lavaMoving || block == Block.fire || block == Block.snow)
            {
                return true;
            }
            return i > 0 && block == null && block1.canPlaceBlockAt(this, j, k, l);
        }

        public virtual PathEntity getPathToEntity(Entity entity, Entity entity1, float f)
        {
            int i = MathHelper.floor_double(entity.posX);
            int j = MathHelper.floor_double(entity.posY);
            int k = MathHelper.floor_double(entity.posZ);
            var l = (int) (f + 16F);
            int i1 = i - l;
            int j1 = j - l;
            int k1 = k - l;
            int l1 = i + l;
            int i2 = j + l;
            int j2 = k + l;
            var chunkcache = new ChunkCache(this, i1, j1, k1, l1, i2, j2);
            return (new Pathfinder(chunkcache)).createEntityPathTo(entity, entity1, f);
        }

        public virtual PathEntity getEntityPathToXYZ(Entity entity, int i, int j, int k, float f)
        {
            int l = MathHelper.floor_double(entity.posX);
            int i1 = MathHelper.floor_double(entity.posY);
            int j1 = MathHelper.floor_double(entity.posZ);
            var k1 = (int) (f + 8F);
            int l1 = l - k1;
            int i2 = i1 - k1;
            int j2 = j1 - k1;
            int k2 = l + k1;
            int l2 = i1 + k1;
            int i3 = j1 + k1;
            var chunkcache = new ChunkCache(this, l1, i2, j2, k2, l2, i3);
            return (new Pathfinder(chunkcache)).createEntityPathTo(entity, i, j, k, f);
        }

        public virtual bool isBlockProvidingPowerTo(int i, int j, int k, int l)
        {
            int i1 = getBlockId(i, j, k);
            if (i1 == 0)
            {
                return false;
            }
            else
            {
                return Block.blocksList[i1].isIndirectlyPoweringTo(this, i, j, k, l);
            }
        }

        public virtual bool isBlockGettingPowered(int i, int j, int k)
        {
            if (isBlockProvidingPowerTo(i, j - 1, k, 0))
            {
                return true;
            }
            if (isBlockProvidingPowerTo(i, j + 1, k, 1))
            {
                return true;
            }
            if (isBlockProvidingPowerTo(i, j, k - 1, 2))
            {
                return true;
            }
            if (isBlockProvidingPowerTo(i, j, k + 1, 3))
            {
                return true;
            }
            if (isBlockProvidingPowerTo(i - 1, j, k, 4))
            {
                return true;
            }
            return isBlockProvidingPowerTo(i + 1, j, k, 5);
        }

        public virtual bool isBlockIndirectlyProvidingPowerTo(int i, int j, int k, int l)
        {
            if (isBlockOpaqueCube(i, j, k))
            {
                return isBlockGettingPowered(i, j, k);
            }
            int i1 = getBlockId(i, j, k);
            if (i1 == 0)
            {
                return false;
            }
            else
            {
                return Block.blocksList[i1].isPoweringTo(this, i, j, k, l);
            }
        }

        public virtual bool isBlockIndirectlyGettingPowered(int i, int j, int k)
        {
            if (isBlockIndirectlyProvidingPowerTo(i, j - 1, k, 0))
            {
                return true;
            }
            if (isBlockIndirectlyProvidingPowerTo(i, j + 1, k, 1))
            {
                return true;
            }
            if (isBlockIndirectlyProvidingPowerTo(i, j, k - 1, 2))
            {
                return true;
            }
            if (isBlockIndirectlyProvidingPowerTo(i, j, k + 1, 3))
            {
                return true;
            }
            if (isBlockIndirectlyProvidingPowerTo(i - 1, j, k, 4))
            {
                return true;
            }
            return isBlockIndirectlyProvidingPowerTo(i + 1, j, k, 5);
        }

        public virtual EntityPlayer getClosestPlayerToEntity(Entity entity, double d)
        {
            return getClosestPlayer(entity.posX, entity.posY, entity.posZ, d);
        }

        public virtual EntityPlayer getClosestPlayer(double d, double d1, double d2, double d3)
        {
            double d4 = -1D;
            EntityPlayer entityplayer = null;
            for (int i = 0; i < playerEntities.size(); i++)
            {
                var entityplayer1 = (EntityPlayer) playerEntities.get(i);
                double d5 = entityplayer1.getDistanceSq(d, d1, d2);
                if ((d3 < 0.0D || d5 < d3*d3) && (d4 == -1D || d5 < d4))
                {
                    d4 = d5;
                    entityplayer = entityplayer1;
                }
            }

            return entityplayer;
        }

        public virtual byte[] getChunkData(int i, int j, int k, int l, int i1, int j1)
        {
            var abyte0 = new byte[(l*i1*j1*5)/2];
            int k1 = i >> 4;
            int l1 = k >> 4;
            int i2 = (i + l) - 1 >> 4;
            int j2 = (k + j1) - 1 >> 4;
            int k2 = 0;
            int l2 = j;
            int i3 = j + i1;
            if (l2 < 0)
            {
                l2 = 0;
            }
            if (i3 > 128)
            {
                i3 = 128;
            }
            for (int j3 = k1; j3 <= i2; j3++)
            {
                int k3 = i - j3*16;
                int l3 = (i + l) - j3*16;
                if (k3 < 0)
                {
                    k3 = 0;
                }
                if (l3 > 16)
                {
                    l3 = 16;
                }
                for (int i4 = l1; i4 <= j2; i4++)
                {
                    int j4 = k - i4*16;
                    int k4 = (k + j1) - i4*16;
                    if (j4 < 0)
                    {
                        j4 = 0;
                    }
                    if (k4 > 16)
                    {
                        k4 = 16;
                    }
                    k2 = getChunkFromChunkCoords(j3, i4).getChunkData(abyte0, k3, l2, j4, l3, i3, k4, k2);
                }
            }

            return abyte0;
        }

        public virtual void checkSessionLock()
        {
            worldFile.func_22091_b();
        }

        public virtual void func_22076_a(long l)
        {
            worldInfo.setWorldTime(l);
        }

        public virtual long func_22079_j()
        {
            return worldInfo.func_22187_b();
        }

        public virtual long getWorldTime()
        {
            return worldInfo.getWorldTime();
        }

        public virtual ChunkCoordinates func_22078_l()
        {
            return new ChunkCoordinates(worldInfo.getSpawnX(), worldInfo.getSpawnY(), worldInfo.getSpawnZ());
        }

        public virtual bool canMineBlock(EntityPlayer entityplayer, int i, int j, int k)
        {
            return true;
        }

        public virtual void func_9206_a(Entity entity, byte byte0)
        {
        }

        public virtual void playNoteAt(int i, int j, int k, int l, int i1)
        {
            int j1 = getBlockId(i, j, k);
            if (j1 > 0)
            {
                Block.blocksList[j1].playBlock(this, i, j, k, l, i1);
            }
        }

        public virtual ISaveHandler func_22075_m()
        {
            return worldFile;
        }

        public virtual WorldInfo getWorldInfo()
        {
            return worldInfo;
        }

        public virtual void updateAllPlayersSleepingFlag()
        {
            allPlayersSleeping = !playerEntities.isEmpty();
            Iterator iterator = playerEntities.iterator();
            do
            {
                if (!iterator.hasNext())
                {
                    break;
                }
                var entityplayer = (EntityPlayer) iterator.next();
                if (entityplayer.isPlayerSleeping())
                {
                    continue;
                }
                allPlayersSleeping = false;
                break;
            } while (true);
        }

        public virtual void wakeUpAllPlayers()
        {
            allPlayersSleeping = false;
            Iterator iterator = playerEntities.iterator();
            do
            {
                if (!iterator.hasNext())
                {
                    break;
                }
                var entityplayer = (EntityPlayer) iterator.next();
                if (entityplayer.isPlayerSleeping())
                {
                    entityplayer.wakeUpPlayer(false, false);
                }
            } while (true);
        }

        public virtual bool isAllPlayersFullyAsleep()
        {
            if (allPlayersSleeping && !singleplayerWorld)
            {
                for (Iterator iterator = playerEntities.iterator(); iterator.hasNext();)
                {
                    var entityplayer = (EntityPlayer) iterator.next();
                    if (!entityplayer.isPlayerFullyAsleep())
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}