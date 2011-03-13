namespace CraftyServer.Core
{
    public class BlockSandStone : Block
    {
        public BlockSandStone(int i)
            : base(i, 192, Material.rock)
        {
        }

        public override int getBlockTextureFromSide(int i)
        {
            if (i == 1)
            {
                return blockIndexInTexture - 16;
            }
            if (i == 0)
            {
                return blockIndexInTexture + 16;
            }
            else
            {
                return blockIndexInTexture;
            }
        }
    }
}