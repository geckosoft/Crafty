namespace CraftyServer.Core
{
    public class TileEntityChest : TileEntity
                                   , IInventory
    {
        public TileEntityChest()
        {
            chestContents = new ItemStack[36];
        }

        public int getSizeInventory()
        {
            return 27;
        }

        public ItemStack getStackInSlot(int i)
        {
            return chestContents[i];
        }

        public ItemStack decrStackSize(int i, int j)
        {
            if (chestContents[i] != null)
            {
                if (chestContents[i].stackSize <= j)
                {
                    ItemStack itemstack = chestContents[i];
                    chestContents[i] = null;
                    onInventoryChanged();
                    return itemstack;
                }
                ItemStack itemstack1 = chestContents[i].splitStack(j);
                if (chestContents[i].stackSize == 0)
                {
                    chestContents[i] = null;
                }
                onInventoryChanged();
                return itemstack1;
            }
            else
            {
                return null;
            }
        }

        public void setInventorySlotContents(int i, ItemStack itemstack)
        {
            chestContents[i] = itemstack;
            if (itemstack != null && itemstack.stackSize > getInventoryStackLimit())
            {
                itemstack.stackSize = getInventoryStackLimit();
            }
            onInventoryChanged();
        }

        public string getInvName()
        {
            return "Chest";
        }

        public override void readFromNBT(NBTTagCompound nbttagcompound)
        {
            base.readFromNBT(nbttagcompound);
            NBTTagList nbttaglist = nbttagcompound.getTagList("Items");
            chestContents = new ItemStack[getSizeInventory()];
            for (int i = 0; i < nbttaglist.tagCount(); i++)
            {
                NBTTagCompound nbttagcompound1 = (NBTTagCompound) nbttaglist.tagAt(i);
                int j = nbttagcompound1.getByte("Slot") & 0xff;
                if (j >= 0 && j < chestContents.Length)
                {
                    chestContents[j] = new ItemStack(nbttagcompound1);
                }
            }
        }

        public override void writeToNBT(NBTTagCompound nbttagcompound)
        {
            base.writeToNBT(nbttagcompound);
            NBTTagList nbttaglist = new NBTTagList();
            for (int i = 0; i < chestContents.Length; i++)
            {
                if (chestContents[i] != null)
                {
                    NBTTagCompound nbttagcompound1 = new NBTTagCompound();
                    nbttagcompound1.setByte("Slot", (byte) i);
                    chestContents[i].writeToNBT(nbttagcompound1);
                    nbttaglist.setTag(nbttagcompound1);
                }
            }

            nbttagcompound.setTag("Items", nbttaglist);
        }

        public int getInventoryStackLimit()
        {
            return 64;
        }

        public virtual bool canInteractWith(EntityPlayer entityplayer)
        {
            if (worldObj.getBlockTileEntity(xCoord, yCoord, zCoord) != this)
            {
                return false;
            }
            return entityplayer.getDistanceSq((double) xCoord + 0.5D, (double) yCoord + 0.5D, (double) zCoord + 0.5D) <=
                   64D;
        }

        private ItemStack[] chestContents;
    }
}