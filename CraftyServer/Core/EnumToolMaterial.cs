namespace CraftyServer.Core
{
    public class EnumToolMaterial
    {
        public static EnumToolMaterial[] values()
        {
            return (EnumToolMaterial[]) field_21182_j.Clone();
        }

        public static EnumToolMaterial valueOf(string s)
        {
            return null; // return (EnumToolMaterial)Enum.valueOf(typeof(EnumToolMaterial), s);
        }

        private EnumToolMaterial(string s, int i, int j, int k, float f, int l)
        {
            //base(s, i);
            harvestLevel = j;
            maxUses = k;
            efficiencyOnProperMaterial = f;
            damageVsEntity = l;
        }

        public int getMaxUses()
        {
            return maxUses;
        }

        public float getEfficiencyOnProperMaterial()
        {
            return efficiencyOnProperMaterial;
        }

        public int getDamageVsEntity()
        {
            return damageVsEntity;
        }

        public int getHarvestLevel()
        {
            return harvestLevel;
        }

        public static EnumToolMaterial WOOD;
        public static EnumToolMaterial STONE;
        public static EnumToolMaterial IRON;
        public static EnumToolMaterial EMERALD;
        public static EnumToolMaterial GOLD;
        private int harvestLevel;
        private int maxUses;
        private float efficiencyOnProperMaterial;
        private int damageVsEntity;
        private static EnumToolMaterial[] field_21182_j; /* synthetic field */

        static EnumToolMaterial()
        {
            WOOD = new EnumToolMaterial("WOOD", 0, 0, 59, 2.0F, 0);
            STONE = new EnumToolMaterial("STONE", 1, 1, 131, 4F, 1);
            IRON = new EnumToolMaterial("IRON", 2, 2, 250, 6F, 2);
            EMERALD = new EnumToolMaterial("EMERALD", 3, 3, 1561, 8F, 3);
            GOLD = new EnumToolMaterial("GOLD", 4, 0, 32, 12F, 0);
            field_21182_j = (new EnumToolMaterial[]
                             {
                                 WOOD, STONE, IRON, EMERALD, GOLD
                             });
        }
    }
}