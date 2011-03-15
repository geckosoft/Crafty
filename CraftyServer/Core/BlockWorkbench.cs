namespace CraftyServer.Core
{
    public class BlockWorkbench : Block
    {
        public BlockWorkbench(int i)
            : base(i, Material.wood)
        {
            blockIndexInTexture = 59;
        }

        public override int getBlockTextureFromSide(int i)
        {
            if (i == 1)
            {
                return blockIndexInTexture - 16;
            }
            if (i == 0)
            {
                return planks.getBlockTextureFromSide(0);
            }
            if (i == 2 || i == 4)
            {
                return blockIndexInTexture + 1;
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
                entityplayer.displayWorkbenchGUI(i, j, k);
                return true;
            }
        }
    }
}