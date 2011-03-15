namespace CraftyServer.Core
{
    public class ItemTool : Item
    {
        private readonly Block[] blocksEffectiveAgainst;
        private readonly int damageVsEntity;
        private readonly float efficiencyOnProperMaterial;
        protected EnumToolMaterial toolMaterial;

        protected ItemTool(int i, int j, EnumToolMaterial enumtoolmaterial, Block[] ablock) : base(i)
        {
            efficiencyOnProperMaterial = 4F;
            toolMaterial = enumtoolmaterial;
            blocksEffectiveAgainst = ablock;
            maxStackSize = 1;
            maxDamage = enumtoolmaterial.getMaxUses();
            efficiencyOnProperMaterial = enumtoolmaterial.getEfficiencyOnProperMaterial();
            damageVsEntity = j + enumtoolmaterial.getDamageVsEntity();
        }

        public override float getStrVsBlock(ItemStack itemstack, Block block)
        {
            for (int i = 0; i < blocksEffectiveAgainst.Length; i++)
            {
                if (blocksEffectiveAgainst[i] == block)
                {
                    return efficiencyOnProperMaterial;
                }
            }

            return 1.0F;
        }

        public override void hitEntity(ItemStack itemstack, EntityLiving entityliving)
        {
            itemstack.damageItem(2);
        }

        public override void hitBlock(ItemStack itemstack, int i, int j, int k, int l)
        {
            itemstack.damageItem(1);
        }

        public override int getDamageVsEntity(Entity entity)
        {
            return damageVsEntity;
        }
    }
}