namespace CraftyServer.Core
{
    public class ItemDye : Item
    {
        public ItemDye(int i) : base(i)
        {
            setHasSubtypes(true);
            setMaxDamage(0);
        }

        public override bool onItemUse(ItemStack itemstack, EntityPlayer entityplayer, World world, int i, int j, int k,
                                       int l)
        {
            if (itemstack.getItemDamage() == 15)
            {
                int i1 = world.getBlockId(i, j, k);
                if (i1 == Block.sapling.blockID)
                {
                    ((BlockSapling) Block.sapling).func_21027_b(world, i, j, k, world.rand);
                    itemstack.stackSize--;
                    return true;
                }
                if (i1 == Block.crops.blockID)
                {
                    ((BlockCrops) Block.crops).func_21028_c(world, i, j, k);
                    itemstack.stackSize--;
                    return true;
                }
            }
            return false;
        }

        public override void saddleEntity(ItemStack itemstack, EntityLiving entityliving)
        {
            if (entityliving is EntitySheep)
            {
                EntitySheep entitysheep = (EntitySheep) entityliving;
                int i = BlockCloth.func_21033_c(itemstack.getItemDamage());
                if (!entitysheep.func_21069_f_() && entitysheep.getFleeceColor() != i)
                {
                    entitysheep.setFleeceColor(i);
                    itemstack.stackSize--;
                }
            }
        }

        public static string[] dyeColors = {
                                               "black", "red", "green", "brown", "blue", "purple", "cyan", "silver",
                                               "gray", "pink",
                                               "lime", "yellow", "lightBlue", "magenta", "orange", "white"
                                           };
    }
}