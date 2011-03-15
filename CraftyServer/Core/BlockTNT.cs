using java.util;

namespace CraftyServer.Core
{
    public class BlockTNT : Block
    {
        public BlockTNT(int i, int j)
            : base(i, j, Material.tnt)
        {
        }

        public override int getBlockTextureFromSide(int i)
        {
            if (i == 0)
            {
                return blockIndexInTexture + 2;
            }
            if (i == 1)
            {
                return blockIndexInTexture + 1;
            }
            else
            {
                return blockIndexInTexture;
            }
        }

        public override void onNeighborBlockChange(World world, int i, int j, int k, int l)
        {
            if (l > 0 && blocksList[l].canProvidePower() && world.isBlockIndirectlyGettingPowered(i, j, k))
            {
                onBlockDestroyedByPlayer(world, i, j, k, 0);
                world.setBlockWithNotify(i, j, k, 0);
            }
        }

        public override int quantityDropped(Random random)
        {
            return 0;
        }

        public override void onBlockDestroyedByExplosion(World world, int i, int j, int k)
        {
            var entitytntprimed = new EntityTNTPrimed(world, i + 0.5F, j + 0.5F,
                                                      k + 0.5F);
            entitytntprimed.fuse = world.rand.nextInt(entitytntprimed.fuse/4) + entitytntprimed.fuse/8;
            world.entityJoinedWorld(entitytntprimed);
        }

        public override void onBlockDestroyedByPlayer(World world, int i, int j, int k, int l)
        {
            if (world.singleplayerWorld)
            {
                return;
            }
            else
            {
                var entitytntprimed = new EntityTNTPrimed(world, i + 0.5F, j + 0.5F,
                                                          k + 0.5F);
                world.entityJoinedWorld(entitytntprimed);
                world.playSoundAtEntity(entitytntprimed, "random.fuse", 1.0F, 1.0F);
                return;
            }
        }
    }
}