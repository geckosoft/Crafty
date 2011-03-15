namespace CraftyServer.Core
{
    public class ItemPainting : Item
    {
        public ItemPainting(int i)
            : base(i)
        {
            maxDamage = 64;
        }

        public override bool onItemUse(ItemStack itemstack, EntityPlayer entityplayer, World world, int i, int j, int k,
                                       int l)
        {
            if (l == 0)
            {
                return false;
            }
            if (l == 1)
            {
                return false;
            }
            byte byte0 = 0;
            if (l == 4)
            {
                byte0 = 1;
            }
            if (l == 3)
            {
                byte0 = 2;
            }
            if (l == 5)
            {
                byte0 = 3;
            }
            var entitypainting = new EntityPainting(world, i, j, k, byte0);
            if (entitypainting.onValidSurface())
            {
                if (!world.singleplayerWorld)
                {
                    world.entityJoinedWorld(entitypainting);
                }
                itemstack.stackSize--;
            }
            return true;
        }
    }
}