namespace CraftyServer.Core
{
    public class ItemSaddle : Item
    {
        public ItemSaddle(int i)
            : base(i)
        {
            maxStackSize = 1;
            maxDamage = 64;
        }

        public override void saddleEntity(ItemStack itemstack, EntityLiving entityliving)
        {
            if (entityliving is EntityPig)
            {
                var entitypig = (EntityPig) entityliving;
                if (!entitypig.func_21065_K())
                {
                    entitypig.func_21064_a(true);
                    itemstack.stackSize--;
                }
            }
        }

        public override void hitEntity(ItemStack itemstack, EntityLiving entityliving)
        {
            saddleEntity(itemstack, entityliving);
        }
    }
}