using java.util;


namespace CraftyServer.Core
{
    public class TileEntityDispenser : TileEntity
                                       , IInventory
    {
        public TileEntityDispenser()
        {
            dispenserContents = new ItemStack[9];
            dispenserRandom = new Random();
        }

        public int getSizeInventory()
        {
            return 9;
        }

        public ItemStack getStackInSlot(int i)
        {
            return dispenserContents[i];
        }

        public ItemStack decrStackSize(int i, int j)
        {
            if (dispenserContents[i] != null)
            {
                if (dispenserContents[i].stackSize <= j)
                {
                    ItemStack itemstack = dispenserContents[i];
                    dispenserContents[i] = null;
                    onInventoryChanged();
                    return itemstack;
                }
                ItemStack itemstack1 = dispenserContents[i].splitStack(j);
                if (dispenserContents[i].stackSize == 0)
                {
                    dispenserContents[i] = null;
                }
                onInventoryChanged();
                return itemstack1;
            }
            else
            {
                return null;
            }
        }

        public ItemStack getRandomStackFromInventory()
        {
            int i = -1;
            int j = 1;
            for (int k = 0; k < dispenserContents.Length; k++)
            {
                if (dispenserContents[k] != null && dispenserRandom.nextInt(j) == 0)
                {
                    i = k;
                    j++;
                }
            }

            if (i >= 0)
            {
                return decrStackSize(i, 1);
            }
            else
            {
                return null;
            }
        }

        public void setInventorySlotContents(int i, ItemStack itemstack)
        {
            dispenserContents[i] = itemstack;
            if (itemstack != null && itemstack.stackSize > getInventoryStackLimit())
            {
                itemstack.stackSize = getInventoryStackLimit();
            }
            onInventoryChanged();
        }

        public string getInvName()
        {
            return "Trap";
        }

        public override void readFromNBT(NBTTagCompound nbttagcompound)
        {
            base.readFromNBT(nbttagcompound);
            NBTTagList nbttaglist = nbttagcompound.getTagList("Items");
            dispenserContents = new ItemStack[getSizeInventory()];
            for (int i = 0; i < nbttaglist.tagCount(); i++)
            {
                NBTTagCompound nbttagcompound1 = (NBTTagCompound) nbttaglist.tagAt(i);
                int j = nbttagcompound1.getByte("Slot") & 0xff;
                if (j >= 0 && j < dispenserContents.Length)
                {
                    dispenserContents[j] = new ItemStack(nbttagcompound1);
                }
            }
        }

        public override void writeToNBT(NBTTagCompound nbttagcompound)
        {
            base.writeToNBT(nbttagcompound);
            NBTTagList nbttaglist = new NBTTagList();
            for (int i = 0; i < dispenserContents.Length; i++)
            {
                if (dispenserContents[i] != null)
                {
                    NBTTagCompound nbttagcompound1 = new NBTTagCompound();
                    nbttagcompound1.setByte("Slot", (byte) i);
                    dispenserContents[i].writeToNBT(nbttagcompound1);
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

        private ItemStack[] dispenserContents;
        private Random dispenserRandom;
    }
}