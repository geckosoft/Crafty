namespace CraftyServer.Core
{
    public class InventoryPlayer
        : IInventory
    {
        public InventoryPlayer(EntityPlayer entityplayer)
        {
            mainInventory = new ItemStack[36];
            armorInventory = new ItemStack[4];
            currentItem = 0;
            inventoryChanged = false;
            player = entityplayer;
        }

        public ItemStack getCurrentItem()
        {
            return mainInventory[currentItem];
        }

        private int getInventorySlotContainItem(int i)
        {
            for (int j = 0; j < mainInventory.Length; j++)
            {
                if (mainInventory[j] != null && mainInventory[j].itemID == i)
                {
                    return j;
                }
            }

            return -1;
        }

        private int func_21082_c(ItemStack itemstack)
        {
            for (int i = 0; i < mainInventory.Length; i++)
            {
                if (mainInventory[i] != null && mainInventory[i].itemID == itemstack.itemID &&
                    mainInventory[i].func_21132_c() && mainInventory[i].stackSize < mainInventory[i].getMaxStackSize() &&
                    mainInventory[i].stackSize < getInventoryStackLimit() &&
                    (!mainInventory[i].getHasSubtypes() || mainInventory[i].getItemDamage() == itemstack.getItemDamage()))
                {
                    return i;
                }
            }

            return -1;
        }

        private int getFirstEmptyStack()
        {
            for (int i = 0; i < mainInventory.Length; i++)
            {
                if (mainInventory[i] == null)
                {
                    return i;
                }
            }

            return -1;
        }

        private int func_21083_d(ItemStack itemstack)
        {
            int i = itemstack.itemID;
            int j = itemstack.stackSize;
            int k = func_21082_c(itemstack);
            if (k < 0)
            {
                k = getFirstEmptyStack();
            }
            if (k < 0)
            {
                return j;
            }
            if (mainInventory[k] == null)
            {
                mainInventory[k] = new ItemStack(i, 0, itemstack.getItemDamage());
            }
            int l = j;
            if (l > mainInventory[k].getMaxStackSize() - mainInventory[k].stackSize)
            {
                l = mainInventory[k].getMaxStackSize() - mainInventory[k].stackSize;
            }
            if (l > getInventoryStackLimit() - mainInventory[k].stackSize)
            {
                l = getInventoryStackLimit() - mainInventory[k].stackSize;
            }
            if (l == 0)
            {
                return j;
            }
            else
            {
                j -= l;
                mainInventory[k].stackSize += l;
                mainInventory[k].animationsToGo = 5;
                return j;
            }
        }

        public void decrementAnimations()
        {
            for (int i = 0; i < mainInventory.Length; i++)
            {
                if (mainInventory[i] != null && mainInventory[i].animationsToGo > 0)
                {
                    mainInventory[i].animationsToGo--;
                }
            }
        }

        public bool consumeInventoryItem(int i)
        {
            int j = getInventorySlotContainItem(i);
            if (j < 0)
            {
                return false;
            }
            if (--mainInventory[j].stackSize <= 0)
            {
                mainInventory[j] = null;
            }
            return true;
        }

        public bool addItemStackToInventory(ItemStack itemstack)
        {
            if (!itemstack.isItemDamaged())
            {
                itemstack.stackSize = func_21083_d(itemstack);
                if (itemstack.stackSize == 0)
                {
                    return true;
                }
            }
            int i = getFirstEmptyStack();
            if (i >= 0)
            {
                mainInventory[i] = itemstack;
                mainInventory[i].animationsToGo = 5;
                return true;
            }
            else
            {
                return false;
            }
        }

        public ItemStack decrStackSize(int i, int j)
        {
            ItemStack[] aitemstack = mainInventory;
            if (i >= mainInventory.Length)
            {
                aitemstack = armorInventory;
                i -= mainInventory.Length;
            }
            if (aitemstack[i] != null)
            {
                if (aitemstack[i].stackSize <= j)
                {
                    ItemStack itemstack = aitemstack[i];
                    aitemstack[i] = null;
                    return itemstack;
                }
                ItemStack itemstack1 = aitemstack[i].splitStack(j);
                if (aitemstack[i].stackSize == 0)
                {
                    aitemstack[i] = null;
                }
                return itemstack1;
            }
            else
            {
                return null;
            }
        }

        public void setInventorySlotContents(int i, ItemStack itemstack)
        {
            ItemStack[] aitemstack = mainInventory;
            if (i >= aitemstack.Length)
            {
                i -= aitemstack.Length;
                aitemstack = armorInventory;
            }
            aitemstack[i] = itemstack;
        }

        public virtual float getStrVsBlock(Block block)
        {
            float f = 1.0F;
            if (mainInventory[currentItem] != null)
            {
                f *= mainInventory[currentItem].getStrVsBlock(block);
            }
            return f;
        }

        public NBTTagList writeToNBT(NBTTagList nbttaglist)
        {
            for (int i = 0; i < mainInventory.Length; i++)
            {
                if (mainInventory[i] != null)
                {
                    NBTTagCompound nbttagcompound = new NBTTagCompound();
                    nbttagcompound.setByte("Slot", (byte) i);
                    mainInventory[i].writeToNBT(nbttagcompound);
                    nbttaglist.setTag(nbttagcompound);
                }
            }

            for (int j = 0; j < armorInventory.Length; j++)
            {
                if (armorInventory[j] != null)
                {
                    NBTTagCompound nbttagcompound1 = new NBTTagCompound();
                    nbttagcompound1.setByte("Slot", (byte) (j + 100));
                    armorInventory[j].writeToNBT(nbttagcompound1);
                    nbttaglist.setTag(nbttagcompound1);
                }
            }

            return nbttaglist;
        }

        public void readFromNBT(NBTTagList nbttaglist)
        {
            mainInventory = new ItemStack[36];
            armorInventory = new ItemStack[4];
            for (int i = 0; i < nbttaglist.tagCount(); i++)
            {
                NBTTagCompound nbttagcompound = (NBTTagCompound) nbttaglist.tagAt(i);
                int j = nbttagcompound.getByte("Slot") & 0xff;
                ItemStack itemstack = new ItemStack(nbttagcompound);
                if (itemstack.getItem() == null)
                {
                    continue;
                }
                if (j >= 0 && j < mainInventory.Length)
                {
                    mainInventory[j] = itemstack;
                }
                if (j >= 100 && j < armorInventory.Length + 100)
                {
                    armorInventory[j - 100] = itemstack;
                }
            }
        }

        public int getSizeInventory()
        {
            return mainInventory.Length + 4;
        }

        public ItemStack getStackInSlot(int i)
        {
            ItemStack[] aitemstack = mainInventory;
            if (i >= aitemstack.Length)
            {
                i -= aitemstack.Length;
                aitemstack = armorInventory;
            }
            return aitemstack[i];
        }

        public string getInvName()
        {
            return "Inventory";
        }

        public int getInventoryStackLimit()
        {
            return 64;
        }

        public virtual int getDamageVsEntity(Entity entity)
        {
            ItemStack itemstack = getStackInSlot(currentItem);
            if (itemstack != null)
            {
                return itemstack.getDamageVsEntity(entity);
            }
            else
            {
                return 1;
            }
        }

        public virtual bool canHarvestBlock(Block block)
        {
            if (block.blockMaterial != Material.rock && block.blockMaterial != Material.iron &&
                block.blockMaterial != Material.builtSnow && block.blockMaterial != Material.snow)
            {
                return true;
            }
            ItemStack itemstack = getStackInSlot(currentItem);
            if (itemstack != null)
            {
                return itemstack.canHarvestBlock(block);
            }
            else
            {
                return false;
            }
        }

        public int getTotalArmorValue()
        {
            int i = 0;
            int j = 0;
            int k = 0;
            for (int l = 0; l < armorInventory.Length; l++)
            {
                if (armorInventory[l] != null && (armorInventory[l].getItem() is ItemArmor))
                {
                    int i1 = armorInventory[l].getMaxDamage();
                    int j1 = armorInventory[l].getItemDamageForDisplay();
                    int k1 = i1 - j1;
                    j += k1;
                    k += i1;
                    int l1 = ((ItemArmor) armorInventory[l].getItem()).damageReduceAmount;
                    i += l1;
                }
            }

            if (k == 0)
            {
                return 0;
            }
            else
            {
                return ((i - 1)*j)/k + 1;
            }
        }

        public void damageArmor(int i)
        {
            for (int j = 0; j < armorInventory.Length; j++)
            {
                if (armorInventory[j] == null || !(armorInventory[j].getItem() is ItemArmor))
                {
                    continue;
                }
                armorInventory[j].damageItem(i);
                if (armorInventory[j].stackSize == 0)
                {
                    armorInventory[j].func_577_a(player);
                    armorInventory[j] = null;
                }
            }
        }

        public void dropAllItems()
        {
            for (int i = 0; i < mainInventory.Length; i++)
            {
                if (mainInventory[i] != null)
                {
                    player.dropPlayerItemWithRandomChoice(mainInventory[i], true);
                    mainInventory[i] = null;
                }
            }

            for (int j = 0; j < armorInventory.Length; j++)
            {
                if (armorInventory[j] != null)
                {
                    player.dropPlayerItemWithRandomChoice(armorInventory[j], true);
                    armorInventory[j] = null;
                }
            }
        }

        public void onInventoryChanged()
        {
            inventoryChanged = true;
        }

        public void setItemStack(ItemStack itemstack)
        {
            itemStack = itemstack;
            player.onItemStackChanged(itemstack);
        }

        public ItemStack getItemStack()
        {
            return itemStack;
        }

        public virtual bool canInteractWith(EntityPlayer entityplayer)
        {
            if (player.isDead)
            {
                return false;
            }
            return entityplayer.getDistanceSqToEntity(player) <= 64D;
        }

        public ItemStack[] mainInventory;
        public ItemStack[] armorInventory;
        public int currentItem;
        private EntityPlayer player;
        private ItemStack itemStack;
        public bool inventoryChanged;
    }
}