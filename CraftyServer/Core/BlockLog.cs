using java.util;


namespace CraftyServer.Core
{
    public class BlockLog : Block
    {
        protected internal BlockLog(int i) : base(i, Material.wood)
        {
            blockIndexInTexture = 20;
        }

        public override int quantityDropped(Random random)
        {
            return 1;
        }

        public override int idDropped(int i, Random random)
        {
            return Block.wood.blockID;
        }

        public override void onBlockRemoval(World world, int i, int j, int k)
        {
            byte byte0 = 4;
            int l = byte0 + 1;
            if (world.checkChunksExist(i - l, j - l, k - l, i + l, j + l, k + l))
            {
                for (int i1 = -byte0; i1 <= byte0; i1++)
                {
                    for (int j1 = -byte0; j1 <= byte0; j1++)
                    {
                        for (int k1 = -byte0; k1 <= byte0; k1++)
                        {
                            int l1 = world.getBlockId(i + i1, j + j1, k + k1);
                            if (l1 != Block.leaves.blockID)
                            {
                                continue;
                            }
                            int i2 = world.getBlockMetadata(i + i1, j + j1, k + k1);
                            if ((i2 & 4) == 0)
                            {
                                world.setBlockMetadata(i + i1, j + j1, k + k1, i2 | 4);
                            }
                        }
                    }
                }
            }
        }

        public override int func_22009_a(int i, int j)
        {
            if (i == 1)
            {
                return 21;
            }
            if (i == 0)
            {
                return 21;
            }
            if (j == 1)
            {
                return 116;
            }
            return j != 2 ? 20 : 117;
        }

        protected override int damageDropped(int i)
        {
            return i;
        }
    }
}