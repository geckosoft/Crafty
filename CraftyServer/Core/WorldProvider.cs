using java.lang;

namespace CraftyServer.Core
{
    public class WorldProvider
    {
        public bool field_4306_c;
        private float[] field_6164_h;
        public bool field_6167_c;
        public bool isHellWorld;
        public float[] lightBrightnessTable;
        public WorldChunkManager worldChunkMgr;
        public World worldObj;
        public int worldType;

        public WorldProvider()
        {
            field_6167_c = false;
            isHellWorld = false;
            field_4306_c = false;
            lightBrightnessTable = new float[16];
            worldType = 0;
            field_6164_h = new float[4];
        }

        public void registerWorld(World world)
        {
            worldObj = world;
            registerWorldChunkManager();
            generateLightBrightnessTable();
        }

        public virtual void generateLightBrightnessTable()
        {
            float f = 0.05F;
            for (int i = 0; i <= 15; i++)
            {
                float f1 = 1.0F - i/15F;
                lightBrightnessTable[i] = ((1.0F - f1)/(f1*3F + 1.0F))*(1.0F - f) + f;
            }
        }

        public virtual void registerWorldChunkManager()
        {
            worldChunkMgr = new WorldChunkManager(worldObj);
        }

        public virtual IChunkProvider getChunkProvider()
        {
            return new ChunkProviderGenerate(worldObj, worldObj.func_22079_j());
        }

        public virtual bool canCoordinateBeSpawn(int i, int j)
        {
            int k = worldObj.getFirstUncoveredBlock(i, j);
            return k == Block.sand.blockID;
        }

        public virtual float calculateCelestialAngle(long l, float f)
        {
            var i = (int) (l%24000L);
            float f1 = (i + f)/24000F - 0.25F;
            if (f1 < 0.0F)
            {
                f1++;
            }
            if (f1 > 1.0F)
            {
                f1--;
            }
            float f2 = f1;
            f1 = 1.0F - (float) ((Math.cos(f1*3.1415926535897931D) + 1.0D)/2D);
            f1 = f2 + (f1 - f2)/3F;
            return f1;
        }

        public static WorldProvider func_4091_a(int i)
        {
            if (i == 0)
            {
                return new WorldProvider();
            }
            if (i == -1)
            {
                return new WorldProviderHell();
            }
            else
            {
                return null;
            }
        }
    }
}