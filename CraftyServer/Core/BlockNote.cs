using java.lang;

namespace CraftyServer.Core
{
    public class BlockNote : BlockContainer
    {
        public BlockNote(int i)
            : base(i, 74, Material.wood)
        {
        }

        public override int getBlockTextureFromSide(int i)
        {
            return blockIndexInTexture;
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            if (l > 0 && blocksList[l].canProvidePower())
            {
                bool flag = world.isBlockGettingPowered(i, j, k);
                var tileentitynote = (TileEntityNote) world.getBlockTileEntity(i, j, k);
                if (tileentitynote.previousRedstoneState != flag)
                {
                    if (flag)
                    {
                        tileentitynote.triggerNote(world, i, j, k);
                    }
                    tileentitynote.previousRedstoneState = flag;
                }
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
                var tileentitynote = (TileEntityNote) world.getBlockTileEntity(i, j, k);
                tileentitynote.changePitch();
                tileentitynote.triggerNote(world, i, j, k);
                return true;
            }
        }

        public override void onBlockClicked(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            if (world.singleplayerWorld)
            {
                return;
            }
            else
            {
                var tileentitynote = (TileEntityNote) world.getBlockTileEntity(i, j, k);
                tileentitynote.triggerNote(world, i, j, k);
                return;
            }
        }

        protected override TileEntity getBlockEntity()
        {
            return new TileEntityNote();
        }

        public override void playBlock(World world, int i, int j, int k, int l, int i1)
        {
            var f = (float) Math.pow(2D, (i1 - 12)/12D);
            string s = "harp";
            if (l == 1)
            {
                s = "bd";
            }
            if (l == 2)
            {
                s = "snare";
            }
            if (l == 3)
            {
                s = "hat";
            }
            if (l == 4)
            {
                s = "bassattack";
            }
            world.playSoundEffect(i + 0.5D, j + 0.5D, k + 0.5D,
                                  (new StringBuilder()).append("note.").append(s).toString(), 3F, f);
            world.spawnParticle("note", i + 0.5D, j + 1.2D, k + 0.5D, i1/24D, 0.0D,
                                0.0D);
        }
    }
}