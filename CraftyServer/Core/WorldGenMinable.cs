using java.util;

namespace CraftyServer.Core
{
    public class WorldGenMinable : WorldGenerator
    {
        private readonly int minableBlockId;
        private readonly int numberOfBlocks;

        public WorldGenMinable(int i, int j)
        {
            minableBlockId = i;
            numberOfBlocks = j;
        }

        public override bool generate(World world, Random random, int i, int j, int k)
        {
            float f = random.nextFloat()*3.141593F;
            double d = (i + 8) + (MathHelper.sin(f)*numberOfBlocks)/8F;
            double d1 = (i + 8) - (MathHelper.sin(f)*numberOfBlocks)/8F;
            double d2 = (k + 8) + (MathHelper.cos(f)*numberOfBlocks)/8F;
            double d3 = (k + 8) - (MathHelper.cos(f)*numberOfBlocks)/8F;
            double d4 = j + random.nextInt(3) + 2;
            double d5 = j + random.nextInt(3) + 2;
            for (int l = 0; l <= numberOfBlocks; l++)
            {
                double d6 = d + ((d1 - d)*l)/numberOfBlocks;
                double d7 = d4 + ((d5 - d4)*l)/numberOfBlocks;
                double d8 = d2 + ((d3 - d2)*l)/numberOfBlocks;
                double d9 = (random.nextDouble()*numberOfBlocks)/16D;
                double d10 = (MathHelper.sin((l*3.141593F)/numberOfBlocks) + 1.0F)*d9 + 1.0D;
                double d11 = (MathHelper.sin((l*3.141593F)/numberOfBlocks) + 1.0F)*d9 + 1.0D;
                var i1 = (int) (d6 - d10/2D);
                var j1 = (int) (d7 - d11/2D);
                var k1 = (int) (d8 - d10/2D);
                var l1 = (int) (d6 + d10/2D);
                var i2 = (int) (d7 + d11/2D);
                var j2 = (int) (d8 + d10/2D);
                for (int k2 = i1; k2 <= l1; k2++)
                {
                    double d12 = ((k2 + 0.5D) - d6)/(d10/2D);
                    if (d12*d12 >= 1.0D)
                    {
                        continue;
                    }
                    for (int l2 = j1; l2 <= i2; l2++)
                    {
                        double d13 = ((l2 + 0.5D) - d7)/(d11/2D);
                        if (d12*d12 + d13*d13 >= 1.0D)
                        {
                            continue;
                        }
                        for (int i3 = k1; i3 <= j2; i3++)
                        {
                            double d14 = ((i3 + 0.5D) - d8)/(d10/2D);
                            if (d12*d12 + d13*d13 + d14*d14 < 1.0D &&
                                world.getBlockId(k2, l2, i3) == Block.stone.blockID)
                            {
                                world.setBlock(k2, l2, i3, minableBlockId);
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}