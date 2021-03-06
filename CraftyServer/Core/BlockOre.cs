using java.util;

namespace CraftyServer.Core
{
    public class BlockOre : Block
    {
        public BlockOre(int i, int j)
            : base(i, j, Material.rock)
        {
        }

        public override int idDropped(int i, Random random)
        {
            if (blockID == oreCoal.blockID)
            {
                return Item.coal.shiftedIndex;
            }
            if (blockID == oreDiamond.blockID)
            {
                return Item.diamond.shiftedIndex;
            }
            if (blockID == oreLapis.blockID)
            {
                return Item.dyePowder.shiftedIndex;
            }
            else
            {
                return blockID;
            }
        }

        public override int quantityDropped(Random random)
        {
            if (blockID == oreLapis.blockID)
            {
                return 4 + random.nextInt(5);
            }
            else
            {
                return 1;
            }
        }

        protected override int damageDropped(int i)
        {
            return blockID != oreLapis.blockID ? 0 : 4;
        }
    }
}