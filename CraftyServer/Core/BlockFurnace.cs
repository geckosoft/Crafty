using java.util;


namespace CraftyServer.Core
{
    public class BlockFurnace : BlockContainer
    {
        public BlockFurnace(int i, bool flag) : base(i, Material.rock)
        {
            isActive = flag;
            blockIndexInTexture = 45;
        }

        public override int idDropped(int i, Random random)
        {
            return Block.stoneOvenIdle.blockID;
        }

        public override void onBlockAdded(World world, int i, int j, int k)
        {
            base.onBlockAdded(world, i, j, k);
            setDefaultDirection(world, i, j, k);
        }

        private void setDefaultDirection(World world, int i, int j, int k)
        {
            int l = world.getBlockId(i, j, k - 1);
            int i1 = world.getBlockId(i, j, k + 1);
            int j1 = world.getBlockId(i - 1, j, k);
            int k1 = world.getBlockId(i + 1, j, k);
            byte byte0 = 3;
            if (Block.opaqueCubeLookup[l] && !Block.opaqueCubeLookup[i1])
            {
                byte0 = 3;
            }
            if (Block.opaqueCubeLookup[i1] && !Block.opaqueCubeLookup[l])
            {
                byte0 = 2;
            }
            if (Block.opaqueCubeLookup[j1] && !Block.opaqueCubeLookup[k1])
            {
                byte0 = 5;
            }
            if (Block.opaqueCubeLookup[k1] && !Block.opaqueCubeLookup[j1])
            {
                byte0 = 4;
            }
            world.setBlockMetadataWithNotify(i, j, k, byte0);
        }

        public override int getBlockTextureFromSide(int i)
        {
            if (i == 1)
            {
                return blockIndexInTexture + 17;
            }
            if (i == 0)
            {
                return blockIndexInTexture + 17;
            }
            if (i == 3)
            {
                return blockIndexInTexture - 1;
            }
            else
            {
                return blockIndexInTexture;
            }
        }

        public override bool blockActivated(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            if (world.singleplayerWorld)
            {
                return true;
            }
            else
            {
                TileEntityFurnace tileentityfurnace = (TileEntityFurnace) world.getBlockTileEntity(i, j, k);
                entityplayer.displayGUIFurnace(tileentityfurnace);
                return true;
            }
        }

        public static void updateFurnaceBlockState(bool flag, World world, int i, int j, int k)
        {
            int l = world.getBlockMetadata(i, j, k);
            TileEntity tileentity = world.getBlockTileEntity(i, j, k);
            if (flag)
            {
                world.setBlockWithNotify(i, j, k, Block.stoneOvenActive.blockID);
            }
            else
            {
                world.setBlockWithNotify(i, j, k, Block.stoneOvenIdle.blockID);
            }
            world.setBlockMetadataWithNotify(i, j, k, l);
            world.setBlockTileEntity(i, j, k, tileentity);
        }

        protected override TileEntity getBlockEntity()
        {
            return new TileEntityFurnace();
        }

        public override void onBlockPlacedBy(World world, int i, int j, int k, EntityLiving entityliving)
        {
            int l = MathHelper.floor_double((double) ((entityliving.rotationYaw*4F)/360F) + 0.5D) & 3;
            if (l == 0)
            {
                world.setBlockMetadataWithNotify(i, j, k, 2);
            }
            if (l == 1)
            {
                world.setBlockMetadataWithNotify(i, j, k, 5);
            }
            if (l == 2)
            {
                world.setBlockMetadataWithNotify(i, j, k, 3);
            }
            if (l == 3)
            {
                world.setBlockMetadataWithNotify(i, j, k, 4);
            }
        }

        private bool isActive;
    }
}