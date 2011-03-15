using java.util;

namespace CraftyServer.Core
{
    public class WorldGenClay : WorldGenerator
    {
        private readonly int clayBlockId;
        private readonly int numberOfBlocks;

        public WorldGenClay(int i)
        {
            clayBlockId = Block.blockClay.blockID;
            numberOfBlocks = i;
        }

        public override bool generate(World world, Random random, int i, int j, int k)
        {
            if (world.getBlockMaterial(i, j, k) != Material.water)
            {
                return false;
            }
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
                for (var i1 = (int) (d6 - d10/2D); i1 <= (int) (d6 + d10/2D); i1++)
                {
                    for (var j1 = (int) (d7 - d11/2D); j1 <= (int) (d7 + d11/2D); j1++)
                    {
                        for (var k1 = (int) (d8 - d10/2D); k1 <= (int) (d8 + d10/2D); k1++)
                        {
                            double d12 = ((i1 + 0.5D) - d6)/(d10/2D);
                            double d13 = ((j1 + 0.5D) - d7)/(d11/2D);
                            double d14 = ((k1 + 0.5D) - d8)/(d10/2D);
                            if (d12*d12 + d13*d13 + d14*d14 >= 1.0D)
                            {
                                continue;
                            }
                            int l1 = world.getBlockId(i1, j1, k1);
                            if (l1 == Block.sand.blockID)
                            {
                                world.setBlock(i1, j1, k1, clayBlockId);
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}