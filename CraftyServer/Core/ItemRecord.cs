namespace CraftyServer.Core
{
    public class ItemRecord : Item
    {
        private readonly string recordName;

        protected internal ItemRecord(int i, string s) : base(i)
        {
            recordName = s;
            maxStackSize = 1;
        }

        public override bool onItemUse(ItemStack itemstack, EntityPlayer entityplayer, World world, int i, int j, int k,
                                       int l)
        {
            if (world.getBlockId(i, j, k) == Block.jukebox.blockID && world.getBlockMetadata(i, j, k) == 0)
            {
                world.setBlockMetadataWithNotify(i, j, k, (shiftedIndex - record13.shiftedIndex) + 1);
                world.playRecord(recordName, i, j, k);
                itemstack.stackSize--;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}