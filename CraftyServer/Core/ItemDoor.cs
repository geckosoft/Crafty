namespace CraftyServer.Core
{
    public class ItemDoor : Item
    {
        private readonly Material field_260_a;

        public ItemDoor(int i, Material material) : base(i)
        {
            field_260_a = material;
            maxDamage = 64;
            maxStackSize = 1;
        }

        public override bool onItemUse(ItemStack itemstack, EntityPlayer entityplayer, World world, int i, int j, int k,
                                       int l)
        {
            if (l != 1)
            {
                return false;
            }
            j++;
            Block block;
            if (field_260_a == Material.wood)
            {
                block = Block.doorWood;
            }
            else
            {
                block = Block.doorSteel;
            }
            if (!block.canPlaceBlockAt(world, i, j, k))
            {
                return false;
            }
            int i1 = MathHelper.floor_double((((entityplayer.rotationYaw + 180F)*4F)/360F) - 0.5D) & 3;
            sbyte byte0 = 0;
            sbyte byte1 = 0;
            if (i1 == 0)
            {
                byte1 = 1;
            }
            if (i1 == 1)
            {
                byte0 = -1;
            }
            if (i1 == 2)
            {
                byte1 = -1;
            }
            if (i1 == 3)
            {
                byte0 = 1;
            }
            int j1 = (world.isBlockOpaqueCube(i - byte0, j, k - byte1) ? 1 : 0) +
                     (world.isBlockOpaqueCube(i - byte0, j + 1, k - byte1) ? 1 : 0);
            int k1 = (world.isBlockOpaqueCube(i + byte0, j, k + byte1) ? 1 : 0) +
                     (world.isBlockOpaqueCube(i + byte0, j + 1, k + byte1) ? 1 : 0);
            bool flag = world.getBlockId(i - byte0, j, k - byte1) == block.blockID ||
                        world.getBlockId(i - byte0, j + 1, k - byte1) == block.blockID;
            bool flag1 = world.getBlockId(i + byte0, j, k + byte1) == block.blockID ||
                         world.getBlockId(i + byte0, j + 1, k + byte1) == block.blockID;
            bool flag2 = false;
            if (flag && !flag1)
            {
                flag2 = true;
            }
            else if (k1 > j1)
            {
                flag2 = true;
            }
            if (flag2)
            {
                i1 = i1 - 1 & 3;
                i1 += 4;
            }
            world.setBlockWithNotify(i, j, k, block.blockID);
            world.setBlockMetadataWithNotify(i, j, k, i1);
            world.setBlockWithNotify(i, j + 1, k, block.blockID);
            world.setBlockMetadataWithNotify(i, j + 1, k, i1 + 8);
            itemstack.stackSize--;
            return true;
        }
    }
}