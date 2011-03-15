namespace CraftyServer.Core
{
    public class WorldProviderHell : WorldProvider
    {
        public override void registerWorldChunkManager()
        {
            worldChunkMgr = new WorldChunkManagerHell(MobSpawnerBase.hell, 1.0D, 0.0D);
            field_6167_c = true;
            isHellWorld = true;
            field_4306_c = true;
            worldType = -1;
        }

        public override void generateLightBrightnessTable()
        {
            float f = 0.1F;
            for (int i = 0; i <= 15; i++)
            {
                float f1 = 1.0F - i/15F;
                lightBrightnessTable[i] = ((1.0F - f1)/(f1*3F + 1.0F))*(1.0F - f) + f;
            }
        }

        public override IChunkProvider getChunkProvider()
        {
            return new ChunkProviderHell(worldObj, worldObj.func_22079_j());
        }

        public override bool canCoordinateBeSpawn(int i, int j)
        {
            int k = worldObj.getFirstUncoveredBlock(i, j);
            if (k == Block.bedrock.blockID)
            {
                return false;
            }
            if (k == 0)
            {
                return false;
            }
            return Block.opaqueCubeLookup[k];
        }

        public override float calculateCelestialAngle(long l, float f)
        {
            return 0.5F;
        }
    }
}