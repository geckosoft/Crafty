using java.util;

namespace CraftyServer.Core
{
    public class WorldChunkManagerHell : WorldChunkManager
    {
        private readonly double field_4260_g;
        private readonly double field_4261_f;
        private readonly MobSpawnerBase field_4262_e;

        public WorldChunkManagerHell(MobSpawnerBase mobspawnerbase, double d, double d1)
        {
            field_4262_e = mobspawnerbase;
            field_4261_f = d;
            field_4260_g = d1;
        }

        public override MobSpawnerBase func_4066_a(ChunkCoordIntPair chunkcoordintpair)
        {
            return field_4262_e;
        }

        public override MobSpawnerBase func_4067_a(int i, int j)
        {
            return field_4262_e;
        }

        public override MobSpawnerBase[] func_4065_a(int i, int j, int k, int l)
        {
            field_4256_d = loadBlockGeneratorData(field_4256_d, i, j, k, l);
            return field_4256_d;
        }

        public override double[] getTemperatures(double[] ad, int i, int j, int k, int l)
        {
            if (ad == null || ad.Length < k*l)
            {
                ad = new double[k*l];
            }
            Arrays.fill(ad, 0, k*l, field_4261_f);
            return ad;
        }

        public override MobSpawnerBase[] loadBlockGeneratorData(MobSpawnerBase[] amobspawnerbase, int i, int j, int k,
                                                                int l)
        {
            if (amobspawnerbase == null || amobspawnerbase.Length < k*l)
            {
                amobspawnerbase = new MobSpawnerBase[k*l];
                temperature = new double[k*l];
                humidity = new double[k*l];
            }
            Arrays.fill(amobspawnerbase, 0, k*l, field_4262_e);
            Arrays.fill(humidity, 0, k*l, field_4260_g);
            Arrays.fill(temperature, 0, k*l, field_4261_f);
            return amobspawnerbase;
        }
    }
}