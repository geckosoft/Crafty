using java.lang;

namespace CraftyServer.Core
{
    public class ItemStack
    {
        public ItemStack(Block block)
            : this(block, 1)
        {
        }

        public ItemStack(Block block, int i)
            : this(block.blockID, i, 0)
        {
        }

        public ItemStack(Block block, int i, int j)
            : this(block.blockID, i, j)
        {
        }

        public ItemStack(Item item)
            : this(item.shiftedIndex, 1, 0)
        {
        }

        public ItemStack(Item item, int i)
            : this(item.shiftedIndex, i, 0)
        {
        }

        public ItemStack(Item item, int i, int j)
            : this(item.shiftedIndex, i, j)
        {
        }

        public ItemStack(int i, int j, int k)
        {
            stackSize = 0;
            itemID = i;
            stackSize = j;
            itemDamage = k;
        }

        public ItemStack(NBTTagCompound nbttagcompound)
        {
            stackSize = 0;
            readFromNBT(nbttagcompound);
        }

        public ItemStack splitStack(int i)
        {
            stackSize -= i;
            return new ItemStack(itemID, i, itemDamage);
        }

        public Item getItem()
        {
            return Item.itemsList[itemID];
        }

        public bool useItem(EntityPlayer entityplayer, World world, int i, int j, int k, int l)
        {
            return getItem().onItemUse(this, entityplayer, world, i, j, k, l);
        }

        public virtual float getStrVsBlock(Block block)
        {
            return getItem().getStrVsBlock(this, block);
        }

        public ItemStack useItemRightClick(World world, EntityPlayer entityplayer)
        {
            return getItem().onItemRightClick(this, world, entityplayer);
        }

        public NBTTagCompound writeToNBT(NBTTagCompound nbttagcompound)
        {
            nbttagcompound.setShort("id", (short) itemID);
            nbttagcompound.setByte("Count", (byte) stackSize);
            nbttagcompound.setShort("Damage", (short) itemDamage);
            return nbttagcompound;
        }

        public void readFromNBT(NBTTagCompound nbttagcompound)
        {
            itemID = nbttagcompound.getShort("id");
            stackSize = nbttagcompound.getByte("Count");
            itemDamage = nbttagcompound.getShort("Damage");
        }

        public int getMaxStackSize()
        {
            return getItem().getItemStackLimit();
        }

        public bool func_21132_c()
        {
            return getMaxStackSize() > 1 && (!isItemStackDamageable() || !isItemDamaged());
        }

        public bool isItemStackDamageable()
        {
            return Item.itemsList[itemID].getMaxDamage() > 0;
        }

        public bool getHasSubtypes()
        {
            return Item.itemsList[itemID].getHasSubtypes();
        }

        public bool isItemDamaged()
        {
            return isItemStackDamageable() && itemDamage > 0;
        }

        public int getItemDamageForDisplay()
        {
            return itemDamage;
        }

        public int getItemDamage()
        {
            return itemDamage;
        }

        public int getMaxDamage()
        {
            return Item.itemsList[itemID].getMaxDamage();
        }

        public void damageItem(int i)
        {
            if (!isItemStackDamageable())
            {
                return;
            }
            itemDamage += i;
            if (itemDamage > getMaxDamage())
            {
                stackSize--;
                if (stackSize < 0)
                {
                    stackSize = 0;
                }
                itemDamage = 0;
            }
        }

        public virtual void hitEntity(EntityLiving entityliving)
        {
            Item.itemsList[itemID].hitEntity(this, entityliving);
        }

        public virtual void hitBlock(int i, int j, int k, int l)
        {
            Item.itemsList[itemID].hitBlock(this, i, j, k, l);
        }

        public virtual int getDamageVsEntity(Entity entity)
        {
            return Item.itemsList[itemID].getDamageVsEntity(entity);
        }

        public virtual bool canHarvestBlock(Block block)
        {
            return Item.itemsList[itemID].canHarvestBlock(block);
        }

        public void func_577_a(EntityPlayer entityplayer)
        {
        }

        public void useItemOnEntity(EntityLiving entityliving)
        {
            Item.itemsList[itemID].saddleEntity(this, entityliving);
        }

        public ItemStack copy()
        {
            return new ItemStack(itemID, stackSize, itemDamage);
        }

        public static bool areItemStacksEqual(ItemStack itemstack, ItemStack itemstack1)
        {
            if (itemstack == null && itemstack1 == null)
            {
                return true;
            }
            if (itemstack == null || itemstack1 == null)
            {
                return false;
            }
            else
            {
                return itemstack.isItemStackEqual(itemstack1);
            }
        }

        private bool isItemStackEqual(ItemStack itemstack)
        {
            if (stackSize != itemstack.stackSize)
            {
                return false;
            }
            if (itemID != itemstack.itemID)
            {
                return false;
            }
            return itemDamage == itemstack.itemDamage;
        }

        public bool isItemEqual(ItemStack itemstack)
        {
            return itemID == itemstack.itemID && itemDamage == itemstack.itemDamage;
        }

        public static ItemStack func_20117_a(ItemStack itemstack)
        {
            return itemstack != null ? itemstack.copy() : null;
        }

        public string toString()
        {
            return
                (new StringBuilder()).append(stackSize).append("x").append(Item.itemsList[itemID].getItemName()).append(
                    "@").append(itemDamage).toString();
        }

        public int stackSize;
        public int animationsToGo;
        public int itemID;
        private int itemDamage;
    }
}