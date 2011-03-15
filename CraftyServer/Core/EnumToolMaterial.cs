namespace CraftyServer.Core
{
    public class EnumToolMaterial
    {
        public static EnumToolMaterial WOOD;
        public static EnumToolMaterial STONE;
        public static EnumToolMaterial IRON;
        public static EnumToolMaterial EMERALD;
        public static EnumToolMaterial GOLD;
        private static readonly EnumToolMaterial[] field_21182_j; /* synthetic field */
        private readonly int damageVsEntity;
        private readonly float efficiencyOnProperMaterial;
        private readonly int harvestLevel;
        private readonly int maxUses;

        static EnumToolMaterial()
        {
            WOOD = new EnumToolMaterial("WOOD", 0, 0, 59, 2.0F, 0);
            STONE = new EnumToolMaterial("STONE", 1, 1, 131, 4F, 1);
            IRON = new EnumToolMaterial("IRON", 2, 2, 250, 6F, 2);
            EMERALD = new EnumToolMaterial("EMERALD", 3, 3, 1561, 8F, 3);
            GOLD = new EnumToolMaterial("GOLD", 4, 0, 32, 12F, 0);
            field_21182_j = (new[]
                             {
                                 WOOD, STONE, IRON, EMERALD, GOLD
                             });
        }

        private EnumToolMaterial(string s, int i, int j, int k, float f, int l)
        {
            //base(s, i);
            harvestLevel = j;
            maxUses = k;
            efficiencyOnProperMaterial = f;
            damageVsEntity = l;
        }

        public static EnumToolMaterial[] values()
        {
            return (EnumToolMaterial[]) field_21182_j.Clone();
        }

        public static EnumToolMaterial valueOf(string s)
        {
            return null; // return (EnumToolMaterial)Enum.valueOf(typeof(EnumToolMaterial), s);
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
    }
}