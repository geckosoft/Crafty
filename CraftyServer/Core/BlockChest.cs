using java.util;

namespace CraftyServer.Core
{
    public class BlockChest : BlockContainer
    {
        private readonly Random random;

        public BlockChest(int i)
            : base(i, Material.wood)
        {
            random = new Random();
            blockIndexInTexture = 26;
        }

        public override int getBlockTextureFromSide(int i)
        {
            if (i == 1)
            {
                return blockIndexInTexture - 1;
            }
            if (i == 0)
            {
                return blockIndexInTexture - 1;
            }
            if (i == 3)
            {
                return blockIndexInTexture + 1;
            }
            else
            {
                return blockIndexInTexture;
            }
        }

        public override bool canPlaceBlockAt(World world, int i, int j, int k)
        {
            int l = 0;
            if (world.getBlockId(i - 1, j, k) == blockID)
            {
                l++;
            }
            if (world.getBlockId(i + 1, j, k) == blockID)
            {
                l++;
            }
            if (world.getBlockId(i, j, k - 1) == blockID)
            {
                l++;
            }
            if (world.getBlockId(i, j, k + 1) == blockID)
            {
                l++;
            }
            if (l > 1)
            {
                return false;
            }
            if (isThereANeighborChest(world, i - 1, j, k))
            {
                return false;
            }
            if (isThereANeighborChest(world, i + 1, j, k))
            {
                return false;
            }
            if (isThereANeighborChest(world, i, j, k - 1))
            {
                return false;
            }
            return !isThereANeighborChest(world, i, j, k + 1);
        }

        private bool isThereANeighborChest(World world, int i, int j, int k)
        {
            if (world.getBlockId(i, j, k) != blockID)
            {
                return false;
            }
            if (world.getBlockId(i - 1, j, k) == blockID)
            {
                return true;
            }
            if (world.getBlockId(i + 1, j, k) == blockID)
            {
                return true;
            }
            if (world.getBlockId(i, j, k - 1) == blockID)
            {
                return true;
            }
            return world.getBlockId(i, j, k + 1) == blockID;
        }

        public override void onBlockRemoval(World world, int i, int j, int k)
        {
            var tileentitychest = (TileEntityChest) world.getBlockTileEntity(i, j, k);

            for (int l = 0; l < tileentitychest.getSizeInventory(); l++)
            {
                ItemStack itemstack = tileentitychest.getStackInSlot(l);
                if (itemstack == null)
                {
                    continue;
                }
                float f = random.nextFloat()*0.8F + 0.1F;
                float f1 = random.nextFloat()*0.8F + 0.1F;
                float f2 = random.nextFloat()*0.8F + 0.1F;
                do
                {
                    if (itemstack.stackSize <= 0)
                    {
                        goto label0;
                    }
                    int i1 = random.nextInt(21) + 10;
                    if (i1 > itemstack.stackSize)
                    {
                        i1 = itemstack.stackSize;
                    }
                    itemstack.stackSize -= i1;
                    var entityitem = new EntityItem(world, i + f, j + f1, k + f2,
                                                    new ItemStack(itemstack.itemID, i1, itemstack.getItemDamage()));
                    float f3 = 0.05F;
                    entityitem.motionX = (float) random.nextGaussian()*f3;
                    entityitem.motionY = (float) random.nextGaussian()*f3 + 0.2F;
                    entityitem.motionZ = (float) random.nextGaussian()*f3;
                    world.entityJoinedWorld(entityitem);
                } while (true);
            }
            label0:
            base.onBlockRemoval(world, i, j, k);
        }

        public override bool blockActivated(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            object obj = world.getBlockTileEntity(i, j, k);
            if (world.isBlockOpaqueCube(i, j + 1, k))
            {
                return true;
            }
            if (world.getBlockId(i - 1, j, k) == blockID && world.isBlockOpaqueCube(i - 1, j + 1, k))
            {
                return true;
            }
            if (world.getBlockId(i + 1, j, k) == blockID && world.isBlockOpaqueCube(i + 1, j + 1, k))
            {
                return true;
            }
            if (world.getBlockId(i, j, k - 1) == blockID && world.isBlockOpaqueCube(i, j + 1, k - 1))
            {
                return true;
            }
            if (world.getBlockId(i, j, k + 1) == blockID && world.isBlockOpaqueCube(i, j + 1, k + 1))
            {
                return true;
            }
            if (world.getBlockId(i - 1, j, k) == blockID)
            {
                obj = new InventoryLargeChest("Large chest", (TileEntityChest) world.getBlockTileEntity(i - 1, j, k),
                                              ((IInventory) (obj)));
            }
            if (world.getBlockId(i + 1, j, k) == blockID)
            {
                obj = new InventoryLargeChest("Large chest", ((IInventory) (obj)),
                                              (TileEntityChest) world.getBlockTileEntity(i + 1, j, k));
            }
            if (world.getBlockId(i, j, k - 1) == blockID)
            {
                obj = new InventoryLargeChest("Large chest", (TileEntityChest) world.getBlockTileEntity(i, j, k - 1),
                                              ((IInventory) (obj)));
            }
            if (world.getBlockId(i, j, k + 1) == blockID)
            {
                obj = new InventoryLargeChest("Large chest", ((IInventory) (obj)),
                                              (TileEntityChest) world.getBlockTileEntity(i, j, k + 1));
            }
            if (world.singleplayerWorld)
            {
                return true;
            }
            else
            {
                entityplayer.displayGUIChest(((IInventory) (obj)));
                return true;
            }
        }

        protected override TileEntity getBlockEntity()
        {
            return new TileEntityChest();
        }
    }
}