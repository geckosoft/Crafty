using java.util;


namespace CraftyServer.Core
{
    public class WorldChunkManager
    {
        protected WorldChunkManager()
        {
        }

        public WorldChunkManager(World world)
        {
            field_4255_e = new NoiseGeneratorOctaves2(new Random(world.func_22079_j()*9871L), 4);
            field_4254_f = new NoiseGeneratorOctaves2(new Random(world.func_22079_j()*39811L), 4);
            field_4253_g = new NoiseGeneratorOctaves2(new Random(world.func_22079_j()*0x84a59L), 2);
        }

        public virtual MobSpawnerBase func_4066_a(ChunkCoordIntPair chunkcoordintpair)
        {
            return func_4067_a(chunkcoordintpair.chunkXPos >> 4, chunkcoordintpair.chunkZPos >> 4);
        }

        public virtual MobSpawnerBase func_4067_a(int i, int j)
        {
            return func_4065_a(i, j, 1, 1)[0];
        }

        public virtual MobSpawnerBase[] func_4065_a(int i, int j, int k, int l)
        {
            field_4256_d = loadBlockGeneratorData(field_4256_d, i, j, k, l);
            return field_4256_d;
        }

        public virtual double[] getTemperatures(double[] ad, int i, int j, int k, int l)
        {
            if (ad == null || ad.Length < k*l)
            {
                ad = new double[k*l];
            }
            ad = field_4255_e.func_4101_a(ad, i, j, k, l, 0.02500000037252903D, 0.02500000037252903D, 0.25D);
            field_4257_c = field_4253_g.func_4101_a(field_4257_c, i, j, k, l, 0.25D, 0.25D, 0.58823529411764708D);
            int i1 = 0;
            for (int j1 = 0; j1 < k; j1++)
            {
                for (int k1 = 0; k1 < l; k1++)
                {
                    double d = field_4257_c[i1]*1.1000000000000001D + 0.5D;
                    double d1 = 0.01D;
                    double d2 = 1.0D - d1;
                    double d3 = (ad[i1]*0.14999999999999999D + 0.69999999999999996D)*d2 + d*d1;
                    d3 = 1.0D - (1.0D - d3)*(1.0D - d3);
                    if (d3 < 0.0D)
                    {
                        d3 = 0.0D;
                    }
                    if (d3 > 1.0D)
                    {
                        d3 = 1.0D;
                    }
                    ad[i1] = d3;
                    i1++;
                }
            }

            return ad;
        }

        public virtual MobSpawnerBase[] loadBlockGeneratorData(MobSpawnerBase[] amobspawnerbase, int i, int j, int k,
                                                               int l)
        {
            if (amobspawnerbase == null || amobspawnerbase.Length < k*l)
            {
                amobspawnerbase = new MobSpawnerBase[k*l];
            }
            temperature = field_4255_e.func_4101_a(temperature, i, j, k, k, 0.02500000037252903D, 0.02500000037252903D,
                                                   0.25D);
            humidity = field_4254_f.func_4101_a(humidity, i, j, k, k, 0.05000000074505806D, 0.05000000074505806D,
                                                0.33333333333333331D);
            field_4257_c = field_4253_g.func_4101_a(field_4257_c, i, j, k, k, 0.25D, 0.25D, 0.58823529411764708D);
            int i1 = 0;
            for (int j1 = 0; j1 < k; j1++)
            {
                for (int k1 = 0; k1 < l; k1++)
                {
                    double d = field_4257_c[i1]*1.1000000000000001D + 0.5D;
                    double d1 = 0.01D;
                    double d2 = 1.0D - d1;
                    double d3 = (temperature[i1]*0.14999999999999999D + 0.69999999999999996D)*d2 + d*d1;
                    d1 = 0.002D;
                    d2 = 1.0D - d1;
                    double d4 = (humidity[i1]*0.14999999999999999D + 0.5D)*d2 + d*d1;
                    d3 = 1.0D - (1.0D - d3)*(1.0D - d3);
                    if (d3 < 0.0D)
                    {
                        d3 = 0.0D;
                    }
                    if (d4 < 0.0D)
                    {
                        d4 = 0.0D;
                    }
                    if (d3 > 1.0D)
                    {
                        d3 = 1.0D;
                    }
                    if (d4 > 1.0D)
                    {
                        d4 = 1.0D;
                    }
                    temperature[i1] = d3;
                    humidity[i1] = d4;
                    amobspawnerbase[i1++] = MobSpawnerBase.getBiomeFromLookup(d3, d4);
                }
            }

            return amobspawnerbase;
        }

        private NoiseGeneratorOctaves2 field_4255_e;
        private NoiseGeneratorOctaves2 field_4254_f;
        private NoiseGeneratorOctaves2 field_4253_g;
        public double[] temperature;
        public double[] humidity;
        public double[] field_4257_c;
        public MobSpawnerBase[] field_4256_d;
    }
}