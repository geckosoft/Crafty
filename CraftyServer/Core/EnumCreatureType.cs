using java.lang;

namespace CraftyServer.Core
{
    public class EnumCreatureType
    {
        public static EnumCreatureType[] values()
        {
            return (EnumCreatureType[]) field_6155_e.Clone();
        }

        public static EnumCreatureType valueOf(string s)
        {
            return null; // return (EnumCreatureType)Enum.valueOf(typeof(EnumCreatureType), s);
        }

        private EnumCreatureType(string s, int i, Class class1, int j, Material material, bool flag)
        {
            creatureClass = class1;
            maxNumberOfCreature = j;
            creatureMaterial = material;
            field_21106_g = flag;
        }

        public Class getCreatureClass()
        {
            return creatureClass;
        }

        public int getMaxNumberOfCreature()
        {
            return maxNumberOfCreature;
        }

        public Material getCreatureMaterial()
        {
            return creatureMaterial;
        }

        public bool func_21103_d()
        {
            return field_21106_g;
        }

        public static EnumCreatureType monster;
        public static EnumCreatureType creature;
        public static EnumCreatureType waterCreature;
        private Class creatureClass;
        private int maxNumberOfCreature;
        private Material creatureMaterial;
        private bool field_21106_g;
        private static EnumCreatureType[] field_6155_e; /* synthetic field */

        static EnumCreatureType()
        {
            monster = new EnumCreatureType("monster", 0, typeof (IMobs), 70, Material.air, false);
            creature = new EnumCreatureType("creature", 1, typeof (EntityAnimals), 15, Material.air, true);
            waterCreature = new EnumCreatureType("waterCreature", 2, typeof (EntityWaterMob), 5, Material.water, true);
            field_6155_e = (new EnumCreatureType[]
                            {
                                monster, creature, waterCreature
                            });
        }
    }
}