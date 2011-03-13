using java.util;
using java.lang;


namespace CraftyServer.Core
{
    public class MobSpawnerBase
    {
        public MobSpawnerBase()
        {
            topBlock = (byte) Block.grass.blockID;
            fillerBlock = (byte) Block.dirt.blockID;
            field_6161_q = 0x4ee031;
            biomeMonsters =
                (new Class[]
                 {
                     typeof (EntitySpider), typeof (EntityZombie), typeof (EntitySkeleton), typeof (EntityCreeper),
                     typeof (EntitySlime)
                 });
            biomeCreatures =
                (new Class[] {typeof (EntitySheep), typeof (EntityPig), typeof (EntityChicken), typeof (EntityCow)});
            biomeWaterCreatures = (new Class[] {typeof (EntitySquid)});
        }

        public static void generateBiomeLookup()
        {
            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < 64; j++)
                {
                    biomeLookupTable[i + j*64] = getBiome((float) i/63F, (float) j/63F);
                }
            }

            desert.topBlock = desert.fillerBlock = (byte) Block.sand.blockID;
            iceDesert.topBlock = iceDesert.fillerBlock = (byte) Block.sand.blockID;
        }

        public virtual WorldGenerator getRandomWorldGenForTrees(Random random)
        {
            if (random.nextInt(10) == 0)
            {
                return new WorldGenBigTree();
            }
            else
            {
                return new WorldGenTrees();
            }
        }

        protected virtual MobSpawnerBase doesNothingForMobSpawnerBase()
        {
            return this;
        }

        protected virtual MobSpawnerBase setBiomeName(string s)
        {
            biomeName = s;
            return this;
        }

        protected virtual MobSpawnerBase func_4080_a(int i)
        {
            field_6161_q = i;
            return this;
        }

        protected virtual MobSpawnerBase setColor(int i)
        {
            color = i;
            return this;
        }

        public static MobSpawnerBase getBiomeFromLookup(double d, double d1)
        {
            int i = (int) (d*63D);
            int j = (int) (d1*63D);
            return biomeLookupTable[i + j*64];
        }

        public static MobSpawnerBase getBiome(float f, float f1)
        {
            f1 *= f;
            if (f < 0.1F)
            {
                return tundra;
            }
            if (f1 < 0.2F)
            {
                if (f < 0.5F)
                {
                    return tundra;
                }
                if (f < 0.95F)
                {
                    return savanna;
                }
                else
                {
                    return desert;
                }
            }
            if (f1 > 0.5F && f < 0.7F)
            {
                return swampland;
            }
            if (f < 0.5F)
            {
                return taiga;
            }
            if (f < 0.97F)
            {
                if (f1 < 0.35F)
                {
                    return shrubland;
                }
                else
                {
                    return forest;
                }
            }
            if (f1 < 0.45F)
            {
                return plains;
            }
            if (f1 < 0.9F)
            {
                return seasonalForest;
            }
            else
            {
                return rainforest;
            }
        }

        public Class[] getEntitiesForType(EnumCreatureType enumcreaturetype)
        {
            if (enumcreaturetype == EnumCreatureType.monster)
            {
                return biomeMonsters;
            }
            if (enumcreaturetype == EnumCreatureType.creature)
            {
                return biomeCreatures;
            }
            if (enumcreaturetype == EnumCreatureType.waterCreature)
            {
                return biomeWaterCreatures;
            }
            else
            {
                return null;
            }
        }

        public static MobSpawnerBase rainforest =
            (new MobSpawnerRainforest()).setColor(0x8fa36).setBiomeName("Rainforest").func_4080_a(0x1ff458);

        public static MobSpawnerBase swampland =
            (new MobSpawnerSwamp()).setColor(0x7f9b2).setBiomeName("Swampland").func_4080_a(0x8baf48);

        public static MobSpawnerBase seasonalForest =
            (new MobSpawnerBase()).setColor(0x9be023).setBiomeName("Seasonal Forest");

        public static MobSpawnerBase forest =
            (new MobSpawnerForest()).setColor(0x56621).setBiomeName("Forest").func_4080_a(0x4eba31);

        public static MobSpawnerBase savanna = (new MobSpawnerDesert()).setColor(0xd9e023).setBiomeName("Savanna");
        public static MobSpawnerBase shrubland = (new MobSpawnerBase()).setColor(0xa1ad20).setBiomeName("Shrubland");

        public static MobSpawnerBase taiga =
            (new MobSpawnerTaiga()).setColor(0x2eb153).setBiomeName("Taiga").doesNothingForMobSpawnerBase().func_4080_a(
                0x7bb731);

        public static MobSpawnerBase desert = (new MobSpawnerDesert()).setColor(0xfa9418).setBiomeName("Desert");
        public static MobSpawnerBase plains = (new MobSpawnerDesert()).setColor(0xffd910).setBiomeName("Plains");

        public static MobSpawnerBase iceDesert =
            (new MobSpawnerDesert()).setColor(0xffed93).setBiomeName("Ice Desert").doesNothingForMobSpawnerBase().
                func_4080_a(0xc4d339);

        public static MobSpawnerBase tundra =
            (new MobSpawnerBase()).setColor(0x57ebf9).setBiomeName("Tundra").doesNothingForMobSpawnerBase().func_4080_a(
                0xc4d339);

        public static MobSpawnerBase hell = (new MobSpawnerHell()).setColor(0xff0000).setBiomeName("Hell");
        public string biomeName;
        public int color;
        public byte topBlock;
        public byte fillerBlock;
        public int field_6161_q;
        protected Class[] biomeMonsters;
        protected Class[] biomeCreatures;
        protected Class[] biomeWaterCreatures;
        private static MobSpawnerBase[] biomeLookupTable = new MobSpawnerBase[4096];

        static MobSpawnerBase()
        {
            generateBiomeLookup();
        }
    }
}