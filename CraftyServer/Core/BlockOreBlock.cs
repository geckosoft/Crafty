namespace CraftyServer.Core
{
    public class BlockOreBlock : Block
    {
        public BlockOreBlock(int i, int j) : base(i, Material.iron)
        {
            blockIndexInTexture = j;
        }

        public override int getBlockTextureFromSide(int i)
        {
            return blockIndexInTexture;
        }
    }
}