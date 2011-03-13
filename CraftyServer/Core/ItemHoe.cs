namespace CraftyServer.Core
{
    public class ItemHoe : Item
    {
        public ItemHoe(int i, EnumToolMaterial enumtoolmaterial) : base(i)
        {
            maxStackSize = 1;
            maxDamage = enumtoolmaterial.getMaxUses();
        }

        public override bool onItemUse(ItemStack itemstack, EntityPlayer entityplayer, World world, int i, int j, int k,
                                       int l)
        {
            int i1 = world.getBlockId(i, j, k);
            Material material = world.getBlockMaterial(i, j + 1, k);
            if (!material.isSolid() && i1 == Block.grass.blockID || i1 == Block.dirt.blockID)
            {
                Block block = Block.tilledField;
                world.playSoundEffect((float) i + 0.5F, (float) j + 0.5F, (float) k + 0.5F, block.stepSound.func_737_c(),
                                      (block.stepSound.func_738_a() + 1.0F)/2.0F, block.stepSound.func_739_b()*0.8F);
                if (world.singleplayerWorld)
                {
                    return true;
                }
                world.setBlockWithNotify(i, j, k, block.blockID);
                itemstack.damageItem(1);
                if (world.rand.nextInt(8) == 0 && i1 == Block.grass.blockID)
                {
                    int j1 = 1;
                    for (int k1 = 0; k1 < j1; k1++)
                    {
                        float f = 0.7F;
                        float f1 = world.rand.nextFloat()*f + (1.0F - f)*0.5F;
                        float f2 = 1.2F;
                        float f3 = world.rand.nextFloat()*f + (1.0F - f)*0.5F;
                        EntityItem entityitem = new EntityItem(world, (float) i + f1, (float) j + f2, (float) k + f3,
                                                               new ItemStack(Item.seeds));
                        entityitem.delayBeforeCanPickup = 10;
                        world.entityJoinedWorld(entityitem);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}