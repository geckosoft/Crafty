namespace CraftyServer.Core
{
    public class ItemArmor : Item
    {
        private static readonly int[] damageReduceAmountArray = {
                                                                    3, 8, 6, 3
                                                                };

        private static readonly int[] maxDamageArray = {
                                                           11, 16, 15, 13
                                                       };

        public int armorLevel;
        public int armorType;
        public int damageReduceAmount;
        public int renderIndex;

        public ItemArmor(int i, int j, int k, int l)
            : base(i)
        {
            armorLevel = j;
            armorType = l;
            renderIndex = k;
            damageReduceAmount = damageReduceAmountArray[l];
            maxDamage = maxDamageArray[l]*3 << j;
            maxStackSize = 1;
        }
    }
}