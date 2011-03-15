namespace CraftyServer.Core
{
    public class ItemBlock : Item
    {
        private readonly int blockID;

        public ItemBlock(int i) : base(i)
        {
            blockID = i + 256;
            setIconIndex(Block.blocksList[i + 256].getBlockTextureFromSide(2));
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
            if (world.canBlockBePlacedAt(blockID, i, j, k, false))
            {
                Block block = Block.blocksList[blockID];
                if (world.setBlockAndMetadataWithNotify(i, j, k, blockID, getMetadata(itemstack.getItemDamage())))
                {
                    Block.blocksList[blockID].onBlockPlaced(world, i, j, k, l);
                    Block.blocksList[blockID].onBlockPlacedBy(world, i, j, k, entityplayer);
                    world.playSoundEffect(i + 0.5F, j + 0.5F, k + 0.5F,
                                          block.stepSound.func_737_c(), (block.stepSound.func_738_a() + 1.0F)/2.0F,
                                          block.stepSound.func_739_b()*0.8F);
                    itemstack.stackSize--;
                }
            }
            return true;
        }

        public override string getItemName()
        {
            return Block.blocksList[blockID].getBlockName();
        }
    }
}