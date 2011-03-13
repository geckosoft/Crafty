using java.util;


namespace CraftyServer.Core
{
    public class BlockBookshelf : Block
    {
        public BlockBookshelf(int i, int j)
            : base(i, j, Material.wood)
        {
        }

        public override int getBlockTextureFromSide(int i)
        {
            if (i <= 1)
            {
                return 4;
            }
            else
            {
                return blockIndexInTexture;
            }
        }

        public override int quantityDropped(Random random)
        {
            return 0;
        }
    }
}