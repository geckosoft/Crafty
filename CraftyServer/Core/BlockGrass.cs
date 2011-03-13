using java.util;


namespace CraftyServer.Core
{
    public class BlockGrass : Block
    {
        protected internal BlockGrass(int i) : base(i, Material.ground)
        {
            blockIndexInTexture = 3;
            setTickOnLoad(true);
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            if (world.singleplayerWorld)
            {
                return;
            }
            if (world.getBlockLightValue(i, j + 1, k) < 4 && world.getBlockMaterial(i, j + 1, k).getCanBlockGrass())
            {
                if (random.nextInt(4) != 0)
                {
                    return;
                }
                world.setBlockWithNotify(i, j, k, Block.dirt.blockID);
            }
            else if (world.getBlockLightValue(i, j + 1, k) >= 9)
            {
                int l = (i + random.nextInt(3)) - 1;
                int i1 = (j + random.nextInt(5)) - 3;
                int j1 = (k + random.nextInt(3)) - 1;
                if (world.getBlockId(l, i1, j1) == Block.dirt.blockID && world.getBlockLightValue(l, i1 + 1, j1) >= 4 &&
                    !world.getBlockMaterial(l, i1 + 1, j1).getCanBlockGrass())
                {
                    world.setBlockWithNotify(l, i1, j1, Block.grass.blockID);
                }
            }
        }

        public override int idDropped(int i, Random random)
        {
            return Block.dirt.idDropped(0, random);
        }
    }
}