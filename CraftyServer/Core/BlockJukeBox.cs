namespace CraftyServer.Core
{
    public class BlockJukeBox : Block
    {
        public BlockJukeBox(int i, int j)
            : base(i, j, Material.wood)
        {
        }

        public override int getBlockTextureFromSide(int i)
        {
            return blockIndexInTexture + (i != 1 ? 0 : 1);
        }

        public override bool blockActivated(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            int l = world.getBlockMetadata(i, j, k);
            if (l > 0)
            {
                ejectRecord(world, i, j, k, l);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ejectRecord(World world, int i, int j, int k, int l)
        {
            world.playRecord(null, i, j, k);
            world.setBlockMetadataWithNotify(i, j, k, 0);
            int i1 = (Item.record13.shiftedIndex + l) - 1;
            float f = 0.7F;
            double d = (world.rand.nextFloat()*f) + (1.0F - f)*0.5D;
            double d1 = (world.rand.nextFloat()*f) + (1.0F - f)*0.20000000000000001D +
                        0.59999999999999998D;
            double d2 = (world.rand.nextFloat()*f) + (1.0F - f)*0.5D;
            var entityitem = new EntityItem(world, i + d, j + d1, k + d2,
                                            new ItemStack(i1, 1, 0));
            entityitem.delayBeforeCanPickup = 10;
            world.entityJoinedWorld(entityitem);
        }

        public override void dropBlockAsItemWithChance(World world, int i, int j, int k, int l, float f)
        {
            if (world.singleplayerWorld)
            {
                return;
            }
            if (l > 0)
            {
                ejectRecord(world, i, j, k, l);
            }
            base.dropBlockAsItemWithChance(world, i, j, k, l, f);
        }
    }
}