namespace CraftyServer.Core
{
    public class ItemSword : Item
    {
        public ItemSword(int i, EnumToolMaterial enumtoolmaterial)
            : base(i)
        {
            maxStackSize = 1;
            maxDamage = enumtoolmaterial.getMaxUses();
            weaponDamage = 4 + enumtoolmaterial.getDamageVsEntity()*2;
        }

        public override float getStrVsBlock(ItemStack itemstack, Block block)
        {
            return 1.5F;
        }

        public override void hitEntity(ItemStack itemstack, EntityLiving entityliving)
        {
            itemstack.damageItem(1);
        }

        public override void hitBlock(ItemStack itemstack, int i, int j, int k, int l)
        {
            itemstack.damageItem(2);
        }

        public override int getDamageVsEntity(Entity entity)
        {
            return weaponDamage;
        }

        private int weaponDamage;
    }
}