using java.lang;
using java.util;

namespace CraftyServer.Core
{
    public class EmptyChunk : Chunk
    {
        public EmptyChunk(World world, int i, int j)
            : base(world, i, j)
        {
            neverSave = true;
        }

        public EmptyChunk(World world, byte[] abyte0, int i, int j)
            : base(world, abyte0, i, j)
        {
            neverSave = true;
        }

        public override bool isAtLocation(int i, int j)
        {
            return i == xPosition && j == zPosition;
        }

        public override int getHeightValue(int i, int j)
        {
            return 0;
        }

        public override void func_348_a()
        {
        }

        public override void func_353_b()
        {
        }

        public override void func_4053_c()
        {
        }

        public override int getBlockID(int i, int j, int k)
        {
            return 0;
        }

        public override bool setBlockIDWithMetadata(int i, int j, int k, int l, int i1)
        {
            return true;
        }

        public override bool setBlockID(int i, int j, int k, int l)
        {
            return true;
        }

        public override int getBlockMetadata(int i, int j, int k)
        {
            return 0;
        }

        public override void setBlockMetadata(int i, int j, int k, int l)
        {
        }

        public override int getSavedLightValue(EnumSkyBlock enumskyblock, int i, int j, int k)
        {
            return 0;
        }

        public override void setLightValue(EnumSkyBlock enumskyblock, int i, int j, int k, int l)
        {
        }

        public override int getBlockLightValue(int i, int j, int k, int l)
        {
            return 0;
        }

        public override void addEntity(Entity entity)
        {
        }

        public override void removeEntity(Entity entity)
        {
        }

        public override void removeEntityAtIndex(Entity entity, int i)
        {
        }

        public override bool canBlockSeeTheSky(int i, int j, int k)
        {
            return false;
        }

        public override TileEntity getChunkBlockTileEntity(int i, int j, int k)
        {
            return null;
        }

        public override void func_349_a(TileEntity tileentity)
        {
        }

        public override void setChunkBlockTileEntity(int i, int j, int k, TileEntity tileentity)
        {
        }

        public override void removeChunkBlockTileEntity(int i, int j, int k)
        {
        }

        public override void onChunkLoad()
        {
        }

        public override void onChunkUnload()
        {
        }

        public override void setChunkModified()
        {
        }

        public override void getEntitiesWithinAABBForEntity(Entity entity, AxisAlignedBB axisalignedbb, List list)
        {
        }

        public override void getEntitiesOfTypeWithinAAAB(Class class1, AxisAlignedBB axisalignedbb, List list)
        {
        }

        public override bool needsSaving(bool flag)
        {
            return false;
        }

        public override int getChunkData(byte[] abyte0, int i, int j, int k, int l, int i1, int j1,
                                         int k1)
        {
            int l1 = l - i;
            int i2 = i1 - j;
            int j2 = j1 - k;
            int k2 = l1*i2*j2;
            int l2 = k2 + (k2/2)*3;
            Arrays.fill(abyte0, k1, k1 + l2, 0);
            return l2;
        }

        public override Random func_334_a(long l)
        {
            return
                new Random(worldObj.func_22079_j() + (xPosition*xPosition*0x4c1906) + (xPosition*0x5ac0db) +
                           (zPosition*zPosition)*0x4307a7L + (zPosition*0x5f24f) ^ l);
        }

        public override bool func_21101_g()
        {
            return true;
        }
    }
}