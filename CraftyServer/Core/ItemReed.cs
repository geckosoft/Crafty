namespace CraftyServer.Core
{
    public class ItemReed : Item
    {
        private readonly int field_253_a;

        public ItemReed(int i, Block block)
            : base(i)
        {
            field_253_a = block.blockID;
        }

        public override bool onItemUse(ItemStack itemstack, EntityPlayer entityplayer, World world, int i, int j, int k,
                                       int l)
        {
            if (world.getBlockId(i, j, k) == Block.snow.blockID)
            {
                l = 0;
            }
            else
            {
                if (l == 0)
                {
                    j--;
                }
                if (l == 1)
                {
                    j++;
                }
                if (l == 2)
                {
                    k--;
                }
                if (l == 3)
                {
                    k++;
                }
                if (l == 4)
                {
                    i--;
                }
                if (l == 5)
                {
                    i++;
                }
            }
            if (itemstack.stackSize == 0)
            {
                return false;
            }
            if (world.canBlockBePlacedAt(field_253_a, i, j, k, false))
            {
                Block block = Block.blocksList[field_253_a];
                if (world.setBlockWithNotify(i, j, k, field_253_a))
                {
                    Block.blocksList[field_253_a].onBlockPlaced(world, i, j, k, l);
                    Block.blocksList[field_253_a].onBlockPlacedBy(world, i, j, k, entityplayer);
                    world.playSoundEffect(i + 0.5F, j + 0.5F, k + 0.5F,
                                          block.stepSound.func_737_c(), (block.stepSound.func_738_a() + 1.0F)/2.0F,
                                          block.stepSound.func_739_b()*0.8F);
                    itemstack.stackSize--;
                }
            }
            return true;
        }
    }
}