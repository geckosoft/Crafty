using java.util;

namespace CraftyServer.Core
{
    public class BlockLeaves : BlockLeavesBase
    {
        private int[] adjacentTreeBlocks;
        private int baseIndexInPNG;

        public BlockLeaves(int i, int j)
            : base(i, j, Material.leaves, false)
        {
            baseIndexInPNG = j;
            setTickOnLoad(true);
        }

        public override void onBlockRemoval(World world, int i, int j, int k)
        {
            int l = 1;
            int i1 = l + 1;
            if (world.checkChunksExist(i - i1, j - i1, k - i1, i + i1, j + i1, k + i1))
            {
                for (int j1 = -l; j1 <= l; j1++)
                {
                    for (int k1 = -l; k1 <= l; k1++)
                    {
                        for (int l1 = -l; l1 <= l; l1++)
                        {
                            int i2 = world.getBlockId(i + j1, j + k1, k + l1);
                            if (i2 == leaves.blockID)
                            {
                                int j2 = world.getBlockMetadata(i + j1, j + k1, k + l1);
                                world.setBlockMetadata(i + j1, j + k1, k + l1, j2 | 4);
                            }
                        }
                    }
                }
            }
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            if (world.singleplayerWorld)
            {
                return;
            }
            int l = world.getBlockMetadata(i, j, k);
            if ((l & 4) != 0)
            {
                byte byte0 = 4;
                int i1 = byte0 + 1;
                byte byte1 = 32;
                int j1 = byte1*byte1;
                int k1 = byte1/2;
                if (adjacentTreeBlocks == null)
                {
                    adjacentTreeBlocks = new int[byte1*byte1*byte1];
                }
                if (world.checkChunksExist(i - i1, j - i1, k - i1, i + i1, j + i1, k + i1))
                {
                    for (int l1 = -byte0; l1 <= byte0; l1++)
                    {
                        for (int k2 = -byte0; k2 <= byte0; k2++)
                        {
                            for (int i3 = -byte0; i3 <= byte0; i3++)
                            {
                                int k3 = world.getBlockId(i + l1, j + k2, k + i3);
                                if (k3 == wood.blockID)
                                {
                                    adjacentTreeBlocks[(l1 + k1)*j1 + (k2 + k1)*byte1 + (i3 + k1)] = 0;
                                    continue;
                                }
                                if (k3 == leaves.blockID)
                                {
                                    adjacentTreeBlocks[(l1 + k1)*j1 + (k2 + k1)*byte1 + (i3 + k1)] = -2;
                                }
                                else
                                {
                                    adjacentTreeBlocks[(l1 + k1)*j1 + (k2 + k1)*byte1 + (i3 + k1)] = -1;
                                }
                            }
                        }
                    }

                    for (int i2 = 1; i2 <= 4; i2++)
                    {
                        for (int l2 = -byte0; l2 <= byte0; l2++)
                        {
                            for (int j3 = -byte0; j3 <= byte0; j3++)
                            {
                                for (int l3 = -byte0; l3 <= byte0; l3++)
                                {
                                    if (adjacentTreeBlocks[(l2 + k1)*j1 + (j3 + k1)*byte1 + (l3 + k1)] != i2 - 1)
                                    {
                                        continue;
                                    }
                                    if (adjacentTreeBlocks[((l2 + k1) - 1)*j1 + (j3 + k1)*byte1 + (l3 + k1)] == -2)
                                    {
                                        adjacentTreeBlocks[((l2 + k1) - 1)*j1 + (j3 + k1)*byte1 + (l3 + k1)] = i2;
                                    }
                                    if (adjacentTreeBlocks[(l2 + k1 + 1)*j1 + (j3 + k1)*byte1 + (l3 + k1)] == -2)
                                    {
                                        adjacentTreeBlocks[(l2 + k1 + 1)*j1 + (j3 + k1)*byte1 + (l3 + k1)] = i2;
                                    }
                                    if (adjacentTreeBlocks[(l2 + k1)*j1 + ((j3 + k1) - 1)*byte1 + (l3 + k1)] == -2)
                                    {
                                        adjacentTreeBlocks[(l2 + k1)*j1 + ((j3 + k1) - 1)*byte1 + (l3 + k1)] = i2;
                                    }
                                    if (adjacentTreeBlocks[(l2 + k1)*j1 + (j3 + k1 + 1)*byte1 + (l3 + k1)] == -2)
                                    {
                                        adjacentTreeBlocks[(l2 + k1)*j1 + (j3 + k1 + 1)*byte1 + (l3 + k1)] = i2;
                                    }
                                    if (adjacentTreeBlocks[(l2 + k1)*j1 + (j3 + k1)*byte1 + ((l3 + k1) - 1)] == -2)
                                    {
                                        adjacentTreeBlocks[(l2 + k1)*j1 + (j3 + k1)*byte1 + ((l3 + k1) - 1)] = i2;
                                    }
                                    if (adjacentTreeBlocks[(l2 + k1)*j1 + (j3 + k1)*byte1 + (l3 + k1 + 1)] == -2)
                                    {
                                        adjacentTreeBlocks[(l2 + k1)*j1 + (j3 + k1)*byte1 + (l3 + k1 + 1)] = i2;
                                    }
                                }
                            }
                        }
                    }
                }
                int j2 = adjacentTreeBlocks[k1*j1 + k1*byte1 + k1];
                if (j2 >= 0)
                {
                    world.setBlockMetadataWithNotify(i, j, k, l & -5);
                }
                else
                {
                    removeLeaves(world, i, j, k);
                }
            }
        }

        private void removeLeaves(World world, int i, int j, int k)
        {
            dropBlockAsItem(world, i, j, k, world.getBlockMetadata(i, j, k));
            world.setBlockWithNotify(i, j, k, 0);
        }

        public override int quantityDropped(Random random)
        {
            return random.nextInt(16) != 0 ? 0 : 1;
        }

        public override int idDropped(int i, Random random)
        {
            return sapling.blockID;
        }

        public override bool isOpaqueCube()
        {
            return !graphicsLevel;
        }

        public override int func_22009_a(int i, int j)
        {
            if ((j & 3) == 1)
            {
                return blockIndexInTexture + 80;
            }
            else
            {
                return blockIndexInTexture;
            }
        }

        public override void onEntityWalking(World world, int i, int j, int k, Entity entity)
        {
            base.onEntityWalking(world, i, j, k, entity);
        }
    }
}