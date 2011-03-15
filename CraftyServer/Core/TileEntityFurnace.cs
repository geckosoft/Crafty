namespace CraftyServer.Core
{
    public class TileEntityFurnace : TileEntity
                                     , IInventory
    {
        public int currentItemBurnTime;
        public int furnaceBurnTime;
        public int furnaceCookTime;
        private ItemStack[] furnaceItemStacks;

        public TileEntityFurnace()
        {
            furnaceItemStacks = new ItemStack[3];
            furnaceBurnTime = 0;
            currentItemBurnTime = 0;
            furnaceCookTime = 0;
        }

        #region IInventory Members

        public int getSizeInventory()
        {
            return furnaceItemStacks.Length;
        }

        public ItemStack getStackInSlot(int i)
        {
            return furnaceItemStacks[i];
        }

        public ItemStack decrStackSize(int i, int j)
        {
            if (furnaceItemStacks[i] != null)
            {
                if (furnaceItemStacks[i].stackSize <= j)
                {
                    ItemStack itemstack = furnaceItemStacks[i];
                    furnaceItemStacks[i] = null;
                    return itemstack;
                }
                ItemStack itemstack1 = furnaceItemStacks[i].splitStack(j);
                if (furnaceItemStacks[i].stackSize == 0)
                {
                    furnaceItemStacks[i] = null;
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
            furnaceItemStacks[i] = itemstack;
            if (itemstack != null && itemstack.stackSize > getInventoryStackLimit())
            {
                itemstack.stackSize = getInventoryStackLimit();
            }
        }

        public string getInvName()
        {
            return "Furnace";
        }

        public int getInventoryStackLimit()
        {
            return 64;
        }

        public bool canInteractWith(EntityPlayer entityplayer)
        {
            if (worldObj.getBlockTileEntity(xCoord, yCoord, zCoord) != this)
            {
                return false;
            }
            return entityplayer.getDistanceSq(xCoord + 0.5D, yCoord + 0.5D, zCoord + 0.5D) <=
                   64D;
        }

        #endregion

        public override void readFromNBT(NBTTagCompound nbttagcompound)
        {
            base.readFromNBT(nbttagcompound);
            NBTTagList nbttaglist = nbttagcompound.getTagList("Items");
            furnaceItemStacks = new ItemStack[getSizeInventory()];
            for (int i = 0; i < nbttaglist.tagCount(); i++)
            {
                var nbttagcompound1 = (NBTTagCompound) nbttaglist.tagAt(i);
                byte byte0 = nbttagcompound1.getByte("Slot");
                if (byte0 >= 0 && byte0 < furnaceItemStacks.Length)
                {
                    furnaceItemStacks[byte0] = new ItemStack(nbttagcompound1);
                }
            }

            furnaceBurnTime = nbttagcompound.getShort("BurnTime");
            furnaceCookTime = nbttagcompound.getShort("CookTime");
            currentItemBurnTime = getItemBurnTime(furnaceItemStacks[1]);
        }

        public override void writeToNBT(NBTTagCompound nbttagcompound)
        {
            base.writeToNBT(nbttagcompound);
            nbttagcompound.setShort("BurnTime", (short) furnaceBurnTime);
            nbttagcompound.setShort("CookTime", (short) furnaceCookTime);
            var nbttaglist = new NBTTagList();
            for (int i = 0; i < furnaceItemStacks.Length; i++)
            {
                if (furnaceItemStacks[i] != null)
                {
                    var nbttagcompound1 = new NBTTagCompound();
                    nbttagcompound1.setByte("Slot", (byte) i);
                    furnaceItemStacks[i].writeToNBT(nbttagcompound1);
                    nbttaglist.setTag(nbttagcompound1);
                }
            }

            nbttagcompound.setTag("Items", nbttaglist);
        }

        public bool isBurning()
        {
            return furnaceBurnTime > 0;
        }

        public override void updateEntity()
        {
            bool flag = furnaceBurnTime > 0;
            bool flag1 = false;
            if (furnaceBurnTime > 0)
            {
                furnaceBurnTime--;
            }
            if (!worldObj.singleplayerWorld)
            {
                if (furnaceBurnTime == 0 && canSmelt())
                {
                    currentItemBurnTime = furnaceBurnTime = getItemBurnTime(furnaceItemStacks[1]);
                    if (furnaceBurnTime > 0)
                    {
                        flag1 = true;
                        if (furnaceItemStacks[1] != null)
                        {
                            furnaceItemStacks[1].stackSize--;
                            if (furnaceItemStacks[1].stackSize == 0)
                            {
                                furnaceItemStacks[1] = null;
                            }
                        }
                    }
                }
                if (isBurning() && canSmelt())
                {
                    furnaceCookTime++;
                    if (furnaceCookTime == 200)
                    {
                        furnaceCookTime = 0;
                        smeltItem();
                        flag1 = true;
                    }
                }
                else
                {
                    furnaceCookTime = 0;
                }
                if (flag != (furnaceBurnTime > 0))
                {
                    flag1 = true;
                    BlockFurnace.updateFurnaceBlockState(furnaceBurnTime > 0, worldObj, xCoord, yCoord, zCoord);
                }
            }
            if (flag1)
            {
                onInventoryChanged();
            }
        }

        private bool canSmelt()
        {
            if (furnaceItemStacks[0] == null)
            {
                return false;
            }
            ItemStack itemstack =
                FurnaceRecipes.smelting().getSmeltingResult(furnaceItemStacks[0].getItem().shiftedIndex);
            if (itemstack == null)
            {
                return false;
            }
            if (furnaceItemStacks[2] == null)
            {
                return true;
            }
            if (!furnaceItemStacks[2].isItemEqual(itemstack))
            {
                return false;
            }
            if (furnaceItemStacks[2].stackSize < getInventoryStackLimit() &&
                furnaceItemStacks[2].stackSize < furnaceItemStacks[2].getMaxStackSize())
            {
                return true;
            }
            return furnaceItemStacks[2].stackSize < itemstack.getMaxStackSize();
        }

        public void smeltItem()
        {
            if (!canSmelt())
            {
                return;
            }
            ItemStack itemstack =
                FurnaceRecipes.smelting().getSmeltingResult(furnaceItemStacks[0].getItem().shiftedIndex);
            if (furnaceItemStacks[2] == null)
            {
                furnaceItemStacks[2] = itemstack.copy();
            }
            else if (furnaceItemStacks[2].itemID == itemstack.itemID)
            {
                furnaceItemStacks[2].stackSize++;
            }
            furnaceItemStacks[0].stackSize--;
            if (furnaceItemStacks[0].stackSize <= 0)
            {
                furnaceItemStacks[0] = null;
            }
        }

        private int getItemBurnTime(ItemStack itemstack)
        {
            if (itemstack == null)
            {
                return 0;
            }
            int i = itemstack.getItem().shiftedIndex;
            if (i < 256 && Block.blocksList[i].blockMaterial == Material.wood)
            {
                return 300;
            }
            if (i == Item.stick.shiftedIndex)
            {
                return 100;
            }
            if (i == Item.coal.shiftedIndex)
            {
                return 1600;
            }
            return i != Item.bucketLava.shiftedIndex ? 0 : 20000;
        }
    }
}