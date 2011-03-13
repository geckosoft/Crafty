namespace CraftyServer.Core
{
    public class ItemInWorldManager
    {
        public ItemInWorldManager(World world)
        {
            field_672_d = 0.0F;
            field_671_e = 0;
            field_670_f = 0.0F;
            thisWorld = world;
        }

        public void func_328_a()
        {
            field_22051_j++;
            if (field_22050_k)
            {
                int i = field_22051_j - field_22046_o;
                int j = thisWorld.getBlockId(field_22049_l, field_22048_m, field_22047_n);
                if (j != 0)
                {
                    Block block = Block.blocksList[j];
                    float f = block.blockStrength(thisPlayer)*(float) (i + 1);
                    if (f >= 1.0F)
                    {
                        field_22050_k = false;
                        func_325_c(field_22049_l, field_22048_m, field_22047_n);
                    }
                }
                else
                {
                    field_22050_k = false;
                }
            }
        }

        public void func_324_a(int i, int j, int k)
        {
            field_22055_d = field_22051_j;
            int l = thisWorld.getBlockId(i, j, k);
            if (l > 0)
            {
                Block.blocksList[l].onBlockClicked(thisWorld, i, j, k, thisPlayer);
            }
            if (l > 0 && Block.blocksList[l].blockStrength(thisPlayer) >= 1.0F)
            {
                func_325_c(i, j, k);
            }
            else
            {
                field_22054_g = i;
                field_22053_h = j;
                field_22052_i = k;
            }
        }

        public void func_22045_b(int i, int j, int k)
        {
            if (i == field_22054_g && j == field_22053_h && k == field_22052_i)
            {
                int l = field_22051_j - field_22055_d;
                int i1 = thisWorld.getBlockId(i, j, k);
                if (i1 != 0)
                {
                    Block block = Block.blocksList[i1];
                    float f = block.blockStrength(thisPlayer)*(float) (l + 1);
                    if (f >= 1.0F)
                    {
                        func_325_c(i, j, k);
                    }
                    else if (!field_22050_k)
                    {
                        field_22050_k = true;
                        field_22049_l = i;
                        field_22048_m = j;
                        field_22047_n = k;
                        field_22046_o = field_22055_d;
                    }
                }
            }
            field_672_d = 0.0F;
            field_671_e = 0;
        }

        public bool removeBlock(int i, int j, int k)
        {
            Block block = Block.blocksList[thisWorld.getBlockId(i, j, k)];
            int l = thisWorld.getBlockMetadata(i, j, k);
            bool flag = thisWorld.setBlockWithNotify(i, j, k, 0);
            if (block != null && flag)
            {
                block.onBlockDestroyedByPlayer(thisWorld, i, j, k, l);
            }
            return flag;
        }

        public bool func_325_c(int i, int j, int k)
        {
            int l = thisWorld.getBlockId(i, j, k);
            int i1 = thisWorld.getBlockMetadata(i, j, k);
            bool flag = removeBlock(i, j, k);
            ItemStack itemstack = thisPlayer.getCurrentEquippedItem();
            if (itemstack != null)
            {
                itemstack.hitBlock(l, i, j, k);
                if (itemstack.stackSize == 0)
                {
                    itemstack.func_577_a(thisPlayer);
                    thisPlayer.destroyCurrentEquippedItem();
                }
            }
            if (flag && thisPlayer.canHarvestBlock(Block.blocksList[l]))
            {
                Block.blocksList[l].harvestBlock(thisWorld, i, j, k, i1);
                ((EntityPlayerMP) thisPlayer).playerNetServerHandler.sendPacket(new Packet53BlockChange(i, j, k,
                                                                                                        thisWorld));
            }
            return flag;
        }

        public bool func_6154_a(EntityPlayer entityplayer, World world, ItemStack itemstack)
        {
            int i = itemstack.stackSize;
            ItemStack itemstack1 = itemstack.useItemRightClick(world, entityplayer);
            if (itemstack1 != itemstack || itemstack1 != null && itemstack1.stackSize != i)
            {
                entityplayer.inventory.mainInventory[entityplayer.inventory.currentItem] = itemstack1;
                if (itemstack1.stackSize == 0)
                {
                    entityplayer.inventory.mainInventory[entityplayer.inventory.currentItem] = null;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool activeBlockOrUseItem(EntityPlayer entityplayer, World world, ItemStack itemstack, int i, int j,
                                         int k, int l)
        {
            int i1 = world.getBlockId(i, j, k);
            if (i1 > 0 && Block.blocksList[i1].blockActivated(world, i, j, k, entityplayer))
            {
                return true;
            }
            if (itemstack == null)
            {
                return false;
            }
            else
            {
                return itemstack.useItem(entityplayer, world, i, j, k, l);
            }
        }

        private World thisWorld;
        public EntityPlayer thisPlayer;
        private float field_672_d;
        private int field_22055_d;
        private int field_671_e;
        private float field_670_f;
        private int field_22054_g;
        private int field_22053_h;
        private int field_22052_i;
        private int field_22051_j;
        private bool field_22050_k;
        private int field_22049_l;
        private int field_22048_m;
        private int field_22047_n;
        private int field_22046_o;
    }
}